SELECT 
	p.id,
	p.name,
	p.registration
FROM Patient p
ORDER BY id
OFFSET ((@PageNumber - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY;