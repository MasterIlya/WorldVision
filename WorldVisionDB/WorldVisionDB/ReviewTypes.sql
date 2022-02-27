CREATE TABLE [dbo].[ReviewTypes]
(
	[ReviewTypeId] INT CONSTRAINT PK_ReviewTypes_ReviewTypeId PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[ReviewType] NVARCHAR(50) NOT NULL
)
