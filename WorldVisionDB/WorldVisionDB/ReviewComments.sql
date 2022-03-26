CREATE TABLE [dbo].[ReviewComments]
(
	[CommentId] INT CONSTRAINT PK_ReviewComments_CommentId PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[UserId] INT NOT NULL,
	[ReviewId] INT NOT NULL,
	[Content] NVARCHAR(1000) NOT NULL,
	[CreateDate] DATETIME NOT NULL 

	CONSTRAINT FK_ReviewComments_Users FOREIGN KEY (UserId) REFERENCES Users (UserId),
	CONSTRAINT FK_ReviewComments_Reviews FOREIGN KEY (ReviewId) REFERENCES Reviews(ReviewId)
)
