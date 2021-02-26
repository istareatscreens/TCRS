#
# TABLE STRUCTURE FOR: Wanted_Vehicle
#

DROP TABLE IF EXISTS `Wanted_Vehicle`;

CREATE TABLE `Wanted_Vehicle` (
  `vehicle_id` int(11) NOT NULL,
  `wanted_id` int(11) NOT NULL,
  PRIMARY KEY (`vehicle_id`,`wanted_id`),
  KEY `wanted_id` (`wanted_id`),
  CONSTRAINT `Wanted_Vehicle_ibfk_1` FOREIGN KEY (`vehicle_id`) REFERENCES `Vehicle` (`vehicle_id`),
  CONSTRAINT `Wanted_Vehicle_ibfk_2` FOREIGN KEY (`wanted_id`) REFERENCES `Wanted` (`wanted_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (9, 3);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (12, 3);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (23, 17);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (53, 1);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (60, 12);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (64, 19);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (65, 26);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (74, 1);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (79, 35);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (83, 10);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (84, 16);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (98, 48);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (99, 37);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (102, 18);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (103, 13);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (114, 45);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (115, 10);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (126, 19);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (128, 35);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (143, 15);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (147, 30);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (175, 35);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (189, 17);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (191, 26);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (217, 38);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (238, 6);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (241, 41);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (243, 18);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (244, 47);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (246, 40);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (259, 22);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (260, 5);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (264, 50);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (295, 46);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (309, 34);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (314, 6);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (322, 45);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (325, 22);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (339, 11);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (346, 8);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (346, 26);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (351, 11);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (353, 16);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (359, 5);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (359, 21);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (368, 29);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (373, 50);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (401, 23);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (402, 38);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (418, 30);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (429, 28);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (488, 15);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (510, 41);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (535, 30);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (545, 34);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (546, 35);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (552, 10);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (590, 2);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (596, 48);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (620, 49);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (636, 11);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (652, 43);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (657, 9);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (663, 6);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (690, 21);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (692, 50);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (738, 14);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (740, 33);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (741, 25);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (753, 45);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (768, 2);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (777, 2);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (779, 43);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (791, 17);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (812, 17);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (836, 3);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (839, 6);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (842, 19);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (850, 2);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (868, 12);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (871, 19);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (872, 4);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (874, 33);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (883, 19);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (893, 4);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (912, 42);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (915, 42);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (926, 21);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (926, 50);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (927, 43);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (939, 45);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (941, 3);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (954, 5);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (967, 49);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (969, 13);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (970, 21);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (977, 50);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (995, 38);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (997, 27);
INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES (998, 50);


