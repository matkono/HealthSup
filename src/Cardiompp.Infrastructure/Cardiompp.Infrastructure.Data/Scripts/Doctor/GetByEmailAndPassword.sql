SELECT 
	d.id,
	d.crm,
	d.email,
	d.isActive
FROM
	Doctor d
WHERE
	d.email = @email AND
	d.password = @password