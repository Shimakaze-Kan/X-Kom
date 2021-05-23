using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XKom.Models.DTOs;
using XKom.Repositories;

namespace XKom.Controllers
{
    [Route("/api/")]
    [ApiController]
    public class MeetingController : Controller //Controller responsible for managing meetings
    {
        private readonly IMeetingRepository _meetingRepository;

        public MeetingController(IMeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }


        /// <summary>
        /// This endpoint returns all meetings with participant data.
        /// </summary>
        [HttpGet("GetMeetingsWithParticipants")]
        public async Task<ActionResult<IEnumerable<MeetingWithParticipantsDto>>> GetMeetingsWithParticipants()
        {
            var result = await _meetingRepository.GetMeetingsWithParticipants();

            return Ok(result);
        }

        /// <summary>
        /// This endpoint returns all meetings.
        /// </summary>
        [HttpGet("GetMeetings")]
        public async Task<ActionResult<IEnumerable<MeetingDto>>> GetMeetings()
        {
            var result = await _meetingRepository.GetMeetings();

            return Ok(result);
        }

        /// <summary>
        /// This endpoint removes the meeting with the specified id
        /// </summary>        
        /// <returns>Returns an confirmation of deletion, or a denial 
        /// with an appropriate error message</returns>
        /// <example>
        /// 400 code example:
        /// {
        /// "errorMessage": "The meeting with the given id does not exist",
        /// "isSuccess": false
        /// }
        /// 
        /// 200 code example:
        /// {
        /// "errorMessage": null,
        /// "isSuccess": true
        /// }
        /// </example>
        [HttpDelete("RemoveMeeting")]
        public async Task<ActionResult<MessageResponseDto>> RemoveMeeting(Guid meetingId)
        {
            var result = await _meetingRepository.RemoveMeeting(meetingId);

            if (result.IsSuccess is false)
            {
                BadRequest(result);
            }

            return result;
        }

        /// <summary>
        /// If the participant does not exist, it creates a record of 
        /// the participant in the database and then signs them up for the meeting
        /// </summary>        
        /// <returns>Returns confirmation of the participant's sign-up or 
        /// denial with an appropriate message</returns>
        /// <example>
        /// 400 code examples:
        /// {
        /// "errorMessage": "The email you entered has already been used with a different first name, please enter the correct first name",
        /// "isSuccess": false
        /// }
        /// 
        /// {
        /// "errorMessage": "This participant is already signed up for this meeting",
        /// "isSuccess": false
        /// }
        /// 
        /// {
        /// "errorMessage": "The maximum number of people per meeting is 25, the number exceeded",
        /// "isSuccess": false
        /// }
        /// 
        /// 200 code example:
        /// {
        /// "errorMessage": null,
        /// "isSuccess": true
        /// }
        /// </example>
        [HttpPost("SignUpParticipant")]
        public async Task<ActionResult<MessageResponseDto>> SignUpParticipant(ParticipantSignUpRequestDto participant)
        {
            var result = await _meetingRepository.SignUpParticipantToMeeting(participant);

            if (result.IsSuccess is false)
            {
                return BadRequest(result);
            }

            return result;
        }


        /// <summary>
        /// Creates a meeting
        /// </summary>
        /// <remarks>Meeting type must exist in the database, otherwise 
        /// a rejection will be returned</remarks>
        /// <returns>Returns an confirmation of the meeting creation 
        /// with its id, or a denial with an appropriate message</returns>
        /// <example>
        /// 400 code example:
        /// {
        /// "meetingId": "00000000-0000-0000-0000-000000000000",
        /// "errorMessage": "The specified meeting type is incorrect",
        /// "isSuccess": false
        /// }
        /// 
        /// 200 code example:
        /// {
        /// "meetingId": "f92c8d9f-570d-4759-8e22-932273dbf47c",
        /// "errorMessage": null,
        /// "isSuccess": true
        /// }
        /// </example>
        [HttpPost("CreateMeeting")]
        public async Task<ActionResult<MeetingResponseDto>> CreateMeeting(MeetingRequestDto meetingRequest)
        {
            var result = await _meetingRepository.CreateMeeting(meetingRequest);

            if (result.IsSuccess is false)
            {
                return BadRequest(result);
            }

            return result;
        }
    }
}
