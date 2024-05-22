CREATE TABLE [dbo].[category] (
    [id]    BIGINT       IDENTITY (1, 1) NOT NULL,
    [name]  VARCHAR (32) NOT NULL,
    [title] VARCHAR (32) NOT NULL,
    CONSTRAINT [PK_category] PRIMARY KEY CLUSTERED ([id] ASC)
);



