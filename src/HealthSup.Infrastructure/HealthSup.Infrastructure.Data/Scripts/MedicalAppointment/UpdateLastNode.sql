UPDATE MedicalAppointment
SET 
	currentNodeId = @currentNodeId
WHERE
	id = @id