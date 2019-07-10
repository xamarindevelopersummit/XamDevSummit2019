CREATE TABLE [dbo].[LookupList] (
    [LookupListId]        INT             IDENTITY (1, 1) NOT NULL,
    [LanguageTypeId]      INT             NOT NULL,
    [ForeignKeyTablePkId] INT             NOT NULL,
    [ForeignKeyTableName] NVARCHAR (200)  NOT NULL,
    [DisplayPriority]     INT             NOT NULL,
    [DisplayText]         NVARCHAR (4000) NOT NULL,
    [Description]         NVARCHAR (2000) NULL,
    [CustomJson]          NVARCHAR (MAX)  NULL,
    [DataVersion]         INT             CONSTRAINT [DF_LookupList_DataVersion] DEFAULT ((1)) NOT NULL,
    [CreatedUtcDate]      DATETIME2 (7)   CONSTRAINT [DF_LookupList_CreatedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [CreatedBy]           NVARCHAR (200)  NOT NULL,
    [ModifiedUtcDate]     DATETIME2 (7)   CONSTRAINT [DF_LookupList_ModifiedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]          NVARCHAR (200)  NOT NULL,
    [IsDeleted]           BIT             CONSTRAINT [DF_LookupList_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_LookupList] PRIMARY KEY CLUSTERED ([LookupListId] ASC),
    CONSTRAINT [FK_LookupList_LanguageType] FOREIGN KEY ([LanguageTypeId]) REFERENCES [dbo].[LanguageType] ([LanguageTypeId])
);


GO
-- [trg_LookupList_Update]
CREATE TRIGGER [dbo].[trg_LookupList_Update] ON [dbo].[LookupList]
FOR UPDATE
AS 

SET NOCOUNT ON

UPDATE a SET 
a.DataVersion = b.DataVersion + 1
FROM LookupList a
INNER JOIN inserted b
    ON a.LookupListId = b.LookupListId

