using HealthSup.Application.DataContracts.v1.Requests.Patient;
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
    public class PatientController : ControllerBase
    {
        public PatientController
        (
            IPatientApplicationService patientApplicationService
        )
        {
            PatientApplicationService = patientApplicationService ?? throw new ArgumentNullException(nameof(patientApplicationService));
        }

        IPatientApplicationService PatientApplicationService { get; set; }

        [HttpPost]
        [Route("listPaged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ListPagedPatients
        (
            [FromBody]ListPagedPatientsRequest argument
        )
        {
            var response = await PatientApplicationService.ListPaged(argument);

            if (response.Errors != null && response.Errors.Any())
                return BadRequest(response);

            return Ok(response);
        }
    }
}