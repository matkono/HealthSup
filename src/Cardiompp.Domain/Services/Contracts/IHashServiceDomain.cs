namespace Cardiompp.Domain.Services.Contracts
{
    public interface IHashServiceDomain
    {
        string GetMd5Hash(string value);
    }
}
