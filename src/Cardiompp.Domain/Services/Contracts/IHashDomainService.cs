namespace Cardiompp.Domain.Services.Contracts
{
    public interface IHashDomainService
    {
        string GetMd5Hash(string value);
    }
}
