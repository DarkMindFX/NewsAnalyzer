
CREATE PROCEDURE [dbo].[p_Article_Insert]
			@ID BIGINT,
			@Title NVARCHAR(255),
			@Content NVARCHAR(4000),
			@Timestamp DATETIME,
			@NewsSourceID BIGINT,
			@Url NVARCHAR(512),
			@NewsTime DATETIME
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Article]
	SELECT 
		@Title,
		@Content,
		@Timestamp,
		@NewsSourceID,
		@Url,
		@NewsTime
	
	

	SELECT
		e.*
	FROM
		[dbo].[Article] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Title IS NOT NULL THEN (CASE WHEN e.[Title] = @Title THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Content IS NOT NULL THEN (CASE WHEN e.[Content] = @Content THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Timestamp IS NOT NULL THEN (CASE WHEN e.[Timestamp] = @Timestamp THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @NewsSourceID IS NOT NULL THEN (CASE WHEN e.[NewsSourceID] = @NewsSourceID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Url IS NOT NULL THEN (CASE WHEN e.[Url] = @Url THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @NewsTime IS NOT NULL THEN (CASE WHEN e.[NewsTime] = @NewsTime THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO