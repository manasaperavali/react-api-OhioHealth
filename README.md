This full-stack web application is built using ASP.NET Core Web API for the backend and React (with Vite) for the frontend. It uses SQLite as the database and includes API Key validation for security.

The application allows saving and retrieving user data, including First Name, City Name, and Year of Joining. It also supports filtering users by any of those fields.

**To run the backend:**

1. Open terminal and navigate to the backend folder:
   cd BackendAPI

2. Apply the database migration and run the API:
   dotnet ef database update
   dotnet run

The backend will run on: http://localhost:5250

**To run the frontend:**

1. Navigate to the frontend folder:
   cd frontend

2. Install dependencies and start the development server:
   npm install
   npm run dev

The frontend will be available at: http://localhost:5173

All API requests require a valid API key in the header:
x-api-key: your-secret-key

The secret key is configured in the backend's appsettings.json and passed in the frontend through axios headers.

Example API call using PowerShell:

$headers = @{ "x-api-key" = "your-secret-key" }

Invoke-WebRequest -Uri "http://localhost:5250/api/user" `
  -Method GET `
  -Headers $headers


**Functionality Overview**

**Save Data - Validation + POST Request**
Form Fields Filled: "meghan", "plano", "2021"
Action: User clicked Save
Outcome:
Frontend validates Year is within the last 5 years.
Sends a POST request with a valid x-api-key header.
Success Alert: "Data saved successfully!"
Backend logs: A new user was added to the SQLite database.

![image](https://github.com/user-attachments/assets/cc52527f-4234-4408-be2b-426522bb97b8)

**Retrieve Data by Year**
Form Filtered By: Year = 2020
Action: Clicked Retrieve
Backend Query: Filters by year (2020)
Displayed Result: Only data matching year 2020 shown:
**nike - New jersey - 2020**

![image](https://github.com/user-attachments/assets/05329737-9fc7-41ab-8b82-51f4fb46e4a6)

**Retrieve by City**
Form Filtered By: City = "Cleveland"
Action: Clicked Retrieve
Backend Query: Filters by CityName=cleveland
Displayed Result:
manasa - cleveland - 2025

![image](https://github.com/user-attachments/assets/553574cc-019a-4590-bb73-f61b68b55acd)

**Retrieve by First Name**
Form Filtered By: FirstName = "nike"
Action: Clicked Retrieve
Backend Query: Filters with FirstName=nike
Displayed Result:
nike - New jersey - 2020
![image](https://github.com/user-attachments/assets/27355ab4-b4ac-4371-beb0-8a6b558bed5a)

**Retrieve All**
Form is Empty
Action: Clicked Retrieve
Backend Query: Returns all records
Displayed Result:
nike - New jersey - 2020  
manasa - cleveland - 2025  
mike - new york - 2023

![image](https://github.com/user-attachments/assets/c762bbef-b6b9-42b8-904c-ccfdd3645f0c)

<img width="673" alt="image" src="https://github.com/user-attachments/assets/9c5d2853-8770-4837-9e5d-140b13782afd" />

The above image response confirms a successful POST request to the API with a valid API key. The user data (peravali, dallas, 2021) was saved, returning a 201 Created status. This verifies that API key validation and data insertion are working as expected.

<img width="674" alt="image" src="https://github.com/user-attachments/assets/0ce7e836-8e0d-4764-aef7-46b46c3aa263" />

The above screenshot shows a successful GET request using an API key to retrieve all user data from the backend API. The server returned a 200 OK status, confirming that the API key is valid and data retrieval is functioning properly.







