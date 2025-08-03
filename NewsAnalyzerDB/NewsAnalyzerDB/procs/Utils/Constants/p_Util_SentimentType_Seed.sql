
/*
Usage:
1. From local file

EXEC dbo.[p_Util_UserRole_Seed] 'd:\Projects\NewsAnalyzer\SeedData\'

2. From Azure Blob

CREATE MASTER KEY ENCRYPTION BY PASSWORD ='<Password>'

CREATE DATABASE SCOPED CREDENTIAL UploadNewsMonitorTestData
WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
SECRET = '<SAS for blob folder>';

CREATE EXTERNAL DATA SOURCE NewsMonitor_Azure_SeedData
WITH (
        TYPE = BLOB_STORAGE,
        LOCATION = 'https://XYZABCD.blob.core.windows.net',
        CREDENTIAL = UploadNewsMonitorSeedData
    );
GO 

EXEC dbo.[p_Util_UserRole_Seed] 'XYZABCD-seed-data/', 'NewsMonitor_Azure_SeedData'

DROP EXTERNAL DATA SOURCE NewsMonitor_Azure_SeedData

DROP DATABASE SCOPED CREDENTIAL UploadNewsMonitorSeedData

DROP MASTER KEY
*/
CREATE PROCEDURE [dbo].[p_Util_SentimentType_Seed]
	@RootFolder NVARCHAR(100),
	@DataSource NVARCHAR(100) = NULL
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @file AS NVARCHAR(100) = 'Constants\SentimentType.csv'
	DECLARE @table AS NVARCHAR(100) = 'SentimentType'
    DECLARE @hasIdentity AS BIT = 1

	EXEC dbo.p_Util_SeedTable @RootFolder, @file, @table, @hasIdentity, @DataSource
END
