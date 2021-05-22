using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XKom.Models.DTOs
{
    public class MessageResponseDto
    {
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }
}
