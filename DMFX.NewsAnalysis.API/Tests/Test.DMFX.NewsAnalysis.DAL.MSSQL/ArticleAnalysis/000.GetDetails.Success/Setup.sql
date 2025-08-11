


DECLARE @ID BIGINT = NULL
DECLARE @Timestamp DATETIME = '8/12/2024 2:24:04 PM'
DECLARE @ArticleID BIGINT = 9
DECLARE @SentimentID BIGINT = 10
DECLARE @AnalyzerID BIGINT = 1
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[ArticleAnalysis]
				WHERE 
	(CASE WHEN @Timestamp IS NOT NULL THEN (CASE WHEN [Timestamp] = @Timestamp THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ArticleID IS NOT NULL THEN (CASE WHEN [ArticleID] = @ArticleID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SentimentID IS NOT NULL THEN (CASE WHEN [SentimentID] = @SentimentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @AnalyzerID IS NOT NULL THEN (CASE WHEN [AnalyzerID] = @AnalyzerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[ArticleAnalysis]
		(
	 [Timestamp],
	 [ArticleID],
	 [SentimentID],
	 [AnalyzerID]
		)
	SELECT 		
			 @Timestamp,
	 @ArticleID,
	 @SentimentID,
	 @AnalyzerID
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[ArticleAnalysis] e
WHERE
	(CASE WHEN @Timestamp IS NOT NULL THEN (CASE WHEN [Timestamp] = @Timestamp THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ArticleID IS NOT NULL THEN (CASE WHEN [ArticleID] = @ArticleID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SentimentID IS NOT NULL THEN (CASE WHEN [SentimentID] = @SentimentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @AnalyzerID IS NOT NULL THEN (CASE WHEN [AnalyzerID] = @AnalyzerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
