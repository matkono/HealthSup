using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthSup.Application.DataContracts.v1.Requests.Node
{
    public class GetNextNodeRequest
    {
        [Required(ErrorMessage = "MedicalAppointmentId is required to get next node.")]
        public int MedicalAppointmentId { get; set; }

        [Required(ErrorMessage = "DoctorId is required to get next node.")]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "QuestionId is required to get next node.")]
        public int QuestionId { get; set; }

        [Required(ErrorMessage = "PossibleAnswerGroupId is required to get next node.")]
        public int PossibleAnswerGroupId { get; set; }

        [Required(ErrorMessage = "Date is required to get next node.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "PossibleAnswersId is required to get next node.")]
        public List<int> PossibleAnswersId { get; set; }

    }
}
