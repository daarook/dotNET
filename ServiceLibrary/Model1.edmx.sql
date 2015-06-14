
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/14/2015 11:50:21
-- Generated from EDMX file: C:\Users\Milamber\Documents\Visual Studio 2013\Projects\ServiceLibrary\ServiceLibrary\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [dotNetEindopdracht];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ProductSet'
CREATE TABLE [dbo].[ProductSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Price] nvarchar(max)  NOT NULL,
    [Stock] nvarchar(max)  NOT NULL,
    [OrderEntryId] int  NOT NULL
);
GO

-- Creating table 'OrderSet'
CREATE TABLE [dbo].[OrderSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CustName] nvarchar(max)  NOT NULL,
    [OrderDate] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'OrderEntrySet'
CREATE TABLE [dbo].[OrderEntrySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Amount] nvarchar(max)  NOT NULL,
    [OrderId] nvarchar(max)  NOT NULL,
    [OrderId1] int  NOT NULL,
    [Product_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ProductSet'
ALTER TABLE [dbo].[ProductSet]
ADD CONSTRAINT [PK_ProductSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [PK_OrderSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderEntrySet'
ALTER TABLE [dbo].[OrderEntrySet]
ADD CONSTRAINT [PK_OrderEntrySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Product_Id] in table 'OrderEntrySet'
ALTER TABLE [dbo].[OrderEntrySet]
ADD CONSTRAINT [FK_OrderEntryProduct]
    FOREIGN KEY ([Product_Id])
    REFERENCES [dbo].[ProductSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderEntryProduct'
CREATE INDEX [IX_FK_OrderEntryProduct]
ON [dbo].[OrderEntrySet]
    ([Product_Id]);
GO

-- Creating foreign key on [OrderId1] in table 'OrderEntrySet'
ALTER TABLE [dbo].[OrderEntrySet]
ADD CONSTRAINT [FK_OrderOrderEntry]
    FOREIGN KEY ([OrderId1])
    REFERENCES [dbo].[OrderSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderOrderEntry'
CREATE INDEX [IX_FK_OrderOrderEntry]
ON [dbo].[OrderEntrySet]
    ([OrderId1]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------