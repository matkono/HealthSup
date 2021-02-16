SELECT 
	d.id,
	d.name
FROM 
	Disease d
WHERE
	d.id = @id