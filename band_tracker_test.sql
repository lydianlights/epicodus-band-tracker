SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";
CREATE DATABASE IF NOT EXISTS `band_tracker_test` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `band_tracker_test`;

DROP TABLE IF EXISTS `bands`;
CREATE TABLE `bands` (
  `id` int(11) NOT NULL,
  `name` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `concerts`;
CREATE TABLE `concerts` (
  `date` date DEFAULT NULL,
  `band_id` int(11) DEFAULT NULL,
  `venue_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `venues`;
CREATE TABLE `venues` (
  `id` int(11) NOT NULL,
  `name` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


ALTER TABLE `bands`
  ADD PRIMARY KEY (`id`);

ALTER TABLE `concerts`
  ADD KEY `band_id` (`band_id`),
  ADD KEY `venue_id` (`venue_id`);

ALTER TABLE `venues`
  ADD PRIMARY KEY (`id`);


ALTER TABLE `bands`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=304;
ALTER TABLE `venues`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=383;

ALTER TABLE `concerts`
  ADD CONSTRAINT `concerts_ibfk_1` FOREIGN KEY (`band_id`) REFERENCES `bands` (`id`),
  ADD CONSTRAINT `concerts_ibfk_2` FOREIGN KEY (`venue_id`) REFERENCES `venues` (`id`);
COMMIT;
