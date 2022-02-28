CREATE TABLE [dbo].[Transportation]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[DepartingWaypointId] INT NOT NULL,
	[ArrivingWaypointId] INT NOT NULL,
	[TripId] INT NOT NULL,
	[StartTime] Time not null,
	[EndTime] Time not null,
	[Description] varchar(250) not null,
	CONSTRAINT [FK_dbo.Transportation_dbo.DepartingWaypoint_Id] FOREIGN KEY ([DepartingWaypointId]) 
		REFERENCES [dbo].[Waypoints] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.Transportation_dbo.ArrivingWaypoint_Id] FOREIGN KEY ([ArrivingWaypointId]) 
		REFERENCES [dbo].[Waypoints] ([Id]) ON DELETE no ACTION,
	CONSTRAINT [FK_dbo.Transportation_dbo.Trip_Id] FOREIGN KEY ([TripId])
		REFERENCES [dbo].[Trip] ([Id]) On delete cascade
)
