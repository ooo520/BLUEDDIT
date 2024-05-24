CREATE TABLE [dbo].[thread] (
    [id]         BIGINT       IDENTITY (1, 1) NOT NULL,
    [title]      VARCHAR (32) NOT NULL,
    [categoryId] BIGINT       NOT NULL,
    CONSTRAINT [PK_thread] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_thread_category] FOREIGN KEY ([categoryId]) REFERENCES [dbo].[category] ([id])
);





