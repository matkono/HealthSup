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

        IMd5HashService Md5HashService { get; set; }

        public AuthenticationService(IUnitOfWork unitOfWork, IConfiguration config, IMd5HashService md5HashService)
        {
            _config = config;
            _unitOfWork = unitOfWork;
            Md5HashService = md5HashService ?? throw new ArgumentNullException(nameof(md5HashService));
        }

        public async Task<GetAuthenticationResponse> AuthenticateAsync(string name, string password)
        {
            var passwordMd5 = Md5HashService.GetMd5Hash(password);
            var cardiomppAgent = await _unitOfWork.CardiomppAgentRepository.GetByNameAndPasswordAsync(name, passwordMd5);
            AuthenticationResponse authenticationResponse = null;

            if (cardiomppAgent != null)
                authenticationResponse = new AuthenticationResponse(BuildToken());
            
            return new GetAuthenticationResponse(authenticationResponse);
        }

        private string BuildToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtToken:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["JwtToken:Issuer"],
              _config["JwtToken:Issuer"],
              expires: DateTime.UtcNow.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
