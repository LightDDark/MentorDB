﻿using Domain;
using Domain.In;
using Domain.Out;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using NuGet.Packaging.Core;
using Services;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MissionsController : ControllerBase
    {
        private readonly MissionService _service;

        public MissionsController(MissionService service)
        {
            _service = service;
        }

        // GET: api/Contacts
        // returns list of contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UiMission>>> GetMissions()
        {
            List<UiMission>? missions = await _service.GetMissions(HttpContext.User.Claims.First().Value);
            if (missions == null)
            {
                return NotFound();
            }

            return missions;
        }


        // PUT: api/Contacts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // update details of contact with the given id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMission(UiMission mission)
        {
            bool res = await _service.UpdateMission(mission, HttpContext.User.Claims.First().Value);
            if (res == false)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Contacts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // add new contact to current user
        [HttpPost]
        public async Task<ActionResult<int>> PostMission(InMission mission)
        {

            int? res = await _service.AddMission(new Mission()
            {
                Title = mission.Title,
                Description = mission.Description,
                Type = mission.Type,
                Length = mission.Length,
                OptionalDays = mission.OptionalDays,
                OptionalHours = mission.OptionalHours,
                DeadLine = mission.DeadLine,
                Priority = mission.Priority
                
            }, HttpContext.User.Claims.First().Value);
            if (res == null)
            {
                return Problem("Entity set 'MentorDataContext.User'  is null.");
            }

            if (res == null)
            {
                return BadRequest();
            }

            return Ok(res.Value);
        }

        // DELETE: api/Contacts/5
        // delete id from contacts
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMission(int id)
        {

            bool? res = await _service.DeleteMission(id, HttpContext.User.Claims.First().Value);
            if (res == null || res == false)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}
