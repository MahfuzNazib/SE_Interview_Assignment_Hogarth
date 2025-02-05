# User Management - Hogarth

### Overview

This is a full-stack web application built with:

Backend: ASP.NET Core Web API

Frontend: Angular

The project follows a clean architecture, implementing RESTful APIs for the backend and a modern UI using Angular for the frontend.

#### Features

- User List, Add User, User Detail, Edit User and Delete User

- For loading large number of user, implement pagination.

- Searching and sorting user list

- Responsive UI with Angular Bootstrap

- Secure API endpoints with proper validation

- Support multiple data source. [MSSQL and JSON]

- Global error handling in formatted way & logging

#### Tech Stack

* Backend (.NET Core Web API)

* Framework: .NET Core

* Database: Entity Framework Core with SQL Server/JSON data

* Logging: Serilog

* API Documentation: Swagger

* Server side validation: Fluent Validation 

* Frontend (Angular)

* UI Library:  Bootstrap

* State Management: NgRx / Services

* Routing: Angular Router

* API Integration: HttpClientModule

### Installation

#### Prerequisites

- .NET Core SDK

- Node.js & npm

- Angular CLI

- SQL Server

#### Project Setup

1. Clone the repository:

`git clone https://github.com/MahfuzNazib/SE_Interview_Assignment_Hogarth.git`
`cd SE_Interview_Assignment_Hogarth`

2. Navigate to `Api\Hogarth.UserManagement.API`

3. Open the solution file. `Hogarth.UserManagement.API.sln`

4. Database Connection String: Change the `DefaultConnection` value from `appsetting.json`. You can find this file to `Hogarth.UserManagement.API` layer.

5. #### Database Migration and Seed for MSSQL Server

6. For database migration and seed navigate to `Hogarth.UserManagement.Infrastructure` folder and open `cmd`. 

7. Then run this command
   `dotnet ef database update -s ..\Hogarth.UserManagement.API\Hogarth.UserManagement.API.csproj`

8. ### Angular Project Setup

9. Go to the root folder and navigate to `Client` folder.

10. Open cmd and run `npm install`

11. Then navigate to `Hogarth.UserManagement\Client\src\environments` and open environment.ts file

12. Modify `environment.ts` file
    ```javascript
    export const environment = {
    production: false,
    baseURL: "https://localhost:7066/api"  // Change the baseURL if you change or in prod
  }

13. Then run command `ng serve`.

14. By default it will open port like `http://localhost:4200/`. Open your browser and past this url. To be specific `http://localhost:4200/user`.

# API Documentation in Postman `https://documenter.getpostman.com/view/10527534/2sAYX6o1y4`

## API Collection Are Include in the root folder

Contact

Author: Nazib Mahfuz

GitHub: MahfuzNazib

Email: nazibmahfuz60@gmail.com

Contact: 01777127618

