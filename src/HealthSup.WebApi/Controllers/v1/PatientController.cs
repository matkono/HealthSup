using HealthSup.Application.DataContracts.v1.Requests;
using HealthSup.Application.DataContracts.v1.Requests.Address;
using HealthSup.Application.DataContracts.v1.Requests.MedicalAppointment;
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ListPaged
        (
            [FromQuery]Pagination pagination
        )
        {
            var response = await PatientApplicationService.ListPaged(pagination);

            if (response.Errors != null && response.Errors.Any())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet]
        [Route("{registration}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ListLastNode
        (
            string registration
        )
        {
            var response = await PatientApplicationService.GetByRegistration(registration);

            if (response.Errors != null && response.Errors.Any())
                return BadRequest(response);

            if (response.Data == null)
                return NoContent();

            return Ok(response);
        }

        [HttpGet]
        [Route("{patientId}/medicalAppointments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ListMedicalAppointments
        (
            int patientId,
            [FromQuery]Pagination pagination
        )
        {
            var response = await PatientApplicationService.ListMedicalAppointments(patientId, pagination);

            if (response.Errors != null && response.Errors.Any())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create
        (
            [FromBody]CreatePatientRequest argument
        )
        {
            var response = await PatientApplicationService.Create(argument);

            if (response.Errors != null && response.Errors.Any())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("{patientId}")]
        public async Task<IActionResult> UpdateAsync
        (
            int patientId,
            [FromBody] UpdateAddressRequest updateAddressRequest
        )
        {
            var argument = new UpdatePatientRequest()
            { 
                PatientId = patientId,
                Address = updateAddressRequest.Address
            };

            var response = await PatientApplicationService.Update(argument);

            if (response.Errors != null && response.Errors.Any())
                return BadRequest(response);

            return Ok(response);
        }
    }
}