using HealthSup.Infrastructure.CrossCutting.Hash.Services.Contracts;
using System.Security.Cryptography;
using System.Text;

namespace HealthSup.Infrastructure.CrossCutting.Services.Hash
{
    public class HashService : IHashService
    {
        public string GetMd5Hash
        (
            string value
        )
        {
            using (MD5 md5Hash = MD5.Create())
            {
                return GenerateMd5hash(md5Hash, value);
            }
        }

        private static string GenerateMd5hash
        (
            MD5 md5Hash, 
            string value
        )
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
