
/*
Usage:
1. From local file

EXEC dbo.[dbo].[p_Util_SeedTable] 'd:\Projects\NewsAnalyzer\SeedData\', '<FileName.csv>', '<TableName>'

2. From Azure Blob

CREATE MASTER KEY ENCRYPTION BY PASSWORD ='<Password>'

CREATE DATABASE SCOPED CREDENTIAL UploadNewsAnalyzerTestData
WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
SECRET = '<SAS for blob folder>';

CREATE EXTERNAL DATA SOURCE NewsAnalyzer_Azure_SeedData
WITH (
        TYPE = BLOB_STORAGE,
        LOCATION = 'https://XYZABCD.blob.core.windows.net',
        CREDENTIAL = UploadNewsAnalyzerSeedData
    );
GO 

EXEC dbo.[p_Util_Industry_Seed] 'roboadvisor-seed-data/', '<FileName.csv>', '<TableName>', 'NewsAnalyzer_Azure_SeedData'

DROP EXTERNAL DATA SOURCE NewsAnalyzer_Azure_SeedData

DROP DATABASE SCOPED CREDENTIAL UploadNewsAnalyzerSeedData

DROP MASTER KEY
*/
CREATE PROCEDURE [dbo].[p_Util_SeedTable]
	@RootFolder NVARCHAR(100),
	@File		NVARCHAR(100),
	@Table		NVARCHAR(100),
	@HasIdentity BIT,
	@DataSource NVARCHAR(100) = NULL
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @Path AS NVARCHAR(MAX)
	DECLARE @sql AS NVARCHAR(MAX)

	BEGIN TRY

		BEGIN TRANSACTION

		PRINT('======= ' + @File + ' -> ' + @Table + ' =======')

			SELECT @Path = CONCAT(@RootFolder, @File)
			IF(@HasIdentity = 1) BEGIN
				SET @sql = 'SET IDENTITY_INSERT dbo.[' + @Table + '] ON;'

				PRINT(@sql)
				EXEC(@sql)
			END


			SET @sql = 'BULK INSERT dbo.[' + @Table + ']
			FROM ''' + @Path + '''
			WITH (' +
			CASE
				WHEN @DataSource IS NOT NULL THEN 'DATA_SOURCE=''' + @DataSource + ''',' 
				ELSE ''
			END +
			'KEEPIDENTITY,
			FIRSTROW = 2,
			FIELDTERMINATOR = '','',
			ROWTERMINATOR=''0x0d0a'',
			BATCHSIZE=2500000);'

			PRINT(@sql)
			EXEC(@sql)

			IF(@HasIdentity = 1) 
			BEGIN
				SET @sql = 'SET IDENTITY_INSERT dbo.[' + @Table + '] OFF;'

				PRINT(@sql)
				EXEC(@sql)
			END

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SELECT 
		@Table
        ,ERROR_NUMBER() AS ErrorNumber  
		,ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
END
