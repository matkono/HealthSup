using HealthSup.Application.DataContracts.v1.Requests.DecisionEngine;
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
        [Route("question/answer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AnswerQuestion
        (
            [FromBody]AnswerQuestionRequest argument
        )
        {
            var response = await DecisionEngineService.AnswerQuestion(argument);

            if (response.Errors != null && response.Errors.Any())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost]
        [Route("action/confirm")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ConfirmAction
        (
            [FromBody]ConfirmActionRequest argument
        )
        {
            var response = await DecisionEngineService.ConfirmAction(argument);

            if (response.Errors != null && response.Errors.Any())
                return BadRequest(response);

            return NoContent();
        }
    }
}