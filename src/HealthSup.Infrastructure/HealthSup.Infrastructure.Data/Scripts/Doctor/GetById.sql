SELECT 
	d.id,
	d.name,
	d.crm,
	d.phone,
	d.userHealthSupId as id
FROM 
	Doctor d
WHERE
	d.id = @id