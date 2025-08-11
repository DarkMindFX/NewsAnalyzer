
CREATE PROCEDURE [dbo].[p_Article_Update]
			@ID BIGINT,
			@Title NVARCHAR(255),
			@Content NVARCHAR(4000),
			@Timestamp DATETIME,
			@NewsSourceID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Article]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Article]
		SET
									[Title] = IIF( @Title IS NOT NULL, @Title, [Title] ) ,
									[Content] = IIF( @Content IS NOT NULL, @Content, [Content] ) ,
									[Timestamp] = IIF( @Timestamp IS NOT NULL, @Timestamp, [Timestamp] ) ,
									[NewsSourceID] = IIF( @NewsSourceID IS NOT NULL, @NewsSourceID, [NewsSourceID] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Article was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[Article] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Title IS NOT NULL THEN (CASE WHEN e.[Title] = @Title THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Content IS NOT NULL THEN (CASE WHEN e.[Content] = @Content THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Timestamp IS NOT NULL THEN (CASE WHEN e.[Timestamp] = @Timestamp THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @NewsSourceID IS NOT NULL THEN (CASE WHEN e.[NewsSourceID] = @NewsSourceID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO