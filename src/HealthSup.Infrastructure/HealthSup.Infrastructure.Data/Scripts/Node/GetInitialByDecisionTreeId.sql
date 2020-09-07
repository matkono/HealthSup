SELECT
	n.id, 
	n.isInitial,
	n.nodeTypeId,
	n.decisionTreeId
FROM
	Node n
WHERE
	n.isInitial = 'True' AND
	N.decisionTreeId = @DecisionTreeId