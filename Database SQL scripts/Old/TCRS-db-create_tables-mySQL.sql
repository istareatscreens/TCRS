#Create tables

#Main user table
CREATE TABLE Person (
   person_id INT NOT NULL auto_increment PRIMARY KEY, 
   first_name VARCHAR(100) NOT NULL,
   last_name VARCHAR(100) NOT NULL,
   email VARCHAR(255) UNIQUE NOT NULL,
   password VARCHAR(255) NOT NULL,
   active BOOLEAN NOT NULL DEFAULT 0
);

#Refresh Token Table
CREATE TABLE RefreshToken (
	token_id INT auto_increment NOT NULL PRIMARY KEY,
	person_id INT NOT NULL,
	token VARCHAR(200) NOT NULL,
	expiry_date DATETIME NOT NULL,
	FOREIGN KEY (person_id) REFERENCES Person (person_id)
);

#Admin table
CREATE TABLE Client_Admin(
   person_id INT,
   PRIMARY KEY (person_id),
   FOREIGN KEY (person_id) REFERENCES Person (person_id)
   ON DELETE CASCADE
);

CREATE TABLE School(
   school_id INT NOT NULL auto_increment,
   name CHAR(30) NOT NULL,
   phone_no VARCHAR(13),
   PRIMARY KEY (school_id)
);

CREATE TABLE School_Rep(
   person_id INT,
   school_id INT NOT NULL,
   PRIMARY KEY(person_id),
   FOREIGN KEY (person_id) REFERENCES Person(person_id),
   FOREIGN KEY (school_id) REFERENCES School(school_id)
);

CREATE TABLE Police_Dept(
   police_dept_id INT auto_increment NOT NULL,
   manager_id INT,
   name VARCHAR(255) NOT NULL UNIQUE,
   PRIMARY KEY (police_dept_id),
   FOREIGN KEY (manager_id) REFERENCES Person(person_id)
);

CREATE TABLE Highway_Patrol_Officer(
   person_id INT,
   position VARCHAR(255) NOT NULL DEFAULT 'personel',
   PRIMARY KEY(person_id),
police_dept_id INT NOT NULL,
   FOREIGN KEY (person_id) REFERENCES Person(person_id),
FOREIGN KEY (police_dept_id) REFERENCES Police_Dept(police_dept_id )
);

CREATE TABLE Municipality(
   munic_id INT auto_increment NOT NULL,
   manager_id INT,
   name VARCHAR(255) NOT NULL UNIQUE,
   PRIMARY KEY (munic_id),
   FOREIGN KEY (manager_id) REFERENCES Person(person_id)
);

CREATE TABLE Municipal_Officer(
   person_id INT,
   position VARCHAR(255) NOT NULL DEFAULT 'personel',
	munic_id INT NOT NULL,
   PRIMARY KEY(person_id),
   FOREIGN KEY (person_id) REFERENCES Person(person_id),
   FOREIGN KEY (munic_id) REFERENCES Municipality(munic_id)
);

CREATE TABLE Citation_Type(
   citation_type_id INT auto_increment NOT NULL,
   name VARCHAR(255) NOT NULL UNIQUE,
   fine DECIMAL(7,2) NOT NULL,
   training_eligable BOOLEAN DEFAULT 0,
   PRIMARY KEY (citation_type_id)
);

CREATE TABLE Course(
   course_id INT NOT NULL auto_increment,
   type VARCHAR(100) NOT NULL,
   address VARCHAR(255) NOT NULL,
   name VARCHAR(255) NOT NULL,
   scheduled DATETIME NOT NULL,
   price DECIMAL(5,2) NOT NULL,
   description TEXT NOT NULL,
   title VARCHAR(255) NOT NULL,
   instructor VARCHAR(255) NOT NULL,
   capacity INT NOT NULL,
   citation_type_id INT,
   school_id INT,
   PRIMARY KEY(course_id),
   FOREIGN KEY (citation_type_id) REFERENCES Citation_Type(citation_type_id),
   FOREIGN KEY (school_id) REFERENCES School(school_id)
);

CREATE TABLE Insurer(
   insurer_id INT auto_increment NOT NULL,
   name VARCHAR(255) NOT NULL,
   PRIMARY KEY (insurer_id)
);

CREATE TABLE Citizen(
   citizen_id INT NOT NULL auto_increment,
   first_name VARCHAR(255) NOT NULL,
   middle_name VARCHAR(255),
   last_name VARCHAR(255) NOT NULL,
   dob DATE NOT NULL,
   home_address VARCHAR(255) NOT NULL,
   insurer_id INT,
   PRIMARY KEY (citizen_id),
   FOREIGN KEY (insurer_id) REFERENCES Insurer(insurer_id)
);

CREATE TABLE Licence(
   licence_id VARCHAR(255) NOT NULL,
   citizen_id INT,
   expiration_date DATE NOT NULL,
   is_revoked BOOLEAN NOT NULL DEFAULT 0,
   is_suspended BOOLEAN NOT NULL DEFAULT 0,
   licence_class VARCHAR(255) NOT NULL,
   PRIMARY KEY (licence_id, citizen_id),
   FOREIGN KEY (citizen_id) REFERENCES Citizen(citizen_id)
);


Create TABLE Citation(
   citation_id INT auto_increment NOT NULL,
   citation_number VARCHAR(32) NOT NULL UNIQUE,
   date_recieved DATETIME NOT NULL,
   citation_type_id INT,
   officer_id INT,
   FOREIGN KEY (officer_id) REFERENCES Person(person_id),
   PRIMARY KEY (citation_id),
   FOREIGN KEY (citation_type_id) REFERENCES Citation_Type(citation_type_id)
);

CREATE TABLE Registration(
   citizen_id INT,
   course_id INT,
   citation_id INT,
   passed BOOLEAN NOT NULL DEFAULT 0,
   PRIMARY KEY (citizen_id, course_id, citation_id),
   FOREIGN KEY (citizen_id) REFERENCES Citizen(citizen_id),
   FOREIGN KEY (course_id) REFERENCES Course(course_id),
   FOREIGN KEY (citation_id) REFERENCES Citation(citation_id)
);

CREATE TABLE Vehicle(
   vehicle_id INT auto_increment NOT NULL,
   vin VARCHAR(30) NOT NULL,
   name CHAR(30) NOT NULL,
   stolen BOOLEAN NOT NULL DEFAULT 0,
   make CHAR(30) NOT NULL,
   registered BOOLEAN NOT NULL DEFAULT 0,
   model CHAR(30) NOT NULL,
   year_made SMALLINT NOT NULL,
   citizen_id INT,
   insurer_id INT,
   FOREIGN KEY (citizen_id) REFERENCES Citizen(citizen_id),
   FOREIGN KEY (insurer_id) REFERENCES Insurer(insurer_id),
   PRIMARY KEY (vehicle_id)
);

CREATE TABLE Vehicle_Record(
   vehicle_id INT, 
   citation_id INT,
   PRIMARY KEY (vehicle_id, citation_id),
   FOREIGN KEY (citation_id) REFERENCES Citation(citation_id),
   FOREIGN KEY (vehicle_id) REFERENCES Vehicle(vehicle_id)
);

CREATE TABLE Driver_Record(
   citizen_id INT, 
   citation_id INT,
   PRIMARY KEY (citizen_id, citation_id),
   FOREIGN KEY (citation_id) REFERENCES Citation(citation_id),
   FOREIGN KEY (citizen_id) REFERENCES Citizen(citizen_id)
);


CREATE TABLE Licence_Plate(
   plate_number VARCHAR(255) NOT NULL,
   vehicle_id INT,
   expired BOOLEAN NOT NULL DEFAULT 1,
   PRIMARY KEY (vehicle_id, plate_number),
   FOREIGN KEY (vehicle_id) REFERENCES Vehicle(vehicle_id)
);

CREATE TABLE Payment(
   citation_id INT,
   payment DECIMAL(7,2) NOT NULL,
   payment_date DATETIME NOT NULL,
   made_by VARCHAR(255) NOT NULL,
   payment_method VARCHAR(255) NOT NULL,
   FOREIGN KEY (citation_id) REFERENCES Citation(citation_id)
);

CREATE TABLE Wanted(
   wanted_id INT NOT NULL auto_increment,
   reference_no VARCHAR(32) NOT NULL UNIQUE,
   dangerous BOOLEAN NOT NULL DEFAULT 1,
   crime VARCHAR(255) NOT NULL,
   PRIMARY KEY (wanted_id)
);

CREATE TABLE Wanted_Citizen(
   citizen_id INT,
   wanted_id INT,
   PRIMARY KEY (citizen_id, wanted_id),
   FOREIGN KEY (wanted_id) REFERENCES Wanted(wanted_id),
   FOREIGN KEY (citizen_id) REFERENCES Citizen(citizen_id)
);

CREATE TABLE Wanted_Vehicle(
   vehicle_id INT,
   wanted_id INT,
   PRIMARY KEY (vehicle_id, wanted_id),
   FOREIGN KEY (vehicle_id) REFERENCES Vehicle(vehicle_id),
   FOREIGN KEY (wanted_id) REFERENCES Wanted(wanted_id)
);