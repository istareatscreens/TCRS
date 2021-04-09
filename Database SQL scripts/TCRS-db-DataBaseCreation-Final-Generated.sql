-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema tcrs-db
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema tcrs-db
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `tcrs-db` DEFAULT CHARACTER SET latin1 ;
USE `tcrs-db` ;

-- -----------------------------------------------------
-- Table `tcrs-db`.`person`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcrs-db`.`person` (
  `person_id` INT(11) NOT NULL AUTO_INCREMENT,
  `first_name` VARCHAR(100) NOT NULL,
  `last_name` VARCHAR(100) NOT NULL,
  `email` VARCHAR(255) NOT NULL,
  `password` VARCHAR(255) NOT NULL,
  `active` TINYINT(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`person_id`),
  UNIQUE INDEX `email` (`email` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 56
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `tcrs-db`.`citation_type`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcrs-db`.`citation_type` (
  `citation_type_id` INT(11) NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(255) NOT NULL,
  `fine` DECIMAL(7,2) NOT NULL,
  `training_eligable` TINYINT(1) NULL DEFAULT '0',
  `due_date_month` INT(2) NOT NULL DEFAULT '3',
  PRIMARY KEY (`citation_type_id`),
  UNIQUE INDEX `name` (`name` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 15
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `tcrs-db`.`citation`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcrs-db`.`citation` (
  `citation_id` INT(11) NOT NULL AUTO_INCREMENT,
  `citation_number` VARCHAR(36) NOT NULL,
  `date_recieved` DATETIME NOT NULL,
  `citation_type_id` INT(11) NULL DEFAULT NULL,
  `officer_id` INT(11) NULL DEFAULT NULL,
  `is_resolved` TINYINT(4) NULL DEFAULT '0',
  PRIMARY KEY (`citation_id`),
  UNIQUE INDEX `citation_number` (`citation_number` ASC) VISIBLE,
  INDEX `officer_id` (`officer_id` ASC) VISIBLE,
  INDEX `citation_type_id` (`citation_type_id` ASC) VISIBLE,
  CONSTRAINT `Citation_ibfk_1`
    FOREIGN KEY (`officer_id`)
    REFERENCES `tcrs-db`.`person` (`person_id`),
  CONSTRAINT `Citation_ibfk_2`
    FOREIGN KEY (`citation_type_id`)
    REFERENCES `tcrs-db`.`citation_type` (`citation_type_id`))
ENGINE = InnoDB
AUTO_INCREMENT = 1116
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `tcrs-db`.`insurer`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcrs-db`.`insurer` (
  `insurer_id` INT(11) NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(255) NOT NULL,
  PRIMARY KEY (`insurer_id`))
ENGINE = InnoDB
AUTO_INCREMENT = 21
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `tcrs-db`.`citizen`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcrs-db`.`citizen` (
  `citizen_id` INT(11) NOT NULL AUTO_INCREMENT,
  `first_name` VARCHAR(255) NOT NULL,
  `middle_name` VARCHAR(255) NULL DEFAULT NULL,
  `last_name` VARCHAR(255) NOT NULL,
  `dob` DATE NOT NULL,
  `home_address` VARCHAR(255) NOT NULL,
  `insurer_id` INT(11) NULL DEFAULT NULL,
  PRIMARY KEY (`citizen_id`),
  INDEX `insurer_id` (`insurer_id` ASC) VISIBLE,
  CONSTRAINT `Citizen_ibfk_1`
    FOREIGN KEY (`insurer_id`)
    REFERENCES `tcrs-db`.`insurer` (`insurer_id`))
ENGINE = InnoDB
AUTO_INCREMENT = 1001
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `tcrs-db`.`client_admin`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcrs-db`.`client_admin` (
  `person_id` INT(11) NOT NULL,
  PRIMARY KEY (`person_id`),
  CONSTRAINT `Client_Admin_ibfk_1`
    FOREIGN KEY (`person_id`)
    REFERENCES `tcrs-db`.`person` (`person_id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `tcrs-db`.`school`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcrs-db`.`school` (
  `school_id` INT(11) NOT NULL AUTO_INCREMENT,
  `name` CHAR(30) NOT NULL,
  `phone_no` VARCHAR(13) NULL DEFAULT NULL,
  PRIMARY KEY (`school_id`))
ENGINE = InnoDB
AUTO_INCREMENT = 11
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `tcrs-db`.`course`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcrs-db`.`course` (
  `course_id` INT(11) NOT NULL AUTO_INCREMENT,
  `type` VARCHAR(100) NOT NULL,
  `address` VARCHAR(255) NOT NULL,
  `name` VARCHAR(255) NOT NULL,
  `scheduled` DATETIME NOT NULL,
  `price` DECIMAL(5,2) NOT NULL,
  `description` TEXT NOT NULL,
  `title` VARCHAR(255) NOT NULL,
  `instructor` VARCHAR(255) NOT NULL,
  `capacity` INT(11) NOT NULL,
  `citation_type_id` INT(11) NULL DEFAULT NULL,
  `school_id` INT(11) NULL DEFAULT NULL,
  `is_full` TINYINT(4) NOT NULL DEFAULT '0',
  `evaluated` TINYINT(4) NOT NULL DEFAULT '0',
  PRIMARY KEY (`course_id`),
  INDEX `citation_type_id` (`citation_type_id` ASC) VISIBLE,
  INDEX `school_id` (`school_id` ASC) VISIBLE,
  CONSTRAINT `Course_ibfk_1`
    FOREIGN KEY (`citation_type_id`)
    REFERENCES `tcrs-db`.`citation_type` (`citation_type_id`),
  CONSTRAINT `Course_ibfk_2`
    FOREIGN KEY (`school_id`)
    REFERENCES `tcrs-db`.`school` (`school_id`))
ENGINE = InnoDB
AUTO_INCREMENT = 119
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `tcrs-db`.`driver_record`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcrs-db`.`driver_record` (
  `citizen_id` INT(11) NOT NULL,
  `citation_id` INT(11) NOT NULL,
  PRIMARY KEY (`citizen_id`, `citation_id`),
  INDEX `citation_id` (`citation_id` ASC) VISIBLE,
  CONSTRAINT `Driver_Record_ibfk_1`
    FOREIGN KEY (`citation_id`)
    REFERENCES `tcrs-db`.`citation` (`citation_id`),
  CONSTRAINT `Driver_Record_ibfk_2`
    FOREIGN KEY (`citizen_id`)
    REFERENCES `tcrs-db`.`citizen` (`citizen_id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `tcrs-db`.`police_dept`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcrs-db`.`police_dept` (
  `police_dept_id` INT(11) NOT NULL AUTO_INCREMENT,
  `manager_id` INT(11) NULL DEFAULT NULL,
  `name` VARCHAR(255) NOT NULL,
  PRIMARY KEY (`police_dept_id`),
  UNIQUE INDEX `name` (`name` ASC) VISIBLE,
  INDEX `manager_id` (`manager_id` ASC) VISIBLE,
  CONSTRAINT `Police_Dept_ibfk_1`
    FOREIGN KEY (`manager_id`)
    REFERENCES `tcrs-db`.`person` (`person_id`))
ENGINE = InnoDB
AUTO_INCREMENT = 5
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `tcrs-db`.`highway_patrol_officer`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcrs-db`.`highway_patrol_officer` (
  `person_id` INT(11) NOT NULL,
  `position` VARCHAR(255) NOT NULL DEFAULT 'personel',
  `police_dept_id` INT(11) NOT NULL,
  PRIMARY KEY (`person_id`),
  INDEX `police_dept_id` (`police_dept_id` ASC) VISIBLE,
  CONSTRAINT `Highway_Patrol_Officer_ibfk_1`
    FOREIGN KEY (`person_id`)
    REFERENCES `tcrs-db`.`person` (`person_id`),
  CONSTRAINT `Highway_Patrol_Officer_ibfk_2`
    FOREIGN KEY (`police_dept_id`)
    REFERENCES `tcrs-db`.`police_dept` (`police_dept_id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `tcrs-db`.`license`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcrs-db`.`license` (
  `license_id` VARCHAR(255) NOT NULL,
  `citizen_id` INT(11) NOT NULL,
  `expiration_date` DATE NOT NULL,
  `is_revoked` TINYINT(1) NOT NULL DEFAULT '0',
  `is_suspended` TINYINT(1) NOT NULL DEFAULT '0',
  `license_class` VARCHAR(255) NOT NULL,
  PRIMARY KEY (`license_id`, `citizen_id`),
  INDEX `citizen_id` (`citizen_id` ASC) VISIBLE,
  CONSTRAINT `Licence_ibfk_1`
    FOREIGN KEY (`citizen_id`)
    REFERENCES `tcrs-db`.`citizen` (`citizen_id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `tcrs-db`.`vehicle`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcrs-db`.`vehicle` (
  `vehicle_id` INT(11) NOT NULL AUTO_INCREMENT,
  `vin` VARCHAR(30) NOT NULL,
  `name` CHAR(30) NOT NULL,
  `stolen` TINYINT(1) NOT NULL DEFAULT '0',
  `make` CHAR(30) NOT NULL,
  `registered` TINYINT(1) NOT NULL DEFAULT '0',
  `model` CHAR(30) NOT NULL,
  `year_made` SMALLINT(6) NOT NULL,
  `citizen_id` INT(11) NULL DEFAULT NULL,
  `insurer_id` INT(11) NULL DEFAULT NULL,
  PRIMARY KEY (`vehicle_id`),
  INDEX `citizen_id` (`citizen_id` ASC) VISIBLE,
  INDEX `insurer_id` (`insurer_id` ASC) VISIBLE,
  CONSTRAINT `Vehicle_ibfk_1`
    FOREIGN KEY (`citizen_id`)
    REFERENCES `tcrs-db`.`citizen` (`citizen_id`),
  CONSTRAINT `Vehicle_ibfk_2`
    FOREIGN KEY (`insurer_id`)
    REFERENCES `tcrs-db`.`insurer` (`insurer_id`))
ENGINE = InnoDB
AUTO_INCREMENT = 1001
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `tcrs-db`.`license_plate`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcrs-db`.`license_plate` (
  `plate_number` VARCHAR(255) NOT NULL,
  `vehicle_id` INT(11) NOT NULL,
  `expired` TINYINT(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`vehicle_id`, `plate_number`),
  CONSTRAINT `Licence_Plate_ibfk_1`
    FOREIGN KEY (`vehicle_id`)
    REFERENCES `tcrs-db`.`vehicle` (`vehicle_id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `tcrs-db`.`municipality`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcrs-db`.`municipality` (
  `munic_id` INT(11) NOT NULL AUTO_INCREMENT,
  `manager_id` INT(11) NULL DEFAULT NULL,
  `name` VARCHAR(255) NOT NULL,
  PRIMARY KEY (`munic_id`),
  UNIQUE INDEX `name` (`name` ASC) VISIBLE,
  INDEX `manager_id` (`manager_id` ASC) VISIBLE,
  CONSTRAINT `Municipality_ibfk_1`
    FOREIGN KEY (`manager_id`)
    REFERENCES `tcrs-db`.`person` (`person_id`))
ENGINE = InnoDB
AUTO_INCREMENT = 5
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `tcrs-db`.`municipal_officer`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcrs-db`.`municipal_officer` (
  `person_id` INT(11) NOT NULL,
  `position` VARCHAR(255) NOT NULL DEFAULT 'personel',
  `munic_id` INT(11) NOT NULL,
  PRIMARY KEY (`person_id`),
  INDEX `munic_id` (`munic_id` ASC) VISIBLE,
  CONSTRAINT `Municipal_Officer_ibfk_1`
    FOREIGN KEY (`person_id`)
    REFERENCES `tcrs-db`.`person` (`person_id`),
  CONSTRAINT `Municipal_Officer_ibfk_2`
    FOREIGN KEY (`munic_id`)
    REFERENCES `tcrs-db`.`municipality` (`munic_id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `tcrs-db`.`payment`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcrs-db`.`payment` (
  `citation_id` INT(11) NULL DEFAULT NULL,
  `payment` DECIMAL(7,2) NOT NULL,
  `payment_date` DATETIME NOT NULL,
  `made_by` VARCHAR(255) NOT NULL,
  `payment_method` VARCHAR(255) NOT NULL,
  INDEX `citation_id` (`citation_id` ASC) VISIBLE,
  CONSTRAINT `Payment_ibfk_1`
    FOREIGN KEY (`citation_id`)
    REFERENCES `tcrs-db`.`citation` (`citation_id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `tcrs-db`.`refreshtoken`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcrs-db`.`refreshtoken` (
  `token_id` INT(11) NOT NULL AUTO_INCREMENT,
  `person_id` INT(11) NOT NULL,
  `token` VARCHAR(200) NOT NULL,
  `expiry_date` DATETIME NOT NULL,
  PRIMARY KEY (`token_id`),
  INDEX `person_id` (`person_id` ASC) VISIBLE,
  CONSTRAINT `refreshtoken_ibfk_1`
    FOREIGN KEY (`person_id`)
    REFERENCES `tcrs-db`.`person` (`person_id`))
ENGINE = InnoDB
AUTO_INCREMENT = 710
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `tcrs-db`.`registration`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcrs-db`.`registration` (
  `citizen_id` INT(11) NOT NULL,
  `course_id` INT(11) NOT NULL,
  `citation_id` INT(11) NOT NULL,
  `passed` TINYINT(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`citizen_id`, `citation_id`, `course_id`),
  INDEX `course_id` (`course_id` ASC) VISIBLE,
  INDEX `citation_id` (`citation_id` ASC) VISIBLE,
  CONSTRAINT `Registration_ibfk_1`
    FOREIGN KEY (`citizen_id`)
    REFERENCES `tcrs-db`.`citizen` (`citizen_id`),
  CONSTRAINT `Registration_ibfk_2`
    FOREIGN KEY (`course_id`)
    REFERENCES `tcrs-db`.`course` (`course_id`),
  CONSTRAINT `Registration_ibfk_3`
    FOREIGN KEY (`citation_id`)
    REFERENCES `tcrs-db`.`citation` (`citation_id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `tcrs-db`.`school_rep`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcrs-db`.`school_rep` (
  `person_id` INT(11) NOT NULL,
  `school_id` INT(11) NOT NULL,
  PRIMARY KEY (`person_id`),
  INDEX `school_id` (`school_id` ASC) VISIBLE,
  CONSTRAINT `School_Rep_ibfk_1`
    FOREIGN KEY (`person_id`)
    REFERENCES `tcrs-db`.`person` (`person_id`),
  CONSTRAINT `School_Rep_ibfk_2`
    FOREIGN KEY (`school_id`)
    REFERENCES `tcrs-db`.`school` (`school_id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `tcrs-db`.`vehicle_record`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcrs-db`.`vehicle_record` (
  `vehicle_id` INT(11) NOT NULL,
  `citation_id` INT(11) NOT NULL,
  PRIMARY KEY (`vehicle_id`, `citation_id`),
  INDEX `citation_id` (`citation_id` ASC) VISIBLE,
  CONSTRAINT `Vehicle_Record_ibfk_1`
    FOREIGN KEY (`citation_id`)
    REFERENCES `tcrs-db`.`citation` (`citation_id`),
  CONSTRAINT `Vehicle_Record_ibfk_2`
    FOREIGN KEY (`vehicle_id`)
    REFERENCES `tcrs-db`.`vehicle` (`vehicle_id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `tcrs-db`.`wanted`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcrs-db`.`wanted` (
  `wanted_id` INT(11) NOT NULL AUTO_INCREMENT,
  `reference_no` VARCHAR(36) NOT NULL,
  `dangerous` TINYINT(1) NOT NULL DEFAULT '1',
  `crime` VARCHAR(255) NOT NULL,
  `active_status` TINYINT(4) NOT NULL DEFAULT '1',
  PRIMARY KEY (`wanted_id`),
  UNIQUE INDEX `reference_no` (`reference_no` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 115
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `tcrs-db`.`wanted_citizen`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcrs-db`.`wanted_citizen` (
  `citizen_id` INT(11) NOT NULL,
  `wanted_id` INT(11) NOT NULL,
  PRIMARY KEY (`citizen_id`, `wanted_id`),
  INDEX `wanted_id` (`wanted_id` ASC) VISIBLE,
  CONSTRAINT `Wanted_Citizen_ibfk_1`
    FOREIGN KEY (`wanted_id`)
    REFERENCES `tcrs-db`.`wanted` (`wanted_id`),
  CONSTRAINT `Wanted_Citizen_ibfk_2`
    FOREIGN KEY (`citizen_id`)
    REFERENCES `tcrs-db`.`citizen` (`citizen_id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `tcrs-db`.`wanted_vehicle`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcrs-db`.`wanted_vehicle` (
  `vehicle_id` INT(11) NOT NULL,
  `wanted_id` INT(11) NOT NULL,
  PRIMARY KEY (`vehicle_id`, `wanted_id`),
  INDEX `wanted_id` (`wanted_id` ASC) VISIBLE,
  CONSTRAINT `Wanted_Vehicle_ibfk_1`
    FOREIGN KEY (`vehicle_id`)
    REFERENCES `tcrs-db`.`vehicle` (`vehicle_id`),
  CONSTRAINT `Wanted_Vehicle_ibfk_2`
    FOREIGN KEY (`wanted_id`)
    REFERENCES `tcrs-db`.`wanted` (`wanted_id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
