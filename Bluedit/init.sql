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
    name         varchar(32)
    constraint UK_user_name UNIQUE                        not null,
    password     varchar(100)                              not null,
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

INSERT INTO bluedit.dbo.category (name, title) VALUES
(N'epita', N'Epita'),
(N'france', N'France'),
(N'tetris', N'Tetris'),
(N'animanga', N'Anime/Manga'),
(N'minecraft', N'Minecraft'),
(N'dotnet', N'.NET'),
(N'bluedit', N'bluedit'),
(N'demineur', N'Démineur'),
(N'nourriture', N'Nourriture'),
(N'pokemon', N'Pokémon');

INSERT INTO bluedit.dbo.[user] (name, password, description, creationDate, mail) VALUES
(N'Dylan', N'﻿XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=', N'Plumber Knight', N'2024-05-24 10:09:57.613', N'dylan.toledano@epita.fr'),
(N'Leo', N'﻿XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=', N'Dark Mage', N'2024-05-24 10:09:57.623', N'leo@epitech.fr'),
(N'Lea', N'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=', N'White Mage', N'2024-05-24 10:09:57.630', N'lea@uwu.fr'),
(N'Quentin', N'﻿XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=', N'Connoisseur', N'2024-05-24 10:09:57.640', N'quentin@42.fr');

INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES
 (N'MTI', 1),
 (N'Vie', 1),
 (N'PFEE', 1),
 (N'JO 2024', 2),
 (N'Paris', 2),
 (N'A mon signal', 2),
 (N'Je n''arrive pas à m''améliorer sur le jeu', 3),
 (N'WR', 3),
 (N'Modes de jeux', 3),
 (N'One piece', 4),
 (N'Crunchyroll', 4),
 (N'UwU', 4),
 (N'Nouveauté', 5),
 (N'Mode de jeux', 5),
 (N'Faction', 5),
 (N'Microsoft', 6),
 (N'Test', 6),
 (N'Smartlinks', 6),
 (N'Nouveauté', 7),
 (N'Poste du jour', 7),
 (N'Tribunal', 7),
 (N'Bug', 8),
 (N'WR', 8),
 (N'Stratégie', 8),
 (N'Marocaine', 9),
 (N'Fruit', 9),
 (N'Vegan', 9),
 (N'ZA', 10),
 (N'Tier list', 10),
 (N'Pikachu', 10);

INSERT INTO bluedit.dbo.answer (content, userId, threadId, creationDate) VALUES
(N'La meilleure majeure !', 3, 1, N'2024-05-24 13:05:47.667'),
(N'Je ne suis pas d''accord.', 1, 1, N'2024-05-24 13:10:29.893'),
(N'First', 4, 1, N'2024-05-24 13:13:35.780'),
(N'c faut tu est deuxieme', 2, 1, N'2024-05-24 13:15:53.060'),
(N'J''adore le tatou !', 1, 13,	N'2024-05-24 13:44:41.623'),
(N'Vous préférez quoi entre mini-jeux, faction et SkyBlock ?', 2, 14, N'2024-05-24 13:45:54.640'),
(N'Je vous le dis tout de suite: vous n''avez aucune chance...', 3,	15,	N'2024-05-24 13:15:53.060');

INSERT INTO bluedit.dbo.opinion (answerId, [like], authorId) VALUES
(1, 0, 1),
(1, 1, 2),
(1, 1, 3),
(1, 1, 4),
(3, 0, 2),
(4, 1, 1);
