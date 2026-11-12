# ASP.NET Core 10 Master–Detail CRUD

A small ASP.NET Core MVC 10 example using Entity Framework Core and SQL Server. It demonstrates a **Student master** record with multiple **Marks detail** rows.

## Features
- List students with their marks.
- Create a student and multiple marks in one form.
- Edit the student and its detail rows.
- Add and remove detail rows dynamically with JavaScript.
- Delete a student; related marks are cascade-deleted.
- Server-side validation and asynchronous EF Core operations.
- Existing detail rows are verified to belong to the student being edited.

## Requirements
- .NET 10 SDK
- SQL Server or SQL Server LocalDB

## Database setup
The default connection string uses SQL Server LocalDB. Change it in `appsettings.json` if you use another SQL Server instance.

From the project folder:

```bash
dotnet tool install --global dotnet-ef --version 10.*
dotnet restore
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

If `dotnet-ef` is already installed, use `dotnet tool update --global dotnet-ef --version 10.*`.

Open the local URL printed by `dotnet run`.

## Project structure
```text
Data/AppDbContext.cs
Models/Student.cs
Models/Mark.cs
ViewModels/StudentViewModel.cs
Controllers/StudentsController.cs
Views/Shared/_Layout.cshtml
Views/Students/Index.cshtml
Views/Students/Create.cshtml
Views/Students/Edit.cshtml
Views/Students/_StudentForm.cshtml
wwwroot/css/site.css
Program.cs
appsettings.json
```

## Relationship
One Student has many Marks. EF Core maps `StudentId` as the foreign key and cascade delete for related marks.

**Note:** This sample uses SQL Server, not Oracle. Oracle 11g requires a separate provider/data-access compatibility decision.

For production, add authentication/authorization, logging, stronger error handling, and automated tests. The detail list submitted by the form is treated as the complete collection; removed rows are deleted when saved.
