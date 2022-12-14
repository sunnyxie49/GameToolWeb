USE [master]
GO
/****** Object:  Database [Rappelz_game_log]    Script Date: 2022-09-01 13:34:35 ******/
CREATE DATABASE [Rappelz_game_log]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Rappelz_game_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.TEST\MSSQL\DATA\Rappelz_game_log.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Rappelz_game_log_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.TEST\MSSQL\DATA\Rappelz_game_log_log.ldf' , SIZE = 3456KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Rappelz_game_log] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Rappelz_game_log].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Rappelz_game_log] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Rappelz_game_log] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Rappelz_game_log] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Rappelz_game_log] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Rappelz_game_log] SET ARITHABORT OFF 
GO
ALTER DATABASE [Rappelz_game_log] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Rappelz_game_log] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Rappelz_game_log] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Rappelz_game_log] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Rappelz_game_log] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Rappelz_game_log] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Rappelz_game_log] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Rappelz_game_log] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Rappelz_game_log] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Rappelz_game_log] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Rappelz_game_log] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Rappelz_game_log] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Rappelz_game_log] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Rappelz_game_log] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Rappelz_game_log] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Rappelz_game_log] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Rappelz_game_log] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Rappelz_game_log] SET RECOVERY FULL 
GO
ALTER DATABASE [Rappelz_game_log] SET  MULTI_USER 
GO
ALTER DATABASE [Rappelz_game_log] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Rappelz_game_log] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Rappelz_game_log] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Rappelz_game_log] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Rappelz_game_log] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Rappelz_game_log', N'ON'
GO
ALTER DATABASE [Rappelz_game_log] SET QUERY_STORE = OFF
GO
USE [Rappelz_game_log]
GO
/****** Object:  Table [dbo].[tb_cash_log]    Script Date: 2022-09-01 13:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_cash_log](
	[dt] [varchar](10) NULL,
	[clientUserNumber] [int] NULL,
	[cash] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_character_info]    Script Date: 2022-09-01 13:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_character_info](
	[server] [varchar](10) NOT NULL,
	[sid] [int] NOT NULL,
	[account_id] [int] NOT NULL,
	[account] [varchar](31) NOT NULL,
	[lv] [int] NOT NULL,
	[job] [int] NOT NULL,
	[jlv] [int] NOT NULL,
	[create_time] [datetime] NOT NULL,
	[delete_time] [datetime] NULL,
	[login_time] [datetime] NULL,
	[play_time] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_game_ru]    Script Date: 2022-09-01 13:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_game_ru](
	[create_time] [varchar](10) NULL,
	[cnt] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_log_Auth]    Script Date: 2022-09-01 13:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_log_Auth](
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
/****** Object:  Table [dbo].[tb_log_auth_connect]    Script Date: 2022-09-01 13:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_log_auth_connect](
	[y] [int] NOT NULL,
	[m] [int] NOT NULL,
	[d] [int] NOT NULL,
	[cnt] [int] NOT NULL,
	[cnt_sum] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_log_game]    Script Date: 2022-09-01 13:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_log_game](
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
/****** Object:  Table [dbo].[tb_log_game_connect]    Script Date: 2022-09-01 13:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_log_game_connect](
	[log_date] [datetime] NOT NULL,
	[server] [varchar](100) NOT NULL,
	[cnt] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_log_gold]    Script Date: 2022-09-01 13:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_log_gold](
	[log_date] [datetime] NOT NULL,
	[server] [varchar](50) NOT NULL,
	[gold] [bigint] NOT NULL,
	[bank_gold] [bigint] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_log_playpoint]    Script Date: 2022-09-01 13:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_log_playpoint](
	[log_date] [datetime] NOT NULL,
	[account_id] [int] NOT NULL,
	[play_time] [int] NOT NULL,
	[pp] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_log_playpoint_Error]    Script Date: 2022-09-01 13:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_log_playpoint_Error](
	[log_date] [datetime] NOT NULL,
	[account_id] [int] NOT NULL,
	[play_time] [int] NOT NULL,
	[pp] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_shop_log]    Script Date: 2022-09-01 13:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_shop_log](
	[buy_date] [datetime] NULL,
	[server_name] [varchar](50) NULL,
	[account_id] [int] NULL,
	[item_code] [int] NULL,
	[item_name] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[Bulk insert_ItemLog_txt]    Script Date: 2022-09-01 13:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE       procedure [dbo].[Bulk insert_ItemLog_txt]
	@DBName varchar(13)
	,@hour varchar(13)
	,@date varchar(13)
as
--declare @Day varchar(13)
Declare @YM varchar(6)
Declare @MD varchar(13)
--Declare @Day varchar(13)
Declare @SQL varchar(3000)



-- Select @Day=  Convert(char(13),dateadd(Hour, -1, GetDate()),20)
--set @Day =(@date+' '+@hour)
Select @MD = Convert(varchar(13), GetDate(), 120)

IF Right(@MD, 5) = '01 00'
   Begin
	Select @YM = Convert(char(6), DateAdd(mm, -1, GetDate()), 112)
   End
Else
   Begin
	Select @YM = Convert(char(6), GetDate(), 112)
   End

Begin


    Set @SQL = ' Bulk insert Log_'+@DBName+'_'+@YM+'.dbo.tb_log_'+@DBName+'_'+@YM+' from 
	    ''C:\Rappelz\GameEngine\log_bin\Log\'+@DBName+'_'+@date+' '+@hour+'.txt '' with (maxerrors = 5000000,TABLOCK,KEEPNULLS,codepage=936)
 '

Exec (@SQL)
end


GO
/****** Object:  StoredProcedure [dbo].[gmtool_auth_login_stastics]    Script Date: 2022-09-01 13:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE       procedure [dbo].[gmtool_auth_login_stastics]
as

Declare @YM varchar(6)
Declare @MD varchar(10)
Declare @SQL varchar(1000)

Select @MD = Convert(varchar(10), GetDate(), 120)

IF Right(@MD, 2) = '01'
   Begin
	Select @YM = Convert(char(6), DateAdd(mm, -1, GetDate()), 112)
   End
Else
   Begin
	Select @YM = Convert(char(6), GetDate(), 112)
   End

Begin

   Set @SQL = ' insert into tb_log_auth_connect 
select Y, M, D, count(*) as cnt, sum(cnt) as cnt_sum from 
(
select  
year(log_date) as Y, month(log_date) as M , day(log_date) as D, s1, count(*) cnt
from Log_Auth_'+@YM+' .dbo.tb_log_auth_'+@YM+' with (NOLOCK)
where log_id = 1001
and year(log_date) = year(GetDate()-1)
and month(log_date) = month(GetDate()-1)
and day(log_date) = day(GetDate()-1)
group by year(log_date), month(log_date), day(log_date), s1
) as a
group by Y, M, D
order by Y, M, D  '

Exec (@SQL)
END





GO
/****** Object:  StoredProcedure [dbo].[gmtool_Create_Auth]    Script Date: 2022-09-01 13:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Batch submitted through debugger: SQLQuery33.sql|7|0|C:\Users\root\AppData\Local\Temp\2\~vsF7BB.sql




CREATE PROCEDURE [dbo].[gmtool_Create_Auth]
	
AS

Declare @YM varchar(6)
Declare @SQL Varchar(4000)

Select @YM = Convert(char(6), DateAdd(mm, 0, GetDate()), 112)

Begin

   Set @SQL = ' 
Use Log_auth_'+@YM+'

CREATE TABLE [dbo].[tb_log_Auth_'+@YM+'] (
	[log_date] [datetime] NOT NULL ,
	[temp] [int] NOT NULL ,
	[log_id] [int] NOT NULL ,
	[n1] [int] NOT NULL ,
	[n2] [int] NOT NULL ,
	[n3] [int] NOT NULL ,
	[n4] [int] NOT NULL ,
	[n5] [int] NOT NULL ,
	[n6] [int] NOT NULL ,
	[n7] [int] NOT NULL ,
	[n8] [int] NOT NULL ,
	[n9] [int] NOT NULL ,
	[n10] [int] NOT NULL ,
	[n11] [bigint] NOT NULL ,
	[s1] [varchar] (255)  NULL ,
	[s2] [varchar] (255)  NULL ,
	[s3] [varchar] (255)  NULL ,
	[s4] [varchar] (255)  NULL 
) ON [PRIMARY]

 CREATE  CLUSTERED  INDEX [IX_tb_log_auth_'+@YM+'] ON [dbo].[tb_log_auth_'+@YM+']([log_date]) ON [PRIMARY]

 CREATE  INDEX [IX_tb_log_auth_'+@YM+'_n1] ON [dbo].[tb_log_auth_'+@YM+']([n1]) ON [PRIMARY]

 CREATE  INDEX [IX_tb_log_auth_'+@YM+'_n2] ON [dbo].[tb_log_auth_'+@YM+']([n2]) ON [PRIMARY]

 CREATE  INDEX [IX_tb_log_auth_'+@YM+'_n11] ON [dbo].[tb_log_auth_'+@YM+']([n11]) ON [PRIMARY]

 CREATE  INDEX [IX_tb_log_auth_'+@YM+'_s1] ON [dbo].[tb_log_auth_'+@YM+']([s1]) ON [PRIMARY]

 CREATE  INDEX [IX_tb_log_auth_'+@YM+'_s2] ON [dbo].[tb_log_auth_'+@YM+']([s2]) ON [PRIMARY]

 CREATE  INDEX [IX_tb_log_auth_'+@YM+'_logid] ON [dbo].[tb_log_auth_'+@YM+']([log_id]) ON [PRIMARY] 


'

Exec (@SQL)
End



GO
/****** Object:  StoredProcedure [dbo].[gmtool_Create_Game]    Script Date: 2022-09-01 13:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[gmtool_Create_Game]
	@DBName varchar(20)
AS

Declare @YM varchar(6)
Declare @SQL Varchar(4000)

Select @YM = Convert(char(6), DateAdd(mm, 0, GetDate()), 112)

Begin

   Set @SQL = '
Use Log_'+@DBName+'_'+@YM+'

CREATE TABLE [dbo].[tb_log_'+@DBName+'_'+@YM+'] (
	[log_date] [datetime] NOT NULL ,
	[temp] [int] NOT NULL ,
	[log_id] [int] NOT NULL ,
	[n1] [bigint] NOT NULL ,
	[n2] [bigint] NOT NULL ,
	[n3] [bigint] NOT NULL ,
	[n4] [bigint] NOT NULL ,
	[n5] [bigint] NOT NULL ,
	[n6] [bigint] NOT NULL ,
	[n7] [bigint] NOT NULL ,
	[n8] [bigint] NOT NULL ,
	[n9] [bigint] NOT NULL ,
	[n10] [bigint] NOT NULL ,
	[n11] [bigint] NOT NULL ,
	[s1] [varchar] (255)  NULL ,
	[s2] [varchar] (255)  NULL ,
	[s3] [varchar] (255)  NULL ,
	[s4] [varchar] (255)  NULL 
) ON [PRIMARY]

 CREATE  CLUSTERED  INDEX [IX_tb_log_'+@DBName+'_'+@YM+'] ON [dbo].[tb_log_'+@DBName+'_'+@YM+']([log_date]) ON [PRIMARY]

 CREATE  INDEX [IX_tb_log_'+@DBName+'_'+@YM+'_n1] ON [dbo].[tb_log_'+@DBName+'_'+@YM+']([n1]) ON [PRIMARY]

 CREATE  INDEX [IX_tb_log_'+@DBName+'_'+@YM+'_n2] ON [dbo].[tb_log_'+@DBName+'_'+@YM+']([n2]) ON [PRIMARY]

 CREATE  INDEX [IX_tb_log_'+@DBName+'_'+@YM+'_n11] ON [dbo].[tb_log_'+@DBName+'_'+@YM+']([n11]) ON [PRIMARY]

 CREATE  INDEX [IX_tb_log_'+@DBName+'_'+@YM+'_s1] ON [dbo].[tb_log_'+@DBName+'_'+@YM+']([s1]) ON [PRIMARY]

 CREATE  INDEX [IX_tb_log_'+@DBName+'_'+@YM+'_s2] ON [dbo].[tb_log_'+@DBName+'_'+@YM+']([s2]) ON [PRIMARY]

 CREATE  INDEX [IX_tb_log_'+@DBName+'_'+@YM+'_logid] ON [dbo].[tb_log_'+@DBName+'_'+@YM+']([log_id]) ON [PRIMARY]



'
Exec (@SQL)
End


GO
/****** Object:  StoredProcedure [dbo].[gmtool_Create_LogDB]    Script Date: 2022-09-01 13:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[gmtool_Create_LogDB]
	@DBName varchar(20)
AS


Declare @YM varchar(6)
Declare @SQL Varchar(2000)

Select @YM = Convert(char(6), DateAdd(mm, 0, GetDate()), 112)

Begin

   Set @SQL = ' Create Database Log_'+@DBName+'_'+@YM+' 
ON 
( NAME = ''Log_'+@DBName+'_'+@YM+'_data'',
  FILENAME = ''C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Log_'+@DBName+'_'+@YM+'_data.Mdf''
 ) 
LOG ON 
( NAME = ''Log_'+@DBName+'_'+@YM+'_log'', 
  FILENAME = ''C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Log_'+@DBName+'_'+@YM+'_Log.Ldf''
 ) 
COLLATE Albanian_CI_AS

ALTER DATABASE Log_'+@DBName+'_'+@YM+' SET RECOVERY simple  '

Exec (@SQL)
End

GO
/****** Object:  StoredProcedure [dbo].[gmtool_game_cu]    Script Date: 2022-09-01 13:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE   procedure [dbo].[gmtool_game_cu]
	@start_date datetime,
	@end_date datetime
as
	select year(log_date) as y, month(log_date) as m, day(log_date) as d, DATEPART(hh, log_date) as h, server, max(cnt) max_cnt,  avg(cnt) avg_cnt,  min(cnt) min_cnt
	from tb_log_game_connect 
	where log_date  between @start_date and @end_date
	group by year(log_date), month(log_date), day(log_date), DATEPART(hh, log_date), server
	order by year(log_date), month(log_date), day(log_date),DATEPART(hh, log_date), server                                        








GO
/****** Object:  StoredProcedure [dbo].[gmtool_game_joblv]    Script Date: 2022-09-01 13:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




create procedure [dbo].[gmtool_game_joblv]
	@log_date datetime
as
	select max(lv) lv from
	(
	select max(lv) lv from tb_log_game001_joblv where log_date = @log_date 
	union
	select max(lv) lv from tb_log_game002_joblv where log_date = @log_date 
	union
	select max(lv) lv from tb_log_game003_joblv where log_date = @log_date 
	union
	select max(lv) lv from tb_log_game004_joblv where log_date = @log_date 
	union
	select max(lv) lv from tb_log_game005_joblv where log_date = @log_date 
	union
	select max(lv) lv from tb_log_game006_joblv where log_date = @log_date 
	) as a
		
	select distinct * from
	(
	select job from tb_log_game001_joblv where log_date = @log_date group by job
	union
	select job from tb_log_game001_joblv where log_date = @log_date group by job
	union
	select job from tb_log_game001_joblv where log_date = @log_date group by job
	union
	select job from tb_log_game001_joblv where log_date = @log_date group by job
	union
	select job from tb_log_game001_joblv where log_date = @log_date group by job
	union
	select job from tb_log_game001_joblv where log_date = @log_date group by job
	) as a
	order by job
	
	select * from tb_log_game001_joblv where log_date = @log_date order by lv, job
	select * from tb_log_game002_joblv where log_date = @log_date order by lv, job
	select * from tb_log_game003_joblv where log_date = @log_date order by lv, job
	select * from tb_log_game004_joblv where log_date = @log_date order by lv, job
	select * from tb_log_game005_joblv where log_date = @log_date order by lv, job
	select * from tb_log_game006_joblv where log_date = @log_date order by lv, job
	








GO
/****** Object:  StoredProcedure [dbo].[gmtool_game_mcu]    Script Date: 2022-09-01 13:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE     procedure [dbo].[gmtool_game_mcu]
	@y int,
	@m int
as

select d, max(cnt) as max_cnt, avg(cnt) as avg_cnt from 
(
	select d, h, m, sum(cnt) as cnt from
	(
		select server, datepart(dd, log_date) as d, datepart(hh, log_date) as h, datepart(mi, log_date) as m, max(cnt) as cnt
		from tb_log_game_connect with (nolock)
		where 
			server like 'game%'
		and	year(log_date) = @y
		and month(log_date) = @m
		group by server, datepart(dd, log_date), datepart(hh, log_date), datepart(mi, log_date)
	) as a group by d, h, m
) as aa 
group by d
order by  d


select d, max(cnt) as max_cnt, avg(cnt) as avg_cnt from 
(
select datepart(dd, log_date) as d, datepart(hh, log_date) as h, datepart(mi, log_date) as m, max(cnt) as cnt
from tb_log_game_connect with (nolock)
where 
	server like 'test%'
and year(log_date) = @y
and month(log_date) = @m
group by  datepart(dd, log_date), datepart(hh, log_date), datepart(mi, log_date)
) as a
group by d
order by  d









GO
/****** Object:  StoredProcedure [dbo].[gmtool_game_uv]    Script Date: 2022-09-01 13:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE  procedure [dbo].[gmtool_game_uv]
	@y int,
	@m int
as
	declare @max int
	select @max = max(cnt) from dbo.tb_log_auth_connect
	select *, (cnt * 100)/ @max as per from dbo.tb_log_auth_connect
	where y=@y and m = @m
	order by y, m, d








GO
/****** Object:  StoredProcedure [dbo].[gmtool_get_auth_conection]    Script Date: 2022-09-01 13:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




create procedure [dbo].[gmtool_get_auth_conection]
	@y int,
	@m int
as
	declare @max int
	select @max = max(cnt) from dbo.tb_log_auth_connect
	select *, (cnt * 100)/ @max as per from dbo.tb_log_auth_connect
	where y=@y and m = @m
	order by y, m, d






GO
/****** Object:  StoredProcedure [dbo].[gmtool_get_connect_log]    Script Date: 2022-09-01 13:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE  procedure [dbo].[gmtool_get_connect_log]
	@start_date datetime,
	@end_date datetime
as
declare @max_cnt int
select @max_cnt = max(cnt) from tb_log_game_connect
select 
DATEPART(yy, log_date) as y, DATEPART(mm, log_date) as m, DATEPART(dd, log_date) as d, DATEPART(hh, log_date) as h,
server, 
avg(cnt) avg_cnt, max(cnt) max_cnt, min(cnt) min_cnt
, (avg(cnt) * 100)/ @max_cnt as per 
 from dbo.tb_log_game_connect
where log_date between @start_date and @end_date
group by  DATEPART(yy, log_date),DATEPART(mm, log_date), DATEPART(dd, log_date), DATEPART(hh, log_date), server
order by y, m, d, h, server






GO
/****** Object:  StoredProcedure [dbo].[gmtool_get_summon_enhance_list]    Script Date: 2022-09-01 13:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[gmtool_get_summon_enhance_list]
	@codeArray varchar(max),
	@server    varchar(max)
AS
BEGIN
	SET NOCOUNT ON;

	declare @sql nvarchar(1000),@database_name nvarchar(50)
	select @database_name=game_database from GmTool.dbo.tb_rappelz_server where code = @server;

	set @sql = 'select TI.Enhance, count(TI.code) as cnt,'''+Convert(varchar(20), GetDate(), 120)+''' as log_date from '+@database_name+'.dbo.Item TI 
	where TI.flag = ''-2147483648''and TI.summon_code <> 0 and TI.code in ( '+@codeArray+' ) group by TI.enhance '

	exec (@sql)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_temp_get_log_file_game002]    Script Date: 2022-09-01 13:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE procedure [dbo].[sp_temp_get_log_file_game002]
as
select top 1 'D:\log\' +  file_name  from tb_log_file_game002 where bool =0  order by sid
update tb_log_file_game002 set bool = 1 where file_name = (select top 1 file_name  from tb_log_file_game002 where bool =0 )






GO
/****** Object:  StoredProcedure [dbo].[sp_update_gamerserver_connect_user]    Script Date: 2022-09-01 13:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE         procedure [dbo].[sp_update_gamerserver_connect_user]
as

Declare @YM varchar(6)
Declare @SQL Varchar(3000)
Declare @date varchar(24)

Select @YM = Convert(char(6), DateAdd(Hour, -1, GetDate()), 112)
Select @date = dateadd(Hour, -1, GetDate() )

Begin
   Set @SQL = ' Insert into Rappelz_game_log.dbo.tb_log_game_connect 
 select log_date, ''game001'', n4 from Log_game001_'+@YM+'.dbo.tb_log_game001_'+@YM+'  with (NOLOCK) where log_id = 104 and datepart(year, log_date) = datepart(year, '''+@date+''') and datepart(month, log_date) = datepart(month, '''+@date+''') and datepart(day, log_date) = datepart(day, '''+@date+''') and datepart(hour, log_date) = datepart(hour, '''+@date+''') 

 '
Exec (@SQL)
End

GO
USE [master]
GO
ALTER DATABASE [Rappelz_game_log] SET  READ_WRITE 
GO
