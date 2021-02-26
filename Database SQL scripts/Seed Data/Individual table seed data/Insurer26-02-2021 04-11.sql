#
# TABLE STRUCTURE FOR: Insurer
#

DROP TABLE IF EXISTS `Insurer`;

CREATE TABLE `Insurer` (
  `insurer_id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`insurer_id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `Insurer` (`insurer_id`, `name`) VALUES (1, 'Halvorson PLC');
INSERT INTO `Insurer` (`insurer_id`, `name`) VALUES (2, 'Yost, Dickens and Kuhic');
INSERT INTO `Insurer` (`insurer_id`, `name`) VALUES (3, 'Howe-Barton');
INSERT INTO `Insurer` (`insurer_id`, `name`) VALUES (4, 'Lakin PLC');
INSERT INTO `Insurer` (`insurer_id`, `name`) VALUES (5, 'O\'Hara-Zieme');


