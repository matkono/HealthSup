SELECT 
	hsa.name
FROM
	HealthSupAgent hsa
WHERE
	hsa.keyAgent = @name AND
	hsa.password = @password