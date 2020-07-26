using HealthSup.Application.DataContracts.v1.Requests.MedicalAppointment;
using HealthSup.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthSup.WebApi.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class MedicalAppointmentController : ControllerBase
    {
        public MedicalAppointmentController
        (
            IMedicalAppointmentApplicationService medicalAppointmentApplicationService
        )
        {
            MedicalAppointmentApplicationService = medicalAppointmentApplicationService ?? throw new ArgumentNullException(nameof(medicalAppointmentApplicationService));
        }

        IMedicalAppointmentApplicationService MedicalAppointmentApplicationService { get; set; }

        [HttpPost]
        [Route("getInitalQuestion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetInitialQuestion
        (
            [FromBody]GetInitialQuestionRequest getInitialQuestionRequest
        )
        {
            var response = await MedicalAppointmentApplicationService.GetInitialByDiseaseId(getInitialQuestionRequest);

            if (response.Errors != null && response.Errors.Any())
                return BadRequest(response);

            return Ok(response);
        }
    }
}