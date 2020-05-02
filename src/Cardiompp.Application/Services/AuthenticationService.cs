using Cardiompp.Application.DataContracts.v1.Responses.Authentication;
using Cardiompp.Application.Services.Contracts;
using Cardiompp.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace Cardiompp.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IConfiguration _config;

        public AuthenticationService(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _config = config;
            _unitOfWork = unitOfWork;
        }

        public async Task<GetAuthenticationResponse> AuthenticateAsync(string name, string password)
        {
            var cardiomppAgent = await _unitOfWork.CardiomppAgentRepository.GetByNameAndPasswordAsync(name, password);
            AuthenticationResponse authenticationResponse = null;
            
            if (cardiomppAgent != null) {
                authenticationResponse = new AuthenticationResponse(BuildToken());
            }
            
            return new GetAuthenticationResponse(authenticationResponse);
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
