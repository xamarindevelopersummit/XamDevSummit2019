CREATE TABLE [dbo].[Announcement] (
    [AnnouncementId]  INT             IDENTITY (1, 1) NOT NULL,
    [Title]           NVARCHAR (1000) NOT NULL,
    [ShortTitle]      NVARCHAR (500)  NULL,
    [Description]     NVARCHAR (4000) NULL,
    [StartTime]       DATETIME2 (7)   NULL,
    [EndTime]         DATETIME2 (7)   NULL,
    [DataVersion]     INT             CONSTRAINT [DF_Announcement_DataVersion] DEFAULT ((1)) NOT NULL,
    [CreatedUtcDate]  DATETIME2 (7)   CONSTRAINT [DF_Announcement_CreatedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [CreatedBy]       NVARCHAR (200)  NOT NULL,
    [ModifiedUtcDate] DATETIME2 (7)   CONSTRAINT [DF_Announcement_ModifiedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]      NVARCHAR (200)  NOT NULL,
    [IsDeleted]       BIT             CONSTRAINT [DF_Announcement_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Announcement] PRIMARY KEY CLUSTERED ([AnnouncementId] ASC)
);


GO

CREATE TRIGGER [dbo].[trg_Announcement_Update] ON [dbo].[Announcement]
      FOR UPDATE
      AS 

      SET NOCOUNT ON

      UPDATE a SET 
        a.DataVersion = b.DataVersion + 1
      FROM Announcement a
        INNER JOIN inserted b
          ON a.AnnouncementId = b.AnnouncementId
