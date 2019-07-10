CREATE TABLE [dbo].[FeedbackInitiatorType] (
    [FeedbackInitiatorTypeId] INT            NOT NULL,
    [Code]                    VARCHAR (50)   NOT NULL,
    [DataVersion]             INT            CONSTRAINT [DF_FeedbackInitiatorType_DataVersion] DEFAULT ((1)) NOT NULL,
    [CreatedUtcDate]          DATETIME2 (7)  CONSTRAINT [DF_FeedbackInitiatorType_CreatedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [CreatedBy]               NVARCHAR (200) NOT NULL,
    [ModifiedUtcDate]         DATETIME2 (7)  CONSTRAINT [DF_FeedbackInitiatorType_ModifiedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]              NVARCHAR (200) NOT NULL,
    [IsDeleted]               BIT            CONSTRAINT [DF_FeedbackInitiatorType_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_FeedbackInitiatorType] PRIMARY KEY CLUSTERED ([FeedbackInitiatorTypeId] ASC)
);


GO





      CREATE TRIGGER [dbo].[trg_FeedbackInitiatorType_Update] ON [dbo].[FeedbackInitiatorType]
      FOR UPDATE
      AS 

      SET NOCOUNT ON

      UPDATE a SET 
        a.DataVersion = b.DataVersion + 1
      FROM FeedbackInitiatorType a
        INNER JOIN inserted b
          ON a.FeedbackInitiatorTypeId = b.FeedbackInitiatorTypeId


