using System.Security.Cryptography;

namespace Cardiompp.Application.Services.Contracts
{
    public interface IHashService
    {
        string GetHash(string value);
    }
}
