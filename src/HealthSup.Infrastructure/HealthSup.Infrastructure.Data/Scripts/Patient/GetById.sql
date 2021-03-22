SELECT 
	p.id,
	p.name,
	p.registration,
	a.id as id,
	a.cep,
	a.city,
	a.neighborhood
FROM 
	Patient p
INNER JOIN Address a ON 
	a.id = p.addressId
WHERE
	p.id =  @id