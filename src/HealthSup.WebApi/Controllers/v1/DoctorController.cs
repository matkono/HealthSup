using HealthSup.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthSup.WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class DoctorController : ControllerBase
    {
        IDoctorApplicationService DoctorService { get; set; }

        public DoctorController
        (
            IDoctorApplicationService doctorService
        )
        {
            DoctorService = doctorService ?? throw new ArgumentNullException(nameof(doctorService));
        }

        [HttpGet]
        [Route("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ListLastNode
        (
            int userId
        )
        {
            var response = await DoctorService.GetByUserId(userId);

            if (response.Errors != null && response.Errors.Any())
                return BadRequest(response);

            if (response.Data == null)
                return NoContent();

            return Ok(response);
        }
    }
}