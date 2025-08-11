
CREATE PROCEDURE [dbo].[p_ArticleAnalysis_GetByAnalyzerID]

	@AnalyzerID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[ArticleAnalysis] c 
				WHERE
					[AnalyzerID] = @AnalyzerID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[ArticleAnalysis] e
		WHERE 
			[AnalyzerID] = @AnalyzerID	

	END
	ELSE
		SET @Found = 0;
END
GO