namespace HealthSup.Domain.Enums
{
    public enum ValidationErrorCodeEnum
    {
        #region Authentication

        EmailOrPasswordInvalid = 100,
        AgentNameOrPasswordInvalid = 101,

        #endregion

        #region MedicalAppointment

        MedicalAppointmentIdIsNull = 200,
        MedicalAppointNotFound = 201,
        MedicalAppointmentIsFinalized = 202,

        #endregion

        #region Doctor

        DoctorIdIsNull = 300,
        DoctorNotFound = 301,

        #endregion

        #region Question

        QuestionIdIsNull = 400,
        QuestionNotFound = 401,
        QuestionIsNotCurrentNode = 402,

        #endregion

        #region PossibleAnswerGroup

        PossibleAnswerGroupIdIsNull = 500,
        PossibleAnswerGroupNotFound = 501,
        PossibleAnswerInvalidForQuestion = 502,

        #endregion

        #region PossibleAnswer

        PossibleAnswerIsNull = 600,
        PossibleAnswerIsEmpty = 601,

        #endregion
    }
}
