using System;
using System.Collections.Generic;

#nullable disable

namespace XKom.Models.ModelsDB
{
    public partial class Meeting
    {
        public Meeting()
        {
            MeetingsParticipants = new HashSet<MeetingsParticipant>();
        }

        public Guid MeetingId { get; set; }
        public DateTime StartDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MeetingType { get; set; }

        public virtual Meetingtype MeetingTypeNavigation { get; set; }
        public virtual ICollection<MeetingsParticipant> MeetingsParticipants { get; set; }
    }
}
