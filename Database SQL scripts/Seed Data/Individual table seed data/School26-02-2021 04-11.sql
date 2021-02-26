#
# TABLE STRUCTURE FOR: School
#

DROP TABLE IF EXISTS `School`;

CREATE TABLE `School` (
  `school_id` int(11) NOT NULL AUTO_INCREMENT,
  `name` char(30) COLLATE utf8mb4_unicode_ci NOT NULL,
  `phone_no` varchar(13) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`school_id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `School` (`school_id`, `name`, `phone_no`) VALUES (1, 'Gibson and Sons', '1-789-883-137');
INSERT INTO `School` (`school_id`, `name`, `phone_no`) VALUES (2, 'McKenzie-Luettgen', '04590114222');
INSERT INTO `School` (`school_id`, `name`, `phone_no`) VALUES (3, 'DuBuque Ltd', '769-537-9796x');


