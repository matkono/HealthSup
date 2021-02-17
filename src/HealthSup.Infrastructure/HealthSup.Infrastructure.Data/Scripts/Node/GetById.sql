SELECT 
	n.id,
	n.isInitial,
	n.nodeTypeId as id,
	n.decisionTreeId as id,
	dt.version,
	dt.description,
	dt.isCurrent
FROM 
	Node n
INNER JOIN DecisionTree dt ON
	dt.id = n.decisionTreeId
WHERE 
	n.id = @id