USE freedomadmin;

CREATE TABLE Menus
(
	Id CHAR(36) NOT NULL,
	Name VARCHAR(255),
	OrderNo INT,
	Icon VARCHAR(255),
	Controller VARCHAR(200),
	Action VARCHAR(200),
	IsActive BIT
);
ALTER TABLE Menus ADD CONSTRAINT PK_Menus_Id PRIMARY KEY(Id);
ALTER TABLE Menus ADD CONSTRAINT UK_Menus_Name UNIQUE(Name);

INSERT INTO Menus (Id,Name,OrderNo,Icon,Controller,Action,IsActive) VALUES('2303D974-8EE1-42E7-AA66-3599AF1D7E0A','Dashboard',1,'settings_input_svideo','Dashboard','SuperAdmin',1);
INSERT INTO Menus (Id,Name,OrderNo,Icon,IsActive) VALUES('BB0B92A0-2560-4FB5-9108-AE3A118F8975','Business',2,'add_to_queue',1);
INSERT INTO Menus (Id,Name,OrderNo,Icon,IsActive) VALUES('58F7608C-E7A6-4548-8B2C-0D8ED123EAE2','Tenent',3,'photo_filter',1);
INSERT INTO Menus (Id,Name,OrderNo,Icon,IsActive) VALUES('9A0245BC-4E55-4BEF-88C4-5C5A63878527','User',4,'face',1);


CREATE TABLE SubMenus
(
	Id CHAR(36) NOT NULL,
	Name VARCHAR(255),
	MenuId CHAR(36) NOT NULL,
	OrderNo INT,
	Icon VARCHAR(255),
	Controller VARCHAR(200),
	Action VARCHAR(200),
	IsActive BIT
);
ALTER TABLE SubMenus ADD CONSTRAINT PK_SubMenus_Id PRIMARY KEY(Id);
ALTER TABLE SubMenus ADD CONSTRAINT UK_SubMenus_Name UNIQUE(Name);
ALTER TABLE SubMenus ADD CONSTRAINT FK_SubMenus_MenuId FOREIGN KEY(MenuId) REFERENCES Menus(Id);

INSERT INTO SubMenus(Id,Name,MenuId,OrderNo,Icon,Controller,Action,IsActive)
VALUES('2EEAC08C-B357-4E29-9819-7052014A7DFD','Create New Business','BB0B92A0-2560-4FB5-9108-AE3A118F8975',1,'radio_button_unchecked','Business','CreateNewBusiness',1);

INSERT INTO SubMenus(Id,Name,MenuId,OrderNo,Icon,Controller,Action,IsActive)
VALUES('A865BBD2-BB28-4D30-A659-5C36446AF404','Manage Businesses','BB0B92A0-2560-4FB5-9108-AE3A118F8975',2,'radio_button_unchecked','Business','ManageBusinesses',1);

INSERT INTO SubMenus(Id,Name,MenuId,OrderNo,Icon,Controller,Action,IsActive)
VALUES('E2E0FE15-B3EC-493F-B0FE-EE6A09104668','Create New Tenent','58F7608C-E7A6-4548-8B2C-0D8ED123EAE2',1,'radio_button_unchecked','Tenent','CreateNewTenent',1);

INSERT INTO SubMenus(Id,Name,MenuId,OrderNo,Icon,Controller,Action,IsActive)
VALUES('26758651-D328-4D4E-BA46-DD062E5193A5','Manage Tenents','58F7608C-E7A6-4548-8B2C-0D8ED123EAE2',1,'radio_button_unchecked','Tenent','ManageTenents',1);

INSERT INTO SubMenus(Id,Name,MenuId,OrderNo,Icon,Controller,Action,IsActive)
VALUES('01997C6B-E565-46E9-8B3C-D9E9EBCAA7CF','Create New User','9A0245BC-4E55-4BEF-88C4-5C5A63878527',1,'radio_button_unchecked','User','CreateNewUser',1);

INSERT INTO SubMenus(Id,Name,MenuId,OrderNo,Icon,Controller,Action,IsActive)
VALUES('202645BE-2C6F-4EAE-9307-916FB6674DD6','Manage Users','9A0245BC-4E55-4BEF-88C4-5C5A63878527',1,'radio_button_unchecked','User','ManageUsers',1);