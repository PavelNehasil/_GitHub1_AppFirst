CREATE TABLE LoginTypes (
	Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	Name TEXT(50) NOT NULL,
	Description TEXT(250)
);

INSERT INTO LoginTypes
(Id, Name, Description)
VALUES(1, 'Create User', NULL);
INSERT INTO LoginTypes
(Id, Name, Description)
VALUES(2, 'Edit User', NULL);
INSERT INTO LoginTypes
(Id, Name, Description)
VALUES(3, 'Login User', NULL);
INSERT INTO LoginTypes
(Id, Name, Description)
VALUES(4, 'Error Password', NULL);
