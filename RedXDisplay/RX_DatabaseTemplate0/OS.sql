/*
The database must have a MEMORY_OPTIMIZED_DATA filegroup
before the memory optimized object can be created.

The bucket count should be set to about two times the 
maximum expected number of distinct values in the 
index key, rounded up to the nearest power of two.
*/

CREATE TABLE [dbo].[OS](
	[Id] INT NOT NULL PRIMARY KEY DEFAULT NEXT VALUE FOR [dbo].[OS_Id_Sequence], 
    [OSname] VARCHAR(25) NOT NULL
)