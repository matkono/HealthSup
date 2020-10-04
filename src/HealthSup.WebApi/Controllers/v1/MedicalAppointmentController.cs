using HealthSup.Application.DataContracts.v1.Responses.Action;
using HealthSup.Application.DataContracts.v1.Responses.MedicalAppointment;
using HealthSup.Application.DataContracts.v1.Responses.Question;
using HealthSup.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthSup.WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class MedicalAppointmentController : ControllerBase
    {
        public MedicalAppointmentController
        (
            IMedicalAppointmentApplicationService medicalAppointmentService
        )
        {
            MedicalAppointmentService = medicalAppointmentService ?? throw new ArgumentNullException(nameof(medicalAppointmentService));
        }

        IMedicalAppointmentApplicationService MedicalAppointmentService { get; set; }

        [HttpGet]
        [Route("medicalAppointment/{medicalAppointmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ListLastNode
        (
            int medicalAppointmentId
        )
        {
            var response = await MedicalAppointmentService.GetLastNode(medicalAppointmentId);

            if (response.Errors != null && response.Errors.Any())
                return BadRequest(response);


            var teste = new GetMedicalAppointmentLastNodeReturn(teste2);

            return Ok(teste);
        }
    }
}