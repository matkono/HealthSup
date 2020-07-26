using HealthSup.Domain.Repositories;
using HealthSup.Infrastructure.CrossCutting.Authentication.DTO;
using HealthSup.Infrastructure.CrossCutting.Authentication.Services.Contracts;
using HealthSup.Infrastructure.CrossCutting.Hash.Services.Contracts;
using HealthSup.Infrastructure.CrossCutting.JwtToken;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace HealthSup.Infrastructure.CrossCutting.Services.Authentication
{
    public class AuthenticationService: IAuthenticationService
    {
        public AuthenticationService
        (
            IUnitOfWork unitOfWork,
            IOptionsMonitor<JwtTokenConfiguration> config,
            IHashService md5HashCrossCuttingService
        )
        {
            _config = config;
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;

        private readonly IOptionsMonitor<JwtTokenConfiguration> _config;

        public async Task<AgentDTO> AuthenticateAgentAsync
        (
            string key, 
            string password
        )
        {
            var agent = await _unitOfWork.HealthSupAgentRepository.GetByKeyAndPassword(key, password);

            return agent;
        }

        public async Task<UserDTO> AuthenticateUserAsync
        (
            string email, 
            string password
        )
        { 
            var user = await _unitOfWork.UserRepository.GetByEmailAndPassword(email, password);

            return user;
        }

        public async Task UpdatePassword
        (
            int userId, 
            string newPassword
        )
        {
            await _unitOfWork.UserRepository.UpdatePassword(userId, newPassword);
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
