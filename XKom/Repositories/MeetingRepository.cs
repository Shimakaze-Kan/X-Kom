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

        public Task<MessageResponseDto> SignUpParticipantToMeeting(ParticipantSignUpRequestDto participant)
        {
            throw new NotImplementedException();
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

        public Task<IEnumerable<MeetingDto>> GetMeetings()
        {
            throw new NotImplementedException();
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
