
CREATE PROCEDURE [dbo].[p_NewsSource_Update]
			@ID BIGINT,
			@Name NVARCHAR(255),
			@Url NVARCHAR(255),
			@IsActive BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[NewsSource]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[NewsSource]
		SET
									[Name] = IIF( @Name IS NOT NULL, @Name, [Name] ) ,
									[Url] = IIF( @Url IS NOT NULL, @Url, [Url] ) ,
									[IsActive] = IIF( @IsActive IS NOT NULL, @IsActive, [IsActive] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'NewsSource was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[NewsSource] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Url IS NOT NULL THEN (CASE WHEN e.[Url] = @Url THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsActive IS NOT NULL THEN (CASE WHEN e.[IsActive] = @IsActive THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO