INSERT INTO MedicalAppointmentFlow
(
	fromNodeId,
	toNodeId,
	medicalAppointmentId
)
VALUES
(
	@fromNodeId,
	@toNodeId,
	@medicalAppointmentId
);