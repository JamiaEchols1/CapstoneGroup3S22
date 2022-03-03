CREATE TABLE [dbo].[Transportations]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[DepartingWaypointId] INT NOT NULL,
	[ArrivingWaypointId] INT NOT NULL,
	[TripId] INT NOT NULL,
	[StartTime] DATETIME not null,
	[EndTime] DATETIME not null,
	[Description] varchar(250) not null,
	CONSTRAINT [FK_dbo.Transportation_dbo.DepartingWaypoint_Id] FOREIGN KEY ([DepartingWaypointId]) 
		REFERENCES [dbo].[Waypoints] ([Id]) ON DELETE no action,
	CONSTRAINT [FK_dbo.Transportation_dbo.ArrivingWaypoint_Id] FOREIGN KEY ([ArrivingWaypointId]) 
		REFERENCES [dbo].[Waypoints] ([Id]) ON DELETE no ACTION,
	CONSTRAINT [FK_dbo.Transportation_dbo.Trip_Id] FOREIGN KEY ([TripId])
		REFERENCES [dbo].[Trips] ([Id]) On delete no action
)
