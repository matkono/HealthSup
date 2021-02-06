SELECT 
	COUNT(1) as 'COUNT'
FROM MedicalAppointment
WHERE
	patientId = @patientId