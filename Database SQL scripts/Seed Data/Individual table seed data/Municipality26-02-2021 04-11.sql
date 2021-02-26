#
# TABLE STRUCTURE FOR: Municipality
#

DROP TABLE IF EXISTS `Municipality`;

CREATE TABLE `Municipality` (
  `munic_id` int(11) NOT NULL AUTO_INCREMENT,
  `manager_id` int(11) DEFAULT NULL,
  `name` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`munic_id`),
  UNIQUE KEY `name` (`name`),
  KEY `manager_id` (`manager_id`),
  CONSTRAINT `Municipality_ibfk_1` FOREIGN KEY (`manager_id`) REFERENCES `Person` (`person_id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `Municipality` (`munic_id`, `manager_id`, `name`) VALUES (1, 9, 'voluptatem');
INSERT INTO `Municipality` (`munic_id`, `manager_id`, `name`) VALUES (2, 38, 'dolorum');
INSERT INTO `Municipality` (`munic_id`, `manager_id`, `name`) VALUES (3, 10, 'explicabo');


