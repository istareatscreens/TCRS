#
# TABLE STRUCTURE FOR: School_Rep
#

DROP TABLE IF EXISTS `School_Rep`;

CREATE TABLE `School_Rep` (
  `person_id` int(11) NOT NULL,
  `school_id` int(11) NOT NULL,
  PRIMARY KEY (`person_id`),
  KEY `school_id` (`school_id`),
  CONSTRAINT `School_Rep_ibfk_1` FOREIGN KEY (`person_id`) REFERENCES `Person` (`person_id`),
  CONSTRAINT `School_Rep_ibfk_2` FOREIGN KEY (`school_id`) REFERENCES `School` (`school_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `School_Rep` (`person_id`, `school_id`) VALUES (46, 1);
INSERT INTO `School_Rep` (`person_id`, `school_id`) VALUES (17, 2);
INSERT INTO `School_Rep` (`person_id`, `school_id`) VALUES (32, 2);
INSERT INTO `School_Rep` (`person_id`, `school_id`) VALUES (29, 3);
INSERT INTO `School_Rep` (`person_id`, `school_id`) VALUES (45, 3);


