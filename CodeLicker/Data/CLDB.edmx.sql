
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/28/2017 20:00:10
-- Generated from EDMX file: C:\Users\Francesco\documents\visual studio 2017\Projects\CodeLicker\CodeLicker\Data\CLDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CLDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[RecentPaths]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RecentPaths];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'RecentPaths'
CREATE TABLE [dbo].[RecentPaths] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Path] nvarchar(1024)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'RecentPaths'
ALTER TABLE [dbo].[RecentPaths]
ADD CONSTRAINT [PK_RecentPaths]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------