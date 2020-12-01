namespace HealthSup.Application.DataContracts.v1.Responses.Decision
{
    public class DecisionBase
    {
        public int Id { get; set; }

        public int Code { get; set; }

        public string Title { get; set; }

        public bool IsDiagnostic { get; set; }
    }
}
