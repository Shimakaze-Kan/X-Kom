using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XKom.Models.DTOs;
using XKom.Repositories;

namespace XKom.Controllers
{
    [Route("/api/")]
    [ApiController]
    public class MeetingController : Controller
    {
        private readonly IMeetingRepository _meetingRepository;

        public MeetingController(IMeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }


        [HttpGet("GetMeetings")]
        public async Task<ActionResult<IEnumerable<MeetingDto>>> GetMeetings()
        {
            var result = await _meetingRepository.GetMeetings();

            return Ok(result);
        }

        [HttpDelete("RemoveMeeting")]
        public async Task<ActionResult<MessageResponseDto>> RemoveMeeting(Guid meetingId)
        {
            var result = await _meetingRepository.RemoveMeeting(meetingId);

            if(result.IsSuccess is false)
            {
                BadRequest(result);
            }

            return result;
        }

        [HttpPost("SignUpParticipant")]
        public async Task<ActionResult<MessageResponseDto>> SignUpParticipant(ParticipantSignUpRequestDto participant)
        {
            var result = await _meetingRepository.SignUpParticipantToMeeting(participant);

            if(result.IsSuccess is false)
            {
                return BadRequest(result);
            }

            return result;
        }

        [HttpPost("CreateMeeting")]
        public async Task<ActionResult<MeetingResponseDto>> CreateMeeting(MeetingRequestDto meetingRequest)
        {
            var result = await _meetingRepository.CreateMeeting(meetingRequest);

            if(result.IsSuccess is false)
            {
                return BadRequest(result);
            }

            return result;
        }
    }
}
