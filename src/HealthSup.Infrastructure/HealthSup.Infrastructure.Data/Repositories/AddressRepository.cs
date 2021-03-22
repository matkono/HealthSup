using Dapper;
using HealthSup.Domain.Entities;
using HealthSup.Domain.Repositories;
using HealthSup.Infrastructure.Data.Scripts;
using System.Linq;
using System.Threading.Tasks;

namespace HealthSup.Infrastructure.Data.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        public AddressRepository
        (
            IUnitOfWork unitOfWork
        )
        {
            UnitOfWork = unitOfWork;
        }

        private IUnitOfWork UnitOfWork { get; }

        public async Task<Address> GetById
        (
            int id
        )
        {
            var query = ScriptManager.GetByName(ScriptManager.FileNames.Address.GetById);

            var result = await UnitOfWork.Connection.QueryAsync<Address>(
                                                                query,
                                                                new { id },
                                                                UnitOfWork.Transaction);

            return result.FirstOrDefault();
        }

        public async Task<Address> GetByCep
        (
            string cep
        )
        {
            var query = ScriptManager.GetByName(ScriptManager.FileNames.Address.GetByCep);

            var result = await UnitOfWork.Connection.QueryAsync<Address>(
                                                                query,
                                                                new { cep },
                                                                UnitOfWork.Transaction);

            return result.FirstOrDefault();
        }

        public async Task<Address> Create
        (
            Address address
        )
        {
            var query = ScriptManager.GetByName(ScriptManager.FileNames.Address.Create);
            var parameters = new DynamicParameters();
            parameters.Add("@neighborhood", address.Neighborhood);
            parameters.Add("@cep", address.Cep);
            parameters.Add("@city", address.City);

            var result = await UnitOfWork.Connection.QueryAsync<int>(query,
                                                      parameters,
                                                      UnitOfWork.Transaction);

            return await GetById(result.Single());
        }
    }
}
