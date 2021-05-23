using System.ComponentModel.DataAnnotations;

namespace XKom.Models.DTOs
{
    public class ParticipantDto
    {
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(255), EmailAddress]
        public string Email { get; set; }
    }
}
