CREATE TABLE [dbo].[SponsorType] (
    [SponsorTypeId]   INT            NOT NULL,
    [Code]            VARCHAR (50)   NOT NULL,
    [DataVersion]     INT            CONSTRAINT [DF_SponsorType_DataVersion] DEFAULT ((1)) NOT NULL,
    [CreatedUtcDate]  DATETIME2 (7)  CONSTRAINT [DF_SponsorType_CreatedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [CreatedBy]       NVARCHAR (200) NOT NULL,
    [ModifiedUtcDate] DATETIME2 (7)  CONSTRAINT [DF_SponsorType_ModifiedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]      NVARCHAR (200) NOT NULL,
    [IsDeleted]       BIT            CONSTRAINT [DF_SponsorType_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_SponsorType] PRIMARY KEY CLUSTERED ([SponsorTypeId] ASC)
);


GO






      CREATE TRIGGER [dbo].[trg_SponsorType_Update] ON [dbo].[SponsorType]
      FOR UPDATE
      AS 

      SET NOCOUNT ON

      UPDATE a SET 
        a.DataVersion = b.DataVersion + 1
      FROM SponsorType a
        INNER JOIN inserted b
          ON a.SponsorTypeId = b.SponsorTypeId


