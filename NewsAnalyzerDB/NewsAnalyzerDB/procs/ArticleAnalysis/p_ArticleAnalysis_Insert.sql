
CREATE PROCEDURE [dbo].[p_ArticleAnalysis_Insert]
			@ID BIGINT,
			@Timestamp DATETIME,
			@ArticleID BIGINT,
			@SentimentID BIGINT,
			@AnalyzerID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[ArticleAnalysis]
	SELECT 
		@Timestamp,
		@ArticleID,
		@SentimentID,
		@AnalyzerID
	
	

	SELECT
		e.*
	FROM
		[dbo].[ArticleAnalysis] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Timestamp IS NOT NULL THEN (CASE WHEN e.[Timestamp] = @Timestamp THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ArticleID IS NOT NULL THEN (CASE WHEN e.[ArticleID] = @ArticleID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SentimentID IS NOT NULL THEN (CASE WHEN e.[SentimentID] = @SentimentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @AnalyzerID IS NOT NULL THEN (CASE WHEN e.[AnalyzerID] = @AnalyzerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO