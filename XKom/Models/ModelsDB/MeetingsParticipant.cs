using System;
using System.Collections.Generic;

#nullable disable

namespace XKom.Models.ModelsDB
{
    public partial class MeetingsParticipant
    {
        public Guid MeetingParticipantId { get; set; }
        public Guid MeetingId { get; set; }
        public Guid ParticipantId { get; set; }

        public virtual Meeting Meeting { get; set; }
        public virtual Participant Participant { get; set; }
    }
}
