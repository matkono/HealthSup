SELECT
	n.id, 
	n.isInitial,
	n.nodeTypeId as id,
	n.decisionTreeId as id
FROM
	Node n
WHERE
	n.isInitial = 'True' AND
	N.decisionTreeId = @DecisionTreeId