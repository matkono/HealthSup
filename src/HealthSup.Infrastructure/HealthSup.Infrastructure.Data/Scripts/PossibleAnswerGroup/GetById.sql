SELECT
	pag.id,
	pag.description
FROM
	PossibleAnswerGroup pag
WHERE
	pag.id = @id