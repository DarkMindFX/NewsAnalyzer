


DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name 1282e8d123b64a029d3f9f46a72302b1'
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[SentimentType]
				WHERE 
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[SentimentType]
		(
	 [Name]
		)
	SELECT 		
			 @Name
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[SentimentType] e
WHERE
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
