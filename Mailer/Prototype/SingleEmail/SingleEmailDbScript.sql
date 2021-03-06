USE [Mailer]
GO
/****** Object:  User [test]    Script Date: 08/10/2017 15:24:04 ******/
CREATE USER [test] FOR LOGIN [test] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Schema [Mailer]    Script Date: 08/10/2017 15:24:04 ******/
CREATE SCHEMA [Mailer]
GO
/****** Object:  Table [Mailer].[EmailMessage]    Script Date: 08/10/2017 15:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Mailer].[EmailMessage](
	[EmailMessageId] [bigint] IDENTITY(1,1) NOT NULL,
	[FromAddress] [varchar](100) NOT NULL,
	[FromPerson] [varchar](100) NOT NULL,
	[SubjectTemplate] [nvarchar](100) NOT NULL,
	[BodyTemplate] [nvarchar](max) NOT NULL,
	[Host] [nvarchar](300) NOT NULL,
	[Port] [int] NOT NULL,
 CONSTRAINT [PK_EmailMessage_1] PRIMARY KEY CLUSTERED 
(
	[EmailMessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Mailer].[EmailQueue]    Script Date: 08/10/2017 15:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Mailer].[EmailQueue](
	[EmailQueueId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmailMessageId] [bigint] NOT NULL,
	[EmailStatus] [tinyint] NOT NULL,
	[EmailType] [int] NOT NULL,
	[TriesLeft] [tinyint] NOT NULL,
	[LastTryDateUtc] [datetime] NULL,
	[LockedDateUtc] [datetime] NULL,
	[AvailableToSendFromUtc] [datetime] NULL,
	[SendDateUtc] [datetime] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[ToEmailAddress] [varchar](100) NOT NULL,
	[ToPerson] [varchar](100) NOT NULL,
 CONSTRAINT [PK_EmailQueue] PRIMARY KEY CLUSTERED 
(
	[EmailQueueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Mailer].[EmailReplacement]    Script Date: 08/10/2017 15:24:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Mailer].[EmailReplacement](
	[EmailReplacementId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmailQueueId] [bigint] NOT NULL,
	[Token] [varchar](50) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_EmailReplacement] PRIMARY KEY CLUSTERED 
(
	[EmailReplacementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [Mailer].[EmailQueue]  WITH CHECK ADD  CONSTRAINT [FK_EmailQueue_EmailMessage] FOREIGN KEY([EmailMessageId])
REFERENCES [Mailer].[EmailMessage] ([EmailMessageId])
GO
ALTER TABLE [Mailer].[EmailQueue] CHECK CONSTRAINT [FK_EmailQueue_EmailMessage]
GO
ALTER TABLE [Mailer].[EmailReplacement]  WITH CHECK ADD  CONSTRAINT [FK_EmailReplacement_EmailQueue] FOREIGN KEY([EmailQueueId])
REFERENCES [Mailer].[EmailQueue] ([EmailQueueId])
GO
ALTER TABLE [Mailer].[EmailReplacement] CHECK CONSTRAINT [FK_EmailReplacement_EmailQueue]
GO
