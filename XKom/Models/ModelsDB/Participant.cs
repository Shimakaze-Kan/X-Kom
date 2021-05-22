using System;
using System.Collections.Generic;

#nullable disable

namespace XKom.Models.ModelsDB
{
    public partial class Participant
    {
        public Participant()
        {
            MeetingsParticipants = new HashSet<MeetingsParticipant>();
        }

        public Guid ParticipantId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public virtual ICollection<MeetingsParticipant> MeetingsParticipants { get; set; }
    }
}
