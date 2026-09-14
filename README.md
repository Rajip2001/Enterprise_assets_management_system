# EAMS — Employee Administration Management System

EAMS (Employee Administration Management System) is a modern backend application designed to manage employees, users, roles, permissions, authentication, and other administrative operations through a secure and scalable RESTful API.

The project is built using **ASP.NET Core .NET 10** and follows **Clean Architecture** principles to maintain separation of concerns, testability, maintainability, and scalability.

---

## 🚀 Tech Stack

### Backend

* **.NET 10**
* **ASP.NET Core Web API**
* **Entity Framework Core**
* **SQL Server**
* **MediatR**
* **AutoMapper**
* **FluentValidation**
* **JWT Authentication**
* **Refresh Token Authentication**
* **Role-Based Access Control (RBAC)**

### Architecture & Design

* Clean Architecture
* CQRS-style request handling with MediatR
* Dependency Injection
* Repository/Service patterns where appropriate
* RESTful API design
* DTO-based communication
* Global exception handling
* Validation pipeline
* Authentication & Authorization

---

## 🏗️ Project Architecture

The solution is organized using Clean Architecture.

```text
EAMS
│
├── EAMS.API
│   ├── Controllers
│   ├── Middleware
│   ├── Extensions
│   ├── Program.cs
│   └── appsettings.json
│
├── EAMS.Application
│   ├── Common
│   ├── Features
│   │   ├── Authentication
│   │   ├── Users
│   │   ├── Roles
│   │   └── Permissions
│   ├── DTOs
│   ├── Behaviors
│   ├── Interfaces
│   └── DependencyInjection.cs
│
├── EAMS.Domain
│   ├── Entities
│   ├── Enums
│   ├── ValueObjects
│   └── Common
│
├── EAMS.Infrastructure
│   ├── Persistence
│   │   ├── Context
│   │   ├── Configurations
│   │   └── Migrations
│   ├── Repositories
│   ├── Services
│   └── DependencyInjection.cs
│
└── EAMS.sln
```

### Layer Responsibilities

#### EAMS.Domain

Contains the core business entities and rules.

This layer has no dependency on external frameworks or infrastructure implementations.

Examples:

* User
* Employee
* Role
* Permission
* RefreshToken

---

#### EAMS.Application

Contains application business logic and use cases.

Responsibilities include:

* Commands
* Queries
* DTOs
* Validators
* MediatR handlers
* Application interfaces
* Mapping profiles
* Pipeline behaviors

---

#### EAMS.Infrastructure

Contains implementations for external concerns.

Responsibilities include:

* Entity Framework Core
* Database configuration
* Migrations
* Repository implementations
* Authentication services
* Password hashing
* Token generation
* External services

---

#### EAMS.API

The entry point of the application.

Responsibilities include:

* HTTP endpoints
* Controllers
* Middleware
* Authentication configuration
* Authorization configuration
* Dependency injection configuration
* Swagger/OpenAPI

---

# 🔐 Authentication & Authorization

EAMS uses **JWT Bearer Authentication** to secure API endpoints.

The authentication system supports:

* User login
* Access tokens
* Refresh tokens
* Token expiration
* Password hashing
* Role-based authorization
* Permission-based authorization

### Authentication Flow

```text
Client
   │
   │ Login
   ▼
POST /api/auth/login
   │
   ▼
Validate Credentials
   │
   ▼
Generate Access Token
   │
   ▼
Generate Refresh Token
   │
   ▼
Return Tokens
   │
   ▼
Client
```

For protected endpoints:

```text
Client
   │
   │ Authorization: Bearer <access-token>
   ▼
ASP.NET Core Authentication
   │
   ▼
JWT Validation
   │
   ▼
Authorization / RBAC
   │
   ▼
Controller
   │
   ▼
Application Handler
```

---

# 👥 Role-Based Access Control

EAMS uses RBAC to control access to protected resources.

Example:

```text
User
 │
 └── Roles
       │
       ├── Admin
       ├── HR
       └── Employee
```

Roles can be associated with permissions.

Example:

```text
Admin
 ├── User.Read
 ├── User.Create
 ├── User.Update
 ├── User.Delete
 ├── Role.Read
 └── Role.Manage
```

This allows authorization rules to be maintained independently from individual controllers.

---

# 🧩 CQRS & MediatR

Application requests are handled using **MediatR**.

A typical feature follows this structure:

```text
Feature
│
├── Commands
│   ├── CreateUserCommand.cs
│   └── CreateUserCommandHandler.cs
│
├── Queries
│   ├── GetUserByIdQuery.cs
│   └── GetUserByIdQueryHandler.cs
│
├── DTOs
│   └── UserDto.cs
│
└── Validators
    └── CreateUserValidator.cs
```

This keeps API controllers lightweight and moves business/application logic into the Application layer.

---

# ✅ Validation

Request validation is handled using **FluentValidation**.

Example:

```text
CreateUserRequest
       │
       ▼
FluentValidation
       │
   ┌───┴───┐
   │       │
Valid   Invalid
   │       │
   ▼       ▼
Handler   Error
```

Validation failures are returned as appropriate API responses.

---

# 🗄️ Database

The project uses:

* Entity Framework Core
* SQL Server
* Code First approach
* EF Core migrations

### Add Migration

```bash
dotnet ef migrations add InitialCreate \
    --project EAMS.Infrastructure \
    --startup-project EAMS.API
```

### Update Database

```bash
dotnet ef database update \
    --project EAMS.Infrastructure \
    --startup-project EAMS.API
```

> Make sure the database connection string is correctly configured before running migrations.

---

# ⚙️ Configuration

Application configuration is maintained in:

```text
EAMS.API/appsettings.json
```

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=EAMS;Trusted_Connection=True;TrustServerCertificate=True"
  },

  "Jwt": {
    "Key": "YOUR_SECRET_KEY",
    "Issuer": "EAMS",
    "Audience": "EAMS.Client",
    "AccessTokenExpirationMinutes": 15,
    "RefreshTokenExpirationDays": 7
  }
}
```

### ⚠️ Security

Do not commit real:

* JWT secret keys
* Database passwords
* API keys
* Connection strings containing credentials
* Production secrets

Use environment variables or user secrets for sensitive configuration.

---

# 📦 Installation

## Prerequisites

Install the following:

* .NET 10 SDK
* SQL Server
* Visual Studio 2026 / VS Code
* Git
* Entity Framework Core CLI

Check .NET version:

```bash
dotnet --version
```

Install EF Core CLI if required:

```bash
dotnet tool install --global dotnet-ef
```

---

# 🔧 Setup

### 1. Clone the repository

```bash
git clone https://github.com/Rajip2001/Enterprise_assets_management_system.git
```

Move into the project directory:

```bash
cd EAMS
```

---

### 2. Restore dependencies

```bash
dotnet restore
```

---

### 3. Configure the database

Update the connection string in:

```text
EAMS.API/appsettings.json
```

---

### 4. Apply migrations

```bash
dotnet ef database update \
    --project EAMS.Infrastructure \
    --startup-project EAMS.API
```

---

### 5. Build the solution

```bash
dotnet build
```

---

### 6. Run the API

```bash
dotnet run --project EAMS.API
```

The API will be available at the URL displayed by ASP.NET Core.

Swagger can be accessed through the configured Swagger endpoint, typically:

```text
https://localhost:<port>/swagger
```

---

# 🧪 Testing

Run the solution tests using:

```bash
dotnet test
```

The project can be extended with:

* Unit tests
* Integration tests
* Authentication tests
* Authorization tests
* Handler tests
* Repository tests

---

# 📡 API Structure

The API follows RESTful conventions.

Example endpoints:

```text
/api/auth/login
/api/auth/refresh-token

/api/users
/api/users/{id}

/api/roles
/api/roles/{id}

/api/permissions
/api/permissions/{id}
```

The exact endpoints may evolve as additional EAMS modules are implemented.

---

# 📋 Development Roadmap

The project is being developed incrementally.

### Authentication

* [x] JWT authentication
* [x] Password hashing
* [x] Access tokens
* [x] Refresh tokens
* [ ] Token revocation improvements
* [ ] Session/device management

### Authorization

* [x] Role-based authorization
* [x] Permission model
* [ ] Fine-grained permission authorization
* [ ] Permission management APIs

### User Management

* [ ] User registration
* [ ] User CRUD
* [ ] User profile
* [ ] Account activation/deactivation
* [ ] Password management

### Employee Management

* [ ] Employee CRUD
* [ ] Department management
* [ ] Position/designation management
* [ ] Employee status management

### System

* [ ] Global exception handling
* [ ] Logging
* [ ] Audit logging
* [ ] Pagination
* [ ] Filtering and sorting
* [ ] API versioning
* [ ] Unit tests
* [ ] Integration tests
* [ ] Docker support
* [ ] CI/CD pipeline

---

# 📁 Git Workflow

Create a feature branch:

```bash
git checkout -b feature/feature-name
```

Check changes:

```bash
git status
```

Stage changes:

```bash
git add .
```

Commit:

```bash
git commit -m "feat: implement feature"
```

Push:

```bash
git push origin feature/feature-name
```

---

# 📝 Commit Convention

The project follows conventional commit-style messages.

Examples:

```text
feat: add user authentication
feat: implement refresh token
feat: add role management

fix: resolve jwt validation issue
fix: fix mediatR handler registration

refactor: improve application structure

docs: update readme

test: add authentication tests
```

---

# 🛡️ Security Considerations

EAMS is designed with security as an important part of the architecture.

The application should:

* Hash passwords securely
* Never store plain-text passwords
* Protect JWT signing keys
* Validate access tokens
* Validate refresh tokens
* Apply authorization to protected endpoints
* Validate incoming requests
* Avoid exposing sensitive exception details
* Store production secrets outside source control

---

# 📌 Project Status

**Status:** 🚧 Active Development

EAMS is currently under active development, with authentication, authorization, Clean Architecture, CQRS/MediatR, validation, and database infrastructure being implemented incrementally.

---

# 👨‍💻 Author

**Rajip Tuitui**

Bachelor of Computer Engineering

GitHub: `Rajip2001`

---

## 📄 License

This project is currently intended for educational and development purposes.

License information will be added as the project progresses.
