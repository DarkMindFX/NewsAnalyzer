

CREATE PROCEDURE [dbo].[p_Article_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Article] e
END
GO