Task Management APP
--------------------
This project was developed as a solution to the test scenario a simple task management application that allows users to create, read, update, and delete tasks. The application should have ta user-friendly frontend interface and a backend that handles data storeage.
The project demonstrates core functionalities such as  Asp.Net Core, Entity Framework, MVC, Repository Pattern, UnitofWork Pattern, SQLite.

Key Features
--------------
Feature 1: Allows users to create, view, update, and delete tasks.
Feature 2: Implements role-based access to restrict certain actions to admins only.
Additional Enhancements: Include validation and exception handling added to enhance the solution.


Installation and Setup
------------------------
Clone the Repository:
git clone https://github.com/HninYuKhaing/TaskManagementApplication.git

Install Dependencies:
Run dotnet restore in the project root to install .NET dependencies

Database Setup:

dotnet ef migrations add InitialCreate, 
dotnet ef database update

Usage Guide
-----------
Here’s how to interact with the application’s main features:
Use username : admin
    password : 123

Task Management:
Navigate to the Tasks page to add, edit, delete, and view task details.
Access is restricted based on user roles. Admins have full access, while regular users have limited permissions.

Additional Notes
----------------
Limitations:
The application currently only supports basic CRUD operations.
