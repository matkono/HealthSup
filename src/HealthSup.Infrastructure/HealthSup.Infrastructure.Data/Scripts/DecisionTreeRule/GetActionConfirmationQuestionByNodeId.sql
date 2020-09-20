SELECT 
	dtr.id,
	dtr.nodeId,
	dtr.nextNodeId,
	dtr.possibleAnswerGroupId
FROM 
	DecisionTreeRules dtr
WHERE
	dtr.nodeId = @NodeId