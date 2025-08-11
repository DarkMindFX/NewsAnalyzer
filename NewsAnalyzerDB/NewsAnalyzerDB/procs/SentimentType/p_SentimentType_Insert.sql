
CREATE PROCEDURE [dbo].[p_SentimentType_Insert]
			@ID BIGINT,
			@Name NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[SentimentType]
	SELECT 
		@Name
	
	

	SELECT
		e.*
	FROM
		[dbo].[SentimentType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO