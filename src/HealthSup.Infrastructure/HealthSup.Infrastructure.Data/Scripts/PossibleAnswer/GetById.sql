SELECT 
	pa.id,
	pa.code,
	pa.title,
	pa.possibleAnswerGropuId as id
FROM 
	PossibleAnswer pa
WHERE 
	pa.id = @id