using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XKom.Models.DTOs
{
    public class MeetingWithParticipantsDto : MeetingDto
    {        
        public IEnumerable<ParticipantDto> Participants { get; set; } = new List<ParticipantDto>();
    }
}
