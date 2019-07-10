CREATE TABLE [dbo].[Session_SessionCategoryType] (
    [SessionId]             INT            NOT NULL,
    [SessionCategoryTypeId] INT            NOT NULL,
    [DataVersion]           INT            CONSTRAINT [DF_Session_SessionCategoryType_DataVersion] DEFAULT ((1)) NOT NULL,
    [CreatedUtcDate]        DATETIME2 (7)  CONSTRAINT [DF_Session_SessionCategoryType_CreatedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [CreatedBy]             NVARCHAR (200) NOT NULL,
    [ModifiedUtcDate]       DATETIME2 (7)  CONSTRAINT [DF_Session_SessionCategoryType_ModifiedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]            NVARCHAR (200) NOT NULL,
    [IsDeleted]             BIT            CONSTRAINT [DF_Session_SessionCategoryType_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Session_SessionCategoryType] PRIMARY KEY CLUSTERED ([SessionId] ASC, [SessionCategoryTypeId] ASC),
    CONSTRAINT [FK_Session_SessionCategoryType_Session] FOREIGN KEY ([SessionId]) REFERENCES [dbo].[Session] ([SessionId]),
    CONSTRAINT [FK_Session_SessionCategoryType_SessionCategoryType] FOREIGN KEY ([SessionCategoryTypeId]) REFERENCES [dbo].[SessionCategoryType] ([SessionCategoryTypeId])
);


GO








      CREATE TRIGGER [dbo].[trg_Session_SessionCategoryType_Update] ON [dbo].[Session_SessionCategoryType]
      FOR UPDATE
      AS 

      SET NOCOUNT ON

      UPDATE a SET 
        a.DataVersion = b.DataVersion + 1
      FROM Session_SessionCategoryType a
        INNER JOIN inserted b
          ON a.SessionId = b.SessionId
		  AND a.SessionCategoryTypeId = b.SessionCategoryTypeId


