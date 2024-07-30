# Web API App

## Overview

The Web API App is a .NET 8-based application that processes and stores SWIFT MT799 messages. The API allows users to upload a specific SWIFT MT799 message file, parses the content, and saves it to a SQLite database. The application includes error handling and logging features, using NLog for logging.

## Features

- **File Upload**: Accepts file uploads via POST requests.
- **File Validation**: Ensures that the uploaded file is the correct one and is not empty.
- **Message Parsing**: Parses SWIFT MT799 messages and extracts fields.
- **Database Storage**: Saves parsed message fields into a SQLite database.
- **Documentation**: Provides Swagger documentation for API testing and interaction.

## How It Works

### Uploading Files

- **Endpoint**: `/Swift/UploadFile`
- **Method**: POST
- **Requirements**:
  - The uploaded file must be named `example_mt799.txt`.
  - The file cannot be empty.

### File Validation

- **Process**:
  - The API validates the uploaded file to ensure it meets the required criteria.
  - If the file does not meet the criteria (wrong name or empty), the API returns a `BadRequest` response with an appropriate error message.

### Message Parsing

- **Process**:
  - Upon successful file upload, the content of the file is read and parsed.
  - The content is processed to extract relevant fields based on the SWIFT MT799 message format.

### Database Storage

- **Database**: SQLite
- **File Name**: `swiftmessages.db`
- **Process**:
  - The extracted fields from the SWIFT MT799 message are stored in the SQLite database.
  - The database schema is designed to efficiently store the parsed message content.

### Documentation and Testing

- **Swagger**: 
  - Swagger documentation is provided for testing and interacting with the API.
  - Access the Swagger UI at `/swagger` to test the API endpoints and view the documentation.

## Technologies Used

- **.NET 8**
- **SQLite**
- **NLog**
- **Swagger**
- **Microsoft.Data.Sqlite**
- **ASP.NET Core**

## Project Structure

The project is organized into several class library projects to separate concerns and improve maintainability. Each project serves a specific purpose:

- **WebApiApp**: 
  - Contains the Web API controller and the `Program.cs` file. 
  - This project defines the HTTP endpoints and handles incoming requests.
  - It sets up the dependency injection, middleware, and routing for the application.

- **Services**: 
  - Contains the core business logic and service implementations.
  - This project is responsible for processing the data and implementing the application’s functionality.
  - Services interact with the data layer and provide operations for the Web API controllers.

- **ServiceContracts**:
  - Contains the interfaces and Data Transfer Objects (DTOs) used for communication between different layers.
  - Defines the contracts for the services and DTOs to ensure a clear separation between the service definitions and their implementations.
  - Facilitates dependency injection and mocking for unit testing.

- **Entities**:
  - Contains the data models or entities used by the application.
  - Defines the structure of the data that will be stored in the database.
  - Ensures that the application's data model aligns with the database schema and application requirements.

This structure promotes a clean separation of concerns, making the codebase easier to navigate, maintain, and extend.

## Database

- **SQLite: The database used for storing parsed SWIFT MT799 messages.**
- **File Location: swiftmessages.db**

## Logging
- **NLog: Configured to log application events and errors. Configuration is located in nlog.config.**
