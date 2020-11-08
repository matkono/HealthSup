SELECT 
	dtr.id,
	dtr.fromNodeId as id,
	dtr.toNodeId as id,
	dtr.possibleAnswerGroupId as id
FROM 
	DecisionTreeRules dtr
WHERE
	dtr.fromNodeId = @fromNodeId