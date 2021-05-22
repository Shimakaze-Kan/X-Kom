using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XKom.Models.DTOs;

namespace XKom.Controllers
{
    [Route("/api/")]
    [ApiController]
    public class MeetingController : Controller
    {
        [HttpGet("GetMeetings")]
        public async Task<ActionResult<IEnumerable<MeetingDto>>> GetMeetings()
        {
            throw new NotImplementedException();
        }

        [HttpDelete("RemoveMeeting")]
        public async Task<ActionResult<MessageResponseDto>> RemoveMeeting(Guid meetingId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("SignUpParticipant")]
        public async Task<ActionResult<MessageResponseDto>> SignUpParticipant(UserDto user)
        {
            throw new NotImplementedException();
        }

        [HttpPost("CreateMeeting")]
        public async Task<ActionResult<MeetingResponseDto>> CreateMeeting(MeetingRequestDto meetingRequest)
        {
            throw new NotImplementedException();
        }
    }
}
