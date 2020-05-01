using Cardiompp.Domain.Entities;
using System.Threading.Tasks;

namespace Cardiompp.Domain.Repositories
{
    public interface ICardiomppAgentRepository
    {
        Task<CardiomppAgent> GetByNameAndPassword(string name, string password);
    }
}
