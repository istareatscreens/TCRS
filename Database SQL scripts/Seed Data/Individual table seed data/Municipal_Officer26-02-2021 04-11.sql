#
# TABLE STRUCTURE FOR: Municipal_Officer
#

DROP TABLE IF EXISTS `Municipal_Officer`;

CREATE TABLE `Municipal_Officer` (
  `person_id` int(11) NOT NULL,
  `position` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL DEFAULT 'personel',
  `munic_id` int(11) NOT NULL,
  PRIMARY KEY (`person_id`),
  KEY `munic_id` (`munic_id`),
  CONSTRAINT `Municipal_Officer_ibfk_1` FOREIGN KEY (`person_id`) REFERENCES `Person` (`person_id`),
  CONSTRAINT `Municipal_Officer_ibfk_2` FOREIGN KEY (`munic_id`) REFERENCES `Municipality` (`munic_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `Municipal_Officer` (`person_id`, `position`, `munic_id`) VALUES (2, 'facilis', 3);
INSERT INTO `Municipal_Officer` (`person_id`, `position`, `munic_id`) VALUES (37, 'quia', 2);
INSERT INTO `Municipal_Officer` (`person_id`, `position`, `munic_id`) VALUES (41, 'vero', 2);
INSERT INTO `Municipal_Officer` (`person_id`, `position`, `munic_id`) VALUES (42, 'deleniti', 1);
INSERT INTO `Municipal_Officer` (`person_id`, `position`, `munic_id`) VALUES (43, 'assumenda', 3);


