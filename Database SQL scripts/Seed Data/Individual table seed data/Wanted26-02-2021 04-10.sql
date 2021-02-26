#
# TABLE STRUCTURE FOR: Wanted
#

DROP TABLE IF EXISTS `Wanted`;

CREATE TABLE `Wanted` (
  `wanted_id` int(11) NOT NULL AUTO_INCREMENT,
  `reference_no` varchar(32) COLLATE utf8mb4_unicode_ci NOT NULL,
  `dangerous` bit(1) NOT NULL DEFAULT b'1',
  `crime` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`wanted_id`),
  UNIQUE KEY `reference_no` (`reference_no`)
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (1, 'cc334f5b-ef29-3d0b-b588-7624c21a', '0', 'voluptas');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (2, '141f80ad-f95c-3ee9-b980-a2e70832', '0', 'aliquam');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (3, '8a210e55-abd3-3781-b49e-10ed36b1', '0', 'qui');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (4, 'e2a50ee0-f1df-3671-bc32-18fa4bec', '1', 'est');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (5, 'e2fb256e-ea73-3244-9fb3-c8e20470', '1', 'dolorem');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (6, '3f58ea52-7263-34be-9be3-5ad3689d', '0', 'et');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (7, 'ca1a8cf7-2dde-34af-9cf4-866d8050', '1', 'saepe');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (8, '1ab86280-ce7a-3572-bba2-2b647c95', '0', 'rerum');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (9, '0b9315c9-58dd-3cab-8624-2cbb5baa', '0', 'magnam');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (10, 'fff1103d-f1ce-31d8-b7ba-9e3efd8c', '1', 'sint');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (11, '35c8b3e7-3858-3397-b9e6-283869d6', '1', 'provident');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (12, '5d77433a-c649-332d-a431-9927571f', '1', 'repellendus');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (13, '5ccc9196-4cdb-37e3-82f6-f22ef8e3', '1', 'consequatur');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (14, '679ba72b-d638-3606-894d-fd64aaa2', '0', 'quia');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (15, 'b9896059-3612-3d7f-9463-65b79e37', '0', 'sed');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (16, '40737d18-64f1-3e8c-b600-01fd6083', '1', 'rerum');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (17, '347fe015-837c-347c-aba8-63bfe545', '1', 'molestiae');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (18, '7f82e789-a010-3258-9239-abaf86a5', '1', 'qui');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (19, '956dc328-8e1c-36a2-a1a4-3819d621', '0', 'debitis');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (20, '13307bd4-af6a-34da-9997-4480e1f0', '1', 'magnam');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (21, 'c38339d7-167a-3fb9-aca8-40010477', '1', 'officia');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (22, '586228dc-fd24-3b5d-aa2a-618ef80c', '0', 'sequi');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (23, '1e068d41-f698-34c2-a69e-a20df689', '1', 'autem');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (24, '04ac343c-d979-3e0a-8dea-dd5f54cf', '0', 'consectetur');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (25, 'c6856ff9-7f7d-3f3c-8e70-445855e5', '0', 'sequi');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (26, '635197a9-8e03-3029-93b5-8383c683', '1', 'perferendis');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (27, 'a3909a4f-addc-3f93-8739-c670b7a0', '0', 'enim');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (28, '0277deb6-0aaa-3228-a4d1-75773587', '0', 'doloribus');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (29, '4bf9ca9f-df35-3172-a62a-a01e57d6', '1', 'harum');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (30, '48312120-d1fe-3d16-ad9f-2216ff2e', '0', 'expedita');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (31, 'df9f9382-5574-3b02-b7f9-91969bc0', '0', 'ad');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (32, '68bbd64d-2fff-3aff-b2c2-d5853a3d', '0', 'optio');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (33, '4cea685d-40ab-32d4-81c4-91e2c3fe', '1', 'architecto');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (34, '9a801aa2-d0d4-3f7e-8cb4-c48a02e3', '1', 'tenetur');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (35, 'c77fa0d2-4683-3fe1-98ef-aa3959d7', '1', 'repudiandae');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (36, '6569a9d2-b9c5-3237-84ba-4d431f1d', '1', 'cumque');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (37, 'd9759966-3aed-34c4-9f42-6d556403', '0', 'consequatur');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (38, '4c3ade77-36bd-3711-a76e-f2df9024', '1', 'laborum');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (39, 'c5559b46-e516-3d6b-9377-cc2748ed', '0', 'autem');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (40, '8b0924ad-b60d-3edf-a7d1-db05ea4b', '1', 'explicabo');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (41, 'eb7476ad-b285-316a-9920-70bee3da', '0', 'totam');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (42, '61fa0854-608f-34ee-b0ab-e8b1db15', '0', 'sed');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (43, 'e09b61f7-db44-3453-ba1f-8d6642db', '0', 'aspernatur');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (44, '8df5557d-0eb8-364f-a931-7429646b', '1', 'fugit');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (45, '53c4e532-8bb3-319d-a9a5-3d31f8bb', '1', 'numquam');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (46, 'c4e0b5fb-9583-3f00-bc40-2c0bd196', '0', 'ad');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (47, '8ad158d4-a852-3337-9c2c-ee1d4f0c', '1', 'dolorem');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (48, '41e41c93-f952-344c-b4d1-843e75ff', '1', 'adipisci');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (49, 'd1a793ef-599c-3834-9982-b796aa65', '0', 'vero');
INSERT INTO `Wanted` (`wanted_id`, `reference_no`, `dangerous`, `crime`) VALUES (50, '2f0ad3ba-75e8-34ba-b002-c8fa8c2c', '0', 'in');


