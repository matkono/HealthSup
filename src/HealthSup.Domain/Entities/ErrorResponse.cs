namespace HealthSup.Domain.Entities
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
        }

        public int Code { get; set; }

        public string Message { get; set; }

        public string Field { get; set; }
    }
}