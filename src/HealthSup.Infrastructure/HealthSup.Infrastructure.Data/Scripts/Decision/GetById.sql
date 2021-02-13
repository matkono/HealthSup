SELECT 
	d.Id,
	d.code,
	d.title,
	d.isDiagnostic,
	n.id,
	n.isInitial,
	n.nodeTypeId as id,
	n.decisionTreeId as id,
	dt.version,
	dt.description,
	dt.isCurrent
FROM 
	Decision d
INNER JOIN Node n ON
	n.id = d.nodeId
INNER JOIN DecisionTree dt ON
	dt.id = n.decisionTreeId
WHERE
	d.id = @id