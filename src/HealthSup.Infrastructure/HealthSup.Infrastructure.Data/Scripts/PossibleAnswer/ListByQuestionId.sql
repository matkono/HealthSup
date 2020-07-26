SELECT 
	pa.id,
	pa.code,
	pa.value
FROM 
	Question q
INNER JOIN QuestionPossibleAnswer qpa ON
	qpa.questionId = q.id
INNER JOIN PossibleAnswer pa ON
	pa.id = qpa.possibleAnswerId
WHERE
	q.id = @QuestionId