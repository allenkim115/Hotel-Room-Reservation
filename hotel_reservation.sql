-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 13, 2025 at 07:08 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `hotel_reservation`
--

-- --------------------------------------------------------

--
-- Table structure for table `reservations`
--

CREATE TABLE `reservations` (
  `Id` int(11) NOT NULL,
  `RoomTypeId` int(11) NOT NULL,
  `RoomType` longtext NOT NULL,
  `Status` longtext NOT NULL,
  `CheckInDate` datetime(6) NOT NULL,
  `CheckOutDate` datetime(6) NOT NULL,
  `NumberOfGuests` int(11) NOT NULL,
  `TotalAmount` decimal(65,30) NOT NULL,
  `SpecialRequests` longtext NOT NULL,
  `UserId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `reservations`
--

INSERT INTO `reservations` (`Id`, `RoomTypeId`, `RoomType`, `Status`, `CheckInDate`, `CheckOutDate`, `NumberOfGuests`, `TotalAmount`, `SpecialRequests`, `UserId`) VALUES
(5, 2, 'Standard Room', 'Rejected', '2025-05-14 00:00:00.000000', '2025-05-16 00:00:00.000000', 1, 160.000000000000000000000000000000, 'None', 10),
(7, 4, 'Suite Room', 'Completed', '2025-05-19 00:00:00.000000', '2025-05-21 00:00:00.000000', 2, 400.000000000000000000000000000000, 'None', 10),
(8, 2, 'Buffet Breakfast', 'Completed', '2025-05-16 00:00:00.000000', '2025-05-16 00:00:00.000000', 2, 30.000000000000000000000000000000, 'No', 10),
(9, 1, 'Deluxe Room', 'Pending', '2025-05-17 00:00:00.000000', '2025-05-18 00:00:00.000000', 1, 120.000000000000000000000000000000, 'Extra Pillow', 11),
(10, 4, 'Suite Room', 'Pending', '2025-05-31 00:00:00.000000', '2025-06-02 00:00:00.000000', 2, 400.000000000000000000000000000000, '', 10);

-- --------------------------------------------------------

--
-- Table structure for table `rooms`
--

CREATE TABLE `rooms` (
  `Id` int(11) NOT NULL,
  `RoomNumber` longtext NOT NULL,
  `RoomType` longtext NOT NULL,
  `Floor` int(11) NOT NULL,
  `Capacity` int(11) NOT NULL,
  `PricePerNight` decimal(65,30) NOT NULL,
  `Description` longtext NOT NULL,
  `Status` int(11) NOT NULL,
  `Features` longtext NOT NULL,
  `LastMaintenance` datetime(6) DEFAULT NULL,
  `NextMaintenance` datetime(6) DEFAULT NULL,
  `IsAvailable` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `roomtypes`
--

CREATE TABLE `roomtypes` (
  `Id` int(11) NOT NULL,
  `Name` longtext NOT NULL,
  `ImageUrl` longtext NOT NULL,
  `PricePerNight` int(11) NOT NULL,
  `BedType` longtext NOT NULL,
  `MaxOccupancy` int(11) NOT NULL,
  `Size` int(11) NOT NULL,
  `Description` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `roomtypes`
--

INSERT INTO `roomtypes` (`Id`, `Name`, `ImageUrl`, `PricePerNight`, `BedType`, `MaxOccupancy`, `Size`, `Description`) VALUES
(1, 'Deluxe Room', '/images/suite.png', 120, 'King', 3, 35, 'A spacious deluxe room with a king bed and modern amenities.'),
(2, 'Standard Room', '/images/standard.png', 80, 'Queen', 2, 28, 'A comfortable standard room perfect for business or leisure.'),
(4, 'Suite Room', 'https://imgs.search.brave.com/XFsLqIWBSkgQe1qZ3A2ZrOrFoUT0LoMAHr1KdWB_rHw/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly9pbWFn/ZWRlbGl2ZXJ5Lm5l/dC9qNDQ0dG1uLWRJ/Q2xGN3QtUTNGUWR3/L2Nvbm5lY3Rpbmct/cm9vbXMvaGVyby5q/cGcvcHVibGlj', 200, 'King', 2, 35, 'Experience the perfect blend of comfort, style, and space in our luxurious suite hotel room — your home away from home.');

-- --------------------------------------------------------

--
-- Table structure for table `services`
--

CREATE TABLE `services` (
  `Id` int(11) NOT NULL,
  `Name` longtext NOT NULL,
  `Category` longtext NOT NULL,
  `Description` longtext NOT NULL,
  `ImageUrl` longtext NOT NULL,
  `OperatingHours` longtext NOT NULL,
  `Location` longtext NOT NULL,
  `PricePerPerson` int(11) NOT NULL,
  `MaxGuests` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `services`
--

INSERT INTO `services` (`Id`, `Name`, `Category`, `Description`, `ImageUrl`, `OperatingHours`, `Location`, `PricePerPerson`, `MaxGuests`) VALUES
(1, 'Spa', 'Wellness', 'Relaxing spa treatment.', '/images/massage.png', '09:00 - 21:00', '2nd Floor', 50, 2),
(2, 'Buffet Breakfast', 'Dining', 'All-you-can-eat breakfast buffet.', '/images/platter.png', '06:00 - 10:00', 'Ground Floor', 15, 4),
(6, 'Gym', 'Wellness', 'Stay Fit and Healthy', 'https://imgs.search.brave.com/J6JVr9xJAw7v0AlubjXMUw_JTc68nTZmIsLhYJYFlPY/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly9pbWFn/ZXMuc3F1YXJlc3Bh/Y2UtY2RuLmNvbS9j/b250ZW50L3YxLzVh/ZmZmNDU3Yjk4YTc4/ZGZlNjRjNzgxYS8x/NjA0NjYxODM5NjE2/LThUWDVZUUY4QTg5/VFBVNEYwQkVTL2d5/bStkZXNpZ24rYXQr/c2l4K2hvdGVsK3N0/b2NraG9sbQ', '7:00 AM - 8:00 PM', '3rd Floor', 30, 5),
(7, 'Valet Parking', 'Additional', 'Parking you car safetly', 'https://imgs.search.brave.com/gF2DwCCAjBQSqKSZqGJBF_oEEAPWU7frKBbOHrYzYME/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly93d3cu/d2Vic3RhdXJhbnRz/dG9yZS5jb20vdXBs/b2Fkcy9ibG9nLzIw/MjIvMS93b21hbi1n/aXZpbmctY2FyLWtl/eS10by12YWxldC1t/aW4uanBn', 'Anytime', 'Ground Floor Entrance', 10, 1);

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `PK_USER_ID` int(11) NOT NULL,
  `FIRSTNAME` longtext NOT NULL,
  `MIDDLENAME` longtext NOT NULL,
  `LASTNAME` longtext NOT NULL,
  `USERNAME` longtext NOT NULL,
  `PASSWORD` longtext NOT NULL,
  `EMAIL` longtext NOT NULL,
  `ROLE` longtext NOT NULL,
  `PHONENUMBER` longtext NOT NULL,
  `ADDRESS` longtext NOT NULL,
  `PREFERREDLANGUAGE` varchar(255) NOT NULL DEFAULT 'wwwroot/images/default_pic.png',
  `EMAILNOTIFICATIONS` tinyint(1) NOT NULL,
  `SMSNOTIFICATIONS` tinyint(1) NOT NULL,
  `PROFILEIMAGEURL` varchar(255) NOT NULL DEFAULT '''wwwroot/images/default_pic.png'''
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`PK_USER_ID`, `FIRSTNAME`, `MIDDLENAME`, `LASTNAME`, `USERNAME`, `PASSWORD`, `EMAIL`, `ROLE`, `PHONENUMBER`, `ADDRESS`, `PREFERREDLANGUAGE`, `EMAILNOTIFICATIONS`, `SMSNOTIFICATIONS`, `PROFILEIMAGEURL`) VALUES
(7, 'Admin', '', 'User', 'admin', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'admin@nustar.com', 'Admin', '1234567890', 'Admin Office', 'English', 1, 1, '/images/default-profile.png'),
(10, 'John', 'Dan', 'Doe', 'j.doe', '27cc6994fc1c01ce6659c6bddca9b69c4c6a9418065e612c69d110b3f7b11f8a', 'johndoe@email.com', 'Guest', '09123456789', 'Cebu City', 'en', 0, 0, '/uploads/profile-images/j.doe_638827428927954725.png'),
(11, 'Allen Kim', 'Calaclan', 'Rafaela', 'user', '27cc6994fc1c01ce6659c6bddca9b69c4c6a9418065e612c69d110b3f7b11f8a', 'allenkimrafaela@gmail.com', 'Guest', '09123456789', 'Cebu City', 'en', 0, 0, ''),
(13, 'Juan', 'undefined', 'Dela cruz', 'juan', '27cc6994fc1c01ce6659c6bddca9b69c4c6a9418065e612c69d110b3f7b11f8a', 'juanD@email.com', 'Guest', '09234512675', 'manila', 'English', 1, 1, '/images/default-profile.png'),
(14, 'Jane', 'Doe', 'Smith', 'jane', '27cc6994fc1c01ce6659c6bddca9b69c4c6a9418065e612c69d110b3f7b11f8a', 'jane@email.com', 'Guest', '09123456789', 'New York', 'en', 0, 0, '/uploads/profile-images/jane_638827515712055236.jfif');

-- --------------------------------------------------------

--
-- Table structure for table `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20250513130516_InitialCreate', '8.0.2'),
('20250513140414_UpdatePriceColumnsToMoney', '8.0.2'),
('20250513143809_ChangePricePerPersonToInt', '8.0.2');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `reservations`
--
ALTER TABLE `reservations`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Reservations_UserId` (`UserId`);

--
-- Indexes for table `rooms`
--
ALTER TABLE `rooms`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `roomtypes`
--
ALTER TABLE `roomtypes`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `services`
--
ALTER TABLE `services`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`PK_USER_ID`);

--
-- Indexes for table `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `reservations`
--
ALTER TABLE `reservations`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `rooms`
--
ALTER TABLE `rooms`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `roomtypes`
--
ALTER TABLE `roomtypes`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `services`
--
ALTER TABLE `services`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `user`
--
ALTER TABLE `user`
  MODIFY `PK_USER_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `reservations`
--
ALTER TABLE `reservations`
  ADD CONSTRAINT `FK_Reservations_USER_UserId` FOREIGN KEY (`UserId`) REFERENCES `user` (`PK_USER_ID`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
