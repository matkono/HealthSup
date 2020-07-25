using HealthSup.Application.DataContracts.v1.Requests.Authenticate;
using HealthSup.Application.DataContracts.v1.Requests.Doctor;
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
    public class AuthenticationController: ControllerBase
    {
        IAuthenticationApplicationService AuthenticationService { get; set; }

        public AuthenticationController(IAuthenticationApplicationService authenticationService)
        {
            AuthenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("agentAuthentication/token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AuthenticationAgent([FromBody]AuthenticationAgentRequest authenticateRequest)
        {
            var response = await AuthenticationService.AuthenticateAgentAsync(authenticateRequest);

            if (response.Errors != null && response.Errors.Any())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost]
        [Route("userAuthentication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AuthenticationUser([FromBody]AuthenticationUserRequest authenticateRequest)
        {
            var response = await AuthenticationService.AuthenticateUserAsync(authenticateRequest);

            if (response.Errors != null && response.Errors.Any())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost]
        [Route("updateUserPassword")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateUserPassword([FromBody] UpdateUserPasswordRequest updateUserPasswordRequest)
        {
            var response = await AuthenticationService.UpdatePassword(updateUserPasswordRequest);

            if (response.Errors != null && response.Errors.Any())
                return BadRequest(response);

            return NoContent();
        }
    }
}
