CREATE PROCEDURE [dbo].[TakePopularReviews]
	@take INT
  AS
  IF OBJECT_ID('tempdb..#ReviewsRating') IS NOT NULL
	BEGIN
		DROP TABLE
	#ReviewsRating
	END
  CREATE TABLE #ReviewsRating
	(ReviewId INT,
	LikeCount INT)

	INSERT INTO #ReviewsRating
	SELECT ReviewId, COUNT(*) as 'LikeCount'
	FROM ReviewLikes
	GROUP BY ReviewId

	SELECT TOP(@take) Reviews.ReviewId, UserId, ReviewTypeId, Title, Content, AuthorScore, CreateDate, UpdateDate, LikeCount
	FROM Reviews
	LEFT JOIN #ReviewsRating ON Reviews.ReviewId = #ReviewsRating.ReviewId
	ORDER BY LikeCount DESC

	DROP TABLE #ReviewsRating