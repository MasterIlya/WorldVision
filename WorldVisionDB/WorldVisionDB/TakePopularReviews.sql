CREATE PROCEDURE [dbo].[TakePopularReviews]
	@take INT
  AS
  SELECT NewT.ReviewId, UserId, ReviewTypeId, Title, Content, AuthorScore, CreateDate, UpdateDate, LikeCount
  FROM (SELECT TOP(@take) ReviewId, COUNT(*) as 'LikeCount'
  FROM ReviewLikes 
  GROUP BY ReviewId 
  ORDER BY LikeCount DESC) newT
  JOIN Reviews ON Reviews.ReviewId = newT.ReviewId
