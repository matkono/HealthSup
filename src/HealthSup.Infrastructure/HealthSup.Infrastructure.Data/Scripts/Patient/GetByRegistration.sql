SELECT 
	p.id,
	p.name,
	p.registration
FROM 
	Patient p
WHERE
	p.registration = @registration