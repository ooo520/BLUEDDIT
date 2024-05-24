CREATE TABLE [dbo].[answer] (
    [id]           BIGINT   IDENTITY (1, 1) NOT NULL,
    [content]      TEXT     NOT NULL,
    [userId]       BIGINT   NOT NULL,
    [threadId]     BIGINT   NOT NULL,
    [creationDate] DATETIME CONSTRAINT [DF_answer_creationDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_answer] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_answer_thread] FOREIGN KEY ([threadId]) REFERENCES [dbo].[thread] ([id]),
    CONSTRAINT [FK_answer_user] FOREIGN KEY ([userId]) REFERENCES [dbo].[user] ([id])
);





