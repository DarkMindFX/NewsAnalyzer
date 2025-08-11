


-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @Timestamp DATETIME = '3/18/2025 11:31:32 PM'
DECLARE @ArticleID BIGINT = 21
DECLARE @SentimentID BIGINT = 7
DECLARE @AnalyzerID BIGINT = 1
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updTimestamp DATETIME = '1/28/2028 9:18:32 AM'
DECLARE @updArticleID BIGINT = 24
DECLARE @updSentimentID BIGINT = 5
DECLARE @updAnalyzerID BIGINT = 5
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[ArticleAnalysis]
				WHERE 
	(CASE WHEN @updTimestamp IS NOT NULL THEN (CASE WHEN [Timestamp] = @updTimestamp THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updArticleID IS NOT NULL THEN (CASE WHEN [ArticleID] = @updArticleID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updSentimentID IS NOT NULL THEN (CASE WHEN [SentimentID] = @updSentimentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updAnalyzerID IS NOT NULL THEN (CASE WHEN [AnalyzerID] = @updAnalyzerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[ArticleAnalysis]
	WHERE 
	(CASE WHEN @Timestamp IS NOT NULL THEN (CASE WHEN [Timestamp] = @Timestamp THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ArticleID IS NOT NULL THEN (CASE WHEN [ArticleID] = @ArticleID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SentimentID IS NOT NULL THEN (CASE WHEN [SentimentID] = @SentimentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @AnalyzerID IS NOT NULL THEN (CASE WHEN [AnalyzerID] = @AnalyzerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[ArticleAnalysis]
	WHERE 
	(CASE WHEN @updTimestamp IS NOT NULL THEN (CASE WHEN [Timestamp] = @updTimestamp THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updArticleID IS NOT NULL THEN (CASE WHEN [ArticleID] = @updArticleID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updSentimentID IS NOT NULL THEN (CASE WHEN [SentimentID] = @updSentimentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updAnalyzerID IS NOT NULL THEN (CASE WHEN [AnalyzerID] = @updAnalyzerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'ArticleAnalysis was not updated', 1
END