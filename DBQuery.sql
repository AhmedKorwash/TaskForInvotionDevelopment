CREATE TABLE [dbo].[Student] (
    [Id]   NVARCHAR (200) NOT NULL,
    [Name] NVARCHAR (250) NOT NULL,
    [Age]  INT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Subject] (
    [SubjectId]   NVARCHAR (250) NOT NULL,
    [SubjectName] NVARCHAR (250) NOT NULL,
    PRIMARY KEY CLUSTERED ([SubjectId] ASC)
);

CREATE TABLE [dbo].[SubjectInroll] (
    [SubjectId] NVARCHAR (250) NOT NULL,
    [StudentId] NVARCHAR (200) NOT NULL,
    CONSTRAINT [Student_Subject_pk] PRIMARY KEY CLUSTERED ([SubjectId] ASC, [StudentId] ASC),
    CONSTRAINT [FK_Student] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Student] ([Id]),
    CONSTRAINT [FK_Subject] FOREIGN KEY ([SubjectId]) REFERENCES [dbo].[Subject] ([SubjectId]) ON DELETE CASCADE
);

