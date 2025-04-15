create database if not exists DapperBeer;
use DapperBeer;

SET FOREIGN_KEY_CHECKS = 0;
drop table if exists Brewer;
drop table if exists Beer;
drop table if exists Cafe;
drop table if exists Sells;
drop table if exists Address;
drop table if exists Brewmaster;
drop table if exists Review;
SET FOREIGN_KEY_CHECKS = 1;

create table Brewer
(
    BrewerId int  not null
        primary key,
    Name      text null,
    Country      text null
);

create table Address (
    AddressId INT PRIMARY KEY AUTO_INCREMENT,
    Street TEXT,
    City TEXT,
    Country TEXT
);

create table Brewmaster (
    BrewmasterId INT PRIMARY KEY AUTO_INCREMENT,
    Name TEXT,
    BrewerId INT REFERENCES Brewer(BrewerId),
    AddressId INT REFERENCES Address(AddressId)
);


create table Beer
(
    BeerId  int    auto_increment not null
        primary key ,
    Name      text   null,
    Type      text   null,
    Style     text   null,
    Alcohol   double null,
    BrewerId int    null,
    constraint Beer_Brewer_BrewerId_fk
        foreign key (BrewerId) references Brewer (BrewerId)
);


create table Cafe
(
    CafeId int  not null
        primary key auto_increment,
    Name        text null,
    Address     text null,
    City        text null
);


create table Sells
(
    CafeId int not null,
    BeerId  int not null,
    primary key (CafeId, BeerId),
    constraint Sells_Beer_BeerId_fk
        foreign key (BeerId) references Beer (BeerId),
    constraint Sells_Cafe_CafeId_fk
        foreign key (CafeId) references Cafe (CafeId)
);

create table Review
(
    ReviewId int auto_increment
        primary key,
    BeerId   int           null,
    Score    decimal(4, 2) null,
    constraint Review_ibfk_1
        foreign key (BeerId) references DapperBeer.Beer (BeerId)
);

create index BeerId
    on DapperBeer.Review (BeerId);

