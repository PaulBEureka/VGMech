-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 23, 2024 at 05:03 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `vgmech`
--

-- --------------------------------------------------------

--
-- Table structure for table `avatar`
--

CREATE TABLE `avatar` (
  `avatar_id` int(11) NOT NULL,
  `avatar_path` varchar(255) NOT NULL,
  `user_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `avatar`
--

INSERT INTO `avatar` (`avatar_id`, `avatar_path`, `user_id`) VALUES
(3, 'Avatars/dogo.jpg', 74);

-- --------------------------------------------------------

--
-- Table structure for table `comment`
--

CREATE TABLE `comment` (
  `comment_id` int(11) NOT NULL,
  `mechanic_title` varchar(50) NOT NULL,
  `comment` varchar(500) NOT NULL,
  `comment_date` timestamp NOT NULL DEFAULT current_timestamp(),
  `parent_comment_id` int(11) DEFAULT NULL,
  `user_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `comment`
--

INSERT INTO `comment` (`comment_id`, `mechanic_title`, `comment`, `comment_date`, `parent_comment_id`, `user_id`) VALUES
(8, 'MOVEMENT MECHANIC', 'hy', '2024-04-12 03:39:27', NULL, 74),
(9, 'MOVEMENT MECHANIC', 'uy', '2024-04-12 03:41:14', NULL, 74),
(10, 'MOVEMENT MECHANIC', 'aa', '2024-04-12 03:42:54', NULL, 74),
(11, 'MOVEMENT MECHANIC', 'aaa', '2024-04-12 03:43:05', NULL, 74),
(12, 'MOVEMENT MECHANIC', 'qq', '2024-04-12 03:44:30', NULL, 74),
(13, 'MOVEMENT MECHANIC', 'update', '2024-04-12 07:42:31', NULL, 74),
(14, 'MOVEMENT MECHANIC', 'rr', '2024-04-12 08:06:58', NULL, 74),
(15, 'MOVEMENT MECHANIC', 'y', '2024-04-12 08:15:26', NULL, 74),
(16, 'MOVEMENT MECHANIC', 'e', '2024-04-12 08:17:31', NULL, 74),
(17, 'MOVEMENT MECHANIC', '@hatdog hihi', '2024-04-12 08:25:14', 8, 74),
(18, 'MOVEMENT MECHANIC', 'yyy', '2024-04-12 08:30:25', NULL, 74),
(19, 'MOVEMENT MECHANIC', 'ii', '2024-04-12 08:40:38', NULL, 74),
(20, 'MOVEMENT MECHANIC', 'aaa', '2024-04-12 08:43:41', NULL, 74),
(21, 'MOVEMENT MECHANIC', 'l', '2024-04-12 08:44:54', NULL, 74),
(22, 'MOVEMENT MECHANIC', 'rr', '2024-04-12 08:52:47', NULL, 74),
(23, 'MOVEMENT MECHANIC', 'hh', '2024-04-12 08:53:02', NULL, 74),
(24, 'MOVEMENT MECHANIC', 'aa', '2024-04-12 08:53:22', NULL, 74),
(25, 'MOVEMENT MECHANIC', 'a', '2024-04-12 08:53:38', NULL, 74),
(26, 'MOVEMENT MECHANIC', 'a', '2024-04-12 08:57:01', NULL, 74),
(27, 'COLLECTING MECHANIC', 'gg', '2024-04-12 09:17:40', NULL, 74),
(28, 'INTERACT MECHANIC', 'I am the first one', '2024-04-15 01:57:22', NULL, 74),
(29, 'MOVEMENT MECHANIC', 'j', '2024-04-22 06:47:34', NULL, 74);

-- --------------------------------------------------------

--
-- Table structure for table `game_record`
--

CREATE TABLE `game_record` (
  `record_id` int(11) NOT NULL,
  `game_title` varchar(50) NOT NULL,
  `game_score` int(11) NOT NULL,
  `ranking_date` timestamp NOT NULL DEFAULT current_timestamp(),
  `user_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `game_record`
--

INSERT INTO `game_record` (`record_id`, `game_title`, `game_score`, `ranking_date`, `user_id`) VALUES
(10, 'Block Breaker', 166, '2024-04-16 07:51:05', 74);

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `user_id` int(11) NOT NULL,
  `username` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `password` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `about_me` varchar(255) DEFAULT NULL,
  `role` enum('user','admin') NOT NULL DEFAULT 'user'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`user_id`, `username`, `password`, `email`, `about_me`, `role`) VALUES
(74, 'hatdog', '$2a$10$8s0bgjNKXCZGJh.eXxsKAeH7g0Tj1q05AZs4bm/ICAgW3TgA..qdK', 'paul.pb331@gmail.com', 'Cute Dogez', 'user'),
(77, 'root', '$2a$10$udhbTveYQQTHxVjMgfzrOe5Axmbe3TVmMAnYgnyLEwQ3QydAxq6qa', NULL, NULL, 'admin');

-- --------------------------------------------------------

--
-- Table structure for table `visited_pages`
--

CREATE TABLE `visited_pages` (
  `visit_id` int(11) NOT NULL,
  `mechanic_title` varchar(50) NOT NULL,
  `visited_timestamp` timestamp NOT NULL DEFAULT current_timestamp(),
  `user_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `visited_pages`
--

INSERT INTO `visited_pages` (`visit_id`, `mechanic_title`, `visited_timestamp`, `user_id`) VALUES
(5, 'MOVEMENT MECHANIC', '2024-04-12 03:39:21', 74),
(6, 'SHOOTING MECHANIC', '2024-04-12 09:15:37', 74),
(7, 'COLLECTING MECHANIC', '2024-04-12 09:17:11', 74),
(8, 'INTERACT MECHANIC', '2024-04-15 01:57:04', 74),
(9, 'HEALTH SYSTEM', '2024-04-19 07:37:41', 74);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `avatar`
--
ALTER TABLE `avatar`
  ADD PRIMARY KEY (`avatar_id`),
  ADD KEY `user_id` (`user_id`);

--
-- Indexes for table `comment`
--
ALTER TABLE `comment`
  ADD PRIMARY KEY (`comment_id`),
  ADD KEY `user_id` (`user_id`);

--
-- Indexes for table `game_record`
--
ALTER TABLE `game_record`
  ADD PRIMARY KEY (`record_id`),
  ADD KEY `user_id` (`user_id`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`user_id`);

--
-- Indexes for table `visited_pages`
--
ALTER TABLE `visited_pages`
  ADD PRIMARY KEY (`visit_id`),
  ADD KEY `user_id` (`user_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `avatar`
--
ALTER TABLE `avatar`
  MODIFY `avatar_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `comment`
--
ALTER TABLE `comment`
  MODIFY `comment_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=30;

--
-- AUTO_INCREMENT for table `game_record`
--
ALTER TABLE `game_record`
  MODIFY `record_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `user`
--
ALTER TABLE `user`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=78;

--
-- AUTO_INCREMENT for table `visited_pages`
--
ALTER TABLE `visited_pages`
  MODIFY `visit_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `avatar`
--
ALTER TABLE `avatar`
  ADD CONSTRAINT `avatar_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `user` (`user_id`) ON DELETE CASCADE;

--
-- Constraints for table `comment`
--
ALTER TABLE `comment`
  ADD CONSTRAINT `comment_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `user` (`user_id`) ON DELETE CASCADE;

--
-- Constraints for table `game_record`
--
ALTER TABLE `game_record`
  ADD CONSTRAINT `game_record_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `user` (`user_id`) ON DELETE CASCADE;

--
-- Constraints for table `visited_pages`
--
ALTER TABLE `visited_pages`
  ADD CONSTRAINT `visited_pages_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `user` (`user_id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
