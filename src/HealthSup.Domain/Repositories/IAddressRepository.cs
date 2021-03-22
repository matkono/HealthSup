using HealthSup.Domain.Entities;
using System.Threading.Tasks;

namespace HealthSup.Domain.Repositories
{
    public interface IAddressRepository
    {
        Task<Address> GetById
        (
            int id
        );

        Task<Address> GetByCep
        (
            string cep
        );

        Task<Address> Create
        (
            Address address
        );
    }
}
