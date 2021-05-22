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
                var previousParticipant = await _xKomContext.Participants.SingleOrDefaultAsync(x => x.Name == participant.Name && x.Email == participant.Email);

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

                var previousAssigment = await _xKomContext.MeetingsParticipants.SingleOrDefaultAsync(x 
                    => x.MeetingId == participant.MeetingId && x.ParticipantId == previousParticipant.ParticipantId);

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

                await _xKomContext.MeetingsParticipants.AddAsync(new() { MeetingId = participant.MeetingId, ParticipantId = previousParticipant.ParticipantId });

                await _xKomContext.SaveChangesAsync();

                var count = _xKomContext.MeetingsParticipants.Count(x => x.MeetingId == participant.MeetingId);

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

        public async Task<IEnumerable<MeetingDto>> GetMeetings()
        {
            return await _xKomContext.Meetings.Include(x => x.MeetingTypeNavigation)
                .Include(x => x.MeetingsParticipants)
                .ThenInclude(x => x.Participant)
                .Select(x => new MeetingDto()
                {
                    Title = x.Title,
                    Description = x.Description,
                    MeetingType = x.MeetingTypeNavigation.TypeName,
                    StartDate = x.StartDate,
                    Participants = x.MeetingsParticipants.Select(x => new ParticipantDto() { Email = x.Participant.Email, Name = x.Participant.Name })
                })
                .OrderByDescending(x => x.StartDate)
                .ToListAsync();
        }

        public async Task<MessageResponseDto> RemoveMeeting(Guid meetingId)
        {
            var meetingToRemove = await _xKomContext.Meetings.SingleOrDefaultAsync(x => x.MeetingId == meetingId);

            if (meetingToRemove is not null)
            {
                _xKomContext.Remove(meetingToRemove);

                await _xKomContext.SaveChangesAsync();

                return new()
                {
                    IsSuccess = true
                };
            }

            return new()
            {
                ErrorMessage = "The meeting with the given id does not exist",
                IsSuccess = false
            };
        }
    }
}
