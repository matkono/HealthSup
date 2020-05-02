SELECT 
	d.id,
	d.name,
	d.crm,
	d.phone,
	d.email,
	d.isActive,
	a.id,
	a.region,
	a.cep
FROM
	Doctor d
INNER JOIN 
	Address a
ON
	d.addressId = a.Id
WHERE
	d.crm = @crm