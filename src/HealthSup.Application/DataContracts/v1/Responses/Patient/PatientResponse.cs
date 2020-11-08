using HealthSup.Application.DataContracts.v1.Responses.Address;
using HealthSup.Application.DataContracts.v1.Responses.MedicalAppointment;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HealthSup.Application.DataContracts.v1.Responses.Patient
{
    [DataContract]
    public class PatientResponse
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Registration { get; set; }

        [DataMember]
        public AddressResponse Address { get; set; }

        [DataMember]
        public List<MedicalAppointmentResponse> MedicalAppointments { get; set; }
    }
}
