

CREATE PROCEDURE [dbo].[p_SentimentType_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[SentimentType] e
END
GO