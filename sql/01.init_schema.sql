-- --------------------------------------------------------
-- 主機:                           127.0.0.1
-- 伺服器版本:                        10.4.21-MariaDB - mariadb.org binary distribution
-- 伺服器作業系統:                      Win64
-- HeidiSQL 版本:                  11.3.0.6337
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- 傾印 ticketsystem 的資料庫結構
CREATE DATABASE IF NOT EXISTS `ticketsystem` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;
USE `ticketsystem`;

-- 傾印  資料表 ticketsystem.role 結構
CREATE TABLE IF NOT EXISTS `role` (
  `roleID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY (`roleID`),
  UNIQUE KEY `Name` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

-- 取消選取資料匯出。

-- 傾印  資料表 ticketsystem.roleissueaction 結構
CREATE TABLE IF NOT EXISTS `roleissueaction` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `roleID` int(11) NOT NULL,
  `issueType` int(11) NOT NULL,
  `issueAction` int(11) NOT NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  UNIQUE KEY `roleID_issueType_issueAction` (`roleID`,`issueType`,`issueAction`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

-- 取消選取資料匯出。

-- 傾印  資料表 ticketsystem.roleissuestatus 結構
CREATE TABLE IF NOT EXISTS `roleissuestatus` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `roleID` int(11) NOT NULL,
  `issueType` int(11) NOT NULL,
  `issueStatus` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `roleID_issueType_issueStatus` (`roleID`,`issueType`,`issueStatus`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 取消選取資料匯出。

-- 傾印  資料表 ticketsystem.issue 結構
CREATE TABLE IF NOT EXISTS `issue` (
  `issueID` int(11) NOT NULL AUTO_INCREMENT,
  `issueType` int(11) NOT NULL,
  `issueStatus` int(11) NOT NULL,
  `summary` varchar(255) NOT NULL COMMENT '摘要',
  `description` varchar(255) NOT NULL COMMENT '描述',
  `isDeleted` bit(1) NOT NULL,
  `createdUser` varchar(50) DEFAULT NULL,
  `createdTime` datetime DEFAULT NULL,
  `updatedUser` varchar(50) DEFAULT NULL,
  `updatedTime` datetime DEFAULT NULL,
  PRIMARY KEY (`ticketID`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

-- 取消選取資料匯出。

-- 傾印  資料表 ticketsystem.issuetype 結構
CREATE TABLE IF NOT EXISTS `issuetype` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `Name` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

-- 取消選取資料匯出。

-- 傾印  資料表 ticketsystem.user 結構
CREATE TABLE IF NOT EXISTS `user` (
  `userID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY (`userID`),
  UNIQUE KEY `Name` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

-- 取消選取資料匯出。

-- 傾印  資料表 ticketsystem.userrole 結構
CREATE TABLE IF NOT EXISTS `userrole` (
  `userID` int(11) NOT NULL,
  `roleID` int(11) NOT NULL,
  KEY `userID_roleID` (`userID`,`roleID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 取消選取資料匯出。

/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
