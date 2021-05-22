using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XKom.Models.DTOs;

namespace XKom.Repositories
{
    public class MeetingRepository : IMeetingRepository
    {
        public Task<MessageResponseDto> SignUpParticipantToMeeting(ParticipantSignUpRequestDto participant)
        {
            throw new NotImplementedException();
        }

        public Task<MeetingResponseDto> CreateMeeting(MeetingRequestDto meetingRequest)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MeetingDto>> GetMeetings()
        {
            throw new NotImplementedException();
        }

        public Task<MessageResponseDto> RemoveMeeting(Guid meetingId)
        {
            throw new NotImplementedException();
        }
    }
}
