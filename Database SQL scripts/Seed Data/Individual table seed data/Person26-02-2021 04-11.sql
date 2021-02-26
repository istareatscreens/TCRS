#
# TABLE STRUCTURE FOR: Person
#

DROP TABLE IF EXISTS `Person`;

CREATE TABLE `Person` (
  `person_id` int(11) NOT NULL AUTO_INCREMENT,
  `first_name` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `last_name` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `email` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `password` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `active` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`person_id`),
  UNIQUE KEY `email` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (1, 'Paxton', 'Hane', 'taylor.jenkins@example.com', 'd38a3104d62401327bc669cf0fe532c6f1acf84d', '1');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (2, 'Oda', 'Donnelly', 'goodwin.eldridge@example.net', '1e0b2d92c328b79817cf66a84266d4f77f04b7ae', '1');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (3, 'Fernando', 'Botsford', 'sallie44@example.org', '5fc13d90fccafbb4dfcda13c0f0d025294584d33', '0');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (4, 'Estel', 'Glover', 'boehm.zander@example.net', '28860c32dc1e2410bde390ac33a9db534bf7d16d', '0');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (5, 'Miguel', 'Windler', 'jalen04@example.com', 'b90473624b5b2402e7dcbc1f5899daf227274318', '1');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (6, 'Cordell', 'Hegmann', 'crona.ephraim@example.net', 'a7ddcb5cec6b993536b98b0dc6bb5520fe1ff21e', '1');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (7, 'Carlos', 'Klocko', 'lucy.ullrich@example.net', 'fe005280cdd75d5cf4b44d5c4f86e1d928ef2cb8', '1');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (8, 'Kameron', 'Cummerata', 'myrna.ledner@example.net', '48048555a29d56d4dde0030b550d734dfad4936b', '0');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (9, 'Royce', 'Hand', 'ivory14@example.com', '8f9fcc2cc1f094758220fff21240411aaa309d13', '0');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (10, 'Emory', 'Runolfsdottir', 'silas.farrell@example.com', '65717213581eb326a814f5ca207369fd3c83dd24', '0');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (11, 'Kayla', 'Sipes', 'bauer@example.org', '359ca34e50f76be8920754a0c3a261c15e44fa03', '0');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (12, 'Soledad', 'Reynolds', 'ogislason@example.com', '2984e32a7ec004ee0449bff00883e2e5edfe3dc7', '0');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (13, 'Heather', 'Sawayn', 'rschoen@example.net', 'e4608014faee134bc4d196ab50f271e7e3067524', '0');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (14, 'Josie', 'Casper', 'mjacobi@example.net', '742ff5136fc3c866b87f727b3a7187936a004c2f', '0');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (15, 'Keely', 'Jacobson', 'renner.alysha@example.com', '44bad21d094e2af0dc8b5c431807cf564330d4e1', '1');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (16, 'Ryan', 'Schamberger', 'mromaguera@example.com', '907bfa25fbaf2f5995076bc9473a8633649aec8e', '0');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (17, 'Maymie', 'Wiegand', 'ressie72@example.org', '07e82ad9792628ffc3f697bf48ee8b0098df6ba8', '0');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (18, 'Horacio', 'Kozey', 'gkuphal@example.org', '9c7b50921ce8188339a4f40e551b87c8e46da591', '0');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (19, 'Meredith', 'Ondricka', 'yschumm@example.com', '3f2e801c7ebd515086a5e1d79a4265f7bca3b9ec', '0');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (20, 'Lue', 'Weimann', 'wunsch.ransom@example.org', '3c9a9e6959312850ed70b7b46debe1891231c2ee', '0');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (21, 'Daisy', 'Kunde', 'dock.veum@example.com', 'ee74ee80a788ea1a9222e67c14ecf73e30396a0c', '1');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (22, 'Missouri', 'Langworth', 'douglas.cordelia@example.net', '0d751c4f605e660a9bccf8110691b4c1b2c8e3af', '0');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (23, 'Ellis', 'Steuber', 'cydney73@example.org', 'd420ab077b564277bf36343e49dc680b853df21d', '1');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (24, 'Jany', 'Kohler', 'junior91@example.org', 'da932171da19c07556970462ece1a37dcea5a6f3', '1');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (25, 'Koby', 'Bins', 'dina27@example.org', 'b4767d9dff3ef2dc9d70b4419e98d6ad0bc20cc4', '1');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (26, 'Tito', 'Brown', 'dickinson.torrey@example.org', 'bff0479c5289a47cfa8434f580c452cebb6e9307', '1');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (27, 'Daphne', 'Murphy', 'owehner@example.org', 'ce0db3c1ad080b9434ec9711b77205df4d47440b', '1');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (28, 'Declan', 'O\'Keefe', 'laura38@example.com', '579ad65e7bc4ed2364b46139a8876ea9720d08e4', '1');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (29, 'Hal', 'Gottlieb', 'fahey.sabrina@example.org', '53920d6e284dc5814d10b52b22429ead6d0450de', '1');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (30, 'Ariane', 'Champlin', 'gregory42@example.org', '28f0848a6798bc41b9205621fda545b3b6ecb75e', '0');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (31, 'Florida', 'Rowe', 'jonathan68@example.net', 'cf43b2b122cd178e9624990e85713bf06d085668', '0');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (32, 'Vaughn', 'Von', 'dorothea00@example.net', 'ab715dee63b677313f27b0c120bebf86aec5802b', '0');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (33, 'Michale', 'Rolfson', 'parisian.alverta@example.org', '863b79a5d378501ad1181101b0781ef8273d5d32', '0');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (34, 'Rachael', 'Gislason', 'anya82@example.com', 'd9e9d98a4184159f0adc555d11d83e32647d5d1c', '1');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (35, 'Myrtie', 'Beer', 'o\'kon.esmeralda@example.com', '704a20138f95193f136ac1b21cc37308bfd47c17', '1');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (36, 'Drake', 'Hayes', 'kbergnaum@example.com', '16f810dfefe42208fbfd9fdaedb0f518b1fbe00a', '1');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (37, 'Anissa', 'Rice', 'fahey.mitchel@example.net', 'c1adc6d02c2899ca4bb8be5f1f662640c4081d04', '1');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (38, 'Garett', 'Eichmann', 'jhalvorson@example.net', '88b84cfcf4e28d80c6a150617ac077bfb2ae817f', '0');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (39, 'Pete', 'Kovacek', 'willms.obie@example.org', 'e75428a0b699356abdf704eecccca1181c8ae065', '1');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (40, 'Aditya', 'Mueller', 'towne.kennith@example.com', '1563a5a2429d2496f9a3b8e94ec9d0bc32e3f8fd', '0');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (41, 'Lucious', 'O\'Keefe', 'hershel.brekke@example.net', 'c8b92289da80cbf0c530daf4cceef4f864a443fd', '1');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (42, 'Tania', 'Von', 'claire61@example.net', 'af2776fb4c89607e6761c78b067c1e3dfebf6d4e', '1');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (43, 'Icie', 'Rolfson', 'caleb03@example.com', 'c1a8c023d70346ff011606dcf3041a24bcc6b1cd', '1');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (44, 'Lionel', 'Lockman', 'bogisich.wilburn@example.com', '13f32e7144b97829b3e5709a817331ad94000757', '1');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (45, 'Dayton', 'Miller', 'stoltenberg.pedro@example.org', '0bb58b9c6c79193a63a38d56531916217ab134a7', '1');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (46, 'Emely', 'Feeney', 'keaton78@example.net', '95231310b4b140b190cdcbc319737b6c14458598', '1');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (47, 'Hulda', 'Durgan', 'xking@example.net', '6224d255153a189d2a9f990ec6ca159f20924467', '1');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (48, 'Marcel', 'Dare', 'sabina.rath@example.net', 'b5e70d9211f12e5e05d8e32d4ec6a3e2f2cfff0a', '0');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (49, 'Cierra', 'Leffler', 'jswift@example.org', '29b55412b2ae474eed817334cd30af2ce15d0206', '0');
INSERT INTO `Person` (`person_id`, `first_name`, `last_name`, `email`, `password`, `active`) VALUES (50, 'Rylee', 'Kuhn', 'amcclure@example.com', '9393a5183762962dead27b9082d68cbe1310ba64', '1');


