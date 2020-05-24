using Cardiompp.Application.DataContracts.v1.Requests.Authenticate;
using Cardiompp.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Cardiompp.WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthenticationController: ControllerBase
    {
        IAuthenticationService AuthenticationService { get; set; }

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            AuthenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateTokenAsync([FromBody]AuthenticateRequest authenticateRequest)
        {
            var response = await AuthenticationService.AuthenticateAsync(authenticateRequest);

            if (response.Data == null)
                return NotFound(response);

            return Ok(response);
        }
    }
}
