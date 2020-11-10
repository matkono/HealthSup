SELECT 
	a.id,
	a.dateAnswered,
	a.questionId as id,
	a.possibleAnswerId as id,
	a.doctorId as id,
	a.medicalAppointmentId as id
FROM 
	Answer a
WHERE 
	a.questionId = @questionId