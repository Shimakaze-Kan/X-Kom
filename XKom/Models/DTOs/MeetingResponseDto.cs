using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XKom.Models.DTOs
{
    public class MeetingResponseDto : MessageResponseDto
    {
        public Guid MeetingId { get; set; }
    }
}
