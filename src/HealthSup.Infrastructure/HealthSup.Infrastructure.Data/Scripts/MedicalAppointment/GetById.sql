SELECT
	ma.id,
	ma.isDiagnostic,
	p.id,
	dt.id,
	ma.currentNodeId as id,
	n.decisionTreeId,
	ma.medicalAppointmentStatusId as id
FROM
	MedicalAppointment ma
INNER JOIN Patient p ON
	p.id = ma.patientId
INNER JOIN DecisionTree dt ON
	dt.id = ma.decisionTreeId
INNER JOIN Node n ON
	n.id = ma.currentNodeId
INNER JOIN MedicalAppoinmentStatus mas ON
	mas.id = ma.medicalAppointmentStatusId
WHERE
	ma.id = @MedicalAppointmentId