SELECT
	ma.id,
	ma.isDiagnostic,
	p.id as patientId,
	d.id as doctorId,
	dt.id as decisionTreeId,
	di.id as diseaseId, 
	ma.lastNodeId as nodeId
FROM
	MedicalAppointment ma
INNER JOIN Patient p ON
	p.id = ma.patientId
INNER JOIN Doctor d ON
	d.id = ma.doctorId
INNER JOIN DecisionTree dt ON
	dt.id = ma.decisionTreeId
INNER JOIN Disease di ON
	di.id = dt.diseaseId
WHERE
	ma.id = 1