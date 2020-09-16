SELECT 
	pa.id,
	pa.code,
	pa.title,
	pa.setPossibleAnswerId as id
FROM 
	PossibleAnswer pa
INNER JOIN QuestionPossibleAnswer qpa ON
	qpa.possibleAnswerId = pa.id
WHERE qpa.questionId = @QuestionId