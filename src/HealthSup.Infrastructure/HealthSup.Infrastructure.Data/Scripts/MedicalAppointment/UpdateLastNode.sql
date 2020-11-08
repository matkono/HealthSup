UPDATE MedicalAppointment
SET 
	currentNodeId = @currenteNodeId
WHERE
	id = @id