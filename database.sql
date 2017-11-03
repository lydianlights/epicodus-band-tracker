-- ---
-- Globals
-- ---

-- SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
-- SET FOREIGN_KEY_CHECKS=0;

-- ---
-- Table 'venues'
--
-- ---

DROP TABLE IF EXISTS `venues`;

CREATE TABLE `venues` (
  `id` INTEGER NULL AUTO_INCREMENT DEFAULT NULL,
  `name` INTEGER(255) NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
);

-- ---
-- Table 'bands'
--
-- ---

DROP TABLE IF EXISTS `bands`;

CREATE TABLE `bands` (
  `id` INTEGER NULL AUTO_INCREMENT DEFAULT NULL,
  `name` INTEGER(255) NULL DEFAULT NULL,
  `genre` INTEGER NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
);

-- ---
-- Table 'concerts'
--
-- ---

DROP TABLE IF EXISTS `concerts`;

CREATE TABLE `concerts` (
  `date` DATE NULL DEFAULT NULL,
  `band_id` INTEGER NULL DEFAULT NULL,
  `venue_id` INTEGER NULL DEFAULT NULL
);

-- ---
-- Foreign Keys
-- ---

ALTER TABLE `concerts` ADD FOREIGN KEY (band_id) REFERENCES `bands` (`id`);
ALTER TABLE `concerts` ADD FOREIGN KEY (venue_id) REFERENCES `venues` (`id`);

-- ---
-- Table Properties
-- ---

-- ALTER TABLE `venues` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `bands` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `concerts` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ---
-- Test Data
-- ---

-- INSERT INTO `venues` (`id`,`name`) VALUES
-- ('','');
-- INSERT INTO `bands` (`id`,`name`,`genre`) VALUES
-- ('','','');
-- INSERT INTO `concerts` (`date`,`band_id`,`venue_id`) VALUES
-- ('','','');
