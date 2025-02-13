# Patient Management API

This is a simple .NET API for managing patients and their records.

## Getting Started

### Prerequisites

*   .NET 8 (or your .NET version) SDK
*   Docker (if you want to run the containerized version)

### Build and Run (Development)

1.  Clone the repository.
2.  Navigate to the `PatientManagement.API` directory.
3.  Run `dotnet run`.
4.  Open your browser to `http://localhost:<port>/swagger` to access the Swagger UI.

### Build and Run (Docker)

1.  Clone the repository.
2.  Navigate to the directory containing the `Dockerfile`.
3.  Build the Docker image: `docker build -t patient-management-api .`
4.  Run the Docker container: `docker run -p 8080:80 patient-management-api`
5.  Open your browser to `http://localhost:8080/swagger` to access the Swagger UI.

## API Documentation

The API documentation is available at `http://localhost:<port>/swagger` (replace `<port>` with the appropriate port).

## Project Structure

*   `PatientManagement.API`: ASP.NET Core Web API project.
*   `PatientManagement.Application`: Class library containing the business logic and models.
*   `PatientManagement.Infrastructure`: Class library containing the database context and data access logic.
*   `PatientManagement.Tests`: xUnit test project.

## Technologies Used

*   .NET 8 (or your .NET version)
*   ASP.NET Core Web API
*   Entity Framework Core
*   SQLite
*   Swashbuckle (Swagger/OpenAPI)
*   xUnit
*   Moq (for testing)
*   Docker
