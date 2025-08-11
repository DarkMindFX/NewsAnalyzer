


DECLARE @ID BIGINT = NULL
DECLARE @Title NVARCHAR(255) = 'Title 31ec91b0edb94bac8b7506761b51c488'
DECLARE @Content NVARCHAR(4000) = 'Content 31ec91b0edb94bac8b7506761b51c488'
DECLARE @Timestamp DATETIME = '9/27/2023 6:09:32 AM'
DECLARE @NewsSourceID BIGINT = 10
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[Article]
				WHERE 
	(CASE WHEN @Title IS NOT NULL THEN (CASE WHEN [Title] = @Title THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Content IS NOT NULL THEN (CASE WHEN [Content] = @Content THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Timestamp IS NOT NULL THEN (CASE WHEN [Timestamp] = @Timestamp THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @NewsSourceID IS NOT NULL THEN (CASE WHEN [NewsSourceID] = @NewsSourceID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[Article]
		(
	 [Title],
	 [Content],
	 [Timestamp],
	 [NewsSourceID]
		)
	SELECT 		
			 @Title,
	 @Content,
	 @Timestamp,
	 @NewsSourceID
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[Article] e
WHERE
	(CASE WHEN @Title IS NOT NULL THEN (CASE WHEN [Title] = @Title THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Content IS NOT NULL THEN (CASE WHEN [Content] = @Content THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Timestamp IS NOT NULL THEN (CASE WHEN [Timestamp] = @Timestamp THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @NewsSourceID IS NOT NULL THEN (CASE WHEN [NewsSourceID] = @NewsSourceID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
