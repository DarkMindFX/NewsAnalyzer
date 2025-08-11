

CREATE PROCEDURE [dbo].[p_NewsSource_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[NewsSource] e
END
GO