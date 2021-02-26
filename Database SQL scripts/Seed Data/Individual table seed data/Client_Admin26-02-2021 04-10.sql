#
# TABLE STRUCTURE FOR: Client_Admin
#

DROP TABLE IF EXISTS `Client_Admin`;

CREATE TABLE `Client_Admin` (
  `person_id` int(11) NOT NULL,
  PRIMARY KEY (`person_id`),
  CONSTRAINT `Client_Admin_ibfk_1` FOREIGN KEY (`person_id`) REFERENCES `Person` (`person_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `Client_Admin` (`person_id`) VALUES (30);
INSERT INTO `Client_Admin` (`person_id`) VALUES (31);
INSERT INTO `Client_Admin` (`person_id`) VALUES (41);


