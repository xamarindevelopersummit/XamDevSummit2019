CREATE TABLE [dbo].[Sponsor_FeaturedEvent] (
    [SponsorId]       INT            NOT NULL,
    [FeaturedEventId] INT            NOT NULL,
    [DataVersion]     INT            CONSTRAINT [DF_Sponsor_FeaturedEvent_DataVersion] DEFAULT ((1)) NOT NULL,
    [CreatedUtcDate]  DATETIME2 (7)  CONSTRAINT [DF_Sponsor_FeaturedEvent_CreatedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [CreatedBy]       NVARCHAR (200) NOT NULL,
    [ModifiedUtcDate] DATETIME2 (7)  CONSTRAINT [DF_Sponsor_FeaturedEvent_ModifiedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]      NVARCHAR (200) NOT NULL,
    [IsDeleted]       BIT            CONSTRAINT [DF_Sponsor_FeaturedEvent_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Sponsor_FeaturedEvent] PRIMARY KEY CLUSTERED ([SponsorId] ASC, [FeaturedEventId] ASC),
    CONSTRAINT [FK_Sponsor_FeaturedEvent_FeaturedEvent] FOREIGN KEY ([FeaturedEventId]) REFERENCES [dbo].[FeaturedEvent] ([FeaturedEventId]),
    CONSTRAINT [FK_Sponsor_FeaturedEvent_Sponsor] FOREIGN KEY ([SponsorId]) REFERENCES [dbo].[Sponsor] ([SponsorId])
);


GO









      CREATE TRIGGER [dbo].[trg_Sponsor_FeaturedEvent_Update] ON [dbo].[Sponsor_FeaturedEvent]
      FOR UPDATE
      AS 

      SET NOCOUNT ON

      UPDATE a SET 
        a.DataVersion = b.DataVersion + 1
      FROM Sponsor_FeaturedEvent a
        INNER JOIN inserted b
          ON a.SponsorId = b.SponsorId
		  AND a.FeaturedEventId = b.FeaturedEventId


