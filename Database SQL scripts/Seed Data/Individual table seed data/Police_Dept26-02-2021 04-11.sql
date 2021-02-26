#
# TABLE STRUCTURE FOR: Police_Dept
#

DROP TABLE IF EXISTS `Police_Dept`;

CREATE TABLE `Police_Dept` (
  `police_dept_id` int(11) NOT NULL AUTO_INCREMENT,
  `manager_id` int(11) DEFAULT NULL,
  `name` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`police_dept_id`),
  UNIQUE KEY `name` (`name`),
  KEY `manager_id` (`manager_id`),
  CONSTRAINT `Police_Dept_ibfk_1` FOREIGN KEY (`manager_id`) REFERENCES `Person` (`person_id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `Police_Dept` (`police_dept_id`, `manager_id`, `name`) VALUES (1, 22, 'Connelly and Sons');
INSERT INTO `Police_Dept` (`police_dept_id`, `manager_id`, `name`) VALUES (2, 38, 'Grady-Wisozk');
INSERT INTO `Police_Dept` (`police_dept_id`, `manager_id`, `name`) VALUES (3, 40, 'Swift Group');


