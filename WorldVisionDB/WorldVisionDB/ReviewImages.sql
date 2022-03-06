CREATE TABLE [dbo].[ReviewImages]
(
	[ImageId] INT CONSTRAINT PK_ReviewImages_ImageId PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[ReviewId] INT NOT NULL,
	[ImageURL] NVARCHAR(MAX) NOT NULL,
	[Size] BIGINT NOT NULL,

	CONSTRAINT FK_ReviewImages_Reviews FOREIGN KEY (ReviewId) REFERENCES Reviews (ReviewId)
)
