#
# TABLE STRUCTURE FOR: Citation_Type
#

DROP TABLE IF EXISTS `Citation_Type`;

CREATE TABLE `Citation_Type` (
  `citation_type_id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `fine` decimal(7,2) NOT NULL,
  `training_eligable` bit(1) DEFAULT b'0',
  PRIMARY KEY (`citation_type_id`),
  UNIQUE KEY `name` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `Citation_Type` (`citation_type_id`, `name`, `fine`, `training_eligable`) VALUES (1, 'possimus', '225.51', '0');
INSERT INTO `Citation_Type` (`citation_type_id`, `name`, `fine`, `training_eligable`) VALUES (2, 'est', '351.15', '1');
INSERT INTO `Citation_Type` (`citation_type_id`, `name`, `fine`, `training_eligable`) VALUES (3, 'beatae', '468.76', '0');
INSERT INTO `Citation_Type` (`citation_type_id`, `name`, `fine`, `training_eligable`) VALUES (4, 'eligendi', '642.14', '0');
INSERT INTO `Citation_Type` (`citation_type_id`, `name`, `fine`, `training_eligable`) VALUES (5, 'ipsam', '70.82', '0');
INSERT INTO `Citation_Type` (`citation_type_id`, `name`, `fine`, `training_eligable`) VALUES (6, 'et', '876.41', '1');
INSERT INTO `Citation_Type` (`citation_type_id`, `name`, `fine`, `training_eligable`) VALUES (7, 'voluptatem', '778.62', '0');
INSERT INTO `Citation_Type` (`citation_type_id`, `name`, `fine`, `training_eligable`) VALUES (8, 'dicta', '234.73', '1');
INSERT INTO `Citation_Type` (`citation_type_id`, `name`, `fine`, `training_eligable`) VALUES (9, 'aut', '326.43', '0');
INSERT INTO `Citation_Type` (`citation_type_id`, `name`, `fine`, `training_eligable`) VALUES (10, 'tenetur', '987.46', '0');


