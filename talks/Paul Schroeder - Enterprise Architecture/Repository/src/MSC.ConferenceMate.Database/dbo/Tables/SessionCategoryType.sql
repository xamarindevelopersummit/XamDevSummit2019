CREATE TABLE [dbo].[SessionCategoryType] (
    [SessionCategoryTypeId] INT            NOT NULL,
    [Code]                  VARCHAR (50)   NOT NULL,
    [Name]                  VARCHAR (50)   NOT NULL,
    [DataVersion]           INT            CONSTRAINT [DF_SessionCategoryType_DataVersion] DEFAULT ((1)) NOT NULL,
    [CreatedUtcDate]        DATETIME2 (7)  CONSTRAINT [DF_SessionCategoryType_CreatedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [CreatedBy]             NVARCHAR (200) NOT NULL,
    [ModifiedUtcDate]       DATETIME2 (7)  CONSTRAINT [DF_SessionCategoryType_ModifiedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]            NVARCHAR (200) NOT NULL,
    [IsDeleted]             BIT            CONSTRAINT [DF_SessionCategoryType_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_SessionCategoryType] PRIMARY KEY CLUSTERED ([SessionCategoryTypeId] ASC)
);


GO







      CREATE TRIGGER [dbo].[trg_SessionCategoryType_Update] ON [dbo].[SessionCategoryType]
      FOR UPDATE
      AS 

      SET NOCOUNT ON

      UPDATE a SET 
        a.DataVersion = b.DataVersion + 1
      FROM SessionCategoryType a
        INNER JOIN inserted b
          ON a.SessionCategoryTypeId = b.SessionCategoryTypeId


