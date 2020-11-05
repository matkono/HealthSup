UPDATE MedicalAppointment
SET 
	lastNodeId = @lastNodeId
WHERE
	id = @id