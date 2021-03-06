using HealthSup.Application.DataContracts.v1.Requests;
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
    public class DiseaseController : ControllerBase
    {
        IDiseaseApplicationService DiseaseApplicationService { get; set; }

        public DiseaseController
        (
            IDiseaseApplicationService diseaseApplicationService
        )
        {
            DiseaseApplicationService = diseaseApplicationService ?? throw new ArgumentNullException(nameof(diseaseApplicationService));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ListPaged
        (
            [FromQuery]Pagination pagination
        )
        {
            var response = await DiseaseApplicationService.ListPaged(pagination);

            if (response.Errors != null && response.Errors.Any())
                return BadRequest(response);

            return Ok(response);
        }
    }
}