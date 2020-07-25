using HealthSup.Application.DataContracts.v1.Responses.Authentication;
using HealthSup.Infrastructure.CrossCutting.Authentication.DTO;

namespace HealthSup.Application.Mappers
{
    public static class AuthenticationUserMapper
    {
        public static AuthenticationUserResponse ToDataContract(this UserDTO user)
            => new AuthenticationUserResponse()
            {
                Email = user.Email,
                IsActive = user.IsActive
            };
    }
}
