using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XKom.Models.DTOs;
using XKom.Models.ModelsDB;

namespace XKom.Repositories
{
    public class MeetingRepository : BaseRepository, IMeetingRepository
    {
        public MeetingRepository(XKomContext xKomContext) : base(xKomContext)
        {
        }

        public async Task<MessageResponseDto> SignUpParticipantToMeeting(ParticipantSignUpRequestDto participant)
        {
            using var transaction = _xKomContext.Database.BeginTransaction();
            try
            {
                //Check if the participant has used the system before
                var previousParticipant = await _xKomContext.Participants.SingleOrDefaultAsync(x => x.Email == participant.Email);

                //If not, add them to the database
                if (previousParticipant is null)
                {
                    previousParticipant = new Participant()
                    {

                        Name = participant.Name,
                        Email = participant.Email,
                        ParticipantId = Guid.NewGuid()
                    };

                    await _xKomContext.Participants.AddAsync(previousParticipant);
                }
                else
                {
                    //If the given email exists in the database, check that the given name also matches
                    if (previousParticipant.Name != participant.Name)
                    {
                        transaction.Rollback();
                        return new()
                        {
                            ErrorMessage = "The email you entered has already been used with a different first name, please enter the correct first name",
                            IsSuccess = false
                        };
                    }
                }

                //Check if the participant is already signed up for the requested meeting
                var previousAssigment = await _xKomContext.MeetingsParticipants.SingleOrDefaultAsync(x 
                    => x.MeetingId == participant.MeetingId && x.ParticipantId == previousParticipant.ParticipantId);

                //If so, return an error message
                if (previousAssigment is not null)
                {
                    transaction.Rollback();
                    return new() 
                    { 
                        ErrorMessage = "This participant is already signed up for this meeting", 
                        IsSuccess = false
                    };
                }

                await _xKomContext.SaveChangesAsync();

                //Add a participant to a meeting
                await _xKomContext.MeetingsParticipants.AddAsync(new() { MeetingId = participant.MeetingId, ParticipantId = previousParticipant.ParticipantId });

                await _xKomContext.SaveChangesAsync();

                var count = _xKomContext.MeetingsParticipants.Count(x => x.MeetingId == participant.MeetingId);

                //Check if the participant is an redundant participant
                if (count > 25)
                {
                    transaction.Rollback();
                    return new() 
                    { 
                        ErrorMessage = "The maximum number of people per meeting is 25, the number exceeded", 
                        IsSuccess = false 
                    };
                }
                else
                {
                    transaction.Commit();
                    return new() { IsSuccess = true };
                }
            }
            catch (Exception e)
            {
                transaction.Rollback();
                return new() { ErrorMessage = e.Message, IsSuccess = false };
            }
        }

        public async Task<MeetingResponseDto> CreateMeeting(MeetingRequestDto meetingRequest)
        {
            //Check if the given meeting type name exists in the database
            var meetingType = await _xKomContext.Meetingtypes.Where(x => x.TypeName == meetingRequest.MeetingType).FirstOrDefaultAsync();

            if (meetingType is null)
            {
                return new()
                {
                    ErrorMessage = "The specified meeting type is incorrect",
                    IsSuccess = false
                };
            }
            else
            {
                //If so, create a meeting
                Meeting meeting = new()
                {
                    MeetingId = Guid.NewGuid(),
                    Title = meetingRequest.Title,
                    Description = meetingRequest.Description,
                    MeetingType = meetingType.MeetingTypeId,
                    StartDate = DateTime.Now
                };

                await _xKomContext.Meetings.AddAsync(meeting);
                await _xKomContext.SaveChangesAsync();

                return new()
                {
                    MeetingId = meeting.MeetingId,
                    IsSuccess = true
                };
            }
        }

        public async Task<IEnumerable<MeetingWithParticipantsDto>> GetMeetingsWithParticipants()
        {
            return await _xKomContext.Meetings.Include(x => x.MeetingTypeNavigation)
                .Include(x => x.MeetingsParticipants)
                .ThenInclude(x => x.Participant)
                .Select(x => new MeetingWithParticipantsDto()
                {
                    MeetingId = x.MeetingId,
                    Title = x.Title,
                    Description = x.Description,
                    MeetingType = x.MeetingTypeNavigation.TypeName,
                    StartDate = x.StartDate,
                    Participants = x.MeetingsParticipants.Select(x => new ParticipantDto() { Email = x.Participant.Email, Name = x.Participant.Name })
                })
                .OrderByDescending(x => x.StartDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<MeetingDto>> GetMeetings()
        {
            return await _xKomContext.Meetings.Include(x => x.MeetingTypeNavigation)
                .Select(x => new MeetingDto()
                {
                    MeetingId = x.MeetingId,
                    Title = x.Title,
                    Description = x.Description,
                    MeetingType = x.MeetingTypeNavigation.TypeName,
                    StartDate = x.StartDate
                })
                .OrderByDescending(x => x.StartDate)
                .ToListAsync();
        }

        public async Task<MessageResponseDto> RemoveMeeting(Guid meetingId)
        {
            //Check if a meeting with this id exists
            var meetingToRemove = await _xKomContext.Meetings.SingleOrDefaultAsync(x => x.MeetingId == meetingId);

            if (meetingToRemove is not null)
            {
                //If so, remove
                _xKomContext.Remove(meetingToRemove);

                await _xKomContext.SaveChangesAsync();

                return new()
                {
                    IsSuccess = true
                };
            }

            return new()
            {
                //If not, return an error message
                ErrorMessage = "The meeting with the given id does not exist",
                IsSuccess = false
            };
        }
    }
}
