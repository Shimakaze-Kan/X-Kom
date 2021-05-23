﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XKom.Models.DTOs;

namespace XKom.Repositories
{
    public interface IMeetingRepository
    {
        Task<IEnumerable<MeetingDto>> GetMeetingsWithParticipants();
        Task<MeetingResponseDto> CreateMeeting(MeetingRequestDto meetingRequest);
        Task<MessageResponseDto> RemoveMeeting(Guid meetingId);
        Task<MessageResponseDto> SignUpParticipantToMeeting(ParticipantSignUpRequestDto participant);
    }
}
