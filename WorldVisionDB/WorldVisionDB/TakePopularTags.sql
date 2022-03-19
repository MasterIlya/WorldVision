CREATE PROCEDURE [dbo].[TakePopularTags]
	@take INT
  AS
  SELECT TOP(@take) Tag, COUNT(*) as 'TagCount'
  FROM ReviewTags 
  GROUP BY Tag
  ORDER BY TagCount DESC

	