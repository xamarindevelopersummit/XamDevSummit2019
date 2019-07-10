CREATE TABLE [dbo].[GenderType] (
    [GenderTypeId]    INT            NOT NULL,
    [Code]            VARCHAR (50)   NOT NULL,
    [DataVersion]     INT            CONSTRAINT [DF_GenderType_DataVersion] DEFAULT ((1)) NOT NULL,
    [CreatedUtcDate]  DATETIME2 (7)  CONSTRAINT [DF_GenderType_CreatedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [CreatedBy]       NVARCHAR (200) NOT NULL,
    [ModifiedUtcDate] DATETIME2 (7)  CONSTRAINT [DF_GenderType_ModifiedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]      NVARCHAR (200) NOT NULL,
    [IsDeleted]       BIT            CONSTRAINT [DF_GenderType_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_GenderType] PRIMARY KEY CLUSTERED ([GenderTypeId] ASC)
);


GO





      CREATE TRIGGER [dbo].[trg_GenderType_Update] ON [dbo].[GenderType]
      FOR UPDATE
      AS 

      SET NOCOUNT ON

      UPDATE a SET 
        a.DataVersion = b.DataVersion + 1
      FROM GenderType a
        INNER JOIN inserted b
          ON a.GenderTypeId = b.GenderTypeId


