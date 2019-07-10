CREATE TABLE [dbo].[Session] (
    [SessionId]       INT             IDENTITY (1, 1) NOT NULL,
    [Title]           NVARCHAR (1000) NOT NULL,
    [ShortTitle]      NVARCHAR (500)  NULL,
    [Description]     NVARCHAR (4000) NULL,
    [RoomId]          INT             NULL,
    [StartTime]       DATETIME2 (7)   NULL,
    [EndTime]         DATETIME2 (7)   NULL,
    [DataVersion]     INT             CONSTRAINT [DF_Session_DataVersion] DEFAULT ((1)) NOT NULL,
    [CreatedUtcDate]  DATETIME2 (7)   CONSTRAINT [DF_Session_CreatedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [CreatedBy]       NVARCHAR (200)  NOT NULL,
    [ModifiedUtcDate] DATETIME2 (7)   CONSTRAINT [DF_Session_ModifiedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]      NVARCHAR (200)  NOT NULL,
    [IsDeleted]       BIT             CONSTRAINT [DF_Session_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED ([SessionId] ASC),
    CONSTRAINT [FK_Session_Room] FOREIGN KEY ([RoomId]) REFERENCES [dbo].[Room] ([RoomId])
);


GO
CREATE TRIGGER [dbo].[trg_Session_Update] ON [dbo].[Session]
      FOR UPDATE
      AS 

      SET NOCOUNT ON

      UPDATE a SET 
        a.DataVersion = b.DataVersion + 1
      FROM Session a
        INNER JOIN inserted b
          ON a.SessionId = b.SessionId
