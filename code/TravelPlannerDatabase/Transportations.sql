CREATE TABLE [dbo].[Transportations]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[TripId] INT NOT NULL,
	[StartTime] DATETIME not null,
	[EndTime] DATETIME not null,
	[Description] varchar(250) null,
	[Type] varchar(250) not null, 
    CONSTRAINT [FK_dbo.Transportation_dbo.Trip_Id] FOREIGN KEY ([TripId])
		REFERENCES [dbo].[Trips] ([Id]) On delete cascade
)
