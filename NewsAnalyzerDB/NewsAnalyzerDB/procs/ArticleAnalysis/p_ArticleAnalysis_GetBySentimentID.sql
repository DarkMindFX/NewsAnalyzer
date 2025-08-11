
CREATE PROCEDURE [dbo].[p_ArticleAnalysis_GetBySentimentID]

	@SentimentID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[ArticleAnalysis] c 
				WHERE
					[SentimentID] = @SentimentID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[ArticleAnalysis] e
		WHERE 
			[SentimentID] = @SentimentID	

	END
	ELSE
		SET @Found = 0;
END
GO