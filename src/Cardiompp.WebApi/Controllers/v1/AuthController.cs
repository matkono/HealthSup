using Cardiompp.Application.DataContracts.v1.Requests.Login;
using Cardiompp.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace Cardiompp.WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController: ControllerBase
    {
        private readonly IConfiguration _config;

        IAuthenticationService AuthenticationService { get; set; }

        public AuthController(IConfiguration config, IAuthenticationService authenticationService)
        {
            _config = config;
            AuthenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateTokenAsync([FromBody]AuthenticateRequest login)
        {
            if (login == null) 
                return Unauthorized();

            var tokenString = string.Empty;
            var validUser = await AuthenticationService.AuthenticateAsync(login.AgentName, login.Password);

            if (validUser)
                tokenString = BuildToken();
            else
                return Unauthorized();

            return Ok(new { Token = tokenString });
        }

        private string BuildToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtToken:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["JwtToken:Issuer"],
              _config["JwtToken:Issuer"],
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
