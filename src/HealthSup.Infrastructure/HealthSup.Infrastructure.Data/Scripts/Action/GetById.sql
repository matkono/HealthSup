SELECT 
	a.Id,
	a.code,
	a.title,
	n.id,
	n.isInitial,
	n.nodeTypeId as id,
	n.decisionTreeId as id,
	dt.version,
	dt.description,
	dt.isCurrent
FROM 
	Action a
INNER JOIN Node n ON
	n.id = a.nodeId
INNER JOIN DecisionTree dt ON
	dt.id = n.decisionTreeId
WHERE
	a.id = @id