namespace HealthSup.Infrastructure.CrossCutting.JwtToken
{
    public class JwtTokenConfiguration
    {
        public string SecretKey { get; set; } = "SecretKeywqewqeqqqqqqqqqqqweeeeeeeeeeeeeeeeeee";

        public string Issuer { get; set; } = "https://localhost:44378";

        public int LifeTime { get; set; } = 30;
    }
}
