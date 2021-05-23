using System;

namespace XKom.Models.DTOs
{
    public class ParticipantSignUpRequestDto : ParticipantDto
    {
        public Guid MeetingId { get; set; }
    }
}
