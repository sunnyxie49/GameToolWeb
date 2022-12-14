USE [master]
GO
/****** Object:  Database [GmTool]    Script Date: 2022-09-01 13:34:01 ******/
CREATE DATABASE [GmTool]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'gmtool', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.TEST\MSSQL\DATA\gmtool.Mdf' , SIZE = 14336KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'gmtool_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.TEST\MSSQL\DATA\gmtool_1.Ldf' , SIZE = 69760KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [GmTool] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GmTool].[dbo].[sp_fulltext_database] @action = 'disable'
end
GO
ALTER DATABASE [GmTool] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GmTool] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GmTool] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GmTool] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GmTool] SET ARITHABORT OFF 
GO
ALTER DATABASE [GmTool] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GmTool] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GmTool] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GmTool] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GmTool] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GmTool] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GmTool] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GmTool] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GmTool] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GmTool] SET  DISABLE_BROKER 
GO
ALTER DATABASE [GmTool] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GmTool] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GmTool] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GmTool] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GmTool] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GmTool] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GmTool] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GmTool] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [GmTool] SET  MULTI_USER 
GO
ALTER DATABASE [GmTool] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GmTool] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GmTool] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GmTool] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [GmTool] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'GmTool', N'ON'
GO
ALTER DATABASE [GmTool] SET QUERY_STORE = OFF
GO
USE [GmTool]
GO
/****** Object:  User [gmtool]    Script Date: 2022-09-01 13:34:01 ******/
CREATE USER [gmtool] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [gmtool]
GO
/****** Object:  Table [dbo].[tb_admin]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_admin](
	[sid] [int] IDENTITY(1,1) NOT NULL,
	[account] [varchar](20) NOT NULL,
	[password] [varchar](20) NOT NULL,
	[name] [varchar](20) NOT NULL,
	[team_id] [int] NOT NULL,
	[grade_id] [int] NOT NULL,
	[is_del] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_admin_authority]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_admin_authority](
	[sid] [int] IDENTITY(1,1) NOT NULL,
	[authority_code_id] [int] NOT NULL,
	[admin_id] [int] NOT NULL,
	[admin_grade_id] [int] NOT NULL,
	[authority_type] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_admin_authority_v3]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_admin_authority_v3](
	[sid] [int] IDENTITY(1,1) NOT NULL,
	[authority_code_id] [int] NOT NULL,
	[admin_id] [int] NOT NULL,
	[admin_grade_id] [int] NOT NULL,
	[authority_type] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_admin_grade]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_admin_grade](
	[sid] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_admin_login_log]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_admin_login_log](
	[sid] [bigint] IDENTITY(1,1) NOT NULL,
	[account_id] [int] NOT NULL,
	[ip] [varchar](20) NOT NULL,
	[login_time] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_admin_team]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_admin_team](
	[sid] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_authority_code]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_authority_code](
	[sid] [int] IDENTITY(1,1) NOT NULL,
	[code] [varchar](255) NOT NULL,
	[sort_a] [int] NOT NULL,
	[sort_b] [int] NOT NULL,
	[category] [varchar](255) NOT NULL,
	[info] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_authority_code_v3]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_authority_code_v3](
	[sid] [int] NOT NULL,
	[code] [varchar](255) NOT NULL,
	[sort_a] [int] NOT NULL,
	[sort_b] [int] NOT NULL,
	[category] [nvarchar](255) NOT NULL,
	[info] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_authority_page]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_authority_page](
	[sid] [int] NOT NULL,
	[category] [varchar](255) NOT NULL,
	[title] [varchar](255) NOT NULL,
	[url] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_event_060614]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_event_060614](
	[server] [varchar](20) NULL,
	[account_id] [int] NOT NULL,
	[account] [varchar](31) NOT NULL,
	[sid] [int] NULL,
	[name] [varchar](61) NOT NULL,
	[lv] [int] NOT NULL,
	[exp] [bigint] NOT NULL,
	[login_time] [datetime] NOT NULL,
	[logout_time] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_gmnick]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_gmnick](
	[sid] [int] NOT NULL,
	[gmnick] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_gmnick_authority]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_gmnick_authority](
	[admin_id] [int] NULL,
	[gmnick_id] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_gmtool_game_cheat_log]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_gmtool_game_cheat_log](
	[sid] [int] IDENTITY(1,1) NOT NULL,
	[log_date] [datetime] NOT NULL,
	[cheat_type] [varchar](20) NOT NULL,
	[admin_id] [int] NOT NULL,
	[admin_name] [varchar](20) NOT NULL,
	[server] [varchar](20) NOT NULL,
	[account_id] [int] NULL,
	[account] [varchar](50) NULL,
	[character_id] [int] NULL,
	[character_name] [varchar](50) NULL,
	[item_id] [bigint] NULL,
	[skill_id] [int] NULL,
	[summon_id] [int] NULL,
	[quest_id] [int] NULL,
	[guild_id] [int] NULL,
	[party_id] [int] NULL,
	[result_msg] [varchar](255) NOT NULL,
 CONSTRAINT [PK_tb_gmtool_game_cheat_log] PRIMARY KEY CLUSTERED 
(
	[sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_gmtool_server_list]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_gmtool_server_list](
	[name] [varchar](40) NOT NULL,
	[view_name] [varchar](40) NOT NULL,
	[server_ip] [varchar](15) NULL,
	[db_ip] [varchar](15) NULL,
	[server_type] [int] NULL,
	[resource_id] [int] NULL,
	[port] [smallint] NULL,
	[password] [varchar](50) NULL,
 CONSTRAINT [PK__tb_gmtool_server__7A9C383C] PRIMARY KEY CLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_rappelz_server]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_rappelz_server](
	[code] [varchar](50) NOT NULL,
	[idx] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[type] [varchar](50) NOT NULL,
	[open_date] [datetime] NOT NULL,
	[close_date] [datetime] NOT NULL,
	[game_server_ip] [varchar](50) NOT NULL,
	[game_port] [smallint] NOT NULL,
	[game_db_ip] [varchar](50) NOT NULL,
	[game_database] [varchar](50) NOT NULL,
	[log_db_ip] [varchar](50) NOT NULL,
	[log_table_name] [varchar](100) NOT NULL,
	[resource_id] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_temp_log_file]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_temp_log_file](
	[sid] [int] IDENTITY(1,1) NOT NULL,
	[source] [varchar](255) NULL,
	[target] [varchar](255) NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tb_admin] ON 

INSERT [dbo].[tb_admin] ([sid], [account], [password], [name], [team_id], [grade_id], [is_del]) VALUES (61, N'sunny', N'123', N'Sunny', 1, 1, 0)
SET IDENTITY_INSERT [dbo].[tb_admin] OFF
SET IDENTITY_INSERT [dbo].[tb_admin_authority_v3] ON 

INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (218, 1, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (219, 2, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (220, 3, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (221, 4, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (222, 54, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (223, 5, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (224, 6, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (225, 7, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (226, 8, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (227, 9, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (228, 10, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (229, 11, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (230, 12, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (231, 13, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (232, 14, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (233, 15, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (234, 16, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (235, 17, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (236, 59, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (237, 18, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (238, 19, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (239, 49, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (240, 51, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (241, 52, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (242, 20, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (243, 21, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (244, 22, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (245, 43, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (246, 44, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (247, 23, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (248, 24, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (249, 25, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (250, 26, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (251, 27, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (252, 28, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (253, 29, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (254, 30, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (255, 31, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (256, 32, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (257, 33, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (258, 34, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (259, 35, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (260, 36, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (261, 37, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (262, 38, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (263, 39, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (264, 40, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (265, 41, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (266, 42, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (267, 45, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (268, 55, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (269, 46, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (270, 47, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (271, 48, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (272, 53, 61, -1, 1)
INSERT [dbo].[tb_admin_authority_v3] ([sid], [authority_code_id], [admin_id], [admin_grade_id], [authority_type]) VALUES (273, 50, 61, -1, 1)
SET IDENTITY_INSERT [dbo].[tb_admin_authority_v3] OFF
SET IDENTITY_INSERT [dbo].[tb_admin_grade] ON 

INSERT [dbo].[tb_admin_grade] ([sid], [name]) VALUES (1, N'[超级管理员]')
INSERT [dbo].[tb_admin_grade] ([sid], [name]) VALUES (2, N'[高级管理员]')
INSERT [dbo].[tb_admin_grade] ([sid], [name]) VALUES (3, N'[中级管理员]')
INSERT [dbo].[tb_admin_grade] ([sid], [name]) VALUES (4, N'[低级管理员]')
INSERT [dbo].[tb_admin_grade] ([sid], [name]) VALUES (5, N'[无权限]')
SET IDENTITY_INSERT [dbo].[tb_admin_grade] OFF
SET IDENTITY_INSERT [dbo].[tb_admin_team] ON 

INSERT [dbo].[tb_admin_team] ([sid], [name]) VALUES (1, N'千梦科技')
SET IDENTITY_INSERT [dbo].[tb_admin_team] OFF
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (1, N'game/default.aspx', 1, 1, N'权限访问页面', N'头像浏览页面')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (2, N'game/character.aspx', 1, 2, N'权限访问页面', N'头像浏览页面（所有服务器）')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (3, N'game/guild.aspx', 1, 3, N'权限访问页面', N'公会页面')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (4, N'game/dungeon.aspx', 1, 4, N'权限访问页面', N'地下城页面')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (5, N'game/character_info.aspx', 1, 10, N'权限访问页面', N'角色信息')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (6, N'game/character_info_item.aspx', 1, 11, N'权限访问页面', N'角色道具信息')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (7, N'game/character_info_skill.aspx', 1, 12, N'权限访问页面', N'角色技能信息')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (8, N'game/character_info_summon.aspx', 1, 13, N'权限访问页面', N'角色召唤兽信息')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (9, N'game/character_info_quest.aspx', 1, 14, N'权限访问页面', N'角色任务信息')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (10, N'game/character_info_bank.aspx', 1, 15, N'权限访问页面', N'角色仓库信息')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (11, N'game/character_info_shop.aspx', 1, 16, N'权限访问页面', N'角色商店信息')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (12, N'game/edit_item.aspx', 1, 18, N'权限访问页面', N'修改道具信息')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (13, N'game/edit_skill.aspx', 1, 19, N'权限访问页面', N'修改技能信息')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (14, N'log/default.aspx', 1, 20, N'权限访问页面', N'游戏日志')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (15, N'log/game_auth.aspx', 1, 21, N'权限访问页面', N'认证服务器日志')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (16, N'log/admin_cheat_log.aspx', 1, 22, N'权限访问页面', N'更改游戏数据日志')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (17, N'notice/client_game.aspx', 1, 30, N'权限访问页面', N'在游戏中发布公告')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (18, N'statistics/game_uv.aspx', 1, 41, N'权限访问页面', N'统计_uv')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (19, N'statistics/game_cu.aspx', 1, 42, N'权限访问页面', N'统计_cu')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (20, N'admin/default.aspx', 1, 51, N'权限访问页面', N'管理员名单')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (21, N'admin/admin_add.aspx', 1, 52, N'权限访问页面', N'添加管理员')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (22, N'admin/admin_modify.aspx', 1, 53, N'权限访问页面', N'修改管理员')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (23, N'resource/default.aspx', 1, 60, N'权限访问页面', N'资源更新')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (24, N'gmtool_give_gm_authority', 2, 1, N'改变游戏数据', N'给与GM权限')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (25, N'gmtool_del_gm_authority', 2, 2, N'改变游戏数据', N'删除GM权限')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (26, N'gmtool_kick', 2, 3, N'改变游戏数据', N'强制下线')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (27, N'gmtool_insert_gold', 2, 4, N'改变游戏数据', N'给与金币')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (28, N'gmtool_taiming_all', 2, 5, N'改变游戏数据', N'驯服所有召唤兽')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (29, N'gmtool_warp', 2, 6, N'改变游戏数据', N'移动角色')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (30, N'gmtool_change_avatar_name', 2, 7, N'改变游戏数据', N'修改角色名称')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (31, N'gmtool_change_character_exp', 2, 8, N'改变游戏数据', N'修改角色经验')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (32, N'gmtool_change_character_jp', 2, 9, N'改变游戏数据', N'修改角色JP')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (33, N'gmtool_change_character_gold', 2, 10, N'改变游戏数据', N'修改角色金币')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (34, N'gmtool_change_character_lac', 2, 11, N'改变游戏数据', N'修改角色灵魂晶石')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (35, N'gmtool_change_character_stamina', 2, 12, N'改变游戏数据', N'修改角色耐久度')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (36, N'gmtool_restore_deleted_character', 2, 13, N'改变游戏数据', N'恢复删除的角色')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (37, N'gmtool_change_character_chat_block_time', 2, 14, N'改变游戏数据', N'改变聊天块时间')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (38, N'gmtool_change_character_ip', 2, 15, N'改变游戏数据', N'改变邪恶的观点')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (39, N'gmtool_change_character_cha', 2, 16, N'改变游戏数据', N'修改 CHA')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (40, N'gmtool_change_character_pkc', 2, 17, N'改变游戏数据', N'修改 PK 数')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (41, N'gmtool_change_character_dkc', 2, 18, N'改变游戏数据', N'修改 DK 数')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (42, N'gmtool_change_character_auto', 2, 19, N'改变游戏数据', N'设置自动')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (43, N'admin/authority_grade.aspx', 1, 54, N'权限访问页面', N'修改等级权限')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (44, N'admin/authority_admin.aspx', 1, 55, N'权限访问页面', N'修改管理员权限')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (45, N'gmtool_edit_item', 2, 20, N'改变游戏数据', N'修改道具信息')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (46, N'gmtool_insert_item', 2, 21, N'改变游戏数据', N'添加道具')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (47, N'gmtool_change_bank_gold', 2, 22, N'改变游戏数据', N'修改仓库金币')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (48, N'gmtool_edit_skill', 2, 23, N'改变游戏数据', N'修改技能')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (49, N'statistics/game_ru.aspx', 1, 43, N'权限访问页面', N'统计_ru')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (50, N'gmtool_set_security_number', 3, 1, N'改变游戏数据', N'Set Security Number')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (51, N'statistics/game_lv.aspx', 1, 44, N'权限访问页面', N'统计_lv')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (52, N'statistics/game_gold.aspx', 1, 45, N'权限访问页面', N'统计_金币')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (53, N'gmtool_modify_guild_info', 2, 24, N'改变游戏数据', N'更改公会信息')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (54, N'game/summon.aspx', 1, 5, N'权限访问页面', N'召唤兽页面')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (55, N'gmtool_change_character_huntaholic_point', 2, 20, N'改变游戏数据', N'修改角色熊之路点数')
INSERT [dbo].[tb_authority_code_v3] ([sid], [code], [sort_a], [sort_b], [category], [info]) VALUES (59, N'notice/all_buff.aspx', 1, 31, N'游戏状态', N'游戏状态')
INSERT [dbo].[tb_gmtool_server_list] ([name], [view_name], [server_ip], [db_ip], [server_type], [resource_id], [port], [password]) VALUES (N'Devel', N'Devel', N'192.168.1.6', N'192.168.1.6', 0, 0, 44, N'znzlsglend12-')
INSERT [dbo].[tb_gmtool_server_list] ([name], [view_name], [server_ip], [db_ip], [server_type], [resource_id], [port], [password]) VALUES (N'QA', N'QA', N'192.168.1.6', N'192.168.1.6', 1, 1, 445, N'znzlsglend12-')
INSERT [dbo].[tb_gmtool_server_list] ([name], [view_name], [server_ip], [db_ip], [server_type], [resource_id], [port], [password]) VALUES (N'RedemptionGS', N'Rappelz_Dev', N'192.168.1.6', N'192.168.1.6', 3, 3, 9881, N'znzlsglend12-')
INSERT [dbo].[tb_gmtool_server_list] ([name], [view_name], [server_ip], [db_ip], [server_type], [resource_id], [port], [password]) VALUES (N'RedemptionGS2', N'测试', N'192.168.1.6', N'192.168.1.6', 2, 2, 5001, N'znzlsglend12-')
INSERT [dbo].[tb_rappelz_server] ([code], [idx], [name], [type], [open_date], [close_date], [game_server_ip], [game_port], [game_db_ip], [game_database], [log_db_ip], [log_table_name], [resource_id]) VALUES (N'GS01', 1, N'正式', N'Service', CAST(N'2010-06-14T00:00:00.000' AS DateTime), CAST(N'9999-12-13T00:00:00.000' AS DateTime), N'192.168.1.6', 4452, N'192.168.1.6', N'telecaster_S952', N'192.168.1.6', N'Log_#@server@#_#@month@#.dbo.tb_log_#@server@#_#@month@#', 3)
INSERT [dbo].[tb_rappelz_server] ([code], [idx], [name], [type], [open_date], [close_date], [game_server_ip], [game_port], [game_db_ip], [game_database], [log_db_ip], [log_table_name], [resource_id]) VALUES (N'GS02', 2, N'测试', N'Test', CAST(N'2010-06-14T00:00:00.000' AS DateTime), CAST(N'9999-12-13T00:00:00.000' AS DateTime), N'192.168.1.6', 4453, N'192.168.1.6', N'telecaster_S952', N'192.168.1.6', N'Log_#@server@#_#@month@#.dbo.tb_log_#@server@#_#@month@#', 2)
INSERT [dbo].[tb_rappelz_server] ([code], [idx], [name], [type], [open_date], [close_date], [game_server_ip], [game_port], [game_db_ip], [game_database], [log_db_ip], [log_table_name], [resource_id]) VALUES (N'GS03', 3, N'QA', N'QA', CAST(N'2010-06-14T00:00:00.000' AS DateTime), CAST(N'9999-12-13T00:00:00.000' AS DateTime), N'192.168.1.6', 4500, N'192.168.1.6', N'telecaster_S952', N'192.168.1.6', N'Log_#@server@#_#@month@#.dbo.tb_log_#@server@#_#@month@#', 1)
INSERT [dbo].[tb_rappelz_server] ([code], [idx], [name], [type], [open_date], [close_date], [game_server_ip], [game_port], [game_db_ip], [game_database], [log_db_ip], [log_table_name], [resource_id]) VALUES (N'GS04', 4, N'Devel', N'Devel', CAST(N'2010-06-14T00:00:00.000' AS DateTime), CAST(N'9999-12-13T00:00:00.000' AS DateTime), N'192.168.1.6', 4500, N'192.168.1.6', N'telecaster_S952', N'192.168.1.6', N'Log_#@server@#_#@month@#.dbo.tb_log_#@server@#_#@month@#', 0)
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__tb_admin__08EA5793]    Script Date: 2022-09-01 13:34:01 ******/
ALTER TABLE [dbo].[tb_admin] ADD UNIQUE NONCLUSTERED 
(
	[account] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__tb_authority_cod__1920BF5C]    Script Date: 2022-09-01 13:34:01 ******/
ALTER TABLE [dbo].[tb_authority_code] ADD UNIQUE NONCLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__tb_authority_cod__1BFD2C07]    Script Date: 2022-09-01 13:34:01 ******/
ALTER TABLE [dbo].[tb_authority_code_v3] ADD UNIQUE NONCLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__tb_authority_pag__1ED998B2]    Script Date: 2022-09-01 13:34:01 ******/
ALTER TABLE [dbo].[tb_authority_page] ADD UNIQUE NONCLUSTERED 
(
	[url] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tb_admin] ADD  CONSTRAINT [DF__tb_admin__team_i__2F10007B]  DEFAULT ((0)) FOR [team_id]
GO
ALTER TABLE [dbo].[tb_admin] ADD  CONSTRAINT [DF__tb_admin__grade___300424B4]  DEFAULT ((0)) FOR [grade_id]
GO
ALTER TABLE [dbo].[tb_admin] ADD  CONSTRAINT [DF__tb_admin__is_del__2180FB33]  DEFAULT ((0)) FOR [is_del]
GO
ALTER TABLE [dbo].[tb_admin_authority] ADD  CONSTRAINT [DF__tb_admin___admin__48CFD27E]  DEFAULT ((-1)) FOR [admin_id]
GO
ALTER TABLE [dbo].[tb_admin_authority] ADD  CONSTRAINT [DF__tb_admin___admin__49C3F6B7]  DEFAULT ((-1)) FOR [admin_grade_id]
GO
ALTER TABLE [dbo].[tb_admin_authority] ADD  CONSTRAINT [DF__tb_admin___autho__4AB81AF0]  DEFAULT ((-1)) FOR [authority_type]
GO
ALTER TABLE [dbo].[tb_admin_authority_v3] ADD  CONSTRAINT [DF__tb_admin___admin__19DFD96B]  DEFAULT ((-1)) FOR [admin_id]
GO
ALTER TABLE [dbo].[tb_admin_authority_v3] ADD  CONSTRAINT [DF__tb_admin___admin__1AD3FDA4]  DEFAULT ((-1)) FOR [admin_grade_id]
GO
ALTER TABLE [dbo].[tb_admin_authority_v3] ADD  CONSTRAINT [DF__tb_admin___autho__1BC821DD]  DEFAULT ((-1)) FOR [authority_type]
GO
/****** Object:  StoredProcedure [dbo].[gmtool_authority_check]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[gmtool_authority_check]
	@admin_id int,
	@code varchar(255)
as
	select IsNull(max(authority_type), 0) authority_type from tb_admin_authority where authority_code_id in (select sid from tb_authority_code where code = @code)
	and (admin_id = @admin_id or admin_grade_id = (select grade_id from tb_admin where sid = @admin_id))
GO
/****** Object:  StoredProcedure [dbo].[gmtool_get_server_info]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[gmtool_get_server_info]
	@name varchar(40)
as
	select * from tb_gmtool_server_list 
	where [name] = @name
	order by server_type, [name]
GO
/****** Object:  StoredProcedure [dbo].[gmtool_get_server_list]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   procedure [dbo].[gmtool_get_server_list]
as
	select * from tb_gmtool_server_list 
	where view_name  NOT like '%X%'
	order by server_type, [name]
GO
/****** Object:  StoredProcedure [dbo].[gmtool_v2_write_cheat_log]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   procedure [dbo].[gmtool_v2_write_cheat_log]
	@cheat_type varchar(20),
	@admin_id int,
	@admin_name varchar(20),
	@server varchar(20),
	@account_id int,
	@account varchar(50),
	@character_id int,
	@character_name varchar(50),
	@item_id bigint,
	@skill_id int,
	@summon_id int,
	@quest_id int,
	@guild_id int,
	@party_id int,
	@result_msg varchar(255)
as
	declare @sid int
	declare @row int
	declare @error int
	INSERT INTO tb_gmtool_game_cheat_log
		(log_date, cheat_type, admin_id, admin_name, server, account_id, account, character_id, character_name, item_id, skill_id, summon_id, quest_id, guild_id, party_id, result_msg)
	VALUES
		(GetDate(), @cheat_type, @admin_id, @admin_name, @server, @account_id, @account,  @character_id, @character_name, @item_id, @skill_id, @summon_id, @quest_id, @guild_id, @party_id, @result_msg)
	return @@ERROR
GO
/****** Object:  StoredProcedure [dbo].[sp_admin_authority_total_list]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   procedure [dbo].[sp_admin_authority_total_list]
	@admin_id int,
	@admin_grade_id int
as

select c.*, d.grade_authority_id, d.grade_authority_type from 
(
	select
		a.*,
		IsNull(b.sid, -1) as authority_id,
		IsNull(b.authority_type, -1) as authority_type	
	from 
		tb_authority_code a 
	left join 
		(select * from tb_admin_authority where admin_id = @admin_id) as b
	on a.sid =  b.authority_code_id 
) as c
left join
(
		select
		a.*,
		IsNull(b.sid, -1) as grade_authority_id,
		IsNull(b.admin_grade_id, -1) as admin_grade_id,
		IsNull(b.authority_type, -1) as grade_authority_type	
	from 
		tb_authority_code a 
	left join 
		(select * from tb_admin_authority where admin_grade_id = @admin_grade_id) as b
	on a.sid =  b.authority_code_id 
) as d
on c.sid = d.sid
order by c.sort_a, c.sort_b
GO
/****** Object:  StoredProcedure [dbo].[sp_admin_authority_total_list_v3]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE      procedure [dbo].[sp_admin_authority_total_list_v3]
	@admin_id int,
	@admin_grade_id int
as

select c.*, IsNull(d.grade_authority_id, -1) as grade_authority_id, IsNull(d.grade_authority_type, -1) as grade_authority_type from 
(
	select
		a.*,
		IsNull(b.sid, -1) as authority_id,
		IsNull(b.authority_type, -1) as authority_type	
	from 
		tb_authority_code_v3 a 
	left join 
		(select * from tb_admin_authority_v3 where admin_id = @admin_id) as b
	on a.sid =  b.authority_code_id 
) as c
left join
(
		select
		a.*,
		IsNull(b.sid, -1) as grade_authority_id,
		IsNull(b.admin_grade_id, -1) as admin_grade_id,
		IsNull(b.authority_type, -1) as grade_authority_type	
	from 
		tb_authority_code_v3 a 
	left join 
		(select * from tb_admin_authority_v3 where admin_grade_id = @admin_grade_id) as b
	on a.sid =  b.authority_code_id 
) as d
on c.sid = d.sid
order by c.sort_a, c.sort_b
GO
/****** Object:  StoredProcedure [dbo].[sp_admin_grade_del]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_admin_grade_del]
	@sid int
as
	delete from tb_admin_grade where sid = @sid
GO
/****** Object:  StoredProcedure [dbo].[sp_admin_grade_insert]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_admin_grade_insert]
	@name varchar(20)
as
	insert into tb_admin_grade (name) values (@name);
GO
/****** Object:  StoredProcedure [dbo].[sp_admin_grade_update]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_admin_grade_update]
	@sid int,
	@name varchar(20)
as
	update 
		tb_admin_grade 
	set 
		name = @name
	where 
		sid = @sid
GO
/****** Object:  StoredProcedure [dbo].[sp_admin_insert]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_admin_insert]
	@account varchar(20),
	@password varchar(20),
	@name varchar(20),
	@team_id int,
	@grade_id int
as
	declare @sid int
	set @sid = 0;
	insert into tb_admin (account, password, name, team_id, grade_id) values (@account, @password, @name, @team_id, @grade_id);
	set @sid = @@IDENTITY;
	select @sid as sid;
GO
/****** Object:  StoredProcedure [dbo].[sp_admin_login_log]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_admin_login_log]
	@account_id int,
	@ip varchar(20)
as
	insert into tb_admin_login_log (account_id, ip, login_time) values (@account_id, @ip, GetDate());
GO
/****** Object:  StoredProcedure [dbo].[sp_admin_password_change]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_admin_password_change]
	@sid int,
	@password varchar(20)
as
	update tb_admin
	set
		password = @password
	where
		sid = @sid
GO
/****** Object:  StoredProcedure [dbo].[sp_admin_team_del]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_admin_team_del]
	@sid int
as
	delete from tb_admin_team where sid = @sid
GO
/****** Object:  StoredProcedure [dbo].[sp_admin_team_insert]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_admin_team_insert]
	@name varchar(20)
as
	insert into tb_admin_team (name) values (@name);
GO
/****** Object:  StoredProcedure [dbo].[sp_admin_team_update]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_admin_team_update]
	@sid int,
	@name varchar(20)
as
	update 
		tb_admin_team 
	set 
		name = @name
	where 
		sid = @sid
GO
/****** Object:  StoredProcedure [dbo].[sp_admin_update]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  procedure [dbo].[sp_admin_update]
	@sid int,
	@name varchar(20),
	@team_id int,
	@grade_id int
as
	update tb_admin
	set
		[name] = @name,
		team_id = @team_id,
		grade_id = @grade_id
	where
		sid = @sid
GO
/****** Object:  StoredProcedure [dbo].[sp_gmnick]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_gmnick]
as
	select * from tb_gmnick
GO
/****** Object:  StoredProcedure [dbo].[sp_gmnick_authority]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_gmnick_authority]
	@admin_id int
as
	select * from tb_gmnick where sid in (select gmnick_id from tb_gmnick_authority where admin_id = @admin_id)
GO
/****** Object:  StoredProcedure [dbo].[temp_log]    Script Date: 2022-09-01 13:34:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[temp_log]
as
	declare @source varchar(255)
	select top 1 @source = source from tb_temp_log_file where target = 'tb_log_game001_200605' order by sid 
	delete from tb_temp_log_file where source = @source
	select @source
GO
USE [master]
GO
ALTER DATABASE [GmTool] SET  READ_WRITE 
GO
