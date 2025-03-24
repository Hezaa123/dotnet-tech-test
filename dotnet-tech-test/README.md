# dotnet-tech-test

This project is a RESTful API built with ASP.NET Core that combines data from two external API endpoints into a single HTTP response. The response shows the combined collection so that each Album contains a collection of its Photos.

## Requirements

- .NET 8 or later
- Visual Studio, Visual Studio Code, JetBrains Rider, or any other IDE that supports .NET development

## Endpoints

The API consists of two operations:
1. `/combined` - Returns all the data from the provided endpoints.
2. `/combined/{userId}` - Returns data relating to a single User ID.

## External API Endpoints

- [Photos](http://jsonplaceholder.typicode.com/photos)
- [Albums](http://jsonplaceholder.typicode.com/albums)

## Getting Started

### Prerequisites

- .NET 8 SDK
- An IDE such as Visual Studio, Visual Studio Code, or Rider

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/dotnet-tech-test.git
   cd dotnet-tech-test

2. Restore the dependencies:
   ```bash
   dotnet restore

3. Build the project:
   ```bash
   dotnet build

### Running the application

1. Run the application:
   ```bash
   dotnet run
   
2. Open your browser and navigate to https://localhost:5001/swagger/index.html to access the Swagger UI.

### Running the tests
   ```bash
   dotnet test
   ```

### Project structure

   ```bash
    dotnet-tech-test
    ├── Interfaces
    │   ├── IAlbumServices.cs
    │   └── IDataCalls.cs
    ├── Models
    │   ├── Album.cs
    │   └── Photo.cs
    ├── Services
    │   ├── AlbumService.cs
    │   └── DataCalls.cs
    └── Program.cs
    dotnet-tech-test.Tests
    └── Services
        └── AlbumServiceTests.cs
```

### Usage
Get All Combined Data
- Endpoint: /combined
- Method: GET
- Response: Returns a list of albums, each containing a list of photos.

Get Combined Data by User ID
- Endpoint: /combined/{userId}
- Method: GET
- Parameters:
  - userId (int): The ID of the user.
- Response: Returns a list of albums for the specified user, each containing a list of photos.
  
Error Handling
- The application includes global exception handling middleware to catch and log unhandled exceptions. Specific error handling within the service methods provides more detailed logging and error messages.

Logging
- The application uses ILogger for logging errors and information. Logs are written to the console.

### TODO
- Write unit tests for the full application
- Refactor with controllers to minimise what's in Program.cs
- Improve the GetData calls so they run in parallel
- Think about timeouts
- Add a front-end