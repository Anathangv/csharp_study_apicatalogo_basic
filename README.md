# csharp_study_apicatalogo_basic
1 part - project in c# that creates Web API's
The project is part of the course: Curso Web API ASP .NET Core Essencial by Macoratti.

Techonologies used:
- Visual Studio 2019
- MySQL Bd

Topics covered on this project:
- Basic creation of web API
- connection with the database
- CRUD operations
- Migrations
- Annotations
- Routting
- Filters
- Logs
- Type of return
- Http Verbs

Create the table on MySQL
- on visual studio 2019
- select the menu Tools > Nuget package Manager > Package Manager Console;
- on the project there is a folder Migrations, and in this folder there are the files to create and populate de tables;
- use the command add-migration {anyNameYouWant} and copy the content in the file 20210321223136_Inicial to the new file;
- than use the command "update-database" to create the tables;
- repeate the steps for the file 20210321223731_populadb, in this case to populate the tables;
- obs: check the wether is necessary change the user or password from the connection string of the project, based on the configurations set on the mysql;
- ApiCatalogo > appsettings.json > DefaultConnection
