using Cardiompp.Domain.Repositories;
using Cardiompp.Infrastructure.CrossCutting.Authentication.DTO;
using Cardiompp.Infrastructure.CrossCutting.Authentication.Services.Contracts;
using Cardiompp.Infrastructure.CrossCutting.Hash.Services.Contracts;
using Cardiompp.Infrastructure.CrossCutting.JwtToken;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace Cardiompp.Infrastructure.CrossCutting.Services.Authentication
{
    public class AuthenticationCrossCuttingService: IAuthenticationCrossCuttingService
    {
        public AuthenticationCrossCuttingService
        (
            IUnitOfWork unitOfWork,
            IOptionsMonitor<JwtTokenConfiguration> config,
            IHashCrossCuttingService md5HashCrossCuttingService
        )
        {
            _config = config;
            _unitOfWork = unitOfWork;
            Md5HashServiceDomain = md5HashCrossCuttingService ?? throw new ArgumentNullException(nameof(md5HashCrossCuttingService));
        }

        private readonly IUnitOfWork _unitOfWork;

        private readonly IOptionsMonitor<JwtTokenConfiguration> _config;

        IHashCrossCuttingService Md5HashServiceDomain { get; set; }

        public async Task<AgentDTO> AuthenticateAsync(string name, string password)
        {
            var passwordMd5 = Md5HashServiceDomain.GetMd5Hash(password);
            var cardiomppAgent = await _unitOfWork.CardiomppAgentRepository.GetByNameAndPasswordAsync(name, passwordMd5);

            return cardiomppAgent;
        }

        public string BuildToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.CurrentValue.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config.CurrentValue.Issuer,
              _config.CurrentValue.Issuer,
              expires: DateTime.UtcNow.AddMinutes(_config.CurrentValue.LifeTime),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
