#Delete tables

#Users
DROP TABLE Person; #1
DROP TABLE Client_Admin; #2
DROP TABLE School_Rep; #3
DROP TABLE Municipal_Officer; #4
DROP TABLE Highway_Patrol_Officer; #5
DROP TABLE Municipality; #6
DROP TABLE Police_Dept; #7
DROP TABLE School; #8
DROP TABLE Course; #9
DROP TABLE Registration; #10
DROP TABLE Citation; #11
DROP TABLE Citation_Type; #12
DROP TABLE Vehicle_Record; #13
DROP TABLE Driver_Record; #14
DROP TABLE Vehicle; #15
DROP TABLE Licence_Plate; #16
DROP TABLE Payment; #17
DROP TABLE Wanted_Vehicle; #18
DROP TABLE Wanted_Citizen; #19
DROP TABLE Wanted; #20
DROP TABLE Citizen; #21
DROP TABLE Insurer; #22
DROP TABLE Licence; #23

#Create tables

#Main user table
CREATE TABLE Person (
   person_id INT NOT NULL AUTO_INCREMENT,
   first_name VARCHAR(100) NOT NULL,
   last_name VARCHAR(100) NOT NULL,
   email VARCHAR(255) UNIQUE NOT NULL,
   password VARCHAR(255) NOT NULL,
   active BOOLEAN NOT NULL DEFAULT DEFAULT 0
   PRIMARY KEY(student_id)
);

#Admin table
CREATE TABLE Client_Admin(
   person_id,
   PRIMARY KEY(student_id),
   FOREIGN KEY (person_id) REFERENCES Person(person_id)
   ON DELETE CASCADE
);

CREATE TABLE School(
   school_id INT NOT NULL AUTO_INCREMENT,
   name CHAR(30) NOT NULL,
   phone_no VARCHAR(13),
   PRIMARY KEY (school_id)
);

CREATE TABLE School_Rep(
   person_id,
   school_id NOT NULL,
   PRIMARY KEY(person_id),
   FOREIGN KEY (person_id) REFERENCES Person(person_id)
   ON DELETE RESTRICT,
   FOREIGN KEY (school_id) REFERENCES School(school_id)
   ON DELETE RESTRICT
);

CREATE TABLE Highway_Patrol_Officer(
   person_id,
   position NOT NULL DEFAULT "personel"
   PRIMARY KEY(person_id),
   FOREIGN KEY (person_id) REFERENCES Person(person_id)
   ON DELETE RESTRICT
);

CREATE TABLE Municipal_Officer(
   person_id,
   position NOT NULL DEFAULT "personel"
   PRIMARY KEY(person_id),
   FOREIGN KEY (person_id) REFERENCES Person(person_id)
   ON DELETE RESTRICT
);

CREATE TABLE Municipality(
   munic_id INT AUTO_INCREMENT NOT NULL,
   manager_id,
   name VARCHAR(255) NOT NULL UNIQUE,
   PRIMARY KEY (munic_id),
   FOREIGN KEY (manager_id) REFERENCES Person(person_id)
);

CREATE TABLE Police_Dept(
   police_dept_id INT AUTO_INCREMENT NOT NULL,
   manager_id,
   name VARCHAR(255) NOT NULL UNIQUE,
   PRIMARY KEY (police_dept_id),
   FOREIGN KEY (manager_id) REFERENCES Person(person_id)
);

CREATE TABLE Course(
   course_id INT NOT NULL AUTO_INCREMENT,
   type VARCHAR(100) NOT NULL,
   address VARCHAR(255) NOT NULL,
   name VARCHAR(255) NOT NULL,
   scheduled DATETIME NOT NULL,
   price DECIMAL(5,2) NOT NULL,
   description TEXT NOT NULL,
   title VARCHAR(255) NOT NULL,
   instructor VARCHAR(255) NOT NULL,
   capacity INT(4),
   citation_type_id,
   school_id,
   PRIMARY KEY(course_id),
   FOREIGN KEY (citation_type_id) REFERENCES Citation_Type(citation_type_id),
   FOREIGN KEY (school_id) REFERENCES School(school_id)
);

CREATE TABLE Registration(
   citizen_id,
   course_id,
   citation_id,
   passed BOOLEAN NOT NULL DEFAULT 0,
   PRIMARY KEY (citizen_id, class_id, citation_id),
   FOREIGN KEY (citizen_id) REFERENCES Citizen(citizen_id),
   FOREIGN KEY (course_id) REFERENCES Course(class_id),
   FOREIGN KEY (citation_id) REFERENCES Citation(citation_id)
);

Create TABLE Citation(
   citation_id INT AUTO_INCREMENT NOT NULL,
   citation_number VARCHAR(32) NOT NULL UNIQUE,
   date_recieved DATETIME NOT NULL,
   citation_type_id,
   officer_id,
   FOREIGN KEY (officer_id) REFERENCES Person(person_id)
   PRIMARY KEY (citation_id),
   FOREIGN KEY (citation_type_id) REFERENCES Citation_Type(citation_type_id)
);

CREATE TABLE Citation_Type(
   citation_type_id INT AUTO_INCREMENT NOT NULL,
   name VARCHAR(255) NOT NULL UNIQUE,
   fine DECIMAL(7,2) NOT NULL,
   training_eligable BOOLEAN DEFAULT 0,
   PRIMARY KEY (citation_type_id)
);

CREATE TABLE Vehicle_Record(
   vehicle_id, 
   citation_id,
   PRIMARY KEY (vehicle_id, citation_id),
   FOREIGN KEY (citation_id) REFERENCES Citation(citation_id)
   FOREIGN KEY (vehicle_id) REFERENCES Vehicle(vehicle_id)
);

CREATE TABLE Driver_Record(
   citizen_id, 
   citation_id,
   PRIMARY KEY (citizen_id, citation_id),
   FOREIGN KEY (citation_id) REFERENCES Citation(citation_id)
   FOREIGN KEY (citizen_id) REFERENCES Citizen(citizen_id),
);

CREATE TABLE Vehicle(
   vehicle_id INT AUTO_INCREMENT NOT NULL,
   vin VARCHAR(30) NOT NULL,
   name CHAR(30) NOT NULL,
   stolen BOOLEAN NOT NULL DEFAULT 0,
   make CHAR(30) NOT NULL,
   registered BOOLEAN NOT NULL DEFAULT 0,
   model CHAR(30) NOT NULL,
   year_made YEAR NOT NULL,
   citizen_id,
   insurer_id,
   FOREIGN KEY (citizen_id) REFERENCES Citizen(citizen_id),
   FOREIGN KEY (insurer_id) REFERENCES Insurer(insurer_id),
   PRIMARY KEY (vehicle_id)
);

CREATE TABLE Licence_Plate(
   plate_number VARCHAR(255) NOT NULL,
   vehicle_id,
   expired BOOLEAN NOT NULL DEFAULT 1,
   PRIMARY KEY (vehicle_id, plate_number),
   FOREIGN KEY (vehicle_id) REFERENCES Vehicle(vehicle_id)
);

CREATE TABLE Payment(
   citation_id,
   payment DECIMAL(7,2) NOT NULL,
   payment_date DATETIME NOT NULL,
   made_by VARCHAR(255) NOT NULL,
   payment_method VARCHAR(255) NOT NULL,
   FOREIGN KEY (citation_id) REFERENCES Citation(citation_id)
);

CREATE TABLE Wanted_Vehicle(
   vehicle_id,
   wanted_id,
   PRIMARY KEY (vehicle_id, wanted_id),
   FOREIGN KEY (vehicle_id) REFERENCES Vehicle(vehicle_id)
   FOREIGN KEY (wanted_id) REFERENCES Wanted(wanted_id)
);

CREATE TABLE Wanted_Citizen(
   citizen_id,
   wanted_id,
   PRIMARY KEY (citizen_id, wanted_id),
   FOREIGN KEY (wanted_id) REFERENCES Wanted(wanted_id),
   FOREIGN KEY (citizen_id) REFERENCES Citizen(citizen_id)
);

CREATE TABLE Wanted(
   wanted_id INT NOT NULL AUTO_INCREMENT,
   reference_no VARCHAR(32) NOT NULL,
   dangerous BOOLEAN NOT NULL DEFAULT 1,
   crime VARCHAR(255) NOT NULL,
   PRIMARY KEY (wanted_id, reference_no)
);

CREATE TABLE Citizen(
   citizen_id INT NOT NULL AUTO_INCREMENT,
   first_name VARCHAR(255) NOT NULL,
   middle_name VARCHAR(255),
   last_name VARCHAR(255) NOT NULL,
   dob DATE NOT NULL,
   insurer_id,
   licence_id,
   PRIMARY KEY (citizen_id),
   FOREIGN KEY (insurer_id) REFERENCES Insurer(insurer_id),
   FOREIGN KEY (licence_id) REFERENCES Licence(licence_id)
);

CREATE TABLE Insurer(
   insurer_id INT AUTO_INCREMENT NOT NULL,
   name VARCHAR(255) NOT NULL
);

CREATE TABLE Licence(
   licence_id VARCHAR(255) NOT NULL,
   citizen_id,
   expiration_date DATE NOT NULL,
   is_revoked BOOLEAN NOT NULL DEFAULT 0,
   is_suspended BOOLEAN NOT NULL DEFAULT 0,
   licence_class VARCHAR(255) NOT NULL,
   PRIMARY KEY (licence_id, citizen_id),
   FOREIGN KEY (citizen_id) REFERENCES Citizen(citizen_id),
);
