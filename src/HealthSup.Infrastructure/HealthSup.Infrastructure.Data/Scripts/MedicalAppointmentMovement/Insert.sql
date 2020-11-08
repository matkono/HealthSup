INSERT INTO MedicalAppointmentMovement
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