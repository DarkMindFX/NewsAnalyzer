/*
Usage:
EXEC [dbo].[p_Util_ClearAll]
*/

CREATE PROCEDURE [dbo].[p_Util_ClearAll]
AS
BEGIN

	DELETE FROM [dbo].[ArticleAnalysis]
	DELETE FROM [dbo].[Article]
	DELETE FROM [dbo].[NewsSource]

	DELETE FROM [dbo].[SentimentType] 

END
