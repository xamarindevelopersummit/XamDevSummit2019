CREATE TABLE [dbo].[Log] (
    [Id]                          INT              IDENTITY (1, 1) NOT NULL,
    [Date]                        DATETIME         NOT NULL,
    [Thread]                      VARCHAR (255)    NOT NULL,
    [Level]                       VARCHAR (50)     NOT NULL,
    [Logger]                      VARCHAR (255)    NOT NULL,
    [Message]                     VARCHAR (4000)   NOT NULL,
    [Exception]                   VARCHAR (2000)   NULL,
    [LogMessageTypeID]            INT              NULL,
    [MethodName]                  NVARCHAR (255)   NULL,
    [UserName]                    NVARCHAR (255)   NULL,
    [ClientIPAddress]             NVARCHAR (255)   NULL,
    [LogGuid]                     UNIQUEIDENTIFIER NULL,
    [ExecutionTimeInMilliseconds] BIGINT           NULL,
    [HttpResponseStatusCode]      NVARCHAR (255)   NULL,
    [Url]                         NVARCHAR (2000)  NULL,
    CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED ([Id] ASC)
);

