USE [blogdb]
GO
/****** Object:  Table [dbo].[Blogs]    Script Date: 5/9/2024 10:07:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Blogs](
	[BlogID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Body] [nvarchar](max) NOT NULL,
	[PublishedDate] [datetime] NOT NULL,
	[UpvoteCount] [int] NOT NULL DEFAULT ((0)),
	[DownvoteCount] [int] NOT NULL DEFAULT ((0)),
	[CommentCount] [int] NOT NULL DEFAULT ((0)),
	[PictureColumn] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[BlogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 5/9/2024 10:07:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[CommentID] [int] IDENTITY(1,1) NOT NULL,
	[BlogID] [int] NULL,
	[UserID] [int] NULL,
	[Content] [nvarchar](max) NOT NULL,
	[PublishedDate] [datetime] NOT NULL,
	[UpvoteCount] [int] NOT NULL,
	[DownvoteCount] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Reactions]    Script Date: 5/9/2024 10:07:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reactions](
	[ReactionID] [int] NOT NULL,
	[ReactionType] [nvarchar](50) NOT NULL,
	[ReactionDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ReactionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/9/2024 10:07:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Role] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Blogs] ON 

INSERT [dbo].[Blogs] ([BlogID], [UserID], [Title], [Body], [PublishedDate], [UpvoteCount], [DownvoteCount], [CommentCount], [PictureColumn]) VALUES (1, 1, N'First Blog Title', N'This is the body of the first blog post.', CAST(N'2024-05-10 00:00:00.000' AS DateTime), 10, 5, 3, NULL)
INSERT [dbo].[Blogs] ([BlogID], [UserID], [Title], [Body], [PublishedDate], [UpvoteCount], [DownvoteCount], [CommentCount], [PictureColumn]) VALUES (2, 2, N'Second Blog Title', N'This is the body of the second blog post.', CAST(N'2024-05-11 00:00:00.000' AS DateTime), 8, 3, 2, NULL)
INSERT [dbo].[Blogs] ([BlogID], [UserID], [Title], [Body], [PublishedDate], [UpvoteCount], [DownvoteCount], [CommentCount], [PictureColumn]) VALUES (3, 3, N'Third Blog Title', N'This is the body of the third blog post.', CAST(N'2024-05-12 00:00:00.000' AS DateTime), 12, 7, 4, NULL)
INSERT [dbo].[Blogs] ([BlogID], [UserID], [Title], [Body], [PublishedDate], [UpvoteCount], [DownvoteCount], [CommentCount], [PictureColumn]) VALUES (4, 4, N'Fourth Blog Title', N'This is the body of the fourth blog post.', CAST(N'2024-05-13 00:00:00.000' AS DateTime), 15, 2, 6, NULL)
INSERT [dbo].[Blogs] ([BlogID], [UserID], [Title], [Body], [PublishedDate], [UpvoteCount], [DownvoteCount], [CommentCount], [PictureColumn]) VALUES (5, 5, N'Fifth Blog Title', N'This is the body of the fifth blog post.', CAST(N'2024-05-14 00:00:00.000' AS DateTime), 20, 10, 8, NULL)
INSERT [dbo].[Blogs] ([BlogID], [UserID], [Title], [Body], [PublishedDate], [UpvoteCount], [DownvoteCount], [CommentCount], [PictureColumn]) VALUES (6, NULL, N'Your Title Here', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed accumsan ligula nec nisl dapibus, eget bibendum nunc tristique. Mauris vestibulum, odio ut placerat dignissim, felis turpis consequat ligula, eget eleifend ligula mauris eget lacus. Integer convallis tempus enim vel consequat. Sed vitae odio nec ipsum fermentum accumsan. Nullam a magna vitae nulla ultrices vehicula. Ut scelerisque lectus auctor, cursus lacus sed, ullamcorper odio. Proin convallis, ligula sit amet vestibulum tempor, urna nisi rhoncus magna, ac eleifend velit ipsum vel sem. Vivamus id odio urna. Suspendisse potenti. Cras nec massa suscipit, varius sem vel, pharetra libero. Nam eget nisl lacus. Integer a vestibulum lorem. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. In non ultrices purus, a sagittis metus. Maecenas posuere urna id nibh elementum, vitae mollis augue feugiat.', CAST(N'2024-05-07 03:28:30.150' AS DateTime), 0, 0, 0, NULL)
SET IDENTITY_INSERT [dbo].[Blogs] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [Username], [Email], [Password], [Name], [Role]) VALUES (1, N'dsdsa', N'usamalaghari29@gmail.com', N'ds', N'dsds', NULL)
INSERT [dbo].[Users] ([UserID], [Username], [Email], [Password], [Name], [Role]) VALUES (2, N'usama', N'usamalaghari302@gmail.com', N'usama132', N'usama ali', NULL)
INSERT [dbo].[Users] ([UserID], [Username], [Email], [Password], [Name], [Role]) VALUES (3, N'usama', N'usamalaghari29@gmail.com', N'usama132', N'dsds', NULL)
INSERT [dbo].[Users] ([UserID], [Username], [Email], [Password], [Name], [Role]) VALUES (4, N'sasa', N'usamalaghari29@gmail.com', N'saa', N'asa', NULL)
INSERT [dbo].[Users] ([UserID], [Username], [Email], [Password], [Name], [Role]) VALUES (5, N'dasas', N'usamalaghari29@gmail.com', N'dasdas', N'asdasd', NULL)
INSERT [dbo].[Users] ([UserID], [Username], [Email], [Password], [Name], [Role]) VALUES (6, N'saas', N'ayaanali18@gmail.com', N'sasa', N'asas', NULL)
INSERT [dbo].[Users] ([UserID], [Username], [Email], [Password], [Name], [Role]) VALUES (7, N'dksajd', N'usamalaghari22@gmail.com', N'skajsk', N'dskj', NULL)
INSERT [dbo].[Users] ([UserID], [Username], [Email], [Password], [Name], [Role]) VALUES (8, N'fsds', N'usamalaghari29@gmail.com', N'dsdsdsds', N'dssd', NULL)
INSERT [dbo].[Users] ([UserID], [Username], [Email], [Password], [Name], [Role]) VALUES (9, N'sasa', N'ayaanali18@gmail.com', N'sasa', N'sasa', NULL)
INSERT [dbo].[Users] ([UserID], [Username], [Email], [Password], [Name], [Role]) VALUES (10, N'usama ali laghari', N'ali12@gmail.com', N'usama132', N'usama', NULL)
INSERT [dbo].[Users] ([UserID], [Username], [Email], [Password], [Name], [Role]) VALUES (11, N'test', N'testing@gmail.com', N'test', N'test', NULL)
INSERT [dbo].[Users] ([UserID], [Username], [Email], [Password], [Name], [Role]) VALUES (12, N'ali12', N'ali12@gmail.com', N'ali132', N'ali laghari', N'User')
INSERT [dbo].[Users] ([UserID], [Username], [Email], [Password], [Name], [Role]) VALUES (13, N'saas', N'ayaanali18@gmail.com', N'sasa', N'sas', N'Admin')
INSERT [dbo].[Users] ([UserID], [Username], [Email], [Password], [Name], [Role]) VALUES (14, N'dsds', N'usamalaghari29@gmail.com', N'dsds', N'dsds', N'Admin')
INSERT [dbo].[Users] ([UserID], [Username], [Email], [Password], [Name], [Role]) VALUES (15, N'admin testing', N'admin@gmail.com', N'admin', N'admin test', N'Admin')
INSERT [dbo].[Users] ([UserID], [Username], [Email], [Password], [Name], [Role]) VALUES (16, N'bloger', N'bloger@gmail.com', N'bloger', N'blgoer', N'User')
INSERT [dbo].[Users] ([UserID], [Username], [Email], [Password], [Name], [Role]) VALUES (17, N'dksdkjs', N'codebuk132@gmail.com', N'usajsai', N'dksjdkw', N'Admin')
SET IDENTITY_INSERT [dbo].[Users] OFF
ALTER TABLE [dbo].[Comments] ADD  DEFAULT ((0)) FOR [UpvoteCount]
GO
ALTER TABLE [dbo].[Comments] ADD  DEFAULT ((0)) FOR [DownvoteCount]
GO
ALTER TABLE [dbo].[Blogs]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD FOREIGN KEY([BlogID])
REFERENCES [dbo].[Blogs] ([BlogID])
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
