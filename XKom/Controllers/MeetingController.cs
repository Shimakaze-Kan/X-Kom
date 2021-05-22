﻿using Microsoft.AspNetCore.Mvc;
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
            throw new NotImplementedException();
        }

        [HttpDelete("RemoveMeeting")]
        public async Task<ActionResult<MessageResponseDto>> RemoveMeeting(Guid meetingId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("SignUpParticipant")]
        public async Task<ActionResult<MessageResponseDto>> SignUpParticipant(ParticipantSignUpRequestDto participant)
        {
            throw new NotImplementedException();
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
