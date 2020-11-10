namespace HealthSup.Application.DataContracts.v1.Requests.DecisionEngine
{
    public class GetPreviousNodeRequest
    {
        public int MedicalAppointmentId { get; set; }

        public int CurrentNodeId { get; set; }
    }
}
