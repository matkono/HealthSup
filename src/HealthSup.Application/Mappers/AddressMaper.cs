using HealthSup.Application.DataContracts.v1.Responses.Address;
using HealthSup.Domain.Entities;

namespace HealthSup.Application.Mappers
{
    public static class AddressMaper
    {
        public static AddressResponse ToDataContract(this Address address)
            => new AddressResponse()
            {
                Cep = address.Cep,
                Neighborhood = address.Neighborhood,
                City = address.City
            };
    }
}
