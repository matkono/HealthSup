using HealthSup.Application.DataContracts.v1.Responses.Address;
using AddressModel = HealthSup.Domain.Entities.Address;
using AddressContract = HealthSup.Application.DataContracts.v1.Requests.Address.Address;

namespace HealthSup.Application.Mappers
{
    public static class AddressMaper
    {
        public static AddressResponse ToDataContract(this AddressModel addressModel)
            => new AddressResponse()
            {
                Cep = addressModel.Cep,
                Neighborhood = addressModel.Neighborhood,
                City = addressModel.City
            };

        public static AddressModel ToModel(this AddressContract addressContract)
            => new AddressModel(addressContract.Neighborhood, addressContract.Cep, addressContract.City);
    }
}
