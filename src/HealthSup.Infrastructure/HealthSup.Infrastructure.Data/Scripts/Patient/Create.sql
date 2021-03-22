INSERT INTO Patient
(
	name,
	registration,
	addressId
)
VALUES
(
	@name, 
	@registration,
	@addressId
)
SELECT SCOPE_IDENTITY();