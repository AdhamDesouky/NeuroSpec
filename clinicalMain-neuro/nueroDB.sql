CREATE DATABASE Neuro;

USE Neuro;

CREATE TABLE User (
	FirstName varchar(30) not null,
    Lastname varchar (30) not null,
    UserID int not null primary key,
    Gender boolean,
    Password text,
	HireDate date,
    Birthdate date,
    Address text,
    PhoneNumber varchar(12),
    Email varchar(100),
    NationalID varchar(30)    
);

CREATE TABLE appointmentType(
	typeID int,
    typeName varchar(255),
    typeDescription Text,
    timeInMinutes int,
    cost double,
    PRIMARY KEY (typeID)
);



CREATE TABLE AttendanceRecord (
	recordID int NOT NULL PRIMARY KEY,
	timeStamp DATE,
	userID int,
	present BOOLEAN,
    FOREIGN KEY (userID) REFERENCES User(userID) ON DELETE CASCADE
);



CREATE TABLE ChatGroup (
    chatGroupID INT NOT NULL ,
    chatGroupName VARCHAR(100),
    PRIMARY KEY (chatGroupID)
);

CREATE TABLE scanTest (
    scanTestID INT NOT NULL,
    scanTestName VARCHAR(100) NOT NULL,
    recommendedLab VARCHAR(255),
    notes TEXT,
    PRIMARY KEY (scanTestID)
);



CREATE TABLE ChatRoom (
    chatRoomID INT NOT NULL ,
    firstUserID int NOT NULL,
    secondUserID int NOT NULL,
    chatRoomName VARCHAR(100),
    LastVisit datetime,
    PRIMARY KEY (chatRoomID),
    FOREIGN KEY (firstUserID) REFERENCES User(userID) ON DELETE CASCADE,
    FOREIGN KEY (secondUserID) REFERENCES User(userID) ON DELETE CASCADE
    );
    
    CREATE TABLE ChatMessage (
    messageID INT NOT NULL,
    senderID int NOT NULL,
    chatRoomID INT NOT NULL,
    messageContent TEXT,
    timeStamp datetime,
    PRIMARY KEY (messageID),
    FOREIGN KEY (senderID) REFERENCES User(userID) ON DELETE CASCADE,
    FOREIGN KEY (chatRoomID) REFERENCES ChatRoom(chatRoomID) ON DELETE CASCADE
	);

CREATE TABLE Patient (
    PatientID INT NOT NULL,
    FirstName VARCHAR(30) NOT NULL,
    LastName VARCHAR(30) NOT NULL,
    Password VARCHAR(30) NOT NULL,
    Birthdate DATE,
    Gender BOOLEAN,
    PhoneNumber VARCHAR(12),
    Email VARCHAR(100),
    Address TEXT,
    Referred BOOLEAN,
    PreviouslyTreated BOOLEAN,
    Height DECIMAL(5,2),
    Weight DECIMAL(5,2),
    DueAmount DECIMAL(10,2),
    DoctorID int,
    ReferringName TEXT,
    ReferringPN TEXT,
    
	FOREIGN KEY (doctorID) REFERENCES User(userID),
	
    PRIMARY KEY (patientID)
);

CREATE TABLE Ontology(
	ID varchar(50) not null,
	Name Text not null,
	Def Text,
	Urls Text,
	Synonyms Text,
	Parent Text
);



CREATE TABLE ChatGroupRelation (
    chatGroupID INT NOT NULL,
    userID int NOT NULL,
    FOREIGN KEY (chatGroupID) REFERENCES ChatGroup(chatGroupID),
    FOREIGN KEY (userID) REFERENCES User(userID)
);



CREATE TABLE DrugRelation (
    drugRelationID INT NOT NULL,
    patientID INT NOT NULL,
    drugName VARCHAR(100) NOT NULL,
    PRIMARY KEY (drugRelationID),
    FOREIGN KEY (patientID) REFERENCES Patient(patientID)
);


CREATE TABLE Visit (
    visitID INT NOT NULL,
    doctorID int not null,
    patientID INT NOT NULL,
    AppointmentTypeID int,
    height double,
    weight double,
    timeStamp DATETIME,
    therapistNotes TEXT,
    isDone boolean,
    PRIMARY KEY (visitID),
    FOREIGN KEY (doctorID) REFERENCES User(userID),
    FOREIGN KEY (AppointmentTypeID) REFERENCES appointmentType(typeID) ON DELETE SET NULL ON UPDATE CASCADE,
    FOREIGN KEY (patientID) REFERENCES Patient(patientID)
    );

CREATE TABLE Prescription (
    prescriptionID INT NOT NULL,
    timeStamp DATETIME,
    patientID INT NOT NULL,
    doctorID int NOT NULL,
    visitID INT,
    PRIMARY KEY (prescriptionID),
    FOREIGN KEY (patientID) REFERENCES Patient(patientID),
    FOREIGN KEY (doctorID) REFERENCES User(userID),
    FOREIGN KEY (visitID) REFERENCES Visit(visitID)
);

CREATE TABLE IssueScan (
    issueID INT NOT NULL,
    scanTestID INT NOT NULL,
    prescriptionID INT NOT NULL,
    patientID INT NOT NULL,
    notes TEXT,
    PRIMARY KEY (issueID),
    FOREIGN KEY (scanTestID) REFERENCES scanTest(scanTestID),
    FOREIGN KEY (prescriptionID) REFERENCES Prescription(prescriptionID),
    FOREIGN KEY (patientID) REFERENCES Patient(patientID)
);

CREATE TABLE MedicalRecord (
    recordID INT NOT NULL,
    type TEXT,
    timeStamp DATETIME,
    report TEXT,
    images text, 				
    patientID INT,
    doctorNotes TEXT,
    PRIMARY KEY (recordID),
    FOREIGN KEY (patientID) REFERENCES Patient(patientID)
);

CREATE TABLE evaluationTest (
    testID INT NOT NULL,
    testName varchar(255),
    testDescription TEXT,
    PRIMARY KEY (testID)
);

CREATE TABLE testFeedBack (
    testFeedBackID INT NOT NULL,
    severity INT NOT NULL,
    visitID INT,
    patientID INT,
    testID INT,
    notes TEXT, 
    timeStamp DATETIME,
    PRIMARY KEY (testFeedBackID),
    FOREIGN KEY (patientID) REFERENCES Patient(patientID),
    FOREIGN KEY (visitID) REFERENCES Visit(visitID),
    FOREIGN KEY (testID) REFERENCES evaluationTest(testID)
);


CREATE TABLE patientChronicRelation (
    patientID INT NOT NULL,
    chronicID INT NOT NULL,
    FOREIGN KEY (patientID) REFERENCES Patient(patientID) ON DELETE CASCADE
    );

    
CREATE TABLE userWorkDayRelation (
    userID INT NOT NULL,
    workDay INT NOT NULL,
    FOREIGN KEY (userID) REFERENCES user(userID) ON DELETE CASCADE
    );
    
CREATE TABLE payment(
	paymentID INT NOT NULL,
    amount double,
    patientID int,
    doctorID int,
    timestamp datetime,
    PRIMARY KEY (paymentID),
    FOREIGN KEY (doctorID) REFERENCES User(userID) ON UPDATE CASCADE ON DELETE SET NULL, 
    FOREIGN KEY (patientID) REFERENCES Patient(patientID) ON UPDATE CASCADE ON DELETE SET NULL 
);


CREATE TABLE calendarEvent(
	eventID INT NOT NULL,
    eventName varchar(255),
    eventText TEXT,
    userID int,
    startTime datetime,
    endTime datetime,
    isDone boolean,
    PRIMARY KEY (eventID),
    FOREIGN KEY (userID) REFERENCES User(userID) ON UPDATE CASCADE ON DELETE CASCADE
);
#globalVars manual
#0-8 (8 included) times for session times in this format: "16:00"
#9 default time for session
#10 cost for consultation
#11 cost for follow-up
#12 cost for exercise session
 
-- CREATE USER 'root'@'%' identified by 'root';
grant all privileges on *.* to 'root'@'%' with grant option;
flush privileges;

CREATE TABLE globalVars(
	varID int,
    varName varchar(255),
    varValue varChar(255) null,
    PRIMARY KEY (varID)
);
