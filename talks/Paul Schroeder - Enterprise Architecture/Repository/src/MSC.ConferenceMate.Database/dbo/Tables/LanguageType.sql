CREATE TABLE [dbo].[LanguageType] (
    [LanguageTypeId]             INT            NOT NULL,
    [Code]                       VARCHAR (50)   NOT NULL,
    [DisplayText]                NVARCHAR (128) NOT NULL,
    [DisplayPriority]            INT            CONSTRAINT [DF_LanguageType_DisplayPriority] DEFAULT ((100)) NOT NULL,
    [NativeName]                 NVARCHAR (100) NOT NULL,
    [ThreeLetterISOLanguageName] NCHAR (3)      NOT NULL,
    [TwoLetterISOLanguageName]   NCHAR (2)      NOT NULL,
    [LanguageCultureIdentifier]  INT            NULL,
    [DataVersion]                INT            CONSTRAINT [DF_LanguageType_DataVersion] DEFAULT ((1)) NOT NULL,
    [CreatedUtcDate]             DATETIME2 (7)  CONSTRAINT [DF_LanguageType_CreatedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [CreatedBy]                  NVARCHAR (200) NOT NULL,
    [ModifiedUtcDate]            DATETIME2 (7)  CONSTRAINT [DF_LanguageType_ModifiedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]                 NVARCHAR (200) NOT NULL,
    [IsDeleted]                  BIT            CONSTRAINT [DF_LanguageType_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_LanguageType] PRIMARY KEY CLUSTERED ([LanguageTypeId] ASC),
    CONSTRAINT [UC_LanguageType_Code] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO





      CREATE TRIGGER [dbo].[trg_LanguageType_Update] ON [dbo].[LanguageType]
      FOR UPDATE
      AS 

      SET NOCOUNT ON

      UPDATE a SET 
        a.DataVersion = b.DataVersion + 1
      FROM LanguageType a
        INNER JOIN inserted b
          ON a.LanguageTypeId = b.LanguageTypeId


