USE [ConferenceMate]
GO
/****** Object:  Table [dbo].[Announcement]    Script Date: 7/8/2019 4:48:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Announcement](
	[AnnouncementId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](1000) NOT NULL,
	[ShortTitle] [nvarchar](500) NULL,
	[Description] [nvarchar](4000) NULL,
	[StartTime] [datetime2](7) NULL,
	[EndTime] [datetime2](7) NULL,
	[DataVersion] [int] NOT NULL,
	[CreatedUtcDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](200) NOT NULL,
	[ModifiedUtcDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](200) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Announcement] PRIMARY KEY CLUSTERED 
(
	[AnnouncementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 7/8/2019 4:48:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 7/8/2019 4:48:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 7/8/2019 4:48:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 7/8/2019 4:48:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 7/8/2019 4:48:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlobFile]    Script Date: 7/8/2019 4:48:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlobFile](
	[BlobFileId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[DiscreteMimeType] [varchar](100) NOT NULL,
	[Content] [varbinary](max) NULL,
	[BlobUri] [nvarchar](2000) NULL,
	[SizeInBytes] [bigint] NULL,
	[ParentBlobFileId] [uniqueidentifier] NULL,
	[BlobFileTypeId] [int] NULL,
	[RequiresResize] [bit] NULL,
	[ResizeComplete] [bit] NULL,
	[DataVersion] [int] NOT NULL,
	[CreatedUtcDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](200) NOT NULL,
	[ModifiedUtcDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](200) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_BlobFile] PRIMARY KEY CLUSTERED 
(
	[BlobFileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlobFileType]    Script Date: 7/8/2019 4:48:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlobFileType](
	[BlobFileTypeId] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[GeneralType] [varchar](50) NULL,
	[CategoryType] [varchar](50) NULL,
	[ResolutionX] [int] NULL,
	[ResolutionY] [int] NULL,
	[ResizeFromBlobFileTypeId] [int] NULL,
	[DataVersion] [int] NOT NULL,
	[CreatedUtcDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](200) NOT NULL,
	[ModifiedUtcDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](200) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_BlobFileType] PRIMARY KEY CLUSTERED 
(
	[BlobFileTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UC_BlobFileType] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FeaturedEvent]    Script Date: 7/8/2019 4:48:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeaturedEvent](
	[FeaturedEventId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](1000) NOT NULL,
	[ShortTitle] [nvarchar](500) NULL,
	[Description] [nvarchar](4000) NULL,
	[Location] [nvarchar](1000) NULL,
	[StartTime] [datetime2](7) NULL,
	[EndTime] [datetime2](7) NULL,
	[IsAllDay] [bit] NOT NULL,
	[DataVersion] [int] NOT NULL,
	[CreatedUtcDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](200) NOT NULL,
	[ModifiedUtcDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](200) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_FeaturedEvent] PRIMARY KEY CLUSTERED 
(
	[FeaturedEventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 7/8/2019 4:48:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[FeedbackId] [uniqueidentifier] NOT NULL,
	[UserProfileId] [int] NOT NULL,
	[Title] [nvarchar](1024) NULL,
	[Description] [nvarchar](2048) NULL,
	[FeedbackTypeId] [int] NOT NULL,
	[FeedbackInitiatorTypeId] [int] NOT NULL,
	[Source] [varchar](50) NOT NULL,
	[RatingValue] [varchar](50) NULL,
	[Latitude] [float] NOT NULL,
	[Longitude] [float] NOT NULL,
	[Dispositioned] [bit] NOT NULL,
	[SessionId] [int] NULL,
	[FeaturedEventId] [int] NULL,
	[IsPublic] [bit] NOT NULL,
	[IsChat] [bit] NULL,
	[DataVersion] [int] NOT NULL,
	[CreatedUtcDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](200) NOT NULL,
	[ModifiedUtcDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](200) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED 
(
	[FeedbackId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FeedbackInitiatorType]    Script Date: 7/8/2019 4:48:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeedbackInitiatorType](
	[FeedbackInitiatorTypeId] [int] NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[DataVersion] [int] NOT NULL,
	[CreatedUtcDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](200) NOT NULL,
	[ModifiedUtcDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](200) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_FeedbackInitiatorType] PRIMARY KEY CLUSTERED 
(
	[FeedbackInitiatorTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FeedbackType]    Script Date: 7/8/2019 4:48:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeedbackType](
	[FeedbackTypeId] [int] NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[DataVersion] [int] NOT NULL,
	[CreatedUtcDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](200) NOT NULL,
	[ModifiedUtcDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](200) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_FeedbackType] PRIMARY KEY CLUSTERED 
(
	[FeedbackTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GenderType]    Script Date: 7/8/2019 4:48:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GenderType](
	[GenderTypeId] [int] NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[DataVersion] [int] NOT NULL,
	[CreatedUtcDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](200) NOT NULL,
	[ModifiedUtcDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](200) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_GenderType] PRIMARY KEY CLUSTERED 
(
	[GenderTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LanguageType]    Script Date: 7/8/2019 4:48:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LanguageType](
	[LanguageTypeId] [int] NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[DisplayText] [nvarchar](128) NOT NULL,
	[DisplayPriority] [int] NOT NULL,
	[NativeName] [nvarchar](100) NOT NULL,
	[ThreeLetterISOLanguageName] [nchar](3) NOT NULL,
	[TwoLetterISOLanguageName] [nchar](2) NOT NULL,
	[LanguageCultureIdentifier] [int] NULL,
	[DataVersion] [int] NOT NULL,
	[CreatedUtcDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](200) NOT NULL,
	[ModifiedUtcDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](200) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_LanguageType] PRIMARY KEY CLUSTERED 
(
	[LanguageTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UC_LanguageType_Code] UNIQUE NONCLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Log]    Script Date: 7/8/2019 4:48:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Thread] [varchar](255) NOT NULL,
	[Level] [varchar](50) NOT NULL,
	[Logger] [varchar](255) NOT NULL,
	[Message] [varchar](4000) NOT NULL,
	[Exception] [varchar](2000) NULL,
	[LogMessageTypeID] [int] NULL,
	[MethodName] [nvarchar](255) NULL,
	[UserName] [nvarchar](255) NULL,
	[ClientIPAddress] [nvarchar](255) NULL,
	[LogGuid] [uniqueidentifier] NULL,
	[ExecutionTimeInMilliseconds] [bigint] NULL,
	[HttpResponseStatusCode] [nvarchar](255) NULL,
	[Url] [nvarchar](2000) NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LogType]    Script Date: 7/8/2019 4:48:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogType](
	[ID] [int] NOT NULL,
	[Type] [nvarchar](150) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LookupList]    Script Date: 7/8/2019 4:48:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LookupList](
	[LookupListId] [int] IDENTITY(1,1) NOT NULL,
	[LanguageTypeId] [int] NOT NULL,
	[ForeignKeyTablePkId] [int] NOT NULL,
	[ForeignKeyTableName] [nvarchar](200) NOT NULL,
	[DisplayPriority] [int] NOT NULL,
	[DisplayText] [nvarchar](4000) NOT NULL,
	[Description] [nvarchar](2000) NULL,
	[CustomJson] [nvarchar](max) NULL,
	[DataVersion] [int] NOT NULL,
	[CreatedUtcDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](200) NOT NULL,
	[ModifiedUtcDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](200) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_LookupList] PRIMARY KEY CLUSTERED 
(
	[LookupListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RefreshToken]    Script Date: 7/8/2019 4:48:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshToken](
	[RefreshTokenId] [int] IDENTITY(1,1) NOT NULL,
	[AspNetUsersId] [nvarchar](128) NULL,
	[Token] [nvarchar](max) NULL,
	[IssuedUtc] [datetime2](7) NOT NULL,
	[ExpiresUtc] [datetime2](7) NOT NULL,
	[ProtectedTicket] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_RefreshToken] PRIMARY KEY CLUSTERED 
(
	[RefreshTokenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UC_RefreshToken_AspNetUsersId] UNIQUE NONCLUSTERED 
(
	[AspNetUsersId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 7/8/2019 4:48:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room](
	[RoomId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](1000) NOT NULL,
	[ShortTitle] [nvarchar](500) NULL,
	[Description] [nvarchar](4000) NULL,
	[SeatingCapacity] [int] NOT NULL,
	[Latitude] [float] NOT NULL,
	[Longitude] [float] NOT NULL,
	[DataVersion] [int] NOT NULL,
	[CreatedUtcDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](200) NOT NULL,
	[ModifiedUtcDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](200) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Session]    Script Date: 7/8/2019 4:48:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Session](
	[SessionId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](1000) NOT NULL,
	[ShortTitle] [nvarchar](500) NULL,
	[Description] [nvarchar](4000) NULL,
	[RoomId] [int] NULL,
	[StartTime] [datetime2](7) NULL,
	[EndTime] [datetime2](7) NULL,
	[DataVersion] [int] NOT NULL,
	[CreatedUtcDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](200) NOT NULL,
	[ModifiedUtcDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](200) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED 
(
	[SessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Session_Like]    Script Date: 7/8/2019 4:48:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Session_Like](
	[SessionId] [int] NOT NULL,
	[UserProfileId] [int] NOT NULL,
	[DataVersion] [int] NOT NULL,
	[CreatedUtcDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](200) NOT NULL,
	[ModifiedUtcDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](200) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Session_Like] PRIMARY KEY CLUSTERED 
(
	[SessionId] ASC,
	[UserProfileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Session_SessionCategoryType]    Script Date: 7/8/2019 4:48:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Session_SessionCategoryType](
	[SessionId] [int] NOT NULL,
	[SessionCategoryTypeId] [int] NOT NULL,
	[DataVersion] [int] NOT NULL,
	[CreatedUtcDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](200) NOT NULL,
	[ModifiedUtcDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](200) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Session_SessionCategoryType] PRIMARY KEY CLUSTERED 
(
	[SessionId] ASC,
	[SessionCategoryTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Session_Speaker]    Script Date: 7/8/2019 4:48:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Session_Speaker](
	[SessionId] [int] NOT NULL,
	[UserProfileId] [int] NOT NULL,
	[DataVersion] [int] NOT NULL,
	[CreatedUtcDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](200) NOT NULL,
	[ModifiedUtcDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](200) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Session_Speaker] PRIMARY KEY CLUSTERED 
(
	[SessionId] ASC,
	[UserProfileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SessionCategoryType]    Script Date: 7/8/2019 4:48:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SessionCategoryType](
	[SessionCategoryTypeId] [int] NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[DataVersion] [int] NOT NULL,
	[CreatedUtcDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](200) NOT NULL,
	[ModifiedUtcDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](200) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_SessionCategoryType] PRIMARY KEY CLUSTERED 
(
	[SessionCategoryTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sponsor]    Script Date: 7/8/2019 4:48:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sponsor](
	[SponsorId] [int] IDENTITY(1,1) NOT NULL,
	[SponsorTypeId] [int] NOT NULL,
	[Title] [nvarchar](1000) NOT NULL,
	[ShortTitle] [nvarchar](500) NULL,
	[Description] [nvarchar](4000) NULL,
	[ImageUrl] [nvarchar](1000) NULL,
	[WebsiteUrl] [nvarchar](1000) NULL,
	[TwitterUrl] [nvarchar](1000) NULL,
	[BoothLocation] [nvarchar](1000) NULL,
	[Rank] [int] NOT NULL,
	[DataVersion] [int] NOT NULL,
	[CreatedUtcDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](200) NOT NULL,
	[ModifiedUtcDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](200) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Sponsor] PRIMARY KEY CLUSTERED 
(
	[SponsorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sponsor_FeaturedEvent]    Script Date: 7/8/2019 4:48:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sponsor_FeaturedEvent](
	[SponsorId] [int] NOT NULL,
	[FeaturedEventId] [int] NOT NULL,
	[DataVersion] [int] NOT NULL,
	[CreatedUtcDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](200) NOT NULL,
	[ModifiedUtcDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](200) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Sponsor_FeaturedEvent] PRIMARY KEY CLUSTERED 
(
	[SponsorId] ASC,
	[FeaturedEventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SponsorType]    Script Date: 7/8/2019 4:48:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SponsorType](
	[SponsorTypeId] [int] NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[DataVersion] [int] NOT NULL,
	[CreatedUtcDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](200) NOT NULL,
	[ModifiedUtcDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](200) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_SponsorType] PRIMARY KEY CLUSTERED 
(
	[SponsorTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 7/8/2019 4:48:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfile](
	[UserProfileId] [int] IDENTITY(1,1) NOT NULL,
	[AspNetUsersId] [nvarchar](128) NOT NULL,
	[BirthDate] [datetime2](7) NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[GenderTypeId] [int] NULL,
	[LastLogin] [datetime2](7) NOT NULL,
	[PreferredLanguageId] [int] NOT NULL,
	[Biography] [nvarchar](4000) NULL,
	[PhotoBlobFileId] [uniqueidentifier] NULL,
	[AvatarUrl] [nvarchar](1000) NULL,
	[CompanyName] [nvarchar](1000) NULL,
	[JobTitle] [nvarchar](1000) NULL,
	[CompanyWebsiteUrl] [nvarchar](1000) NULL,
	[BlogUrl] [nvarchar](1000) NULL,
	[TwitterUrl] [nvarchar](1000) NULL,
	[LinkedInUrl] [nvarchar](1000) NULL,
	[DataVersion] [int] NOT NULL,
	[CreatedUtcDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](200) NOT NULL,
	[ModifiedUtcDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](200) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_UserProfile] PRIMARY KEY CLUSTERED 
(
	[UserProfileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UC_UserProfile_AspNetUsersId] UNIQUE NONCLUSTERED 
(
	[AspNetUsersId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 7/8/2019 4:48:12 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 7/8/2019 4:48:12 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 7/8/2019 4:48:12 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoleId]    Script Date: 7/8/2019 4:48:12 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 7/8/2019 4:48:12 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 7/8/2019 4:48:12 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 7/8/2019 4:48:12 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[RefreshToken]
(
	[AspNetUsersId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Announcement] ADD  CONSTRAINT [DF_Announcement_DataVersion]  DEFAULT ((1)) FOR [DataVersion]
GO
ALTER TABLE [dbo].[Announcement] ADD  CONSTRAINT [DF_Announcement_CreatedUtcDate]  DEFAULT (getutcdate()) FOR [CreatedUtcDate]
GO
ALTER TABLE [dbo].[Announcement] ADD  CONSTRAINT [DF_Announcement_ModifiedUtcDate]  DEFAULT (getutcdate()) FOR [ModifiedUtcDate]
GO
ALTER TABLE [dbo].[Announcement] ADD  CONSTRAINT [DF_Announcement_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  CONSTRAINT [DF_AspNetUsers_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[BlobFile] ADD  CONSTRAINT [DF_BlobFile_DataVersion]  DEFAULT ((1)) FOR [DataVersion]
GO
ALTER TABLE [dbo].[BlobFile] ADD  CONSTRAINT [DF_BlobFile_CreatedUtcDate]  DEFAULT (getutcdate()) FOR [CreatedUtcDate]
GO
ALTER TABLE [dbo].[BlobFile] ADD  CONSTRAINT [DF_BlobFile_ModifiedUtcDate]  DEFAULT (getutcdate()) FOR [ModifiedUtcDate]
GO
ALTER TABLE [dbo].[BlobFile] ADD  CONSTRAINT [DF_BlobFile_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[BlobFileType] ADD  CONSTRAINT [DF_BlobFileType_DataVersion]  DEFAULT ((1)) FOR [DataVersion]
GO
ALTER TABLE [dbo].[BlobFileType] ADD  CONSTRAINT [DF_BlobFileType_CreatedUtcDate]  DEFAULT (getutcdate()) FOR [CreatedUtcDate]
GO
ALTER TABLE [dbo].[BlobFileType] ADD  CONSTRAINT [DF_BlobFileType_ModifiedUtcDate]  DEFAULT (getutcdate()) FOR [ModifiedUtcDate]
GO
ALTER TABLE [dbo].[BlobFileType] ADD  CONSTRAINT [DF_BlobFileType_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[FeaturedEvent] ADD  CONSTRAINT [DF_FeaturedEvent_IsAllDay]  DEFAULT ((0)) FOR [IsAllDay]
GO
ALTER TABLE [dbo].[FeaturedEvent] ADD  CONSTRAINT [DF_FeaturedEvent_DataVersion]  DEFAULT ((1)) FOR [DataVersion]
GO
ALTER TABLE [dbo].[FeaturedEvent] ADD  CONSTRAINT [DF_FeaturedEvent_CreatedUtcDate]  DEFAULT (getutcdate()) FOR [CreatedUtcDate]
GO
ALTER TABLE [dbo].[FeaturedEvent] ADD  CONSTRAINT [DF_FeaturedEvent_ModifiedUtcDate]  DEFAULT (getutcdate()) FOR [ModifiedUtcDate]
GO
ALTER TABLE [dbo].[FeaturedEvent] ADD  CONSTRAINT [DF_FeaturedEvent_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Feedback] ADD  CONSTRAINT [DF_Feedback_IsPublic]  DEFAULT ((0)) FOR [IsPublic]
GO
ALTER TABLE [dbo].[Feedback] ADD  CONSTRAINT [DF_Feedback_IsChat]  DEFAULT ((0)) FOR [IsChat]
GO
ALTER TABLE [dbo].[Feedback] ADD  CONSTRAINT [DF_Feedback_DataVersion]  DEFAULT ((1)) FOR [DataVersion]
GO
ALTER TABLE [dbo].[Feedback] ADD  CONSTRAINT [DF_Feedback_CreatedUtcDate]  DEFAULT (getutcdate()) FOR [CreatedUtcDate]
GO
ALTER TABLE [dbo].[Feedback] ADD  CONSTRAINT [DF_Feedback_ModifiedUtcDate]  DEFAULT (getutcdate()) FOR [ModifiedUtcDate]
GO
ALTER TABLE [dbo].[Feedback] ADD  CONSTRAINT [DF_Feedback_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[FeedbackInitiatorType] ADD  CONSTRAINT [DF_FeedbackInitiatorType_DataVersion]  DEFAULT ((1)) FOR [DataVersion]
GO
ALTER TABLE [dbo].[FeedbackInitiatorType] ADD  CONSTRAINT [DF_FeedbackInitiatorType_CreatedUtcDate]  DEFAULT (getutcdate()) FOR [CreatedUtcDate]
GO
ALTER TABLE [dbo].[FeedbackInitiatorType] ADD  CONSTRAINT [DF_FeedbackInitiatorType_ModifiedUtcDate]  DEFAULT (getutcdate()) FOR [ModifiedUtcDate]
GO
ALTER TABLE [dbo].[FeedbackInitiatorType] ADD  CONSTRAINT [DF_FeedbackInitiatorType_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[FeedbackType] ADD  CONSTRAINT [DF_FeedbackType_DataVersion]  DEFAULT ((1)) FOR [DataVersion]
GO
ALTER TABLE [dbo].[FeedbackType] ADD  CONSTRAINT [DF_FeedbackType_CreatedUtcDate]  DEFAULT (getutcdate()) FOR [CreatedUtcDate]
GO
ALTER TABLE [dbo].[FeedbackType] ADD  CONSTRAINT [DF_FeedbackType_ModifiedUtcDate]  DEFAULT (getutcdate()) FOR [ModifiedUtcDate]
GO
ALTER TABLE [dbo].[FeedbackType] ADD  CONSTRAINT [DF_FeedbackType_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[GenderType] ADD  CONSTRAINT [DF_GenderType_DataVersion]  DEFAULT ((1)) FOR [DataVersion]
GO
ALTER TABLE [dbo].[GenderType] ADD  CONSTRAINT [DF_GenderType_CreatedUtcDate]  DEFAULT (getutcdate()) FOR [CreatedUtcDate]
GO
ALTER TABLE [dbo].[GenderType] ADD  CONSTRAINT [DF_GenderType_ModifiedUtcDate]  DEFAULT (getutcdate()) FOR [ModifiedUtcDate]
GO
ALTER TABLE [dbo].[GenderType] ADD  CONSTRAINT [DF_GenderType_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[LanguageType] ADD  CONSTRAINT [DF_LanguageType_DisplayPriority]  DEFAULT ((100)) FOR [DisplayPriority]
GO
ALTER TABLE [dbo].[LanguageType] ADD  CONSTRAINT [DF_LanguageType_DataVersion]  DEFAULT ((1)) FOR [DataVersion]
GO
ALTER TABLE [dbo].[LanguageType] ADD  CONSTRAINT [DF_LanguageType_CreatedUtcDate]  DEFAULT (getutcdate()) FOR [CreatedUtcDate]
GO
ALTER TABLE [dbo].[LanguageType] ADD  CONSTRAINT [DF_LanguageType_ModifiedUtcDate]  DEFAULT (getutcdate()) FOR [ModifiedUtcDate]
GO
ALTER TABLE [dbo].[LanguageType] ADD  CONSTRAINT [DF_LanguageType_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[LookupList] ADD  CONSTRAINT [DF_LookupList_DataVersion]  DEFAULT ((1)) FOR [DataVersion]
GO
ALTER TABLE [dbo].[LookupList] ADD  CONSTRAINT [DF_LookupList_CreatedUtcDate]  DEFAULT (getutcdate()) FOR [CreatedUtcDate]
GO
ALTER TABLE [dbo].[LookupList] ADD  CONSTRAINT [DF_LookupList_ModifiedUtcDate]  DEFAULT (getutcdate()) FOR [ModifiedUtcDate]
GO
ALTER TABLE [dbo].[LookupList] ADD  CONSTRAINT [DF_LookupList_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Room] ADD  CONSTRAINT [DF_Room_DataVersion]  DEFAULT ((1)) FOR [DataVersion]
GO
ALTER TABLE [dbo].[Room] ADD  CONSTRAINT [DF_Room_CreatedUtcDate]  DEFAULT (getutcdate()) FOR [CreatedUtcDate]
GO
ALTER TABLE [dbo].[Room] ADD  CONSTRAINT [DF_Room_ModifiedUtcDate]  DEFAULT (getutcdate()) FOR [ModifiedUtcDate]
GO
ALTER TABLE [dbo].[Room] ADD  CONSTRAINT [DF_Room_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Session] ADD  CONSTRAINT [DF_Session_DataVersion]  DEFAULT ((1)) FOR [DataVersion]
GO
ALTER TABLE [dbo].[Session] ADD  CONSTRAINT [DF_Session_CreatedUtcDate]  DEFAULT (getutcdate()) FOR [CreatedUtcDate]
GO
ALTER TABLE [dbo].[Session] ADD  CONSTRAINT [DF_Session_ModifiedUtcDate]  DEFAULT (getutcdate()) FOR [ModifiedUtcDate]
GO
ALTER TABLE [dbo].[Session] ADD  CONSTRAINT [DF_Session_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Session_Like] ADD  CONSTRAINT [DF_Session_Like_DataVersion]  DEFAULT ((1)) FOR [DataVersion]
GO
ALTER TABLE [dbo].[Session_Like] ADD  CONSTRAINT [DF_Session_Like_CreatedUtcDate]  DEFAULT (getutcdate()) FOR [CreatedUtcDate]
GO
ALTER TABLE [dbo].[Session_Like] ADD  CONSTRAINT [DF_Session_Like_ModifiedUtcDate]  DEFAULT (getutcdate()) FOR [ModifiedUtcDate]
GO
ALTER TABLE [dbo].[Session_Like] ADD  CONSTRAINT [DF_Session_Like_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Session_SessionCategoryType] ADD  CONSTRAINT [DF_Session_SessionCategoryType_DataVersion]  DEFAULT ((1)) FOR [DataVersion]
GO
ALTER TABLE [dbo].[Session_SessionCategoryType] ADD  CONSTRAINT [DF_Session_SessionCategoryType_CreatedUtcDate]  DEFAULT (getutcdate()) FOR [CreatedUtcDate]
GO
ALTER TABLE [dbo].[Session_SessionCategoryType] ADD  CONSTRAINT [DF_Session_SessionCategoryType_ModifiedUtcDate]  DEFAULT (getutcdate()) FOR [ModifiedUtcDate]
GO
ALTER TABLE [dbo].[Session_SessionCategoryType] ADD  CONSTRAINT [DF_Session_SessionCategoryType_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Session_Speaker] ADD  CONSTRAINT [DF_Session_Speaker_DataVersion]  DEFAULT ((1)) FOR [DataVersion]
GO
ALTER TABLE [dbo].[Session_Speaker] ADD  CONSTRAINT [DF_Session_Speaker_CreatedUtcDate]  DEFAULT (getutcdate()) FOR [CreatedUtcDate]
GO
ALTER TABLE [dbo].[Session_Speaker] ADD  CONSTRAINT [DF_Session_Speaker_ModifiedUtcDate]  DEFAULT (getutcdate()) FOR [ModifiedUtcDate]
GO
ALTER TABLE [dbo].[Session_Speaker] ADD  CONSTRAINT [DF_Session_Speaker_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[SessionCategoryType] ADD  CONSTRAINT [DF_SessionCategoryType_DataVersion]  DEFAULT ((1)) FOR [DataVersion]
GO
ALTER TABLE [dbo].[SessionCategoryType] ADD  CONSTRAINT [DF_SessionCategoryType_CreatedUtcDate]  DEFAULT (getutcdate()) FOR [CreatedUtcDate]
GO
ALTER TABLE [dbo].[SessionCategoryType] ADD  CONSTRAINT [DF_SessionCategoryType_ModifiedUtcDate]  DEFAULT (getutcdate()) FOR [ModifiedUtcDate]
GO
ALTER TABLE [dbo].[SessionCategoryType] ADD  CONSTRAINT [DF_SessionCategoryType_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Sponsor] ADD  CONSTRAINT [DF_Sponsor_Rank]  DEFAULT ((100)) FOR [Rank]
GO
ALTER TABLE [dbo].[Sponsor] ADD  CONSTRAINT [DF_Sponsor_DataVersion]  DEFAULT ((1)) FOR [DataVersion]
GO
ALTER TABLE [dbo].[Sponsor] ADD  CONSTRAINT [DF_Sponsor_CreatedUtcDate]  DEFAULT (getutcdate()) FOR [CreatedUtcDate]
GO
ALTER TABLE [dbo].[Sponsor] ADD  CONSTRAINT [DF_Sponsor_ModifiedUtcDate]  DEFAULT (getutcdate()) FOR [ModifiedUtcDate]
GO
ALTER TABLE [dbo].[Sponsor] ADD  CONSTRAINT [DF_Sponsor_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Sponsor_FeaturedEvent] ADD  CONSTRAINT [DF_Sponsor_FeaturedEvent_DataVersion]  DEFAULT ((1)) FOR [DataVersion]
GO
ALTER TABLE [dbo].[Sponsor_FeaturedEvent] ADD  CONSTRAINT [DF_Sponsor_FeaturedEvent_CreatedUtcDate]  DEFAULT (getutcdate()) FOR [CreatedUtcDate]
GO
ALTER TABLE [dbo].[Sponsor_FeaturedEvent] ADD  CONSTRAINT [DF_Sponsor_FeaturedEvent_ModifiedUtcDate]  DEFAULT (getutcdate()) FOR [ModifiedUtcDate]
GO
ALTER TABLE [dbo].[Sponsor_FeaturedEvent] ADD  CONSTRAINT [DF_Sponsor_FeaturedEvent_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[SponsorType] ADD  CONSTRAINT [DF_SponsorType_DataVersion]  DEFAULT ((1)) FOR [DataVersion]
GO
ALTER TABLE [dbo].[SponsorType] ADD  CONSTRAINT [DF_SponsorType_CreatedUtcDate]  DEFAULT (getutcdate()) FOR [CreatedUtcDate]
GO
ALTER TABLE [dbo].[SponsorType] ADD  CONSTRAINT [DF_SponsorType_ModifiedUtcDate]  DEFAULT (getutcdate()) FOR [ModifiedUtcDate]
GO
ALTER TABLE [dbo].[SponsorType] ADD  CONSTRAINT [DF_SponsorType_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[UserProfile] ADD  CONSTRAINT [DF_UserProfile_PreferredLanguageId]  DEFAULT ((1)) FOR [PreferredLanguageId]
GO
ALTER TABLE [dbo].[UserProfile] ADD  CONSTRAINT [DF_UserProfile_DataVersion]  DEFAULT ((1)) FOR [DataVersion]
GO
ALTER TABLE [dbo].[UserProfile] ADD  CONSTRAINT [DF_UserProfile_CreatedUtcDate]  DEFAULT (getutcdate()) FOR [CreatedUtcDate]
GO
ALTER TABLE [dbo].[UserProfile] ADD  CONSTRAINT [DF_UserProfile_ModifiedUtcDate]  DEFAULT (getutcdate()) FOR [ModifiedUtcDate]
GO
ALTER TABLE [dbo].[UserProfile] ADD  CONSTRAINT [DF_UserProfile_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[BlobFile]  WITH CHECK ADD  CONSTRAINT [FK_BlobFileType] FOREIGN KEY([BlobFileTypeId])
REFERENCES [dbo].[BlobFileType] ([BlobFileTypeId])
GO
ALTER TABLE [dbo].[BlobFile] CHECK CONSTRAINT [FK_BlobFileType]
GO
ALTER TABLE [dbo].[BlobFileType]  WITH CHECK ADD  CONSTRAINT [FK_BlobFileType_BlobFileType] FOREIGN KEY([ResizeFromBlobFileTypeId])
REFERENCES [dbo].[BlobFileType] ([BlobFileTypeId])
GO
ALTER TABLE [dbo].[BlobFileType] CHECK CONSTRAINT [FK_BlobFileType_BlobFileType]
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_FeaturedEvent] FOREIGN KEY([FeaturedEventId])
REFERENCES [dbo].[FeaturedEvent] ([FeaturedEventId])
GO
ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [FK_Feedback_FeaturedEvent]
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_FeedbackInitiatorType] FOREIGN KEY([FeedbackInitiatorTypeId])
REFERENCES [dbo].[FeedbackInitiatorType] ([FeedbackInitiatorTypeId])
GO
ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [FK_Feedback_FeedbackInitiatorType]
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_FeedbackType] FOREIGN KEY([FeedbackTypeId])
REFERENCES [dbo].[FeedbackType] ([FeedbackTypeId])
GO
ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [FK_Feedback_FeedbackType]
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Session] FOREIGN KEY([SessionId])
REFERENCES [dbo].[Session] ([SessionId])
GO
ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [FK_Feedback_Session]
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_UserProfile] FOREIGN KEY([UserProfileId])
REFERENCES [dbo].[UserProfile] ([UserProfileId])
GO
ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [FK_Feedback_UserProfile]
GO
ALTER TABLE [dbo].[LookupList]  WITH CHECK ADD  CONSTRAINT [FK_LookupList_LanguageType] FOREIGN KEY([LanguageTypeId])
REFERENCES [dbo].[LanguageType] ([LanguageTypeId])
GO
ALTER TABLE [dbo].[LookupList] CHECK CONSTRAINT [FK_LookupList_LanguageType]
GO
ALTER TABLE [dbo].[RefreshToken]  WITH CHECK ADD  CONSTRAINT [FK_RefreshToken_AspNetUsers_AspNetUsersId] FOREIGN KEY([AspNetUsersId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[RefreshToken] CHECK CONSTRAINT [FK_RefreshToken_AspNetUsers_AspNetUsersId]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_Session_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([RoomId])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_Session_Room]
GO
ALTER TABLE [dbo].[Session_Like]  WITH CHECK ADD  CONSTRAINT [FK_Session_Like_Session] FOREIGN KEY([SessionId])
REFERENCES [dbo].[Session] ([SessionId])
GO
ALTER TABLE [dbo].[Session_Like] CHECK CONSTRAINT [FK_Session_Like_Session]
GO
ALTER TABLE [dbo].[Session_Like]  WITH CHECK ADD  CONSTRAINT [FK_Session_Like_UserProfile] FOREIGN KEY([UserProfileId])
REFERENCES [dbo].[UserProfile] ([UserProfileId])
GO
ALTER TABLE [dbo].[Session_Like] CHECK CONSTRAINT [FK_Session_Like_UserProfile]
GO
ALTER TABLE [dbo].[Session_SessionCategoryType]  WITH CHECK ADD  CONSTRAINT [FK_Session_SessionCategoryType_Session] FOREIGN KEY([SessionId])
REFERENCES [dbo].[Session] ([SessionId])
GO
ALTER TABLE [dbo].[Session_SessionCategoryType] CHECK CONSTRAINT [FK_Session_SessionCategoryType_Session]
GO
ALTER TABLE [dbo].[Session_SessionCategoryType]  WITH CHECK ADD  CONSTRAINT [FK_Session_SessionCategoryType_SessionCategoryType] FOREIGN KEY([SessionCategoryTypeId])
REFERENCES [dbo].[SessionCategoryType] ([SessionCategoryTypeId])
GO
ALTER TABLE [dbo].[Session_SessionCategoryType] CHECK CONSTRAINT [FK_Session_SessionCategoryType_SessionCategoryType]
GO
ALTER TABLE [dbo].[Session_Speaker]  WITH CHECK ADD  CONSTRAINT [FK_Session_Speaker_Session] FOREIGN KEY([SessionId])
REFERENCES [dbo].[Session] ([SessionId])
GO
ALTER TABLE [dbo].[Session_Speaker] CHECK CONSTRAINT [FK_Session_Speaker_Session]
GO
ALTER TABLE [dbo].[Session_Speaker]  WITH CHECK ADD  CONSTRAINT [FK_Session_Speaker_UserProfile] FOREIGN KEY([UserProfileId])
REFERENCES [dbo].[UserProfile] ([UserProfileId])
GO
ALTER TABLE [dbo].[Session_Speaker] CHECK CONSTRAINT [FK_Session_Speaker_UserProfile]
GO
ALTER TABLE [dbo].[Sponsor]  WITH CHECK ADD  CONSTRAINT [FK_Sponsor_SponsorType] FOREIGN KEY([SponsorTypeId])
REFERENCES [dbo].[SponsorType] ([SponsorTypeId])
GO
ALTER TABLE [dbo].[Sponsor] CHECK CONSTRAINT [FK_Sponsor_SponsorType]
GO
ALTER TABLE [dbo].[Sponsor_FeaturedEvent]  WITH CHECK ADD  CONSTRAINT [FK_Sponsor_FeaturedEvent_FeaturedEvent] FOREIGN KEY([FeaturedEventId])
REFERENCES [dbo].[FeaturedEvent] ([FeaturedEventId])
GO
ALTER TABLE [dbo].[Sponsor_FeaturedEvent] CHECK CONSTRAINT [FK_Sponsor_FeaturedEvent_FeaturedEvent]
GO
ALTER TABLE [dbo].[Sponsor_FeaturedEvent]  WITH CHECK ADD  CONSTRAINT [FK_Sponsor_FeaturedEvent_Sponsor] FOREIGN KEY([SponsorId])
REFERENCES [dbo].[Sponsor] ([SponsorId])
GO
ALTER TABLE [dbo].[Sponsor_FeaturedEvent] CHECK CONSTRAINT [FK_Sponsor_FeaturedEvent_Sponsor]
GO
ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_UserProfile_AspNetUsers] FOREIGN KEY([AspNetUsersId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK_UserProfile_AspNetUsers]
GO
ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_UserProfile_BlobFile] FOREIGN KEY([PhotoBlobFileId])
REFERENCES [dbo].[BlobFile] ([BlobFileId])
GO
ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK_UserProfile_BlobFile]
GO
ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_UserProfile_GenderType_GenderTypeId] FOREIGN KEY([GenderTypeId])
REFERENCES [dbo].[GenderType] ([GenderTypeId])
GO
ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK_UserProfile_GenderType_GenderTypeId]
GO
ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_UserProfile_LanguageType] FOREIGN KEY([PreferredLanguageId])
REFERENCES [dbo].[LanguageType] ([LanguageTypeId])
GO
ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK_UserProfile_LanguageType]
GO
/****** Object:  Trigger [dbo].[trg_Announcement_Update]    Script Date: 7/8/2019 4:48:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
GO
ALTER TABLE [dbo].[Announcement] ENABLE TRIGGER [trg_Announcement_Update]
GO
/****** Object:  Trigger [dbo].[trg_BlobFile_Update]    Script Date: 7/8/2019 4:48:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


      CREATE TRIGGER [dbo].[trg_BlobFile_Update] ON [dbo].[BlobFile]
      FOR UPDATE
      AS 

      SET NOCOUNT ON

      UPDATE a SET 
        a.DataVersion = b.DataVersion + 1
      FROM [BlobFile] a
        INNER JOIN inserted b
          ON a.BlobFileId = b.BlobFileId


GO
ALTER TABLE [dbo].[BlobFile] ENABLE TRIGGER [trg_BlobFile_Update]
GO
/****** Object:  Trigger [dbo].[trg_BlobFileType_Update]    Script Date: 7/8/2019 4:48:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

      CREATE TRIGGER [dbo].[trg_BlobFileType_Update] ON [dbo].[BlobFileType]
      FOR UPDATE
      AS 

      SET NOCOUNT ON

      UPDATE a SET 
        a.DataVersion = b.DataVersion + 1
      FROM BlobFileType a
        INNER JOIN inserted b
          ON a.BlobFileTypeId = b.BlobFileTypeId

GO
ALTER TABLE [dbo].[BlobFileType] ENABLE TRIGGER [trg_BlobFileType_Update]
GO
/****** Object:  Trigger [dbo].[trg_FeaturedEvent_Update]    Script Date: 7/8/2019 4:48:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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


GO
ALTER TABLE [dbo].[FeaturedEvent] ENABLE TRIGGER [trg_FeaturedEvent_Update]
GO
/****** Object:  Trigger [dbo].[trg_Feedback_Update]    Script Date: 7/8/2019 4:48:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
GO
ALTER TABLE [dbo].[Feedback] ENABLE TRIGGER [trg_Feedback_Update]
GO
/****** Object:  Trigger [dbo].[trg_FeedbackInitiatorType_Update]    Script Date: 7/8/2019 4:48:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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


GO
ALTER TABLE [dbo].[FeedbackInitiatorType] ENABLE TRIGGER [trg_FeedbackInitiatorType_Update]
GO
/****** Object:  Trigger [dbo].[trg_FeedbackType_Update]    Script Date: 7/8/2019 4:48:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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


GO
ALTER TABLE [dbo].[FeedbackType] ENABLE TRIGGER [trg_FeedbackType_Update]
GO
/****** Object:  Trigger [dbo].[trg_GenderType_Update]    Script Date: 7/8/2019 4:48:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





      CREATE TRIGGER [dbo].[trg_GenderType_Update] ON [dbo].[GenderType]
      FOR UPDATE
      AS 

      SET NOCOUNT ON

      UPDATE a SET 
        a.DataVersion = b.DataVersion + 1
      FROM GenderType a
        INNER JOIN inserted b
          ON a.GenderTypeId = b.GenderTypeId


GO
ALTER TABLE [dbo].[GenderType] ENABLE TRIGGER [trg_GenderType_Update]
GO
/****** Object:  Trigger [dbo].[trg_LanguageType_Update]    Script Date: 7/8/2019 4:48:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





      CREATE TRIGGER [dbo].[trg_LanguageType_Update] ON [dbo].[LanguageType]
      FOR UPDATE
      AS 

      SET NOCOUNT ON

      UPDATE a SET 
        a.DataVersion = b.DataVersion + 1
      FROM LanguageType a
        INNER JOIN inserted b
          ON a.LanguageTypeId = b.LanguageTypeId


GO
ALTER TABLE [dbo].[LanguageType] ENABLE TRIGGER [trg_LanguageType_Update]
GO
/****** Object:  Trigger [dbo].[trg_LookupList_Update]    Script Date: 7/8/2019 4:48:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- [trg_LookupList_Update]
CREATE TRIGGER [dbo].[trg_LookupList_Update] ON [dbo].[LookupList]
FOR UPDATE
AS 

SET NOCOUNT ON

UPDATE a SET 
a.DataVersion = b.DataVersion + 1
FROM LookupList a
INNER JOIN inserted b
    ON a.LookupListId = b.LookupListId

GO
ALTER TABLE [dbo].[LookupList] ENABLE TRIGGER [trg_LookupList_Update]
GO
/****** Object:  Trigger [dbo].[trg_Room_Update]    Script Date: 7/8/2019 4:48:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
GO
ALTER TABLE [dbo].[Room] ENABLE TRIGGER [trg_Room_Update]
GO
/****** Object:  Trigger [dbo].[trg_Session_Update]    Script Date: 7/8/2019 4:48:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
GO
ALTER TABLE [dbo].[Session] ENABLE TRIGGER [trg_Session_Update]
GO
/****** Object:  Trigger [dbo].[trg_Session_Like_Update]    Script Date: 7/8/2019 4:48:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE TRIGGER [dbo].[trg_Session_Like_Update] ON [dbo].[Session_Like]
      FOR UPDATE
      AS 

      SET NOCOUNT ON

      UPDATE a SET 
        a.DataVersion = b.DataVersion + 1
      FROM Session_Like a
        INNER JOIN inserted b
          ON a.SessionId = b.SessionId
		  AND a.UserProfileId = b.UserProfileId
GO
ALTER TABLE [dbo].[Session_Like] ENABLE TRIGGER [trg_Session_Like_Update]
GO
/****** Object:  Trigger [dbo].[trg_Session_SessionCategoryType_Update]    Script Date: 7/8/2019 4:48:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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


GO
ALTER TABLE [dbo].[Session_SessionCategoryType] ENABLE TRIGGER [trg_Session_SessionCategoryType_Update]
GO
/****** Object:  Trigger [dbo].[trg_Session_Speaker_Update]    Script Date: 7/8/2019 4:48:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[trg_Session_Speaker_Update] ON [dbo].[Session_Speaker]
      FOR UPDATE
      AS 

      SET NOCOUNT ON

      UPDATE a SET 
        a.DataVersion = b.DataVersion + 1
      FROM Session_Speaker a
        INNER JOIN inserted b
          ON a.SessionId = b.SessionId
		  AND a.UserProfileId = b.UserProfileId
GO
ALTER TABLE [dbo].[Session_Speaker] ENABLE TRIGGER [trg_Session_Speaker_Update]
GO
/****** Object:  Trigger [dbo].[trg_SessionCategoryType_Update]    Script Date: 7/8/2019 4:48:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







      CREATE TRIGGER [dbo].[trg_SessionCategoryType_Update] ON [dbo].[SessionCategoryType]
      FOR UPDATE
      AS 

      SET NOCOUNT ON

      UPDATE a SET 
        a.DataVersion = b.DataVersion + 1
      FROM SessionCategoryType a
        INNER JOIN inserted b
          ON a.SessionCategoryTypeId = b.SessionCategoryTypeId


GO
ALTER TABLE [dbo].[SessionCategoryType] ENABLE TRIGGER [trg_SessionCategoryType_Update]
GO
/****** Object:  Trigger [dbo].[trg_Sponsor_Update]    Script Date: 7/8/2019 4:48:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







      CREATE TRIGGER [dbo].[trg_Sponsor_Update] ON [dbo].[Sponsor]
      FOR UPDATE
      AS 

      SET NOCOUNT ON

      UPDATE a SET 
        a.DataVersion = b.DataVersion + 1
      FROM Sponsor a
        INNER JOIN inserted b
          ON a.SponsorId = b.SponsorId


GO
ALTER TABLE [dbo].[Sponsor] ENABLE TRIGGER [trg_Sponsor_Update]
GO
/****** Object:  Trigger [dbo].[trg_Sponsor_FeaturedEvent_Update]    Script Date: 7/8/2019 4:48:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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


GO
ALTER TABLE [dbo].[Sponsor_FeaturedEvent] ENABLE TRIGGER [trg_Sponsor_FeaturedEvent_Update]
GO
/****** Object:  Trigger [dbo].[trg_SponsorType_Update]    Script Date: 7/8/2019 4:48:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






      CREATE TRIGGER [dbo].[trg_SponsorType_Update] ON [dbo].[SponsorType]
      FOR UPDATE
      AS 

      SET NOCOUNT ON

      UPDATE a SET 
        a.DataVersion = b.DataVersion + 1
      FROM SponsorType a
        INNER JOIN inserted b
          ON a.SponsorTypeId = b.SponsorTypeId


GO
ALTER TABLE [dbo].[SponsorType] ENABLE TRIGGER [trg_SponsorType_Update]
GO
/****** Object:  Trigger [dbo].[trg_UserProfile_Update]    Script Date: 7/8/2019 4:48:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[trg_UserProfile_Update] ON [dbo].[UserProfile]
      FOR UPDATE
      AS 

      SET NOCOUNT ON

      UPDATE a SET 
        a.DataVersion = b.DataVersion + 1
      FROM UserProfile a
        INNER JOIN inserted b
          ON a.UserProfileId = b.UserProfileId
GO
ALTER TABLE [dbo].[UserProfile] ENABLE TRIGGER [trg_UserProfile_Update]
GO
