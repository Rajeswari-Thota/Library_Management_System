create database library
use library
create table Student
(
Roll_no int Identity(1,1) primary key,
Student_Name varchar(20),
Department varchar(30)
)
select*from Student

create table Book
(
Id int Identity(1,1) primary key,
Title varchar(50),
Author varchar(50),
Published_year int,
Quantity int
)

select*from Book

create table Login_Details
(
Roll_no int references Student(Roll_no),
username varchar(50),
password varchar(20)
)
insert into Login_Details values(1,'raji','raji@123')
select * from Login_Details

create table Issues(
Id int Identity(1,1) primary key,
Student_roll_no int references Student(Roll_no),
Issued_book_id int references Book(Id),
Issued_Date date,
Returned_Date date
)

select*from Issues


