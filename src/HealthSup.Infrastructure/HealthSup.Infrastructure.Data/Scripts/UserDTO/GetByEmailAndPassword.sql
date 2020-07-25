SELECT 
	u.id,
	u.email, 
	u.isActive
FROM
	UserHealthSup u
WHERE
	u.email = @email AND
	u.password = @password