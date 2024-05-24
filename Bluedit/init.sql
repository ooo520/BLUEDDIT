create database Bluedit
go

use Bluedit
go

create table category
(
    id    bigint identity
        constraint PK_category
            primary key,
    name  varchar(32) not null,
    title varchar(32) not null
)
go

create table [user]
(
    id           bigint identity
        constraint PK_user
            primary key,
    name         varchar(32)                              not null,
    password     varchar(32)                              not null,
    description  varchar(256)                             not null,
    creationDate datetime
        constraint DF_user_creationDate default getdate() not null,
    mail         varchar(64)                              not null
)
go

create table answer
(
    id           bigint                                     not null
        constraint PK_answer
            primary key,
    content      text                                       not null,
    userId       bigint identity
        constraint FK_answer_user1
            references [user],
    threadId     bigint                                     not null,
    creationDate datetime
        constraint DF_answer_creationDate default getdate() not null
)
go

create table opinion
(
    id       bigint identity
        constraint PK_opinion
            primary key,
    answerId bigint not null
        constraint FK_opinion_answer1
            references answer,
    [like]   bit    not null,
    authorId bigint not null
        constraint FK_opinion_answer
            references [user]
)
go

create table thread
(
    id           bigint identity
        constraint PK_thread
            primary key,
    title        varchar(32) not null,
    categoryId   bigint      not null
        constraint FK_thread_category
            references category,
    rootAnswerId bigint      not null
        constraint FK_thread_answer
            references answer
)
go

alter table answer
    add constraint FK_answer_user
        foreign key (threadId) references thread
go

INSERT INTO Bluedit.dbo.category (name, title) VALUES (N'depression', N'depression');
INSERT INTO Bluedit.dbo.category (name, title) VALUES (N'minecraft', N'minecraft');
INSERT INTO Bluedit.dbo.category (name, title) VALUES (N'demineur', N'demineur');
INSERT INTO Bluedit.dbo.category (name, title) VALUES (N'tetris', N'tetris');

