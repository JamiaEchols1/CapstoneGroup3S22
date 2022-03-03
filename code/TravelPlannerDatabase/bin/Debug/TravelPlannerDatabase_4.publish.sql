﻿/*
Deployment script for TravelPlannerDatabase

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "TravelPlannerDatabase"
:setvar DefaultFilePrefix "TravelPlannerDatabase"
:setvar DefaultDataPath "C:\Users\benev\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"
:setvar DefaultLogPath "C:\Users\benev\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Creating Table [dbo].[Lodging]...';


GO
CREATE TABLE [dbo].[Lodging] (
    [Id]        INT          NOT NULL,
    [TripId]    INT          NOT NULL,
    [Location]  VARCHAR (50) NOT NULL,
    [StartTime] DATETIME     NOT NULL,
    [EndTime]   DATETIME     NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Foreign Key [dbo].[FK_dbo.Lodging_dbo.Trip_Id]...';


GO
ALTER TABLE [dbo].[Lodging] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Lodging_dbo.Trip_Id] FOREIGN KEY ([TripId]) REFERENCES [dbo].[Trip] ([Id]) ON DELETE CASCADE;


GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
GO

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Lodging] WITH CHECK CHECK CONSTRAINT [FK_dbo.Lodging_dbo.Trip_Id];


GO
PRINT N'Update complete.';


GO