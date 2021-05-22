﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XKom.Models.DTOs
{
    public class ParticipantSignUpRequestDto
    {
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(255), EmailAddress]        
        public string Email { get; set; }
        public Guid MeetingId { get; set; }
    }
}