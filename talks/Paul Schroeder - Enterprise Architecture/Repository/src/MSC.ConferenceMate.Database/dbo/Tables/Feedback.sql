CREATE TABLE [dbo].[Feedback] (
    [FeedbackId]              UNIQUEIDENTIFIER NOT NULL,
    [UserProfileId]           INT              NOT NULL,
    [Title]                   NVARCHAR (1024)  NULL,
    [Description]             NVARCHAR (2048)  NULL,
    [FeedbackTypeId]          INT              NOT NULL,
    [FeedbackInitiatorTypeId] INT              NOT NULL,
    [Source]                  VARCHAR (50)     NOT NULL,
    [RatingValue]             VARCHAR (50)     NULL,
    [Latitude]                FLOAT (53)       NOT NULL,
    [Longitude]               FLOAT (53)       NOT NULL,
    [Dispositioned]           BIT              NOT NULL,
    [SessionId]               INT              NULL,
    [FeaturedEventId]         INT              NULL,
    [IsPublic]                BIT              CONSTRAINT [DF_Feedback_IsPublic] DEFAULT ((0)) NOT NULL,
    [IsChat]                  BIT              CONSTRAINT [DF_Feedback_IsChat] DEFAULT ((0)) NULL,
    [DataVersion]             INT              CONSTRAINT [DF_Feedback_DataVersion] DEFAULT ((1)) NOT NULL,
    [CreatedUtcDate]          DATETIME2 (7)    CONSTRAINT [DF_Feedback_CreatedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [CreatedBy]               NVARCHAR (200)   NOT NULL,
    [ModifiedUtcDate]         DATETIME2 (7)    CONSTRAINT [DF_Feedback_ModifiedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]              NVARCHAR (200)   NOT NULL,
    [IsDeleted]               BIT              CONSTRAINT [DF_Feedback_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED ([FeedbackId] ASC),
    CONSTRAINT [FK_Feedback_FeaturedEvent] FOREIGN KEY ([FeaturedEventId]) REFERENCES [dbo].[FeaturedEvent] ([FeaturedEventId]),
    CONSTRAINT [FK_Feedback_FeedbackInitiatorType] FOREIGN KEY ([FeedbackInitiatorTypeId]) REFERENCES [dbo].[FeedbackInitiatorType] ([FeedbackInitiatorTypeId]),
    CONSTRAINT [FK_Feedback_FeedbackType] FOREIGN KEY ([FeedbackTypeId]) REFERENCES [dbo].[FeedbackType] ([FeedbackTypeId]),
    CONSTRAINT [FK_Feedback_Session] FOREIGN KEY ([SessionId]) REFERENCES [dbo].[Session] ([SessionId]),
    CONSTRAINT [FK_Feedback_UserProfile] FOREIGN KEY ([UserProfileId]) REFERENCES [dbo].[UserProfile] ([UserProfileId])
);




GO
CREATE TRIGGER [dbo].[trg_Feedback_Update] ON [dbo].[Feedback]
      FOR UPDATE
      AS 

      SET NOCOUNT ON

      UPDATE a SET 
        a.DataVersion = b.DataVersion + 1
      FROM Feedback a
        INNER JOIN inserted b
          ON a.FeedbackId = b.FeedbackId
