


DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(255) = 'Name e09ba99ad4a045f6bfabec88cec970f2'
DECLARE @IsActive BIT = 1
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[Analyzer]
				WHERE 
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsActive IS NOT NULL THEN (CASE WHEN [IsActive] = @IsActive THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[Analyzer]
		(
	 [Name],
	 [IsActive]
		)
	SELECT 		
			 @Name,
	 @IsActive
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[Analyzer] e
WHERE
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsActive IS NOT NULL THEN (CASE WHEN [IsActive] = @IsActive THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
