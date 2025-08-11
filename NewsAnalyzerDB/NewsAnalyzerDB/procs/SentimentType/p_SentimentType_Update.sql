
CREATE PROCEDURE [dbo].[p_SentimentType_Update]
			@ID BIGINT,
			@Name NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[SentimentType]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[SentimentType]
		SET
									[Name] = IIF( @Name IS NOT NULL, @Name, [Name] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'SentimentType was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[SentimentType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO