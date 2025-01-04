﻿CREATE TABLE Users (
	Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	UserName TEXT(50) NOT NULL,
	Password TEXT,
	IsAdmin INTEGER DEFAULT (0) NOT NULL,
	Email TEXT(200),
	FirstName TEXT(100),
	LastName TEXT(100)
);