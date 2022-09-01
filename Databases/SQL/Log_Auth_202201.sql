USE [master]
GO
/****** Object:  Database [Log_Auth_201901]    Script Date: 2022-09-01 13:33:29 ******/
CREATE DATABASE [Log_Auth_201901]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Log_Auth_2019', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.TEST\MSSQL\DATA\Log_Auth_2019.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Log_Auth_2019_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.TEST\MSSQL\DATA\Log_Auth_2019_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Log_Auth_201901] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Log_Auth_201901].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Log_Auth_201901] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Log_Auth_201901] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Log_Auth_201901] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Log_Auth_201901] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Log_Auth_201901] SET ARITHABORT OFF 
GO
ALTER DATABASE [Log_Auth_201901] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Log_Auth_201901] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Log_Auth_201901] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Log_Auth_201901] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Log_Auth_201901] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Log_Auth_201901] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Log_Auth_201901] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Log_Auth_201901] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Log_Auth_201901] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Log_Auth_201901] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Log_Auth_201901] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Log_Auth_201901] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Log_Auth_201901] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Log_Auth_201901] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Log_Auth_201901] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Log_Auth_201901] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Log_Auth_201901] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Log_Auth_201901] SET RECOVERY FULL 
GO
ALTER DATABASE [Log_Auth_201901] SET  MULTI_USER 
GO
ALTER DATABASE [Log_Auth_201901] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Log_Auth_201901] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Log_Auth_201901] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Log_Auth_201901] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Log_Auth_201901] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Log_Auth_201901', N'ON'
GO
ALTER DATABASE [Log_Auth_201901] SET QUERY_STORE = OFF
GO
USE [Log_Auth_201901]
GO
/****** Object:  Table [dbo].[tb_log_Auth_201901]    Script Date: 2022-09-01 13:33:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_log_Auth_201901](
	[log_date] [datetime] NOT NULL,
	[temp] [int] NOT NULL,
	[log_id] [int] NOT NULL,
	[n1] [int] NOT NULL,
	[n2] [int] NOT NULL,
	[n3] [int] NOT NULL,
	[n4] [int] NOT NULL,
	[n5] [int] NOT NULL,
	[n6] [int] NOT NULL,
	[n7] [int] NOT NULL,
	[n8] [int] NOT NULL,
	[n9] [int] NOT NULL,
	[n10] [int] NOT NULL,
	[n11] [bigint] NOT NULL,
	[s1] [varchar](255) NULL,
	[s2] [varchar](255) NULL,
	[s3] [varchar](255) NULL,
	[s4] [varchar](255) NULL
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [Log_Auth_201901] SET  READ_WRITE 
GO
