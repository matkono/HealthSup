INSERT INTO MedicalAppointment
(
	isDiagnostic,
	patientId,
	decisionTreeId,
	currentNodeId,
	medicalAppointmentStatusId
)
VALUES
(
	'FALSE',
	@patientId,
	@decisionTreeId,
	@currenteNodeId,
	@medicalAppointmentStatusId
)
SELECT SCOPE_IDENTITY();