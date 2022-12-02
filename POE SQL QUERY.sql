create database newtimer;
CREATE TABLE TimerData (
id INT NOT NULL PRIMARY KEY IDENTITY,
Module_Name VARCHAR (100) NOT NULL,
Module_Code VARCHAR (150) NOT NULL UNIQUE,
Number_Of_Credits int not NULL,
Class_Hours_Per_Week int not NULL,
Number_Of_Weeks_In_Semester int not null,
Study_Hours_Remaining as Number_Of_Credits * 10 / Number_Of_Weeks_In_Semester - Class_Hours_Per_Week,
created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

insert into TimerData (Module_Name ,Module_Code , Number_Of_Credits, Class_Hours_Per_Week, Number_Of_Weeks_In_Semester)
values
('Programming','PROG6212',60,9,12)


select * from TimerData;