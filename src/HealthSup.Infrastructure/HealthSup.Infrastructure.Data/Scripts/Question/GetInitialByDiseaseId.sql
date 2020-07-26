SELECT
	q.id,
	q.code,
	q.title,
	q.isInitial,
	qt.id,
	qt.code,
	qt.name,
	pa.id,
	pa.code,
	pa.value
FROM 
	Question q
INNER JOIN QuestionType qt ON
	qt.id = q.questionTypeId
INNER JOIN Node n ON
	n.questionId = q.id
INNER JOIN DecisionTree dt ON
	dt.id = n.decisionTreeId
INNER JOIN Disease d ON
	d.id = dt.diseaseId
INNER JOIN QuestionPossibleAnswer qpa ON
	qpa.questionId = q.id
INNER JOIN PossibleAnswer pa ON
	pa.id = qpa.possibleAnswerId
WHERE
	q.isInitial = 'True' AND
	d.Id = @DiseaseId