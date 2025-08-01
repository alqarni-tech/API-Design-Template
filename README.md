# API Design Template

[![Build Status](https://img.shields.io/badge/build-passing-brightgreen)](https://github.com/your-github-username/API-Design-Template/actions)
[![Tests](https://img.shields.io/badge/tests-passing-brightgreen)](https://github.com/your-github-username/API-Design-Template/actions)
[![Coverage](https://img.shields.io/badge/coverage-100%25-brightgreen)](https://codecov.io/gh/your-github-username/API-Design-Template)
<!-- Update badge URLs for your actual CI/CD and coverage provider -->

A comprehensive .NET 8 API template featuring JWT authentication, role-based authorization, API versioning, rate limiting, and Swagger documentation.

## Features

- **JWT Authentication**: Secure token-based authentication
- **Role-Based Authorization**: Admin and User roles with protected endpoints
- **API Versioning**: Built-in versioning support with URL-based versioning
- **Rate Limiting**: Configurable rate limiting to prevent abuse
- **Swagger Documentation**: Interactive API documentation with JWT support
- **Clean Architecture**: Separation of concerns with Core, Application, Infrastructure layers
- **Entity Framework Core**: In-memory database for development
- **SOLID Principles**: Well-structured, maintainable code

## Project Structure

```
src/
├── API.Design.Template/              # API Layer (Controllers, Program.cs)
├── API.Design.Template.Application/  # Application Layer (Services, DTOs)
├── API.Design.Template.Core/         # Core Layer (Entities, Interfaces)
└── API.Design.Template.Infrastructure/ # Infrastructure Layer (DbContext, Repositories)
```

## Getting Started

### Prerequisites

- .NET 8 SDK
- Visual Studio 2022 or VS Code

### Installation

1. Clone the repository
2. Navigate to the project directory:
   ```bash
   cd SaaS-Starter-Solutions/API-Design-Template
   ```

3. Restore dependencies:
   ```bash
   dotnet restore
   ```

4. Run the application:
   ```bash
   dotnet run --project src/API.Design.Template
   ```

5. Open your browser and navigate to `https://localhost:7001` to access Swagger UI

## API Endpoints

### Authentication

- `POST /api/auth/register` - Register a new user
- `POST /api/auth/login` - Login and get JWT token

### User Management

- `GET /api/user/profile` - Get current user profile (requires authentication)
- `PUT /api/user/profile` - Update user profile (requires authentication)
- `GET /api/user/admin/users` - Get all users (requires Admin role)

## Authentication

The API uses JWT Bearer tokens for authentication. To access protected endpoints:

1. Register or login to get a JWT token
2. Include the token in the Authorization header:
   ```
   Authorization: Bearer <your-jwt-token>
   ```

## Configuration

Key configuration settings in `appsettings.json`:

```json
{
  "Jwt": {
    "Secret": "YourSuperSecretKeyHere12345678901234567890",
    "Issuer": "API-Design-Template",
    "Audience": "API-Design-Template-Users",
    "ExpiryInHours": 1
  },
  "RateLimiting": {
    "PermitLimit": 100,
    "WindowInMinutes": 1
  }
}
```

## Development

### Adding New Endpoints

1. Create DTOs in the Application layer
2. Add business logic in Application services
3. Create controllers in the API layer
4. Add repository methods if needed

### Database

The project uses Entity Framework Core with an in-memory database for development. For production, update the connection string in `Program.cs`.

## Testing

Run the tests:
```bash
dotnet test
```

## 🧪 Code Coverage & Static Analysis

- **Code Coverage:**
  - Use [Coverlet](https://github.com/coverlet-coverage/coverlet) for .NET code coverage.
  - Example CI step:
    ```yaml
    - name: Test with coverage
      run: dotnet test --collect:"XPlat Code Coverage"
    - name: Upload coverage to Codecov
      uses: codecov/codecov-action@v4
      with:
        files: ./TestResults/**/coverage.cobertura.xml
    ```
  - Update your pipeline to upload coverage reports to [Codecov](https://codecov.io/) or [Coveralls](https://coveralls.io/).

- **Static Analysis:**
  - Integrate [SonarQube](https://www.sonarqube.org/) or [dotnet format](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-format) in your CI for code quality.
  - Example CI step:
    ```yaml
    - name: Run SonarQube analysis
      run: dotnet sonarscanner begin ...
    ```
  - Add rules and quality gates as needed.

<!-- Update your CI/CD pipeline to include these steps for best results. -->

## Deployment

The project includes GitHub Actions CI/CD pipeline that:
- Builds the solution
- Runs tests
- Publishes artifacts
- Deploys to staging/production environments

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests
5. Submit a pull request

## License

This project is licensed under the MIT License. 