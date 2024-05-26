CREATE DATABASE Clinical;

USE Clinical;

CREATE TABLE User (
	  firstName varchar(30) not null,
    lastname varchar (30) not null,
    userID int not null primary key,
    gender boolean,
    password text,
	  hireDate date,
    birthdate date,
    address text,
    phoneNumber varchar(12),
    email varchar(100),
    nationalID varchar(30)    
);

CREATE TABLE appointmentType(
	typeID int,
    typeName varchar(255),
    typeDescription Text,
    timeInMinutes int,
    cost double,
    PRIMARY KEY (typeID)
);

CREATE TABLE Package (
    PackageID INT PRIMARY KEY,
    PackageName VARCHAR(255) NOT NULL,
    NumberOfSessions INT NOT NULL,
    Price DOUBLE NOT NULL,
    Description TEXT
);


CREATE TABLE Room (
    roomID INT NOT NULL,
    roomNumber VARCHAR(50) NOT NULL,
    PRIMARY KEY (roomID)
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

CREATE TABLE Equipment (
    equipmentID INT NOT NULL,
    equipmentName VARCHAR(100) NOT NULL,
    equipmentFunction TEXT,
    roomID int,
    latestMaintenanceDate DATE,
    toCheck BOOLEAN,
    PRIMARY KEY (equipmentID),
    FOREIGN KEY (roomID) REFERENCES Room(roomID)
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
    patientID INT NOT NULL,
    firstName VARCHAR(30) NOT NULL,
    lastName VARCHAR(30) NOT NULL,
    birthdate DATE,
    gender BOOLEAN,
    phoneNumber VARCHAR(12),
    email VARCHAR(100),
    address TEXT,
    referred BOOLEAN,
    previouslyTreated BOOLEAN,
    height DECIMAL(5,2),
    weight DECIMAL(5,2),
    dueAmount DECIMAL(10,2),
    physicianID int,
    referringName TEXT,
    referringPN TEXT,
    activePackageID int,
    remainingSessions int Default 0,
    
	FOREIGN KEY (activePackageID) REFERENCES Package(PackageID),
	FOREIGN KEY (physicianID) REFERENCES User(userID),
	
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

CREATE TABLE Exercise (
    exerciseID INT NOT NULL ,
    exerciseName VARCHAR(100) NOT NULL,
    explanationLink TEXT, 
    notes TEXT,
    PRIMARY KEY (exerciseID)
);

CREATE TABLE ChatGroupRelation (
    chatGroupID INT NOT NULL,
    userID int NOT NULL,
    FOREIGN KEY (chatGroupID) REFERENCES ChatGroup(chatGroupID),
    FOREIGN KEY (userID) REFERENCES User(userID)
);


CREATE TABLE Injury (
    injuryID INT NOT NULL ,
    injuryName TEXT NOT NULL,
    injuryLocation TEXT NOT NULL,
    severity INT,
    description text,
    PRIMARY KEY (injuryID)
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
    userID int not null,
    patientID INT NOT NULL,
    packageID INT,
    visitTypeID int,
    height double,
    weight double,
    timeStamp DATETIME,
    roomID INT,
    therapistNotes TEXT,
    isDone boolean,
    PRIMARY KEY (visitID),
    FOREIGN KEY (UserID) REFERENCES User(userID),
    FOREIGN KEY (visitTypeID) REFERENCES appointmentType(typeID) ON DELETE SET NULL ON UPDATE CASCADE,
    FOREIGN KEY (patientID) REFERENCES Patient(patientID),
    FOREIGN KEY (packageID) REFERENCES package(packageID),
    FOREIGN KEY (roomID) REFERENCES Room(roomID)
);

CREATE TABLE TreatmentPlan (
    planID INT NOT NULL,
    planName VARCHAR(100) NOT NULL,
    planTime INT NOT NULL,
    injuryID INT,
    price DECIMAL(10,2),
    notes TEXT,
    keyWords TEXT,
    patientID int,
    visitID int,
    timeStamp DateTime,
    PRIMARY KEY (planID),
    FOREIGN KEY (injuryID) REFERENCES Injury(injuryID),
    FOREIGN KEY (patientID) REFERENCES Patient(patientID),
    FOREIGN KEY (visitID) REFERENCES Visit(visitID)
);

CREATE TABLE Prescription (
    prescriptionID INT NOT NULL,
    timeStamp DATETIME,
    patientID INT NOT NULL,
    userID int NOT NULL,
    visitID INT,
    PRIMARY KEY (prescriptionID),
    FOREIGN KEY (patientID) REFERENCES Patient(patientID),
    FOREIGN KEY (userID) REFERENCES User(userID),
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

CREATE TABLE IssueExercise (
    issueID INT NOT NULL,
    exerciseID INT NOT NULL,
    patientID INT NOT NULL,
    treatmentPlanID INT NOT NULL,
    frequency TEXT,
    notes TEXT,
    PRIMARY KEY (issueID),
    FOREIGN KEY (exerciseID) REFERENCES Exercise(exerciseID),
    FOREIGN KEY (patientID) REFERENCES Patient(patientID),
    FOREIGN KEY (treatmentPlanID) REFERENCES treatmentPlan(planID)
);




CREATE TABLE MedicalRecord (
    recordID INT NOT NULL,
    type TEXT,
    timeStamp DATETIME,
    report TEXT,
    images text, 				
    patientID INT,
    physicianNotes TEXT,
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


CREATE TABLE patientInjuryRelation (
    patientID INT NOT NULL,
    injuryID INT NOT NULL,
    FOREIGN KEY (patientID) REFERENCES Patient(patientID) ON DELETE CASCADE,
    FOREIGN KEY (injuryID) REFERENCES Injury(injuryID) ON DELETE CASCADE
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
    physicianID int,
    timestamp datetime,
    PRIMARY KEY (paymentID),
    FOREIGN KEY (physicianID) REFERENCES User(userID) ON UPDATE CASCADE ON DELETE SET NULL, 
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
 
CREATE USER 'root'@'%' identified by 'root';
grant all privileges on *.* to 'root'@'%' with grant option;
flush privileges;

CREATE TABLE globalVars(
	varID int,
    varName varchar(255),
    varValue varChar(255) null,
    PRIMARY KEY (varID)
);

