CREATE TABLE [Profile] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255),
  [Active] bit,
  [CreationDate] datetime,
  [UpdateDate] datetime,
  [UserCreation] int,
  [UserUpdate] int,
  [IdHostCreation] nvarchar(255),
  [IdHostUpdate] nvarchar(255)
)
GO

CREATE TABLE [AccountUser] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [IdProfile] int,
  [Username] nvarchar(255),
  [Password] nvarchar(255),
  [Active] bit,
  [CreationDate] datetime,
  [UpdateDate] datetime,
  [UserCreation] int,
  [UserUpdate] int,
  [IdHostCreation] nvarchar(255),
  [IdHostUpdate] nvarchar(255)
)
GO

CREATE TABLE [Patients] (
  [AccountNum] int PRIMARY KEY IDENTITY(1, 1),
  [FullName] nvarchar(255),
  [Birthdate] datetime,
  [Gender] nvarchar(255),
  [DPI] nvarchar(255),
  [Active] bit,
  [CreationDate] datetime,
  [UpdateDate] datetime,
  [UserCreation] int,
  [UserUpdate] int,
  [IdHostCreation] nvarchar(255),
  [IdHostUpdate] nvarchar(255),
  [IdAccountUser] int
)
GO

CREATE TABLE [Medic] (
  [AccountNum] int PRIMARY KEY IDENTITY(1, 1),
  [FullName] nvarchar(255),
  [Birthdate] datetime,
  [Gender] nvarchar(255),
  [DPI] nvarchar(255),
  [RegistrationNumber] int,
  [Specialty] nvarchar(255),
  [Active] bit,
  [CreationDate] datetime,
  [UpdateDate] datetime,
  [UserCreation] int,
  [UserUpdate] int,
  [IdHostCreation] nvarchar(255),
  [IdHostUpdate] nvarchar(255),
  [IdAccountUser] int
)
GO

CREATE TABLE [Appointments_medical] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [IdPatient] int,
  [IdMedic] int,
  [Scheduleddate] date,
  [Scheduledhour] nvarchar(255),
  [Scheduledhourend] nvarchar(255),
  [RequestDate] datetime,
  [Status] nvarchar(255),
  [Comments] nvarchar(255),
  [Medicalservice] nvarchar(255),
  [Address] nvarchar(255),
  [Active] bit,
  [CreationDate] datetime,
  [UpdateDate] datetime,
  [UserCreation] int,
  [UserUpdate] int,
  [IdHostCreation] nvarchar(255),
  [IdHostUpdate] nvarchar(255)
)
GO

CREATE TABLE [Appointments_attached] (
  [id] int PRIMARY KEY,
  [IdApp_medical] int,
  [FileName] nvarchar(255),
  [Active] bit,
  [CreationDate] datetime,
  [UpdateDate] datetime,
  [UserCreation] int,
  [UserUpdate] int,
  [IdHostCreation] nvarchar(255),
  [IdHostUpdate] nvarchar(255)
)
GO

ALTER TABLE [AccountUser] ADD FOREIGN KEY ([IdProfile]) REFERENCES [Profile] ([Id])
GO

ALTER TABLE [Patients] ADD FOREIGN KEY ([IdAccountUser]) REFERENCES [AccountUser] ([Id])
GO

ALTER TABLE [Medic] ADD FOREIGN KEY ([IdAccountUser]) REFERENCES [AccountUser] ([Id])
GO

ALTER TABLE [Appointments_medical] ADD FOREIGN KEY ([IdMedic]) REFERENCES [Medic] ([AccountNum])
GO

ALTER TABLE [Appointments_medical] ADD FOREIGN KEY ([IdPatient]) REFERENCES [Patients] ([AccountNum])
GO

ALTER TABLE [Appointments_attached] ADD FOREIGN KEY ([IdApp_medical]) REFERENCES [Appointments_medical] ([Id])
GO
