using Domain;
using Domain.Out;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using Services;

namespace API.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AlgoController : ControllerBase {
        private readonly AlgoService _service;
        private readonly string _userId;

        public AlgoController(AlgoService service) {
            _service = service;
            _userId = HttpContext.User.Claims.First().Value;
        }

        // Post: api/Algo
        // returns list of contacts
        [HttpPost]
        public async Task<ActionResult<UiComplete>> NewSchedule(AlgoComplete newMissions) {
            UiComplete? missions = await _service.CalculateSchedule(_userId, newMissions);
            if (missions == null) {
                return NotFound();
            }
            return missions;
        }
    }
}
