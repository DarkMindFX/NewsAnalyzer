
CREATE PROCEDURE [dbo].[p_Analyzer_Update]
			@ID BIGINT,
			@Name NVARCHAR(255),
			@IsActive BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Analyzer]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Analyzer]
		SET
									[Name] = IIF( @Name IS NOT NULL, @Name, [Name] ) ,
									[IsActive] = IIF( @IsActive IS NOT NULL, @IsActive, [IsActive] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Analyzer was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[Analyzer] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsActive IS NOT NULL THEN (CASE WHEN e.[IsActive] = @IsActive THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO