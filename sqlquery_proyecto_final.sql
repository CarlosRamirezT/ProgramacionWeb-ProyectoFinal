create database web_final
use web_final

drop table users

create table users(
	idUser int not null identity primary key,
	email varchar(40) not null constraint unq_users_email unique,
	password varchar(60) not null
)

insert into users(email,password) values('admin','admin')

create table partnerships(
	idPartnership int not null identity primary key,
	description varchar(40) not null
)

insert into partnerships(description) values('general')
insert into partnerships(description) values('premium')

create table genders(
	idGender int not null identity primary key,
	description char(1) not null constraint ck_genders_description check(description in('M','F'))
)

insert into genders(description) values('M')
insert into genders(description) values('F')

create table partners(
	idPartner int not null identity primary key,
	name varchar(40) not null,
	lastname varchar(40) not null,
	idcard varchar(13),
	photo varchar(max),
	address varchar(80),
	phone varchar(12),
	gender int constraint fk_partners_gender foreign key(gender) references genders(idGender),
	age int not null,
	birthdate date,
	idAfilliate int constraint fk_partners_idAfilliate foreign key(idAfilliate) references partners(idPartner),
	idPartnership int not null constraint fk_partners_idPartnership foreign key(idPartnership) references partnerships(idPartnership),
	workplace varchar(60),
	officeaddress varchar(80),
	officephone varchar(12),
	partnershipstatus int not null constraint ck_partners_partnershipstatus check(partnershipstatus in(0,1)),
	addmisiondate date,
	departuredate date
)

select * from users
select * from genders
select * from partnerships

truncate table users

drop table partners