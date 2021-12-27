netcore
USE [master]
GO

CREATE DATABASE [.netcore]
GO

USE [.netcore]
GO

CREATE TABLE [dbo].[dbo.Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[Username] [nvarchar](256) NOT NULL,
	[Created] [datetime] NOT NULL,
	[Updated] [datetime] NOT NULL,
	[Deleted] [bit] NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE UNIQUE INDEX uidx_eid ON [.netcore].[dbo].[dbo.Users] (Email);
GO

--alter table [.netcore].[dbo].[dbo.Users] Drop Column Created;  
--alter table [.netcore].[dbo].[dbo.Users] ADD Created Datetime Not Null;

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'uspLogin')
BEGIN
	DROP PROCEDURE uspLogin
END
GO
CREATE PROCEDURE uspLogin 
	@Email nvarchar(256),
	@PasswordHash nvarchar(Max)
AS
BEGIN
	SELECT [Id]
      ,[Email]
      ,[PasswordHash]
      ,[Username]
      ,[Created]
      ,[Updated] FROM [.netcore].[dbo].[dbo.Users] 
	  WHERE [Email] = @Email AND [PasswordHash] = @PasswordHash;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'uspAddUser')
BEGIN
	DROP PROCEDURE uspAddUser
END
GO
CREATE PROCEDURE uspAddUser
	@Id uniqueidentifier,
	@Email nvarchar(256),
	@PasswordHash nvarchar(Max),
	@Username nvarchar(256),
	@Created datetime,
	@Updated datetime,
	@Deleted bit
AS
BEGIN
	INSERT INTO [.netcore].[dbo].[dbo.Users] 
	VALUES (@Id, @Email, @PasswordHash, @Username, @Created, @Updated, @Deleted);
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'uspRemoveUser')
BEGIN
	DROP PROCEDURE uspRemoveUser
END
GO
CREATE PROCEDURE uspRemoveUser
	@Id uniqueidentifier
AS
BEGIN
	DELETE FROM [.netcore].[dbo].[dbo.Users] WHERE Id = @Id;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'uspUpdateUser')
BEGIN
	DROP PROCEDURE uspUpdateUser
END
GO
CREATE PROCEDURE uspUpdateUser
	@Id uniqueidentifier,
	@Email nvarchar(256),
	@PasswordHash nvarchar(Max),
	@Username nvarchar(256),
	@Updated datetime
AS
BEGIN
	UPDATE [.netcore].[dbo].[dbo.Users]
	SET Email = @Email, 
	PasswordHash = @PasswordHash, 
	Username = @Username,
	Updated = @Updated
	WHERE Id = @Id;
END
GO
