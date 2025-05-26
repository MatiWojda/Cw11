using Microsoft.AspNetCore.Mvc;
using Cw11.DTOs;
using Cw11.Services;
using System;
using System.Threading.Tasks;

namespace Cw11.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionsController : ControllerBase {
        private readonly IDbService _service;
        public PrescriptionsController(IDbService service) {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddPrescription([FromBody] PrescriptionRequestDto dto)
        {
            try
            {
                var id = await _service.AddPrescriptionAsync(dto);
                return Ok(new { IdPrescription = id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}