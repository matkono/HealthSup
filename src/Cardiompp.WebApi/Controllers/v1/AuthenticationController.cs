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
    public class AuthenticationController: ControllerBase
    {
        IAuthenticationApplicationService AuthenticationService { get; set; }

        public AuthenticationController(IAuthenticationApplicationService authenticationService)
        {
            AuthenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticateAgent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AuthenticateAgent([FromBody]AuthenticateAgentRequest authenticateRequest)
        {
            var response = await AuthenticationService.AuthenticateAsync(authenticateRequest);

            if (response.Errors != null && response.Errors.Any())
                return BadRequest(response);

            return Ok(response);
        }
    }
}
