SELECT 
	q.Id as questionId,
	q.code,
	q.title,
	n.id,
	n.isInitial,
	q.questionTypeId as id,
	n.nodeTypeId as id,
	n.decisionTreeId as id
FROM 
	Question q
INNER JOIN Node n On
	n.id = q.nodeId
WHERE
	q.nodeId = @NodeId