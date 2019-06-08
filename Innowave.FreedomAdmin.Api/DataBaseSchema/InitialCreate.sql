
START TRANSACTION;

CREATE DATABASE freedomadmin;

USE freedomadmin;

CREATE TABLE Products
(
	Id CHAR(36),
	Name VARCHAR(255),
	Description VARCHAR(1000),
	IsActive BIT
);

ALTER TABLE Products ADD CONSTRAINT PK_Products_Id PRIMARY KEY(Id);
ALTER TABLE Products ADD CONSTRAINT UK_Products_Name UNIQUE (Name);

INSERT INTO Products (Id,Name,Description,IsActive) VALUES('C467A178-51AF-41E0-A91B-4759738BEB70','FreedomRis','Patient Billing',1);
INSERT INTO Products (Id,Name,Description,IsActive) VALUES('0657652E-F796-4581-9096-D9A5704266EF','FreedomWorklist','Tele Radiology',1);
INSERT INTO Products (Id,Name,Description,IsActive) VALUES('CA61C351-A336-43A2-9A1A-09A48436B727','FreedomLIS','Complete LAB solution',1);

CREATE TABLE Roles
(
	Id CHAR(36),
	Description VARCHAR(255),
	Code VARCHAR(20)
);

ALTER TABLE Roles ADD CONSTRAINT PK_Roles_Id PRIMARY KEY(Id);
ALTER TABLE Roles ADD CONSTRAINT UK_Roles_Description UNIQUE (Description);
ALTER TABLE Roles ADD CONSTRAINT UK_Roles_Code UNIQUE (Code);

INSERT INTO Roles(Id,Description,Code) VALUES('80F4ADFF-0F91-46BF-96BB-A794B69F7D27','Super Admin','SA');
INSERT INTO Roles(Id,Description,Code) VALUES('A3493440-4721-4E26-AE27-C3F907DB2862','Admin','ADMIN');
INSERT INTO Roles(Id,Description,Code) VALUES('FCE3B058-2AAC-4F23-9971-6CA2F7D37EB4','Front Office','FO');
INSERT INTO Roles(Id,Description,Code) VALUES('461DDEB2-C954-4672-A99D-CC8604E0A7EB','PRO','PRO');
INSERT INTO Roles(Id,Description,Code) VALUES('7DFAA68A-11FD-4A1A-A356-E812FF5A2934','Credit Department','CREDIT');
INSERT INTO Roles(Id,Description,Code) VALUES('0A407D1C-D7DE-49D9-A914-52570A44EFF9','Accounts','ACCOUNTS');
INSERT INTO Roles(Id,Description,Code) VALUES('7F056284-66CB-49A2-8B56-499F9A1B6BCD','Radiology','RAD');
INSERT INTO Roles(Id,Description,Code) VALUES('B94FE99D-F176-48B1-A315-088C4C9F05B2','Technician','TECH');
INSERT INTO Roles(Id,Description,Code) VALUES('7E0F9F96-0CA9-46BE-B632-0623FE4F718A','Phlebotomists','PHLEBOTOMIST');
INSERT INTO Roles(Id,Description,Code) VALUES('8E437323-F568-4C90-91A9-D761121E3E5E','SR. Lab Technician','SR. LAB TECH');
INSERT INTO Roles(Id,Description,Code) VALUES('77B51541-E38F-4FBD-BC2B-95BCC63C3000','Lab Technician','LAB TECH');

CREATE TABLE ProductRoles
(
	ProductId CHAR(36) NOT NULL,
	RoleId CHAR(36) NOT NULL
);

ALTER TABLE ProductRoles ADD CONSTRAINT FK_ProductRoles_ProductId FOREIGN KEY(ProductId) REFERENCES Products(Id);
ALTER TABLE ProductRoles ADD CONSTRAINT FK_ProductRoles_RoleId FOREIGN KEY(RoleId) REFERENCES Roles(Id);

INSERT INTO ProductRoles(ProductId,RoleId) VALUES('C467A178-51AF-41E0-A91B-4759738BEB70','A3493440-4721-4E26-AE27-C3F907DB2862');
INSERT INTO ProductRoles(ProductId,RoleId) VALUES('C467A178-51AF-41E0-A91B-4759738BEB70','FCE3B058-2AAC-4F23-9971-6CA2F7D37EB4');
INSERT INTO ProductRoles(ProductId,RoleId) VALUES('C467A178-51AF-41E0-A91B-4759738BEB70','461DDEB2-C954-4672-A99D-CC8604E0A7EB');
INSERT INTO ProductRoles(ProductId,RoleId) VALUES('C467A178-51AF-41E0-A91B-4759738BEB70','7DFAA68A-11FD-4A1A-A356-E812FF5A2934');
INSERT INTO ProductRoles(ProductId,RoleId) VALUES('C467A178-51AF-41E0-A91B-4759738BEB70','0A407D1C-D7DE-49D9-A914-52570A44EFF9');

CREATE TABLE Addresses
(
	Id CHAR(36),
	AddressLine1 VARCHAR(255),
	AddressLine2 VARCHAR(255),
	City VARCHAR(100),
	State VARCHAR(100),
	Country VARCHAR(100),
	PostalCode VARCHAR(15)
);

CREATE TABLE Users
(
	Id CHAR(36) NOT NULL,
	FirstName VARCHAR(255) NOT NULL,
	LastName VARCHAR(36),
	UserName VARCHAR(255) NOT NULL,
	Password VARCHAR(255) NOT NULL,
	DefaultProductId CHAR(36),
	DefaultRoleId CHAR(36) NOT NULL,
	BusinessId CHAR(36),
	DefaultTenentId CHAR(36),
	IsActive BIT,
	Created DATETIME,
	CreatedBy CHAR(36),
	LastUpdated DATETIME,
	LastUpdatedBy CHAR(36),
	LastLogin DATETIME
);

ALTER TABLE Users ADD CONSTRAINT PK_Users_Id PRIMARY KEY(Id);
ALTER TABLE Users ADD CONSTRAINT UK_Users_UserName UNIQUE(UserName);

INSERT INTO Users(Id,FirstName,UserName,Password,DefaultRoleId,IsActive,Created,LastUpdated)
VALUES('B9244AB8-62A3-405A-B722-B7EAC04AC2F6','Innowave Healthcare Pvt Ltd','sa','JeR7ql+hnVt0FfdYVg+mtQ==','80F4ADFF-0F91-46BF-96BB-A794B69F7D27',1,NOW(),NOW());

CREATE TABLE ProductsByUsers
(
	ProductId CHAR(36) NOT NULL,
	ProductRoleId CHAR(36) NOT NULL,
	UserId CHAR(36) NOT NULL,
	AssignedBy CHAR(36),
	AssignedDate DATETIME
);
ALTER TABLE ProductsByUsers ADD CONSTRAINT FK_ProductsByUsers_ProductId FOREIGN KEY(ProductId) REFERENCES Products(Id);
ALTER TABLE ProductsByUsers ADD CONSTRAINT FK_ProductsByUsers_ProductRoleId FOREIGN KEY(ProductRoleId) REFERENCES Roles(Id);
ALTER TABLE ProductsByUsers ADD CONSTRAINT FK_ProductsByUsers_UserId FOREIGN KEY(UserId) REFERENCES Users(Id);
ALTER TABLE ProductsByUsers ADD CONSTRAINT FK_ProductsByUsers_AssignedBy FOREIGN KEY(AssignedBy) REFERENCES Users(Id);


CREATE TABLE Businesses
(
	Id CHAR(36) NOT NULL,
	Name VARCHAR(255) NOT NULL,
	IsActive BIT,
	Logo VARCHAR(500),
	MobileTelephone VARCHAR(20),
	WorkTelephone VARCHAR(20),
	Fax VARCHAR(20),
	Website VARCHAR(255),
	Email VARCHAR(255),
	Reason VARCHAR(1000),
	Created DATETIME,
	CreatedBy CHAR(36),
	LastUpdated DATETIME,
	LastUpdatedBy CHAR(36)
);

ALTER TABLE Businesses ADD CONSTRAINT PK_Businesses_Id PRIMARY KEY(Id);
ALTER TABLE Businesses ADD CONSTRAINT UK_Businesses_Name UNIQUE(Name);
ALTER TABLE Users ADD CONSTRAINT FK_Users_BusinessId FOREIGN KEY(BusinessId) REFERENCES Businesses(Id);

CREATE TABLE ProductsByBusinesses
(
	ProductId CHAR(36) NOT NULL,
	BusinessId CHAR(36) NOT NULL,
	AssignedBy CHAR(36),
	AssignedDate DATETIME
);

ALTER TABLE ProductsByBusinesses ADD CONSTRAINT FK_ProductsByBusinesses_ProductId FOREIGN KEY(ProductId) REFERENCES Products(Id);
ALTER TABLE ProductsByBusinesses ADD CONSTRAINT FK_ProductsByBusinesses_BusinessId FOREIGN KEY(BusinessId) REFERENCES Businesses(Id);

CREATE TABLE Tenents
(
	Id CHAR(36) NOT NULL,
	Name VARCHAR(255) NOT NULL,
	Code VARCHAR(50),
	BusinessId CHAR(36),
	ProductId CHAR(36),
	IsActive BIT,
	MobileTelephone VARCHAR(20),
	WorkTelephone VARCHAR(20),
	Fax VARCHAR(20),
	Website VARCHAR(255),
	Email VARCHAR(255),
	Created DATETIME,
	CreatedBy CHAR(36),
	LastUpdated DATETIME,
	LastUpdatedBy CHAR(36)
);

ALTER TABLE Tenents ADD CONSTRAINT PK_Tenents_Id PRIMARY KEY(Id);
ALTER TABLE Tenents ADD CONSTRAINT PK_Tenents_Name UNIQUE(Name);
ALTER TABLE Tenents ADD CONSTRAINT PK_Tenents_Code UNIQUE(Code);
ALTER TABLE Tenents ADD CONSTRAINT FK_Tenents_BusinessId FOREIGN KEY(BusinessId) REFERENCES Businesses(Id);
ALTER TABLE Tenents ADD CONSTRAINT FK_Tenents_CreatedBy FOREIGN KEY(CreatedBy) REFERENCES Users(Id);
ALTER TABLE Tenents ADD CONSTRAINT FK_Tenents_LastUpdatedBy FOREIGN KEY(LastUpdatedBy) REFERENCES Users(Id);
ALTER TABLE Users ADD CONSTRAINT FK_Users_DefaultTenentId FOREIGN KEY(DefaultTenentId) REFERENCES Tenents(Id);

COMMIT;