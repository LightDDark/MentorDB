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

        public AlgoController(AlgoService service) {
            _service = service;
        }

        // Post: api/Algo
        // returns list of contacts
        [HttpPost]
        public async Task<ActionResult<UiComplete>> NewSchedule(ScheduleSetting setting, List<int> missionsId) {
            UiComplete? missions = await _service.CalculateSchedule(HttpContext.User.Claims.First().Value, setting, missionsId);
            if (missions == null) {
                return NotFound();
            }
            return missions;
        }
    }
}
