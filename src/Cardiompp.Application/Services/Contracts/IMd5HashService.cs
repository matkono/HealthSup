using System.Security.Cryptography;

namespace Cardiompp.Application.Services.Contracts
{
    public interface IMd5HashService
    {
        string GetMd5Hash(string value);
    }
}
