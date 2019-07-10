CREATE TABLE [dbo].[FeaturedEvent] (
    [FeaturedEventId] INT             IDENTITY (1, 1) NOT NULL,
    [Title]           NVARCHAR (1000) NOT NULL,
    [ShortTitle]      NVARCHAR (500)  NULL,
    [Description]     NVARCHAR (4000) NULL,
    [Location]        NVARCHAR (1000) NULL,
    [StartTime]       DATETIME2 (7)   NULL,
    [EndTime]         DATETIME2 (7)   NULL,
    [IsAllDay]        BIT             CONSTRAINT [DF_FeaturedEvent_IsAllDay] DEFAULT ((0)) NOT NULL,
    [DataVersion]     INT             CONSTRAINT [DF_FeaturedEvent_DataVersion] DEFAULT ((1)) NOT NULL,
    [CreatedUtcDate]  DATETIME2 (7)   CONSTRAINT [DF_FeaturedEvent_CreatedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [CreatedBy]       NVARCHAR (200)  NOT NULL,
    [ModifiedUtcDate] DATETIME2 (7)   CONSTRAINT [DF_FeaturedEvent_ModifiedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]      NVARCHAR (200)  NOT NULL,
    [IsDeleted]       BIT             CONSTRAINT [DF_FeaturedEvent_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_FeaturedEvent] PRIMARY KEY CLUSTERED ([FeaturedEventId] ASC)
);


GO







      CREATE TRIGGER [dbo].[trg_FeaturedEvent_Update] ON [dbo].[FeaturedEvent]
      FOR UPDATE
      AS 

      SET NOCOUNT ON

      UPDATE a SET 
        a.DataVersion = b.DataVersion + 1
      FROM FeaturedEvent a
        INNER JOIN inserted b
          ON a.FeaturedEventId = b.FeaturedEventId


