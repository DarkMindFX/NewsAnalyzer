-- Table contains list of articles
CREATE TABLE [dbo].[Article]
(
	[ID] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Title] NVARCHAR(255) NOT NULL,
	[Content] NVARCHAR(MAX) NOT NULL,
	[Timestamp]	DATETIME NOT NULL DEFAULT GETDATE(),
	[NewsSourceID]	BIGINT NOT NULL,

	[Url] NVARCHAR(512) NOT NULL, 
    [NewsTime] DATETIME NOT NULL, 
    CONSTRAINT [FK_Articale_NewsSource] FOREIGN KEY ([NewsSourceID]) REFERENCES [dbo].[NewsSource]([ID])

)
