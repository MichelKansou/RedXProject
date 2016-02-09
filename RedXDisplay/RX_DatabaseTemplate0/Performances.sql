CREATE TABLE [dbo].[Performances](
	[Date] VARCHAR(255) NOT NULL PRIMARY KEY, 
	[OS] VARCHAR(255) NOT NULL, 
    
    [MemoryUsage] REAL NOT NULL, 
    [CpuUsage] REAL NOT NULL 

)
