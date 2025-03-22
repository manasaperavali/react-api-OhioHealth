This is a full-stack web application built using ASP.NET Core Web API for the backend and React (with Vite) for the frontend. It uses SQLite as the database and includes API Key validation for security.

The application allows saving and retrieving user data which includes: First Name, City Name, and Year of Joining. It also supports filtering users by any of those fields.

To run the backend:

1. Open terminal and navigate to the backend folder:
   cd BackendAPI

2. Apply the database migration and run the API:
   dotnet ef database update
   dotnet run

The backend will run on: http://localhost:5250

To run the frontend:

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

