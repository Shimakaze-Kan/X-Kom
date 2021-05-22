using System;
using System.Collections.Generic;

#nullable disable

namespace XKom.Models.ModelsDB
{
    public partial class Meetingtype
    {
        public Meetingtype()
        {
            Meetings = new HashSet<Meeting>();
        }

        public int MeetingTypeId { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<Meeting> Meetings { get; set; }
    }
}
