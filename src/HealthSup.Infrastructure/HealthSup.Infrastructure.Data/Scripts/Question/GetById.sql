SELECT 
	q.Id,
	q.code,
	q.title,
	n.id,
	n.isInitial,
	q.questionTypeId as id,
	n.nodeTypeId as id,
	n.decisionTreeId as id,
	dt.version,
	dt.description,
	dt.isCurrent
FROM 
	Question q
INNER JOIN Node n On
	n.id = q.nodeId
INNER JOIN DecisionTree dt ON
	dt.id = n.decisionTreeId
WHERE
	q.id = @id