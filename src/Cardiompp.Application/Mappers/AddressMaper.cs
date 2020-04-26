using Cardiompp.Application.DataContracts.v1.Responses.Address;
using Cardiompp.Domain.Entities;

namespace Cardiompp.Application.Mappers
{
    public static class AddressMaper
    {
        public static AddressResponse ToDataContract(this Address address)
            => new AddressResponse()
            {
                Cep = address.Cep,
                Region = address.Region
            };
    }
}
