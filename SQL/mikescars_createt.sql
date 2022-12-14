DROP DATABASE MikesCarsDatabase;
GO
/****** Object:  Database [MikesCarsDatabase]    Script Date: 10/17/2022 4:41:40 PM ******/
CREATE DATABASE [MikesCarsDatabase]
GO
ALTER DATABASE [MikesCarsDatabase] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MikesCarsDatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MikesCarsDatabase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MikesCarsDatabase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MikesCarsDatabase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MikesCarsDatabase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MikesCarsDatabase] SET ARITHABORT OFF 
GO
ALTER DATABASE [MikesCarsDatabase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MikesCarsDatabase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MikesCarsDatabase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MikesCarsDatabase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MikesCarsDatabase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MikesCarsDatabase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MikesCarsDatabase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MikesCarsDatabase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MikesCarsDatabase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MikesCarsDatabase] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MikesCarsDatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MikesCarsDatabase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MikesCarsDatabase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MikesCarsDatabase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MikesCarsDatabase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MikesCarsDatabase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MikesCarsDatabase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MikesCarsDatabase] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MikesCarsDatabase] SET  MULTI_USER 
GO
ALTER DATABASE [MikesCarsDatabase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MikesCarsDatabase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MikesCarsDatabase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MikesCarsDatabase] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MikesCarsDatabase] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MikesCarsDatabase] SET QUERY_STORE = OFF
GO
USE [MikesCarsDatabase]
GO
/****** Object:  Table [dbo].[favorite]    Script Date: 10/17/2022 4:41:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[favorite](
	[id] [int] identity,
	[userId] [int] NULL,
	[listingId] [int] NULL
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)  ON [PRIMARY]
GO
/****** Object:  Table [dbo].[item]    Script Date: 10/17/2022 4:41:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[id] [int] identity,
	[firstName] [varchar](50) NULL,
	[lastName] [varchar](50) NULL,
	[firebaseId] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order]    Script Date: 10/17/2022 4:41:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[listing](
	[id] [int] identity,
	[userId] [int] NULL,
    [type] [varchar](50) NULL,
	[maker] [varchar](50) NULL,
	[address] [varchar](100) NULL,
	[price] [float] NULL,
	[dateOfListing] [date] NULL,
	[favorites] [int] NULL,
	[purchased] [bit] NULL,
	[inCart] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orderItem]    Script Date: 10/17/2022 4:41:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[image](
	[id] [int] identity,
	[listingId] [int] NULL,
	[img] [varchar](200) NULL,
	[displayOrder] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 10/17/2022 4:41:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fact](
	[id] [int] identity,
	[electric] [bit] NULL,
	[listingId] [int] NULL,
	[mpg] [float] NULL,
	[crashes] [int] NULL,
	[miles] [int] NULL,
	[warranty] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cart](
	[id] [int] identity,
	[userId] [int] NULL,
	[listingId] [int] NULL,
	[purchased] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[favorite]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[listing]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[cart]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[favorite]  WITH CHECK ADD FOREIGN KEY([listingId])
REFERENCES [dbo].[listing] ([id])
GO
ALTER TABLE [dbo].[cart]  WITH CHECK ADD FOREIGN KEY([listingId])
REFERENCES [dbo].[listing] ([id])
GO
ALTER TABLE [dbo].[fact]  WITH CHECK ADD FOREIGN KEY([listingId])
REFERENCES [dbo].[listing] ([id])
GO
ALTER TABLE [dbo].[image]  WITH CHECK ADD FOREIGN KEY([listingId])
REFERENCES [dbo].[listing] ([id])
GO
USE [master]
GO
ALTER DATABASE [MikesCarsDatabase] SET  READ_WRITE 
GO
