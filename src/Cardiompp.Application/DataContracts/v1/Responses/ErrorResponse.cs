namespace HealthSup.Application.DataContracts.Responses
{
    public class ErrorResponse
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public string Field { get; set; }
    }
}
