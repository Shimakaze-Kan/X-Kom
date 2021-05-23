using System;

namespace XKom.Models.DTOs
{
    public class MeetingResponseDto : MessageResponseDto
    {
        public Guid MeetingId { get; set; }
    }
}
