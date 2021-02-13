SELECT 
	d.id,
	d.name
FROM 
	Disease d
ORDER BY d.id
OFFSET ((@PageNumber - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY;