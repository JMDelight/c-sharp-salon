# _Hair Salon Website_

#### _A basic C# database web application to manage a team of stylists and clients for each stylist with a sql database. July 15, 2016_

#### By _**Joel Delight**_

## Description

_This is a C# web application that is designed to allow a user to create and manage a team of stylists and their clients using a sql database in a web browser._

## Setup/Installation Requirements

* _Download the repository_
* _In the command line, navigate to the repository_
* _Run the command dnu restore_
* _Either: Create databses from sql script files_
* _Or: In sqlcmd, run the following commands-_
  * CREATE DATABASE hair_salon;
  * GO
  * USE hair_salon;
  * GO
  * CREATE TABLE stylists (id INT IDENTITY(1,1), stylist_name VARCHAR(255));
  * CREATE TABLE clients (id INT IDENTITY(1,1), client_name VARCHAR(255), stylist_id INT);
  * GO
  * CREATE DATABASE hair_salon_test;
  * GO
  * USE hair_salon;
  * GO
  * CREATE TABLE stylists (id INT IDENTITY(1,1), stylist_name VARCHAR(255));
  * CREATE TABLE clients (id INT IDENTITY(1,1), client_name VARCHAR(255), stylist_id INT);
  * GO
* _Run the command dnx kestrel_
* _using a web browser, navigate to http://localhost:5004_

## Known Bugs

_There are no known issues with this page_

## Support and contact details

_If you have any questions or concerns, please email me at thefencingflutist@gmail.com_


## Technologies Used

_C#, Nancy, Razor, xUnit, mssql_

### License

*Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.*

Copyright (c) 2016 **_Joel Delight_**
