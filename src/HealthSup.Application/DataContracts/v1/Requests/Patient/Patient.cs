namespace HealthSup.Application.DataContracts.v1.Requests.Patient
{
    public class Patient
    {
        public string Name { get; set; }

        public string Registration { get; set; }

        public Address.Address Address { get; set;}
    }
}
