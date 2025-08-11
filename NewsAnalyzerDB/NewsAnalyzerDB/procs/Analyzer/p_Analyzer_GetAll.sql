

CREATE PROCEDURE [dbo].[p_Analyzer_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Analyzer] e
END
GO