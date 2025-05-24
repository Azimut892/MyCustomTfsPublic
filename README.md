# MyCustomTfsPublic

A custom Task and Team Management System built with ASP.NET Core, featuring comprehensive project management, team collaboration, and task tracking capabilities.

## 🚀 Features

### Core Functionality
- **Task Management**: Create, assign, track, and manage tasks with different states (New, Defined, In Work, Completed)
- **Team Management**: Organize users into teams with designated managers
- **Project Management**: Create and manage projects with associated tasks
- **User Management**: Role-based access control with Identity framework
- **Task Analytics**: Track task statistics and progress across users and teams

### Key Capabilities
- **Role-Based Access**: Support for different user roles including Manager role
- **Team Hierarchy**: Teams with managers and members
- **Task Assignment**: Assign tasks to specific users with automatic team association
- **Project Organization**: Group tasks under projects for better organization
- **Time Tracking**: Estimated time tracking for tasks
- **Data Protection**: Built-in data protection with Entity Framework integration

## 🛠️ Technology Stack

- **Framework**: ASP.NET Core 6.0+
- **Database**: SQL Server with Entity Framework Core
- **Authentication**: ASP.NET Core Identity
- **Architecture**: Clean Architecture with Service Layer pattern
- **ORM**: Entity Framework Core
- **Dependency Injection**: Built-in ASP.NET Core DI container

## 📋 Prerequisites

- .NET 6.0 SDK or later
- SQL Server (LocalDB, Express or Full)
- Visual Studio 2022 or VS Code
- Package Manager Console access

## 🔧 Installation & Setup

### 1. Clone the Repository
```bash
git clone https://github.com/Azimut892/MyCustomTfsPublic.git
cd MyCustomTfsPublic
```

### 2. Database Setup
Open Package Manager Console in Visual Studio and run:

```powershell
# For first-time setup
Add-Migration InitialCreate
Update-Database
```

For subsequent updates:
```powershell
Add-Migration "YourChangeDescription"
Update-Database
```

### 3. Configure Connection String
Update your `appsettings.json` with your SQL Server connection string:

```json
{
  "ConnectionStrings": {
    "MyFirstWebAppContextConnection": "Server=(localdb)\\mssqllocaldb;Database=MyFirstWebApp;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

### 4. Run the Application
```bash
dotnet run
```

The application will be available at:
- HTTPS: `https://localhost:7017`
- HTTP: `http://localhost:5064`

## 🏗️ Architecture Overview

### Models
- **ApplicationUser**: Extended Identity user with team relationships
- **TaskItem**: Core task entity with states and assignments
- **Team**: Team management with manager-member relationships
- **ProjectModel**: Project organization for tasks
- **UserRoleViewModel**: Role management view model

### Services
- **UserService**: User and role management operations
- **TaskService**: Task CRUD operations and analytics
- **TeamService**: Team management and member operations
- **ProjectService**: Project management operations
- **DropdownService**: Utility service for dropdown lists
- **EnumService**: Enum handling utilities

### Key Features
- **Service Layer Pattern**: Clean separation of concerns
- **Repository Pattern**: Data access abstraction via Entity Framework
- **Dependency Injection**: All services properly registered
- **Database Relationships**: Proper foreign key relationships with cascade behaviors

## 🔒 Security Features

- **ASP.NET Core Identity**: Built-in authentication and authorization
- **Role-Based Access**: Manager and user role distinctions
- **Data Protection**: Entity Framework data protection integration
- **Secure Relationships**: Proper foreign key constraints and cascade behaviors

## 🚢 Docker Support

The application includes Docker support. Use the Container (Dockerfile) launch profile to run in Docker:

```bash
# Build and run with Docker
docker build -t mycustomtfs .
docker run -p 8080:8080 -p 8081:8081 mycustomtfs
```

## 📊 Database Schema

### Key Relationships
- Users belong to Teams (optional)
- Teams have Managers (required)
- Tasks can be assigned to Users
- Tasks can belong to Projects
- Tasks inherit Team from assigned User

### Task States
- **New**: Newly created tasks
- **Defined**: Tasks with clear requirements
- **In Work**: Tasks currently being worked on
- **Completed**: Finished tasks

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 Usage Examples

### Creating a Task
Tasks are automatically associated with the assignee's team and can be linked to projects for better organization.

### Managing Teams
Teams consist of a manager and multiple members. Tasks assigned to team members are automatically associated with their team.

### Project Organization
Projects serve as containers for related tasks, providing better organization and tracking capabilities.

## 🔄 Development Workflow

### Database Migrations
When making model changes:
1. Update your model classes
2. Run `Add-Migration "DescriptiveChangeDescription"`
3. Review the generated migration
4. Run `Update-Database` to apply changes

### Adding New Features
The application follows a clean architecture pattern:
1. Add/modify models in the `Models` folder
2. Create/update service interfaces in `Interfaces`
3. Implement services in the `Services` folder
4. Register services in `ServiceCollectionExtensions`

---

**Note**: This is a custom TFS (Team Foundation Server) alternative, designed for smaller teams and projects requiring task and team management capabilities.
