using System.Collections.Generic;

namespace XKom.Models.DTOs
{
    public class MeetingWithParticipantsDto : MeetingDto
    {
        public IEnumerable<ParticipantDto> Participants { get; set; } = new List<ParticipantDto>();
    }
}
