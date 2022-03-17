CREATE TABLE [dbo].[ReviewLikes]
(
	[LikeId] INT CONSTRAINT PK_ReviewLikes_LikeId PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[UserId] INT NOT NULL,
	[ReviewId] INT NOT NULL

	CONSTRAINT FK_ReviewLikes_Users FOREIGN KEY (UserId) REFERENCES Users (UserId)
	CONSTRAINT FK_ReviewLikes_Reviews FOREIGN KEY (ReviewId) REFERENCES Reviews(ReviewId)
)
