INSERT INTO Address
(
	neighborhood,
	cep,
	city
)
VALUES
(
	@neighborhood,
	@cep,
	@city
)
SELECT SCOPE_IDENTITY();