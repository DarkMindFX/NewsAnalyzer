


-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @Title NVARCHAR(255) = 'Title 31ec91b0edb94bac8b7506761b51c488'
DECLARE @Content NVARCHAR(4000) = 'Content 31ec91b0edb94bac8b7506761b51c488'
DECLARE @Timestamp DATETIME = '9/27/2023 6:09:32 AM'
DECLARE @NewsSourceID BIGINT = 10
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updTitle NVARCHAR(255) = 'Title a8c0d7c2ea9d4299b9f377bdceeb2a14'
DECLARE @updContent NVARCHAR(4000) = 'Content a8c0d7c2ea9d4299b9f377bdceeb2a14'
DECLARE @updTimestamp DATETIME = '9/27/2023 6:09:32 AM'
DECLARE @updNewsSourceID BIGINT = 5
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[Article]
				WHERE 
	(CASE WHEN @updTitle IS NOT NULL THEN (CASE WHEN [Title] = @updTitle THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updContent IS NOT NULL THEN (CASE WHEN [Content] = @updContent THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updTimestamp IS NOT NULL THEN (CASE WHEN [Timestamp] = @updTimestamp THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updNewsSourceID IS NOT NULL THEN (CASE WHEN [NewsSourceID] = @updNewsSourceID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[Article]
	WHERE 
	(CASE WHEN @Title IS NOT NULL THEN (CASE WHEN [Title] = @Title THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Content IS NOT NULL THEN (CASE WHEN [Content] = @Content THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Timestamp IS NOT NULL THEN (CASE WHEN [Timestamp] = @Timestamp THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @NewsSourceID IS NOT NULL THEN (CASE WHEN [NewsSourceID] = @NewsSourceID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[Article]
	WHERE 
	(CASE WHEN @updTitle IS NOT NULL THEN (CASE WHEN [Title] = @updTitle THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updContent IS NOT NULL THEN (CASE WHEN [Content] = @updContent THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updTimestamp IS NOT NULL THEN (CASE WHEN [Timestamp] = @updTimestamp THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updNewsSourceID IS NOT NULL THEN (CASE WHEN [NewsSourceID] = @updNewsSourceID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'Article was not updated', 1
END