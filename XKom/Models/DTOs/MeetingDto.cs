using System;
using System.ComponentModel.DataAnnotations;

namespace XKom.Models.DTOs
{
    public class MeetingDto
    {
        public Guid MeetingId { get; set; }
        [MaxLength(255)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string MeetingType { get; set; }
        public DateTime StartDate { get; set; }
    }
}
