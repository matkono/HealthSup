SELECT
	ma.id,
	ma.isDiagnostic,
	p.id,
	dt.id,
	di.id, 
	ma.lastNodeId as id
FROM
	MedicalAppointment ma
INNER JOIN Patient p ON
	p.id = ma.patientId
INNER JOIN DecisionTree dt ON
	dt.id = ma.decisionTreeId
INNER JOIN Disease di ON
	di.id = dt.diseaseId
WHERE
	ma.id = @MedicalAppointmentId