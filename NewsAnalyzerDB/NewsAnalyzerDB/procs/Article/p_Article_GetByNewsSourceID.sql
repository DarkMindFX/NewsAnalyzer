
CREATE PROCEDURE [dbo].[p_Article_GetByNewsSourceID]

	@NewsSourceID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Article] c 
				WHERE
					[NewsSourceID] = @NewsSourceID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Article] e
		WHERE 
			[NewsSourceID] = @NewsSourceID	

	END
	ELSE
		SET @Found = 0;
END
GO