﻿CREATE TABLE [dbo].[ReviewTags]
(
	[TagId] INT CONSTRAINT PK_ReviewTags_TagId PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[ReviewId] INT NOT NULL,
	[Tag] NVARCHAR(100) NOT NULL

	CONSTRAINT FK_ReviewTags_Reviews FOREIGN KEY (ReviewId) REFERENCES Reviews(ReviewId)
)