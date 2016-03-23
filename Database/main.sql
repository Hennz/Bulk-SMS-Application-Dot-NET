USE [master]
GO
/****** Object:  Database [Project_SMS]    Script Date: 11/24/2015 00:19:53 ******/
CREATE DATABASE [Project_SMS] ON  PRIMARY 
( NAME = N'Project_SMS', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\Project_SMS.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Project_SMS_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\Project_SMS_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Project_SMS] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Project_SMS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Project_SMS] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [Project_SMS] SET ANSI_NULLS OFF
GO
ALTER DATABASE [Project_SMS] SET ANSI_PADDING OFF
GO
ALTER DATABASE [Project_SMS] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [Project_SMS] SET ARITHABORT OFF
GO
ALTER DATABASE [Project_SMS] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [Project_SMS] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [Project_SMS] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [Project_SMS] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [Project_SMS] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [Project_SMS] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [Project_SMS] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [Project_SMS] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [Project_SMS] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [Project_SMS] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [Project_SMS] SET  DISABLE_BROKER
GO
ALTER DATABASE [Project_SMS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [Project_SMS] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [Project_SMS] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [Project_SMS] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [Project_SMS] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [Project_SMS] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [Project_SMS] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [Project_SMS] SET  READ_WRITE
GO
ALTER DATABASE [Project_SMS] SET RECOVERY SIMPLE
GO
ALTER DATABASE [Project_SMS] SET  MULTI_USER
GO
ALTER DATABASE [Project_SMS] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [Project_SMS] SET DB_CHAINING OFF
GO
USE [Project_SMS]
GO
/****** Object:  Table [dbo].[User_Master]    Script Date: 11/24/2015 00:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User_Master](
	[User_Id] [int] NOT NULL,
	[User_Name] [varchar](50) NOT NULL,
	[First_Name] [varchar](50) NOT NULL,
	[Last_Name] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Email_Hash] [varchar](100) NOT NULL,
	[Mobile] [bigint] NOT NULL,
	[Address] [varchar](200) NOT NULL,
	[Message_Remaining] [int] NOT NULL,
	[Verification_Status] [varchar](50) NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[Online_Status] [varchar](5) NULL,
	[Login_TimeStamp] [datetime2](7) NULL,
	[API_URL] [varchar](200) NULL,
	[API_USER_ID] [varchar](100) NULL,
	[API_USER_PASSWORD] [varchar](100) NULL,
	[API_SENDER_ID] [varchar](100) NULL,
	[Header] [varchar](1000) NULL,
	[Footer] [varchar](1000) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Transaction_Master]    Script Date: 11/24/2015 00:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Transaction_Master](
	[Transaction_Id] [int] NOT NULL,
	[User_Id] [int] NOT NULL,
	[TotalMessages_Issued] [int] NOT NULL,
	[Issue_Date] [varchar](10) NOT NULL,
	[Amount] [int] NOT NULL,
	[Remarks] [varchar](100) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Send_Master]    Script Date: 11/24/2015 00:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Send_Master](
	[Message_Id] [int] NOT NULL,
	[Member_Id] [int] NOT NULL,
	[Member_Name] [varchar](50) NOT NULL,
	[Message_Text] [varchar](1000) NOT NULL,
	[Message_Date] [varchar](10) NOT NULL,
	[Message_Time] [time](7) NOT NULL,
	[Message_Length] [int] NOT NULL,
	[Message_SendTotal] [int] NOT NULL,
	[Status] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Member_Master]    Script Date: 11/24/2015 00:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Member_Master](
	[Member_Id] [int] NOT NULL,
	[Category_Id] [int] NOT NULL,
	[Member_Name] [varchar](50) NOT NULL,
	[Member_Mobile] [bigint] NOT NULL,
	[Member_Email] [varchar](50) NOT NULL,
	[Member_Address] [varchar](200) NOT NULL,
	[Status] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LoginRecord_Master]    Script Date: 11/24/2015 00:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginRecord_Master](
	[Login_RecordId] [int] NOT NULL,
	[Login_TimeStamp] [datetime2](7) NOT NULL,
	[User_Id] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IndividualSend_Master]    Script Date: 11/24/2015 00:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[IndividualSend_Master](
	[IndividualMsg_Id] [int] NOT NULL,
	[User_Id] [int] NOT NULL,
	[Mobile_Number] [bigint] NOT NULL,
	[Message_Text] [varchar](1000) NOT NULL,
	[Message_Date] [varchar](10) NOT NULL,
	[Message_Time] [time](7) NOT NULL,
	[Message_Length] [int] NOT NULL,
	[Message_SendTotal] [int] NOT NULL,
	[Status] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Group_Master]    Script Date: 11/24/2015 00:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Group_Master](
	[Group_Id] [int] NOT NULL,
	[User_Id] [int] NOT NULL,
	[Group_Name] [varchar](50) NOT NULL,
	[Status] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Category_Master]    Script Date: 11/24/2015 00:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Category_Master](
	[Category_Id] [int] NOT NULL,
	[Group_Id] [int] NOT NULL,
	[Category_Name] [varchar](50) NOT NULL,
	[Status] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
