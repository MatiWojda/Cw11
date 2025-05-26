using Microsoft.AspNetCore.Mvc;
using Cw11.Services;
using System.Threading.Tasks;

namespace Cw11.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase {
        private readonly IDbService _service;
        public PatientsController(IDbService service) {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) {
            var result = await _service.GetPatientDetailsAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}