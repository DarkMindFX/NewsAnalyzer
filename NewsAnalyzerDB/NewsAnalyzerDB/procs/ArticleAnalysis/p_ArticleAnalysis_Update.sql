
CREATE PROCEDURE [dbo].[p_ArticleAnalysis_Update]
			@ID BIGINT,
			@Timestamp DATETIME,
			@ArticleID BIGINT,
			@SentimentID BIGINT,
			@AnalyzerID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[ArticleAnalysis]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[ArticleAnalysis]
		SET
									[Timestamp] = IIF( @Timestamp IS NOT NULL, @Timestamp, [Timestamp] ) ,
									[ArticleID] = IIF( @ArticleID IS NOT NULL, @ArticleID, [ArticleID] ) ,
									[SentimentID] = IIF( @SentimentID IS NOT NULL, @SentimentID, [SentimentID] ) ,
									[AnalyzerID] = IIF( @AnalyzerID IS NOT NULL, @AnalyzerID, [AnalyzerID] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'ArticleAnalysis was not found', 1;
	END	

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