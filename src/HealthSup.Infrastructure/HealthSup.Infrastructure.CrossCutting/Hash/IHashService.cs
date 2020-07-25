namespace HealthSup.Infrastructure.CrossCutting.Hash.Services.Contracts
{
    public interface IHashService
    {
        string GetMd5Hash(string value);
    }
}
