﻿CREATE TABLE [dbo].[Todos]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Task] NVARCHAR(50) NOT NULL, 
    [AssignedTo] INT NOT NULL, 
    [IsComplete] BIT NOT NULL
)
