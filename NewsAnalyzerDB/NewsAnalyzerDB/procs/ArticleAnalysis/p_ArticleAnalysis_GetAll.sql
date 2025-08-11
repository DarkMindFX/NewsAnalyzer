

CREATE PROCEDURE [dbo].[p_ArticleAnalysis_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[ArticleAnalysis] e
END
GO