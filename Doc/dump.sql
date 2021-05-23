-- MySQL dump 10.13  Distrib 8.0.23, for Win64 (x86_64)
--
-- Host: localhost    Database: xkom
-- ------------------------------------------------------
-- Server version	8.0.23

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `meetings`
--

DROP TABLE IF EXISTS `meetings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `meetings` (
  `meeting_id` char(36) NOT NULL,
  `start_date` datetime NOT NULL,
  `title` varchar(255) NOT NULL,
  `description` text,
  `meeting_type` int NOT NULL DEFAULT '1',
  PRIMARY KEY (`meeting_id`),
  UNIQUE KEY `meeting_id_UNIQUE` (`meeting_id`),
  KEY `meetings_meeting_type_id_idx` (`meeting_type`),
  CONSTRAINT `meetings_meeting_type_id` FOREIGN KEY (`meeting_type`) REFERENCES `meetingtypes` (`meeting_type_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `meetings`
--

LOCK TABLES `meetings` WRITE;
/*!40000 ALTER TABLE `meetings` DISABLE KEYS */;
INSERT INTO `meetings` VALUES ('008538fc-c479-42b7-af94-335ffd5789ed','2021-05-23 14:18:13','Testowe spotkanie2','Testowy opis2',2),('30c5e921-f249-42f8-b1aa-462f652893eb','2021-05-23 14:15:44','Testowe spotkanie1','Testowy opis',3);
/*!40000 ALTER TABLE `meetings` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `meetings_participants`
--

DROP TABLE IF EXISTS `meetings_participants`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `meetings_participants` (
  `meeting_participant_id` char(36) NOT NULL,
  `meeting_id` char(36) NOT NULL,
  `participant_id` char(36) NOT NULL,
  PRIMARY KEY (`meeting_participant_id`),
  UNIQUE KEY `meeting_user_id_UNIQUE` (`meeting_participant_id`),
  KEY `meetings_users_meeting_id_idx` (`meeting_id`),
  KEY `meetings_participants_participant_id_idx` (`participant_id`),
  CONSTRAINT `meetings_participants_meeting_id` FOREIGN KEY (`meeting_id`) REFERENCES `meetings` (`meeting_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `meetings_participants_participant_id` FOREIGN KEY (`participant_id`) REFERENCES `participant` (`participant_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `meetings_participants`
--

LOCK TABLES `meetings_participants` WRITE;
/*!40000 ALTER TABLE `meetings_participants` DISABLE KEYS */;
INSERT INTO `meetings_participants` VALUES ('08d91de4-9dd1-4763-8b75-ce677b44d533','30c5e921-f249-42f8-b1aa-462f652893eb','d83e9ac8-7f3a-4eb5-a0a0-cda474c365ed');
/*!40000 ALTER TABLE `meetings_participants` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `meetingtypes`
--

DROP TABLE IF EXISTS `meetingtypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `meetingtypes` (
  `meeting_type_id` int NOT NULL AUTO_INCREMENT,
  `type_name` varchar(45) NOT NULL,
  PRIMARY KEY (`meeting_type_id`),
  UNIQUE KEY `meeting_type_id_UNIQUE` (`meeting_type_id`),
  UNIQUE KEY `type_name_UNIQUE` (`type_name`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `meetingtypes`
--

LOCK TABLES `meetingtypes` WRITE;
/*!40000 ALTER TABLE `meetingtypes` DISABLE KEYS */;
INSERT INTO `meetingtypes` VALUES (2,'event'),(1,'meeting'),(3,'seminar');
/*!40000 ALTER TABLE `meetingtypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `participant`
--

DROP TABLE IF EXISTS `participant`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `participant` (
  `participant_id` char(36) NOT NULL,
  `name` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  PRIMARY KEY (`participant_id`),
  UNIQUE KEY `participant_id_UNIQUE` (`participant_id`),
  UNIQUE KEY `email_UNIQUE` (`email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `participant`
--

LOCK TABLES `participant` WRITE;
/*!40000 ALTER TABLE `participant` DISABLE KEYS */;
INSERT INTO `participant` VALUES ('d83e9ac8-7f3a-4eb5-a0a0-cda474c365ed','Testowy użytkownik1','test@test1.com');
/*!40000 ALTER TABLE `participant` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'xkom'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-05-23 15:10:39
