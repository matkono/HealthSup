UPDATE MedicalAppointment
SET
	medicalAppointmentStatusId = @statusId
WHERE 
	id = @id