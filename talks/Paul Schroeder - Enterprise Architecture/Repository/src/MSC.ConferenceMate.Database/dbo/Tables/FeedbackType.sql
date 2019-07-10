CREATE TABLE [dbo].[FeedbackType] (
    [FeedbackTypeId]  INT            NOT NULL,
    [Code]            VARCHAR (50)   NOT NULL,
    [DataVersion]     INT            CONSTRAINT [DF_FeedbackType_DataVersion] DEFAULT ((1)) NOT NULL,
    [CreatedUtcDate]  DATETIME2 (7)  CONSTRAINT [DF_FeedbackType_CreatedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [CreatedBy]       NVARCHAR (200) NOT NULL,
    [ModifiedUtcDate] DATETIME2 (7)  CONSTRAINT [DF_FeedbackType_ModifiedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]      NVARCHAR (200) NOT NULL,
    [IsDeleted]       BIT            CONSTRAINT [DF_FeedbackType_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_FeedbackType] PRIMARY KEY CLUSTERED ([FeedbackTypeId] ASC)
);


GO





      CREATE TRIGGER [dbo].[trg_FeedbackType_Update] ON [dbo].[FeedbackType]
      FOR UPDATE
      AS 

      SET NOCOUNT ON

      UPDATE a SET 
        a.DataVersion = b.DataVersion + 1
      FROM FeedbackType a
        INNER JOIN inserted b
          ON a.FeedbackTypeId = b.FeedbackTypeId


