/*
Navicat SQL Server Data Transfer

Source Server         : 250SQLServer
Source Server Version : 100000
Source Host           : 172.16.0.250:1433
Source Database       : PNM
Source Schema         : dbo

Target Server Type    : SQL Server
Target Server Version : 100000
File Encoding         : 65001

Date: 2014-11-14 15:28:27
*/


-- ----------------------------
-- Table structure for table_A
-- ----------------------------
DROP TABLE [dbo].[table_A]
GO
CREATE TABLE [dbo].[table_A] (
[id] int NOT NULL ,
[createtime] datetime NOT NULL ,
[note] varchar(50) NULL 
)


GO

-- ----------------------------
-- Records of table_A
-- ----------------------------
INSERT INTO [dbo].[table_A] ([id], [createtime], [note]) VALUES (N'101', N'2014-11-14 00:00:00.000', N'2014-11-14 15:24:00')
GO
GO

-- ----------------------------
-- Table structure for table_B
-- ----------------------------
DROP TABLE [dbo].[table_B]
GO
CREATE TABLE [dbo].[table_B] (
[id] int NOT NULL ,
[createtime] datetime NOT NULL ,
[note] varchar(50) NULL 
)


GO

-- ----------------------------
-- Records of table_B
-- ----------------------------
INSERT INTO [dbo].[table_B] ([id], [createtime], [note]) VALUES (N'202', N'2014-11-14 00:00:00.000', N'2014-11-14 15:24:10')
GO
GO

-- ----------------------------
-- Indexes structure for table table_A
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table table_A
-- ----------------------------
ALTER TABLE [dbo].[table_A] ADD PRIMARY KEY ([id])
GO

-- ----------------------------
-- Indexes structure for table table_B
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table table_B
-- ----------------------------
ALTER TABLE [dbo].[table_B] ADD PRIMARY KEY ([id])
GO
