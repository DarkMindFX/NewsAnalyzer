-- Table contains list of news sources - i.e. websites or feeds from which articles are fetched
CREATE TABLE [dbo].[NewsSource]
(
	[ID]			BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Name]			NVARCHAR(255) NOT NULL,
	[Url]			NVARCHAR(255) NOT NULL,
	[IsActive]		BIT NOT NULL DEFAULT 0

)
