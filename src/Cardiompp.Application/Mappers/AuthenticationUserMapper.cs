using Cardiompp.Application.DataContracts.v1.Responses.Authentication;
using Cardiompp.Infrastructure.CrossCutting.Authentication.DTO;

namespace Cardiompp.Application.Mappers
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
