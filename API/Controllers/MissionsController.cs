using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using Services;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MissionsController : ControllerBase
    {
        private readonly UsersService _service;
        private readonly string _userId;

        public MissionsController(UsersService service)
        {
            _service = service;
            _userId = HttpContext.User.Claims.First().Value;
        }

        // GET: api/Contacts
        // returns list of contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mission>>> GetMissions()
        {
            List<Mission>? missions = await _service.GetMissions(_userId);
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
        public async Task<IActionResult> PutMission(int missionId)
        {
            bool res = await _service.UpdateMission(missionId, _userId);
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
        public async Task<IActionResult> PostMission(Mission mission)
        {

            bool? res = await _service.AddMission(new Mission()
            {
                Title = mission.Title,
                Descroption = mission.Descroption,
                StartDate = mission.StartDate,
                EndDate = mission.EndDate
            }, _userId);
            if (res == null)
            {
                return Problem("Entity set 'MentorDataContext.User'  is null.");
            }

            if (res == false)
            {
                return BadRequest();
            }

            return Ok();
        }

        // DELETE: api/Contacts/5
        // delete id from contacts
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMission(int id)
        {

            bool? res = await _service.DeleteMission(id, _userId);
            if (res == null || res == false)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}
