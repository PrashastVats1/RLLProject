create database VaccineMgmt
use VaccineMgmt

CREATE TABLE UserType (
    UserTypeID INT PRIMARY KEY IDENTITY(1,1),
    UserTypeDescription NVARCHAR(50)
);

CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    Email NVARCHAR(100) UNIQUE,
    Password NVARCHAR(200),
    UserTypeID INT,
    FOREIGN KEY(UserTypeID) REFERENCES UserType(UserTypeID)
);

CREATE TABLE Vaccine (
    VaccineID INT PRIMARY KEY IDENTITY(1,1),
    VaccineName NVARCHAR(100),
    VaccineDescription NVARCHAR(255)
);

CREATE TABLE Booking (
    BookingID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,
    VaccineID INT,
    BookingDate DATE,
    Slot TIME,
    FOREIGN KEY(UserID) REFERENCES Users(UserID),
    FOREIGN KEY(VaccineID) REFERENCES Vaccine(VaccineID)
);

CREATE TABLE VaccineAvailability (
	AvailabilityID INT PRIMARY KEY IDENTITY(1,1),
    VaccineID INT,
    DateAvailable DATE,
    QuantityAvailable INT,
    FOREIGN KEY(VaccineID) REFERENCES Vaccine(VaccineID)
);

INSERT INTO UserType(UserTypeDescription) VALUES ('Admin');
INSERT INTO UserType(UserTypeDescription) VALUES ('User');
