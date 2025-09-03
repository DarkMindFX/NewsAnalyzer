

CREATE PROCEDURE [dbo].[p_Article_GetDetailsByUrl]
		@Url NVARCHAR(250),	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Article] c 
				WHERE 
								[Url] = @Url	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Article] e
		WHERE 
								[Url] = @Url	
				END
	ELSE
		SET @Found = 0;
END
GO