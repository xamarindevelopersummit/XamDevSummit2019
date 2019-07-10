CREATE TABLE [dbo].[Room] (
    [RoomId]          INT             IDENTITY (1, 1) NOT NULL,
    [Title]           NVARCHAR (1000) NOT NULL,
    [ShortTitle]      NVARCHAR (500)  NULL,
    [Description]     NVARCHAR (4000) NULL,
    [SeatingCapacity] INT             NOT NULL,
    [Latitude]        FLOAT (53)      NOT NULL,
    [Longitude]       FLOAT (53)      NOT NULL,
    [DataVersion]     INT             CONSTRAINT [DF_Room_DataVersion] DEFAULT ((1)) NOT NULL,
    [CreatedUtcDate]  DATETIME2 (7)   CONSTRAINT [DF_Room_CreatedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [CreatedBy]       NVARCHAR (200)  NOT NULL,
    [ModifiedUtcDate] DATETIME2 (7)   CONSTRAINT [DF_Room_ModifiedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]      NVARCHAR (200)  NOT NULL,
    [IsDeleted]       BIT             CONSTRAINT [DF_Room_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED ([RoomId] ASC)
);


GO

CREATE TRIGGER [dbo].[trg_Room_Update] ON [dbo].[Room]
      FOR UPDATE
      AS 

      SET NOCOUNT ON

      UPDATE a SET 
        a.DataVersion = b.DataVersion + 1
      FROM Room a
        INNER JOIN inserted b
          ON a.RoomId = b.RoomId
