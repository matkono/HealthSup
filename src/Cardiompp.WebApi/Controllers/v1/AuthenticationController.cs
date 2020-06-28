using Cardiompp.Application.DataContracts.v1.Requests.Authenticate;
using Cardiompp.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cardiompp.WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class AuthenticationController: ControllerBase
    {
        IAuthenticationApplicationService AuthenticationService { get; set; }

        public AuthenticationController(IAuthenticationApplicationService authenticationService)
        {
            AuthenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticationAgent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AuthenticationAgent([FromBody]AuthenticationAgentRequest authenticateRequest)
        {
            var response = await AuthenticationService.AuthenticateAgentAsync(authenticateRequest);

            if (response.Errors != null && response.Errors.Any())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost]
        [Route("authenticationUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AuthenticationUser([FromBody]AuthenticationUserRequest authenticateRequest)
        {
            var response = await AuthenticationService.AuthenticateUserAsync(authenticateRequest);

            if (response.Errors != null && response.Errors.Any())
                return BadRequest(response);

            return Ok(response);
        }
    }
}
