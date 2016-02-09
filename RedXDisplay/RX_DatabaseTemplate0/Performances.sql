CREATE TABLE [dbo].[Performances](
	[Date] DATE NOT NULL PRIMARY KEY, 
	[IdOperatingSystem] INT NOT NULL, 
    
    [MemoryUsage] REAL NOT NULL, 
    [CpuUsage] REAL NOT NULL, 

    CONSTRAINT [FK_Performances_ToOS] FOREIGN KEY ([IdOperatingSystem]) REFERENCES [OS]([id]), 
)
