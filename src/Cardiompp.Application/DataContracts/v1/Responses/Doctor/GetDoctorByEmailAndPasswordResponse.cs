namespace Cardiompp.Application.DataContracts.v1.Responses.Doctor
{
    public class GetDoctorByEmailAndPasswordResponse
    {
        public string Crm { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }
    }
}
