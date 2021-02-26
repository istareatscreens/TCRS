#
# TABLE STRUCTURE FOR: Highway_Patrol_Officer
#

DROP TABLE IF EXISTS `Highway_Patrol_Officer`;

CREATE TABLE `Highway_Patrol_Officer` (
  `person_id` int(11) NOT NULL,
  `position` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL DEFAULT 'personel',
  `police_dept_id` int(11) NOT NULL,
  PRIMARY KEY (`person_id`),
  KEY `police_dept_id` (`police_dept_id`),
  CONSTRAINT `Highway_Patrol_Officer_ibfk_1` FOREIGN KEY (`person_id`) REFERENCES `Person` (`person_id`),
  CONSTRAINT `Highway_Patrol_Officer_ibfk_2` FOREIGN KEY (`police_dept_id`) REFERENCES `Police_Dept` (`police_dept_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `Highway_Patrol_Officer` (`person_id`, `position`, `police_dept_id`) VALUES (12, 'et', 3);
INSERT INTO `Highway_Patrol_Officer` (`person_id`, `position`, `police_dept_id`) VALUES (23, 'esse', 2);
INSERT INTO `Highway_Patrol_Officer` (`person_id`, `position`, `police_dept_id`) VALUES (24, 'aut', 3);
INSERT INTO `Highway_Patrol_Officer` (`person_id`, `position`, `police_dept_id`) VALUES (34, 'esse', 3);
INSERT INTO `Highway_Patrol_Officer` (`person_id`, `position`, `police_dept_id`) VALUES (43, 'sed', 2);


