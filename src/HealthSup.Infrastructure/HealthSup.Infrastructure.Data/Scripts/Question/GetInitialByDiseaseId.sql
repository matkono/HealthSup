SELECT
	q.id,
	q.code,
	q.title,
	q.IsInitial,
	qt.id,
	qt.code,
	qt.name
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
WHERE
	q.isInitial = 'True' AND
	d.Id = @DiseaseId