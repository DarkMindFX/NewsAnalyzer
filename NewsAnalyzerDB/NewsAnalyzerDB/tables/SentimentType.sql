-- Enum of values: Positive, Negative, Neutral
CREATE TABLE [dbo].[SentimentType]
(	
	[ID]	BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Name]	NVARCHAR(50) NOT NULL
)
