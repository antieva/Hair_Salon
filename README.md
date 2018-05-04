# _Hair Salon App_

#### _Hair Salon, 05/04/18_

#### By _**Eva Antipina**_

## Description

_A web app to manage Hair Salon stylists and clients database._
_It will let salon employee to:_
* _be able to see a list of all salon stylists;_  
* _be able to select a stylist, see their details, and see a list of all clients that belong to that stylist;_
* _add new stylists to the system when they are hired;_
* _add new clients to a specific stylist and should not be able to add a client if no stylists have been added._

## Specifications

* _The program returns the list of hair salon stylists if user click on "All stylists.", or "There are no stylist currently hired." if there are no stylists in the database._


## Setup/Installation Requirements

* _Clone or download the repository._
* _Unzip the files into a single directory._
* _Open the Windows PowerShell and move to the directory_
* _Run "dotnet restore" command in the PowerShell._
* _Run "dotnet build" command in the PowerShell._
* _Run "dotnet run" command in the PowerShell._
* _Open a web browser of choice._
* _Enter "localhost:5000/home" into the address bar._

# Add Database to the Project

* _> CREATE DATABASE hair_salon;_
* _> USE hair_salon;_
* _> CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255), phoneNumber VARCHAR(255), specialization VARCHAR(255), workingDays VARCHAR(255), time VARCHAR(255));_
* _> CREATE TABLE clients (id serial PRIMARY KEY, name VARCHAR(255), stylistID VARCHAR(255), phoneNumber VARCHAR(255), DateOfBirth VARCHAR(255), notes VARCHAR(255));_

## Known Bugs

_None._

## Support and contact details

_If You run into any issues or have questions, ideas, concerns or would like to make a contribution to the code, please contact me via email: eva.antipina@gmail.com_

## Technologies Used

_C#, HTML, Bootstrap_

### License

*Not licensed.*

Copyright (c) 2018 **_Eva Antipina_**
