using HealthSup.Application.DataContracts.v1.Requests.Node;
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
    public class DecisionEngineController : ControllerBase
    {
        public DecisionEngineController
        (
            IDecisionEngineApplicationService decisionEngineService
        )
        {
            DecisionEngineService = decisionEngineService ?? throw new ArgumentNullException(nameof(decisionEngineService));
        }

        IDecisionEngineApplicationService DecisionEngineService { get; set; }

        [HttpPost]
        [Route("nextNode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> NextNode
        (
            [FromBody]GetNextNodeRequest argument
        )
        {
            var response = await DecisionEngineService.GetNextNode(argument);

            if (response.Errors != null && response.Errors.Any())
                return BadRequest(response);

            return Ok(response);
        }
    }
}