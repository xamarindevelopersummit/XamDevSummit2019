CREATE TABLE [dbo].[UserProfile] (
    [UserProfileId]       INT             IDENTITY (1, 1) NOT NULL,
    [AspNetUsersId]       NVARCHAR (128)  NOT NULL,
    [BirthDate]           DATETIME2 (7)   NULL,
    [FirstName]           NVARCHAR (255)  NULL,
    [LastName]            NVARCHAR (255)  NULL,
    [GenderTypeId]        INT             NULL,
    [LastLogin]           DATETIME2 (7)   NOT NULL,
    [PreferredLanguageId] INT             CONSTRAINT [DF_UserProfile_PreferredLanguageId] DEFAULT ((1)) NOT NULL,
    [Biography]           NVARCHAR (4000) NULL,
    [PhotoUrl]            NVARCHAR (1000) NULL,
    [AvatarUrl]           NVARCHAR (1000) NULL,
    [CompanyName]         NVARCHAR (1000) NULL,
    [JobTitle]            NVARCHAR (1000) NULL,
    [CompanyWebsiteUrl]   NVARCHAR (1000) NULL,
    [BlogUrl]             NVARCHAR (1000) NULL,
    [TwitterUrl]          NVARCHAR (1000) NULL,
    [LinkedInUrl]         NVARCHAR (1000) NULL,
    [DataVersion]         INT             CONSTRAINT [DF_UserProfile_DataVersion] DEFAULT ((1)) NOT NULL,
    [CreatedUtcDate]      DATETIME2 (7)   CONSTRAINT [DF_UserProfile_CreatedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [CreatedBy]           NVARCHAR (200)  NOT NULL,
    [ModifiedUtcDate]     DATETIME2 (7)   CONSTRAINT [DF_UserProfile_ModifiedUtcDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]          NVARCHAR (200)  NOT NULL,
    [IsDeleted]           BIT             CONSTRAINT [DF_UserProfile_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_UserProfile] PRIMARY KEY CLUSTERED ([UserProfileId] ASC),
    CONSTRAINT [FK_UserProfile_AspNetUsers] FOREIGN KEY ([AspNetUsersId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_UserProfile_GenderType_GenderTypeId] FOREIGN KEY ([GenderTypeId]) REFERENCES [dbo].[GenderType] ([GenderTypeId]),
    CONSTRAINT [FK_UserProfile_LanguageType] FOREIGN KEY ([PreferredLanguageId]) REFERENCES [dbo].[LanguageType] ([LanguageTypeId]),
    CONSTRAINT [UC_UserProfile_AspNetUsersId] UNIQUE NONCLUSTERED ([AspNetUsersId] ASC)
);


GO
CREATE TRIGGER [dbo].[trg_UserProfile_Update] ON dbo.UserProfile
      FOR UPDATE
      AS 

      SET NOCOUNT ON

      UPDATE a SET 
        a.DataVersion = b.DataVersion + 1
      FROM UserProfile a
        INNER JOIN inserted b
          ON a.UserProfileId = b.UserProfileId