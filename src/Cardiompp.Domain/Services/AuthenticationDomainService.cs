using Cardiompp.Domain.Entities;
using Cardiompp.Domain.Enums;
using Cardiompp.Domain.Repositories;
using Cardiompp.Domain.Services.Contracts;
using Cardiompp.Infrastructure.CrossCutting.JwtToken;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace Cardiompp.Domain.Services
{
    public class AuthenticationDomainService : IAuthenticationDomainService
    {
        public AuthenticationDomainService
        (
            IUnitOfWork unitOfWork,
            IOptionsMonitor<JwtTokenConfiguration> config,
            IHashDomainService md5HashServiceDomain
        )
        {
            _config = config;
            _unitOfWork = unitOfWork;
            Md5HashServiceDomain = md5HashServiceDomain ?? throw new ArgumentNullException(nameof(md5HashServiceDomain));
        }

        private readonly IUnitOfWork _unitOfWork;

        private readonly IOptionsMonitor<JwtTokenConfiguration> _config;

        IHashDomainService Md5HashServiceDomain { get; set; }

        public async Task<CardiomppAgent> AuthenticateAsync(string name, string password)
        {
            var passwordMd5 = Md5HashServiceDomain.GetMd5Hash(password);
            var cardiomppAgent = await _unitOfWork.CardiomppAgentRepository.GetByNameAndPasswordAsync(name, passwordMd5);

            if (cardiomppAgent == null)
            {
                cardiomppAgent = new CardiomppAgent();
                cardiomppAgent.AddError
                (
                    (int)ValidationErrorCodeEnum.AgentNameOrPasswordInvalid,
                    "Agent name or password is invalid.",
                    null
                );
            }

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
