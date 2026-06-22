BEGIN TRANSACTION;

--------------------------------------------------
-- 1. AUTHORS
--------------------------------------------------

DECLARE @Tolstoy UNIQUEIDENTIFIER = NEWID();
DECLARE @Dostoevsky UNIQUEIDENTIFIER = NEWID();
DECLARE @Pushkin UNIQUEIDENTIFIER = NEWID();
DECLARE @Bulgakov UNIQUEIDENTIFIER = NEWID();
DECLARE @Chekhov UNIQUEIDENTIFIER = NEWID();
DECLARE @Turgenev UNIQUEIDENTIFIER = NEWID();
DECLARE @Gogol UNIQUEIDENTIFIER = NEWID();
DECLARE @Remarque UNIQUEIDENTIFIER = NEWID();
DECLARE @Orwell UNIQUEIDENTIFIER = NEWID();
DECLARE @Murakami UNIQUEIDENTIFIER = NEWID();

INSERT INTO Authors (Id, Name, Biography) VALUES
(@Tolstoy, N'Лев Толстой', N'Русский писатель, автор романа "Война и мир"'),
(@Dostoevsky, N'Фёдор Достоевский', N'Русский писатель, философ'),
(@Pushkin, N'Александр Пушкин', N'Русский поэт и прозаик'),
(@Bulgakov, N'Михаил Булгаков', N'Автор романа "Мастер и Маргарита"'),
(@Chekhov, N'Антон Чехов', N'Русский драматург и писатель'),
(@Turgenev, N'Иван Тургенев', N'Автор романа "Отцы и дети"'),
(@Gogol, N'Николай Гоголь', N'Автор произведения "Мёртвые души"'),
(@Remarque, N'Эрих Мария Ремарк', N'Немецкий писатель'),
(@Orwell, N'Джордж Оруэлл', N'Английский писатель и публицист'),
(@Murakami, N'Харуки Мураками', N'Современный японский писатель');

--------------------------------------------------
-- 2. EDITIONS
--------------------------------------------------

DECLARE @Classic UNIQUEIDENTIFIER = NEWID();
DECLARE @Foreign UNIQUEIDENTIFIER = NEWID();
DECLARE @School UNIQUEIDENTIFIER = NEWID();
DECLARE @Philosophy UNIQUEIDENTIFIER = NEWID();
DECLARE @Drama UNIQUEIDENTIFIER = NEWID();
DECLARE @XXCentury UNIQUEIDENTIFIER = NEWID();
DECLARE @Poetry UNIQUEIDENTIFIER = NEWID();
DECLARE @Dystopia UNIQUEIDENTIFIER = NEWID();
DECLARE @Modern UNIQUEIDENTIFIER = NEWID();
DECLARE @Gold UNIQUEIDENTIFIER = NEWID();

INSERT INTO Editions (Id, Name, Description) VALUES
(@Classic, N'Русская классика', N'Сборник произведений русских писателей XIX века'),
(@Foreign, N'Зарубежная литература', N'Лучшие произведения зарубежных авторов'),
(@School, N'Школьная программа 9 класс', N'Книги, входящие в школьную программу'),
(@Philosophy, N'Философские романы', N'Произведения с глубоким философским смыслом'),
(@Drama, N'Драматургия', N'Сборник пьес и театральных произведений'),
(@XXCentury, N'Романы XX века', N'Популярные романы двадцатого века'),
(@Poetry, N'Поэзия XIX века', N'Сборник стихотворений русских поэтов'),
(@Dystopia, N'Антиутопии', N'Книги жанра антиутопии'),
(@Modern, N'Современная проза', N'Современные авторы'),
(@Gold, N'Золотая коллекция', N'Лучшие произведения мировой литературы');

--------------------------------------------------
-- 3. BOOKS
--------------------------------------------------

INSERT INTO Books (Id, Name, PageCount, AuthorId) VALUES
(NEWID(), N'Война и мир', 1225, @Tolstoy),
(NEWID(), N'Преступление и наказание', 650, @Dostoevsky),
(NEWID(), N'Евгений Онегин', 300, @Pushkin),
(NEWID(), N'Мастер и Маргарита', 480, @Bulgakov),
(NEWID(), N'Вишнёвый сад', 250, @Chekhov),
(NEWID(), N'Отцы и дети', 400, @Turgenev),
(NEWID(), N'Мёртвые души', 350, @Gogol),
(NEWID(), N'Три товарища', 500, @Remarque),
(NEWID(), N'1984', 350, @Orwell),
(NEWID(), N'Норвежский лес', 420, @Murakami);

--------------------------------------------------
-- 4. MANY-TO-MANY (AuthorEdition)
--------------------------------------------------

INSERT INTO AuthorEdition (AuthorsId, EditionsId) VALUES
(@Tolstoy, @Classic),
(@Tolstoy, @School),
(@Tolstoy, @Gold),

(@Dostoevsky, @Classic),
(@Dostoevsky, @Philosophy),

(@Pushkin, @Classic),
(@Pushkin, @Poetry),

(@Bulgakov, @XXCentury),

(@Chekhov, @Drama),
(@Turgenev, @Classic),
(@Gogol, @Classic),

(@Remarque, @Foreign),
(@Orwell, @Dystopia),
(@Murakami, @Modern);

--------------------------------------------------

COMMIT;