SELECT 
	a.id,
	a.neighborhood,
	a.cep,
	a.city
FROM 
	Address a
WHERE
	a.cep = @cep