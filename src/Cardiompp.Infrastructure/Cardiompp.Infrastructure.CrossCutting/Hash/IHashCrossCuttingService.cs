namespace Cardiompp.Infrastructure.CrossCutting.Hash.Services.Contracts
{
    public interface IHashCrossCuttingService
    {
        string GetMd5Hash(string value);
    }
}
