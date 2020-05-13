using Cardiompp.Application.Configuration.JwtToken;
using Cardiompp.Application.DataContracts.v1.Responses.Authentication;
using Cardiompp.Application.Services.Contracts;
using Cardiompp.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace Cardiompp.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationService
        (
            IUnitOfWork unitOfWork,
            IOptionsMonitor<JwtTokenConfiguration> config, 
            IHashService md5HashService
        )
        {
            _config = config;
            _unitOfWork = unitOfWork;
            Md5HashService = md5HashService ?? throw new ArgumentNullException(nameof(md5HashService));
        }

        private readonly IUnitOfWork _unitOfWork;

        private readonly IOptionsMonitor<JwtTokenConfiguration> _config;

        IHashService Md5HashService { get; set; }

        public async Task<GetAuthenticationResponse> AuthenticateAsync(string name, string password)
        {
            var passwordMd5 = Md5HashService.GetHash(password);
            var cardiomppAgent = await _unitOfWork.CardiomppAgentRepository.GetByNameAndPasswordAsync(name, passwordMd5);
            AuthenticationResponse authenticationResponse = null;

            if (cardiomppAgent != null)
                authenticationResponse = new AuthenticationResponse(BuildToken());
            
            return new GetAuthenticationResponse(authenticationResponse);
        }

        private string BuildToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.CurrentValue.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config.CurrentValue.Issuer,
              _config.CurrentValue.Issuer,
              expires: DateTime.UtcNow.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
