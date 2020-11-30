namespace HealthSup.Application.DataContracts.v1.Requests.DecisionEngine
{
    public class ConfirmDecisionRequest
    {
        public int MedicalAppointmentId { get; set; }

        public int DecisionId { get; set; }
    }
}
