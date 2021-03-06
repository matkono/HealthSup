﻿SELECT
	mpm.id,
	mpm.fromNodeId as id,
	mpm.toNodeId as id,
	mpm.medicalAppointmentId as id
FROM
	MedicalAppointmentMovement mpm
WHERE 
	mpm.medicalAppointmentId = @medicalAppointmentId
ORDER BY mpm.id DESC