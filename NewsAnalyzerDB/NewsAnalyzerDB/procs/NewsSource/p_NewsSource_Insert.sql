
CREATE PROCEDURE [dbo].[p_NewsSource_Insert]
			@ID BIGINT,
			@Name NVARCHAR(255),
			@Url NVARCHAR(255),
			@IsActive BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[NewsSource]
	SELECT 
		@Name,
		@Url,
		@IsActive
	
	

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