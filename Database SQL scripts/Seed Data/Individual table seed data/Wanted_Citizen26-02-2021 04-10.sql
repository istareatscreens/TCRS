#
# TABLE STRUCTURE FOR: Wanted_Citizen
#

DROP TABLE IF EXISTS `Wanted_Citizen`;

CREATE TABLE `Wanted_Citizen` (
  `citizen_id` int(11) NOT NULL,
  `wanted_id` int(11) NOT NULL,
  PRIMARY KEY (`citizen_id`,`wanted_id`),
  KEY `wanted_id` (`wanted_id`),
  CONSTRAINT `Wanted_Citizen_ibfk_1` FOREIGN KEY (`wanted_id`) REFERENCES `Wanted` (`wanted_id`),
  CONSTRAINT `Wanted_Citizen_ibfk_2` FOREIGN KEY (`citizen_id`) REFERENCES `Citizen` (`citizen_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (8, 30);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (12, 12);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (14, 42);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (16, 48);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (31, 17);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (34, 49);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (56, 13);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (79, 44);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (89, 1);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (89, 49);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (104, 39);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (118, 1);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (124, 6);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (166, 30);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (185, 17);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (192, 32);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (200, 7);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (211, 49);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (214, 31);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (218, 30);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (218, 32);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (240, 31);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (243, 43);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (272, 25);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (274, 3);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (275, 32);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (293, 26);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (296, 9);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (302, 16);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (310, 25);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (312, 25);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (317, 17);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (317, 50);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (318, 11);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (347, 28);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (348, 25);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (349, 29);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (359, 35);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (383, 42);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (392, 39);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (400, 8);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (422, 15);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (422, 31);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (431, 19);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (439, 19);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (455, 23);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (462, 49);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (462, 50);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (465, 16);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (466, 16);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (479, 40);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (483, 48);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (496, 24);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (507, 27);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (535, 17);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (536, 42);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (547, 32);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (556, 24);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (580, 4);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (582, 21);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (583, 41);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (589, 48);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (611, 37);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (611, 39);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (617, 9);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (622, 33);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (628, 16);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (665, 11);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (668, 45);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (693, 30);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (699, 36);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (704, 9);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (716, 32);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (719, 19);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (722, 17);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (722, 22);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (736, 5);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (739, 49);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (746, 30);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (756, 41);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (790, 40);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (817, 36);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (824, 28);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (832, 18);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (844, 37);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (867, 9);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (872, 25);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (878, 6);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (896, 5);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (897, 21);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (912, 41);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (926, 21);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (927, 49);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (928, 47);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (937, 13);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (948, 7);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (962, 30);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (968, 26);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (983, 29);
INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES (996, 17);


