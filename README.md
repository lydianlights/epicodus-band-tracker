# _Band Tracker_

#### _Code review project for Epicodus C# week 4_

#### By _**Rane Fields**_

## Description

_This app allows a user to schedule concerts between bands and venues._

## Setup/Installation Requirements

_To download and use the source code of this project:_

* _Clone the project using this link: `https://gitlab.com/lydianlights-epicodus/csharp/band-tracker.git`_
* _Install `.NET Core 1.1`. You can get it  [here](https://github.com/dotnet/core/blob/master/release-notes/download-archives/1.1.4-download.md)._
* _An SQL server is required for this project. If you have no SQL server environment on your computer, you can get MAMP [here](https://www.mamp.info/en/downloads/)_
* _Configure your server to listen on port 8889 and start it_
* _Once logged into your server, run the following SQL commands:_
  * `CREATE DATABASE band_tracker;`
  * `CREATE TABLE band_tracker.bands (id int(11) NOT NULL AUTO_INCREMENT, name varchar(255) DEFAULT NULL, PRIMARY KEY(id));`
  * `CREATE TABLE band_tracker.venues (id int(11) NOT NULL AUTO_INCREMENT, name varchar(255) DEFAULT NULL, PRIMARY KEY(id));`
  * `CREATE TABLE band_tracker.concerts (date date DEFAULT NULL, band_id int(11) DEFAULT NULL, venue_id int(11) DEFAULT NULL);`
* _Open the project directory `/BandTracker` using terminal or powershell_
* _From the directory `/BandTracker` run the command `dotnet restore` to fetch the project dependencies._
* _The application can now compiled and started by using the command `dotnet run`. It will be hosted at `localhost:5000`_

## Setting Up Test Server

_To set up the non-live database to test code against:_
* _First follow all previous installation instructions_
* _Login to your SQL server (once again using port 8889) and run the following commands:_
* `CREATE DATABASE band_tracker_test;`
* `CREATE TABLE band_tracker_test.bands (id int(11) NOT NULL AUTO_INCREMENT, name varchar(255) DEFAULT NULL, PRIMARY KEY(id));`
* `CREATE TABLE band_tracker_test.venues (id int(11) NOT NULL AUTO_INCREMENT, name varchar(255) DEFAULT NULL, PRIMARY KEY(id));`
* `CREATE TABLE band_tracker_test.concerts (date date DEFAULT NULL, band_id int(11) DEFAULT NULL, venue_id int(11) DEFAULT NULL);`
* _Open the project directory `/BandTracker.Tests` using terminal or powershell_
* _From the directory `/BandTracker.Tests` run the command `dotnet restore` to fetch the project dependencies._
* _Tests can be run using the command `dotnet test`_

## Technologies Used

_This project is powered by the [ASP .NET](https://docs.microsoft.com/en-us/aspnet/core/) framework and uses the [C# MySQL Connector Library](https://dev.mysql.com/downloads/connector/net/) for MySQL integration._

## Known Bugs

* _If a user tries to add a concert with either no bands or no venues available, the dropdown doesn't fail gracefully_

### License

*This page is hereby released as public domain. No permission necessary for modification and distribution.*

Copyright (c) 2017 **_Rane Fields_**
