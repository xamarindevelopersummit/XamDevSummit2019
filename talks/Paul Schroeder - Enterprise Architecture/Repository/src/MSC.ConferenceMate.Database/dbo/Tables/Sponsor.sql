CREATE TABLE [dbo].[Sponsor] (
    [SponsorId]       INT             IDENTITY (1, 1) NOT NULL,
    [SponsorTypeId]   INT             NOT NULL,
    [Title]           NVARCHAR (1000) NOT NULL,
    [ShortTitle]      NVARCHAR (500)  NULL,
    [Description]     NVARCHAR (4000) NULL,
    [ImageUrl]        NVARCHAR (1000) NULL,
    [WebsiteUrl]      NVARCHAR (1000) NULL,
    [TwitterUrl]      NVARCHAR (1000) NULL,
    [BoothLocation]   NVARCHAR (1000) NULL,
    [Rank]            INT             CONSTRAINT [DF_Sponsor_Rank] DEFAULT ((100)) NOT NULL,
    [DataVersion]     INT             CONSTRAINT [DF_Sponsor_DataVersion] DEFAULT ((1)) NOT NULL,
    [CreatedUtcDate]  DATETIME2 (7)   CONSTRAINT [DF_Sponsor_CreatedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [CreatedBy]       NVARCHAR (200)  NOT NULL,
    [ModifiedUtcDate] DATETIME2 (7)   CONSTRAINT [DF_Sponsor_ModifiedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]      NVARCHAR (200)  NOT NULL,
    [IsDeleted]       BIT             CONSTRAINT [DF_Sponsor_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Sponsor] PRIMARY KEY CLUSTERED ([SponsorId] ASC),
    CONSTRAINT [FK_Sponsor_SponsorType] FOREIGN KEY ([SponsorTypeId]) REFERENCES [dbo].[SponsorType] ([SponsorTypeId])
);


GO







      CREATE TRIGGER [dbo].[trg_Sponsor_Update] ON [dbo].[Sponsor]
      FOR UPDATE
      AS 

      SET NOCOUNT ON

      UPDATE a SET 
        a.DataVersion = b.DataVersion + 1
      FROM Sponsor a
        INNER JOIN inserted b
          ON a.SponsorId = b.SponsorId


