CREATE PROCEDURE [dbo].[p_Util_ClearDataForAsOfDate]
	@AsOfDate DATE
AS
BEGIN
	DECLARE @DayStart AS DATETIME
	DECLARE @DayEnd AS DATETIME

	SET @DayStart = DATEADD(day, DATEDIFF(day,'19000101',@AsOfDate), CAST('0:00' AS DATETIME2(7)))
	SET @DayEnd = DATEADD(day, DATEDIFF(day,'19000101',@AsOfDate), CAST('23:59:59' AS DATETIME2(7)))

	SELECT @DayStart, @DayEnd

	DELETE 
	FROM [dbo].[ArticleAnalysis]
	WHERE Timestamp >= @DayStart AND Timestamp <= @DayEnd

	DELETE 
	FROM [dbo].[Article]
	WHERE Timestamp >= @DayStart AND Timestamp <= @DayEnd
	
END
