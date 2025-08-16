-- Table contains information about article analysis conducted by specific analyzer
CREATE TABLE [dbo].[ArticleAnalysis]
(
	[ID]			BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Timestamp]		DATETIME NOT NULL DEFAULT GETDATE(),
	[ArticleID]		BIGINT NOT NULL,
	[SentimentID]	BIGINT NOT NULL,
	[AnalyzerID]	BIGINT NOT NULL,

	CONSTRAINT [FK_ArticleAnalysis_Articale] FOREIGN KEY ([ArticleID]) REFERENCES [dbo].[Article]([ID]) ON DELETE CASCADE,
	CONSTRAINT [FK_ArticleAnalysis_SentimentType] FOREIGN KEY ([SentimentID]) REFERENCES [dbo].[SentimentType]([ID]),
	CONSTRAINT [FK_ArticleAnalysis_Analyzer] FOREIGN KEY ([AnalyzerID]) REFERENCES [dbo].[Analyzer]([ID]) ON DELETE CASCADE

)
