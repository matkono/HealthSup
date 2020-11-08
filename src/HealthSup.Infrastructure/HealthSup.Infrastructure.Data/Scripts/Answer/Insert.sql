INSERT INTO Answer
(
	dateAnswered, 
	questionId, 
	possibleAnswerId, 
	doctorId, 
	medicalAppointmentId
)
VALUES 
(
	@dateAnswered, 
	@questionId,
	@possibleAnswerId, 
	@doctorId, 
	@medicalAppointmentId
);