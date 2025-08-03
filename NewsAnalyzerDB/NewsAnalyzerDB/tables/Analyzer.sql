-- Contains list of analyzers used to prompt 
CREATE TABLE [dbo].[Analyzer]
(	
	[ID]		BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Name]		NVARCHAR(255) NOT NULL,
	[IsActive]	BIT NOT NULL DEFAULT 1
)
