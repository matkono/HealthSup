using Cardiompp.Domain.Services.Contracts;
using System.Security.Cryptography;
using System.Text;

namespace Cardiompp.Domain.Services
{
    public class HashDomainService: IHashDomainService
    {
        public string GetMd5Hash(string value)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                return GenerateMd5hash(md5Hash, value);
            }
        }

        private static string GenerateMd5hash(MD5 md5Hash, string value)
        {
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(value));
            var sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
