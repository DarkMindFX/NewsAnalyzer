


-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(255) = 'Name a57d013fc9e048c296e8e7a9f41303d5'
DECLARE @IsActive BIT = 1
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updName NVARCHAR(255) = 'Name 5ee0c46faee34c3cbc9d648289d7159a'
DECLARE @updIsActive BIT = 1
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[Analyzer]
				WHERE 
	(CASE WHEN @updName IS NOT NULL THEN (CASE WHEN [Name] = @updName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsActive IS NOT NULL THEN (CASE WHEN [IsActive] = @updIsActive THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[Analyzer]
	WHERE 
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsActive IS NOT NULL THEN (CASE WHEN [IsActive] = @IsActive THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[Analyzer]
	WHERE 
	(CASE WHEN @updName IS NOT NULL THEN (CASE WHEN [Name] = @updName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsActive IS NOT NULL THEN (CASE WHEN [IsActive] = @updIsActive THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'Analyzer was not updated', 1
END