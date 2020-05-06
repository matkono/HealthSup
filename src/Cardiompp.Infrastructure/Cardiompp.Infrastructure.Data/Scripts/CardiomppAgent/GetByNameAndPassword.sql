SELECT 
	ca.name
FROM
	CardiomppAgent ca
WHERE
	ca.name = @name AND
	ca.password = @password