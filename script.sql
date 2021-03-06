USE [university]
GO
/****** Object:  Table [dbo].[courses]    Script Date: 6/13/2017 4:52:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[courses](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[number] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[students]    Script Date: 6/13/2017 4:52:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[students](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[enroll_date] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[students_courses]    Script Date: 6/13/2017 4:52:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[students_courses](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[student_id] [int] NULL,
	[course_id] [int] NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[courses] ON 

INSERT [dbo].[courses] ([id], [name], [number]) VALUES (11, N'Computer Science', N'')
SET IDENTITY_INSERT [dbo].[courses] OFF
