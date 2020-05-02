using Cardiompp.Application.DataContracts.v1.Requests.Login;
using Cardiompp.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        public async Task<IActionResult> CreateTokenAsync([FromBody]AuthenticateRequest login)
        {
            var response = await AuthenticationService.AuthenticateAsync(login.AgentName, login.Password);

            if (response.Data == null)
                return NotFound(response);

            return Ok(response);
        }
    }
}
