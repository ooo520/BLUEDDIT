CREATE TABLE [dbo].[opinion] (
    [id]       BIGINT IDENTITY (1, 1) NOT NULL,
    [answerId] BIGINT NOT NULL,
    [like]     BIT    NOT NULL,
    [authorId] BIGINT NOT NULL,
    CONSTRAINT [PK_opinion] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_opinion_answer] FOREIGN KEY ([authorId]) REFERENCES [dbo].[user] ([id])
);



