SELECT 
	dt.id,
	dt.version,
	dt.description,
	dt.isCurrent,
	dt.diseaseId as Id,
	d.name
FROM
	DecisionTree dt
INNER JOIN Disease d ON
	dt.diseaseId = d.id
WHERE 
	dt.diseaseId = @diseaseId AND
	dt.isCurrent = 'TRUE'