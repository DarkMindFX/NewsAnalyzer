


-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @Timestamp DATETIME = '12/21/2027 5:58:04 AM'
DECLARE @ArticleID BIGINT = 35
DECLARE @SentimentID BIGINT = 8
DECLARE @AnalyzerID BIGINT = 9
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updTimestamp DATETIME = '5/9/2025 6:25:04 AM'
DECLARE @updArticleID BIGINT = 35
DECLARE @updSentimentID BIGINT = 1
DECLARE @updAnalyzerID BIGINT = 3
 

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