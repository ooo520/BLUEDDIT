CREATE TABLE [dbo].[user] (
    [id]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [name]         VARCHAR (32)  NOT NULL,
    [password]     VARCHAR (32)  NOT NULL,
    [description]  VARCHAR (256) NOT NULL,
    [creationDate] DATETIME      CONSTRAINT [DF_user_creationDate] DEFAULT (getdate()) NOT NULL,
    [mail]         VARCHAR (64)  NOT NULL,
    CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED ([id] ASC)
);



