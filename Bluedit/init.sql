create database bluedit
go

use bluedit
go

create table category
(
    id    bigint identity
        constraint PK_category
            primary key,
    name  varchar(64) not null,
    title varchar(64) not null
)
go

create table [user]
(
    id           bigint identity
        constraint PK_user
            primary key,
    name         varchar(32)
    constraint UK_user_name UNIQUE                        not null,
    password     varchar(128)                             not null,
    description  varchar(256)                             not null,
    creationDate datetime
        constraint DF_user_creationDate default getdate() not null,
    mail         varchar(128)                             not null
)
go

create table thread
(
    id           bigint identity
        constraint PK_thread
            primary key,
    title        varchar(128) not null,
    categoryId   bigint       not null
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
(N'Dylan', N'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=', N'Plumber Knight', N'2024-05-24 10:09:57.613', N'dylan.toledano@epita.fr'),
(N'Leo', N'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=', N'Dark Mage', N'2024-05-24 10:09:57.623', N'leo@epitech.fr'),
(N'Lea', N'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=', N'White Mage', N'2024-05-24 10:09:57.630', N'lea@uwu.fr'),
(N'Quentin', N'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=', N'Connoisseur', N'2024-05-24 10:09:57.640', N'quentin@42.fr');







INSERT INTO bluedit.dbo.thread (title, categoryId) VALUES
 (N'MTI', 1),
 (N'Vie', 1),
 (N'PFEE', 1),
 (N'JO 2024', 2),
 (N'Paris', 2),
 (N'A mon signal', 2),
 (N'Comment s''améliorer sur le jeu', 3),
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
(N'THIS IS A ROOT ANSWER', 3, 2, N'2024-05-24 13:05:47.667'),
(N'C vraiment lundi le contrôle', 3, 3, N'2024-05-24 13:05:47.667'),
(N'Trop hâte pour les JO de 2024 à Paris !', 1, 4, N'2024-05-21 13:15:53.060'),
(N'« Paris outragé, Paris brisé, Paris martyrisé mais Paris libéré ! » – Général de Gaulle', 1, 5, N'2024-05-21 13:15:53.060'),
(N'Elle sert à quoi l''applis', 1, 6, N'2024-05-21 13:15:53.060'),
(N'jihjgcfvhjkljhg\ncvghbjknlmljkhgjhfxfghgjhgfdfgzyudifdcev bc^pvpkj\nf ensdiwchjbn sdhidzgsixughsghjokbebsdnjk', 1, 7, N'2024-05-21 13:15:53.060'),
(N'Combien??', 4, 8, N'2024-05-21 13:15:53.060'),
(N'Quels sont vos avis sur le nouveau mode de jeu ??', 1, 9, N'2024-05-21 13:15:53.060'),
(N'Le One Piece est il les amis que l''on a rencontré sur le cheamin', 1, 10, N'2024-05-21 13:15:53.060'),
(N'tdtrseeqsdftgyuhio !', 1, 11, N'2024-05-21 13:15:53.060'),
(N'oWo', 3, 12, N'2024-05-21 12:15:53.060'),
(N'Cringe', 2, 12, N'2024-05-21 13:21:53.060'),
(N'J''adore le tatou !', 1, 13,	N'2024-05-24 13:44:41.623'),
(N'Vous préférez quoi entre mini-jeux, faction et SkyBlock ?', 2, 14, N'2024-05-24 13:45:54.640'),
(N'Je vous le dis tout de suite: vous n''avez aucune chance...', 3,	15,	N'2024-05-24 13:15:53.060'),
(N'C lent et plein de spywares', 1, 16, N'2024-05-21 13:15:53.060'),
(N'Hello World', 1, 17, N'2024-05-21 13:15:53.060'),
(N'
Dictumst ut, imperdiet inceptos tempor velit! 
Congue sed non odio dapibus interdum at ridiculus sapien montes. 
Massa montes inceptos nibh cum ac, a sociosqu. Felis platea aliquet penatibus aenean. 
Elit sed turpis senectus porta. Convallis pretium ullamcorper facilisis sapien eget hendrerit curabitur class a euismod. 
Class ut sodales pretium, primis laoreet ad pretium. Conubia egestas vivamus.

Sodales pharetra convallis fames sodales aliquet elementum neque nullam vel. Diam eget aliquam tempor integer nec montes tempus duis. 
Porttitor auctor tincidunt semper. Malesuada, vivamus et aliquet! Mus odio posuere augue inceptos porta nec! Tortor magna blandit, diam et. 
Euismod ut lobortis dolor phasellus fringilla quisque nascetur cras. Conubia fames velit parturient feugiat habitasse himenaeos. Litora vitae 
gravida congue nam viverra pellentesque molestie nunc vulputate augue. Facilisi nec pretium egestas luctus. Parturient praesent aliquam
placerat! Placerat urna duis accumsan pellentesque. Montes praesent mattis taciti quis at a ullamcorper tincidunt dolor malesuada 
tristique nec! Est lectus posuere semper, aliquet.

Blandit suscipit tortor nascetur a dolor. Tortor etiam arcu etiam! Tristique varius cras lacinia arcu fermentum faucibus class in adipiscing 
purus litora. Ad amet metus, laoreet phasellus praesent quis. Sollicitudin convallis, ridiculus ultricies nostra massa sed quis. Dolor primis
adipiscing luctus. Tincidunt lobortis ultricies metus taciti feugiat dapibus sed gravida!

Dictumst leo, mauris elementum commodo vulputate lobortis. Eget erat suspendisse cras ultricies mauris hendrerit iaculis cum dapibus mauris! 
Hac taciti consectetur eleifend dignissim egestas aliquam laoreet integer adipiscing molestie facilisis. Ipsum orci dictumst, fermentum taciti. 
Nascetur magnis aenean lobortis lectus sodales ipsum. Convallis risus sapien nunc dignissim. Lobortis odio tempor tristique faucibus malesuada purus 
nibh, rutrum himenaeos eros natoque lorem! Nisi habitasse natoque dui lobortis.

















Posuere mus ullamcorper nec eros fermentum erat mattis risus? Orci accumsan hac ipsum dictum vestibulum dictumst tempor. Nostra magna donec 
aenean per tristique, est lacus massa dictum vitae. Odio pellentesque felis nisl scelerisque per. Ultrices interdum consequat a ultrices et 
mattis. Dictum quam euismod fringilla eu pulvinar sagittis eros neque velit! Gravida tortor non condimentum per magnis vivamus. Non elit
varius tempor dapibus natoque. Platea eu aptent conubia tristique posuere quam. Metus magna sociosqu nam fermentum! Ipsum sollicitudin 
sociis leo tempus sapien nam ac felis varius nostra. At dapibus duis commodo habitant! Torquent cubilia primis auctor.



', 1, 18, N'2024-05-21 13:15:53.060'),
(N'NEW ()',3 , 19, N'2024-05-21 13:15:53.060'),
(N'Bonsoir', 1, 20, N'2024-05-21 13:15:53.060'),
(N'Ici sont les damnés perdu à leur sentence.', 1, 21, N'2024-05-21 13:15:53.060'),
(N'J''ai instant perdu', 1, 22, N'2025-05-21 13:15:53.060'),
(N'faire plus de 30 secondes en expert c abusé un peu', 1, 23, N'2006-05-21 13:15:53.060'),
(N'Le pif ça marche?', 1, 24, N'2000-05-21 13:15:53.060'),
(N'Oui tkt', 2, 24, N'2001-05-21 13:15:53.060'),
(N'Quels plats marocains recommandez-vous ?', 1, 25, N'2024-05-21 13:15:53.060'),
(N'Quel est votre fruit préféré ?', 1, 26, N'2024-05-21 13:15:53.060'),
(N'Quels sont les avantages du véganisme ?', 1, 27, N'2024-05-21 13:15:53.060'),
(N'Pourquoi ZA est-il votre préféré ?', 1, 28, N'2024-05-21 13:15:53.060'),
(N'Partagez vos tier lists ici !', 1, 29, N'2025-05-21 13:15:53.060'),
(N'Le POAT ?', 1, 30, N'2002-05-21 21:05:53.060'),
(N'Je prend ce manque de réponse en 20 ans pour un oui', 1, 30, N'2022-01-01 13:15:53.060');

INSERT INTO bluedit.dbo.opinion (answerId, [like], authorId) VALUES
(1, 0, 1),
(1, 1, 2),
(1, 1, 3),
(1, 1, 4),
(3, 0, 2),
(4, 1, 1);
