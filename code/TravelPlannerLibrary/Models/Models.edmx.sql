
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/26/2022 15:01:27
-- Generated from EDMX file: C:\Users\jamia\source\repos\JamiaEchols1\CapstoneGroup3S22\code\TravelPlannerLibrary\Models\Models.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TravelPlannerDatabase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_dbo_Trip_dbo_User_Id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Trip] DROP CONSTRAINT [FK_dbo_Trip_dbo_User_Id];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_Waypoint_dbo_Trip_Id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Waypoints] DROP CONSTRAINT [FK_dbo_Waypoint_dbo_Trip_Id];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[__RefactorLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[__RefactorLog];
GO
IF OBJECT_ID(N'[dbo].[Trip]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Trip];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO
IF OBJECT_ID(N'[dbo].[Waypoints]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Waypoints];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'C__RefactorLog'
CREATE TABLE [dbo].[C__RefactorLog] (
    [OperationKey] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Trips'
CREATE TABLE [dbo].[Trips] (
    [Id] int  NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int  NOT NULL,
    [Username] varchar(50)  NOT NULL,
    [Password] varchar(50)  NOT NULL
);
GO

-- Creating table 'Waypoints'
CREATE TABLE [dbo].[Waypoints] (
    [Id] int  NOT NULL,
    [Location] varchar(50)  NOT NULL,
    [DateTime] datetime  NOT NULL,
    [TripId] int  NOT NULL
);
GO

-- Creating table 'Transportations'
CREATE TABLE [dbo].[Transportations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DepartingWaypointId] int  NOT NULL,
    [ArrivingWaypointId] int  NOT NULL,
    [StartTime] datetime  NOT NULL,
    [EndTime] datetime  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Waypoint_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [OperationKey] in table 'C__RefactorLog'
ALTER TABLE [dbo].[C__RefactorLog]
ADD CONSTRAINT [PK_C__RefactorLog]
    PRIMARY KEY CLUSTERED ([OperationKey] ASC);
GO

-- Creating primary key on [Id] in table 'Trips'
ALTER TABLE [dbo].[Trips]
ADD CONSTRAINT [PK_Trips]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Waypoints'
ALTER TABLE [dbo].[Waypoints]
ADD CONSTRAINT [PK_Waypoints]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Transportations'
ALTER TABLE [dbo].[Transportations]
ADD CONSTRAINT [PK_Transportations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'Trips'
ALTER TABLE [dbo].[Trips]
ADD CONSTRAINT [FK_dbo_Trip_dbo_User_Id]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_Trip_dbo_User_Id'
CREATE INDEX [IX_FK_dbo_Trip_dbo_User_Id]
ON [dbo].[Trips]
    ([UserId]);
GO

-- Creating foreign key on [TripId] in table 'Waypoints'
ALTER TABLE [dbo].[Waypoints]
ADD CONSTRAINT [FK_dbo_Waypoint_dbo_Trip_Id]
    FOREIGN KEY ([TripId])
    REFERENCES [dbo].[Trips]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_Waypoint_dbo_Trip_Id'
CREATE INDEX [IX_FK_dbo_Waypoint_dbo_Trip_Id]
ON [dbo].[Waypoints]
    ([TripId]);
GO

-- Creating foreign key on [Waypoint_Id] in table 'Transportations'
ALTER TABLE [dbo].[Transportations]
ADD CONSTRAINT [FK_TransportationWaypoint]
    FOREIGN KEY ([Waypoint_Id])
    REFERENCES [dbo].[Waypoints]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TransportationWaypoint'
CREATE INDEX [IX_FK_TransportationWaypoint]
ON [dbo].[Transportations]
    ([Waypoint_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------