CREATE TABLE [dbo].[RefreshToken] (
    [RefreshTokenId]  INT            IDENTITY (1, 1) NOT NULL,
    [AspNetUsersId]   NVARCHAR (128) NULL,
    [Token]           NVARCHAR (MAX) NULL,
    [IssuedUtc]       DATETIME2 (7)  NOT NULL,
    [ExpiresUtc]      DATETIME2 (7)  NOT NULL,
    [ProtectedTicket] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_RefreshToken] PRIMARY KEY CLUSTERED ([RefreshTokenId] ASC),
    CONSTRAINT [FK_RefreshToken_AspNetUsers_AspNetUsersId] FOREIGN KEY ([AspNetUsersId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [UC_RefreshToken_AspNetUsersId] UNIQUE NONCLUSTERED ([AspNetUsersId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[RefreshToken]([AspNetUsersId] ASC);

