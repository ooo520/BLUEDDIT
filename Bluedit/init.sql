create database bluedit
go

use bluedit
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
    password     varchar(50)                              not null,
    description  varchar(256)                             not null,
    creationDate datetime
        constraint DF_user_creationDate default getdate() not null,
    mail         varchar(64)                              not null
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
            references category
)
go

create table answer
(
    id           bigint	identity
        constraint PK_answer
            primary key,
    content      text                                       not null,
    userId       bigint					    not null
        constraint FK_answer_user
            references [user],
    threadId     bigint                                     not null
        constraint FK_answer_thread
            references thread,
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
        constraint FK_opinion_answer
            references answer,
    [like]   bit    not null,
    authorId bigint not null
        constraint FK_opinion_user
            references [user]
)
go

INSERT INTO bluedit.dbo.category (name, title) VALUES (N'epita', N'Epita');
INSERT INTO bluedit.dbo.category (name, title) VALUES (N'france', N'France');
INSERT INTO bluedit.dbo.category (name, title) VALUES (N'tetris', N'Tetris');
INSERT INTO bluedit.dbo.category (name, title) VALUES (N'animanga', N'Anime/Manga');
INSERT INTO bluedit.dbo.category (name, title) VALUES (N'minecraft', N'Minecraft');
INSERT INTO bluedit.dbo.category (name, title) VALUES (N'dotnet', N'.NET');
INSERT INTO bluedit.dbo.category (name, title) VALUES (N'bluedit', N'bluedit');
INSERT INTO bluedit.dbo.category (name, title) VALUES (N'demineur', N'Démineur');
INSERT INTO bluedit.dbo.category (name, title) VALUES (N'nourriture', N'Nourriture');
INSERT INTO bluedit.dbo.category (name, title) VALUES (N'pokemon', N'Pokémon');

INSERT INTO bluedit.dbo.[user] (name, password, description, creationDate, mail) VALUES (N'Dylan', N'password', N'Plumber Knight', N'2024-05-24 10:09:57.613', N'dylan.toledano@epita.fr');
INSERT INTO bluedit.dbo.[user] (name, password, description, creationDate, mail) VALUES (N'Leo', N'password', N'Dark Mage', N'2024-05-24 10:09:57.623', N'leo@epitech.fr');
INSERT INTO bluedit.dbo.[user] (name, password, description, creationDate, mail) VALUES (N'Lea', N'password', N'White Mage', N'2024-05-24 10:09:57.630', N'lea@uwu.fr');
INSERT INTO bluedit.dbo.[user] (name, password, description, creationDate, mail) VALUES (N'Quentin', N'password', N'Connoisseur', N'2024-05-24 10:09:57.640', N'quentin@42.fr');

INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES (N'MTI', 1);
INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES (N'Vie', 1);
INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES (N'PFEE', 1);
INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES (N'JO 2024', 2);
INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES (N'Paris', 2);
INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES (N'A mon signal', 2);
INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES (N'Je n''arrive pas à m''améliorer sur le jeu', 3);
INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES (N'WR', 3);
INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES (N'Modes de jeux', 3);
INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES (N'One piece', 4);
INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES (N'Crunchyroll', 4);
INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES (N'UwU', 4);
INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES (N'Nouveauté', 5);
INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES (N'Mode de jeux', 5);
INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES (N'Faction', 5);
INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES (N'Microsoft', 6);
INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES (N'Test', 6);
INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES (N'Smartlinks', 6);
INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES (N'Nouveauté', 7);
INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES (N'Poste du jour', 7);
INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES (N'Tribunal', 7);
INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES (N'Bug', 8);
INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES (N'WR', 8);
INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES (N'Stratégie', 8);
INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES (N'Marocaine', 9);
INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES (N'Fruit', 9);
INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES (N'Vegan', 9);
INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES (N'ZA', 10);
INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES (N'Tier list', 10);
INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES (N'Pikachu', 10);

INSERT INTO bluedit.dbo.answer (content, userId, threadId, creationDate) VALUES (N'La meilleure majeure !', 3, 1, N'2024-05-24 13:05:47.667');
INSERT INTO bluedit.dbo.answer (content, userId, threadId, creationDate) VALUES (N'Je ne suis pas d''accord.', 1, 1, N'2024-05-24 13:10:29.893');
INSERT INTO bluedit.dbo.answer (content, userId, threadId, creationDate) VALUES (N'First', 4, 1, N'2024-05-24 13:13:35.780');
INSERT INTO bluedit.dbo.answer (content, userId, threadId, creationDate) VALUES (N'c faut tu est deuxieme', 2, 1, N'2024-05-24 13:15:53.060');

INSERT INTO bluedit.dbo.opinion (answerId, [like], authorId) VALUES (1, 0, 1);
INSERT INTO bluedit.dbo.opinion (answerId, [like], authorId) VALUES (1, 1, 2);
INSERT INTO bluedit.dbo.opinion (answerId, [like], authorId) VALUES (1, 1, 3);
INSERT INTO bluedit.dbo.opinion (answerId, [like], authorId) VALUES (1, 1, 4);
INSERT INTO bluedit.dbo.opinion (answerId, [like], authorId) VALUES (3, 0, 2);
INSERT INTO bluedit.dbo.opinion (answerId, [like], authorId) VALUES (4, 1, 1);
