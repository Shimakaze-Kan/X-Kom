using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XKom.Models.DTOs
{
    public class MeetingDto
    {
        [MaxLength(255)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string MeetingType { get; set; }
        public DateTime StartDate { get; set; }
        public IEnumerable<ParticipantDto> Participants { get; set; } = new List<ParticipantDto>();
    }
}
