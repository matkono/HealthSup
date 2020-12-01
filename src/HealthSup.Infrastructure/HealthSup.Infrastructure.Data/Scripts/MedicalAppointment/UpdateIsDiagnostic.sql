UPDATE MedicalAppointment 
SET
	isDiagnostic = @isDiagnostic
WHERE 
	id = @id