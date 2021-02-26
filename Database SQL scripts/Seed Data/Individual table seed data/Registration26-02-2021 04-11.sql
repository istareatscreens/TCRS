#
# TABLE STRUCTURE FOR: Registration
#

DROP TABLE IF EXISTS `Registration`;

CREATE TABLE `Registration` (
  `citizen_id` int(11) NOT NULL,
  `course_id` int(11) NOT NULL,
  `citation_id` int(11) NOT NULL,
  `passed` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`citizen_id`,`course_id`,`citation_id`),
  KEY `course_id` (`course_id`),
  KEY `citation_id` (`citation_id`),
  CONSTRAINT `Registration_ibfk_1` FOREIGN KEY (`citizen_id`) REFERENCES `Citizen` (`citizen_id`),
  CONSTRAINT `Registration_ibfk_2` FOREIGN KEY (`course_id`) REFERENCES `Course` (`course_id`),
  CONSTRAINT `Registration_ibfk_3` FOREIGN KEY (`citation_id`) REFERENCES `Citation` (`citation_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (30, 92, 140, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (52, 77, 344, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (60, 94, 88, '1');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (106, 29, 970, '1');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (122, 50, 877, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (133, 6, 731, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (146, 59, 553, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (159, 2, 284, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (174, 42, 806, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (179, 67, 867, '1');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (221, 26, 692, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (227, 17, 474, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (244, 40, 920, '1');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (244, 61, 633, '1');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (255, 6, 883, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (262, 12, 542, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (267, 82, 339, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (294, 78, 679, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (341, 70, 50, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (356, 26, 127, '1');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (367, 100, 642, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (400, 13, 880, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (463, 58, 885, '1');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (465, 87, 447, '1');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (476, 5, 572, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (488, 27, 126, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (498, 9, 706, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (505, 16, 990, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (513, 56, 866, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (517, 71, 723, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (587, 84, 799, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (590, 67, 52, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (590, 92, 87, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (614, 25, 477, '1');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (645, 17, 470, '1');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (677, 70, 971, '1');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (696, 64, 670, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (708, 62, 175, '1');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (723, 6, 464, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (764, 47, 968, '1');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (791, 61, 115, '1');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (811, 17, 622, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (816, 16, 840, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (833, 65, 191, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (883, 46, 353, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (908, 76, 215, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (950, 31, 491, '1');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (956, 71, 94, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (970, 10, 491, '0');
INSERT INTO `Registration` (`citizen_id`, `course_id`, `citation_id`, `passed`) VALUES (983, 6, 436, '0');


