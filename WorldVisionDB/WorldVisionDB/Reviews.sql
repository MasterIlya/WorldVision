CREATE TABLE [dbo].[Reviews]
(
	[ReviewId] INT CONSTRAINT PK_Reviews_ReviewId PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[UserId] INT NOT NULL,
	[ReviewTypeId] INT NOT NULL,
	[Title] NVARCHAR(50) NOT NULL,
	[Content] NVARCHAR(MAX) NOT NULL,
	[AuthorScore] TINYINT NOT NULL, 
	[CreateDate] DATETIME NOT NULL,
	[UpdateDate] DATETIME

	CONSTRAINT FK_Reviews_Users FOREIGN KEY (UserId) REFERENCES Users (UserId)
	CONSTRAINT FK_Reviews_ReviewTypes FOREIGN KEY (ReviewTypeId) REFERENCES ReviewTypes (ReviewTypeId)
)
