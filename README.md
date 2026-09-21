# 📁 Project Structure

```text
🏠 MyCompany.MyProject
│
├── 📄 MyCompany.MyProject.sln
├── 📄 NuGet.Config
├── 📄 .gitignore
├── 📄 .dockerignore
│
├── 🔨 build/
│   ├── 📜 build-mvc.ps1
│   └── 📜 build-with-ng.sh
│
├── 🐳 docker/
│   └── 📦 mvc/
│       ├── 🐳 docker-compose.yml
│       ├── ⬇️ down.ps1
│       └── ⬆️ up.ps1
│
└── 📂 src/
    │
    ├── 🧠 MyCompany.MyProject.Core/
    │   │
    │   ├── ⚙️ Configuration/
    │   ├── 🔐 Authorization/
    │   │   ├── 👤 Users/
    │   │   └── 👥 Roles/
    │   │
    │   ├── 🎓 Academics/
    │   │   ├── 📘 Class.cs
    │   │   ├── 📗 GradeLevel.cs
    │   │   ├── 📕 Semester.cs
    │   │   └── 📙 Subject.cs
    │   │
    │   ├── 📝 Grading/
    │   │   ├── 📊 AcademicPerformance.cs
    │   │   ├── 🔒 GradeLockStatus.cs
    │   │   ├── 📋 GradeRecord.cs
    │   │   └── 👨‍🏫 TeachingAssignment.cs
    │   │
    │   ├── 👨‍🎓 Profiles/
    │   │   ├── 🎓 Student.cs
    │   │   └── 👨‍🏫 Teacher.cs
    │   │
    │   ├── 🏢 MultiTenancy/
    │   ├── 🔑 Identity/
    │   ├── 🌐 Localization/
    │   ├── 🧩 Features/
    │   ├── 📦 Editions/
    │   ├── ⏱️ Timing/
    │   └── ✅ Validation/
    │
    │
    ├── ⚙️ MyCompany.MyProject.Application/
    │   │
    │   ├── 🎓 Academics/
    │   │   ├── ⚙️ ClassAppService.cs
    │   │   ├── ⚙️ SemesterAppService.cs
    │   │   ├── ⚙️ SubjectAppService.cs
    │   │   ├── ⚙️ TeachingAssignmentAppService.cs
    │   │   └── 📦 Dto/
    │   │       ├── 📄 ClassDto.cs
    │   │       ├── 📄 SemesterDto.cs
    │   │       ├── 📄 SubjectDto.cs
    │   │       └── 📄 TeachingAssignmentDto.cs
    │   │
    │   ├── 📝 Grading/
    │   │   ├── ⚙️ GradeAppService.cs
    │   │   └── 📦 Dto/
    │   │       ├── 📊 GetGradeBookInput.cs
    │   │       ├── 📄 GradeRecordDtoBase.cs
    │   │       ├── 👨‍🎓 GradeStudentRowDto.cs
    │   │       ├── 🔒 LockGradeInput.cs
    │   │       ├── 💾 SaveGradeItemDto.cs
    │   │       └── 💾 SaveGradeListInput.cs
    │   │
    │   ├── 👤 Users/
    │   │   ├── ⚙️ UserAppService.cs
    │   │   ├── 📄 IUserAppService.cs
    │   │   └── 📦 Dto/
    │   │
    │   ├── 👥 Roles/
    │   │   ├── ⚙️ RoleAppService.cs
    │   │   ├── 📄 IRoleAppService.cs
    │   │   └── 📦 Dto/
    │   │
    │   ├── 👨‍🎓 Profiles/
    │   │   ├── 🎓 StudentAppService.cs
    │   │   ├── 👨‍🏫 TeacherAppService.cs
    │   │   └── 📦 Dto/
    │   │
    │   ├── 🔐 Authorization/
    │   ├── ⚙️ Configuration/
    │   ├── 🏢 MultiTenancy/
    │   └── 🕐 Sessions/
    │
    │
    ├── 🗄️ MyCompany.MyProject.EntityFrameworkCore/
    │   │
    │   ├── 🗄️ EntityFrameworkCore/
    │   │   ├── 💾 MyProjectDbContext.cs
    │   │   ├── ⚙️ MyProjectDbContextConfigurer.cs
    │   │   ├── 🏭 MyProjectDbContextFactory.cs
    │   │   ├── 🔧 AbpZeroDbMigrator.cs
    │   │   │
    │   │   ├── 📚 Repositories/
    │   │   │   └── 📦 MyProjectRepositoryBase.cs
    │   │   │
    │   │   └── 🌱 Seed/
    │   │       ├── 🌱 SeedHelper.cs
    │   │       ├── 🏠 Host/
    │   │       └── 🏢 Tenants/
    │   │
    │   └── 🔄 Migrations/
    │       ├── 🏫 Initial_School_Entities
    │       ├── 📊 Add_Grading_System_Entities
    │       └── 📸 MyProjectDbContextModelSnapshot.cs
    │
    │
    ├── 🔄 MyCompany.MyProject.Migrator/
    │   ├── ▶️ Program.cs
    │   ├── ⚙️ appsettings.json
    │   ├── 📝 Log.cs
    │   ├── 🔄 MultiTenantMigrateExecuter.cs
    │   └── ⚙️ DependencyInjection/
    │       └── ServiceCollectionRegistrar.cs
    │
    │
    ├── 🌐 MyCompany.MyProject.Web.Core/
    │   │
    │   ├── 🔐 Authentication/
    │   │   ├── 🔑 JwtBearer/
    │   │   │   ├── JwtTokenMiddleware.cs
    │   │   │   └── TokenAuthConfiguration.cs
    │   │   └── 🌍 External/
    │   │
    │   ├── 🎮 Controllers/
    │   │   ├── MyProjectControllerBase.cs
    │   │   └── TokenAuthController.cs
    │   │
    │   ├── 👤 Identity/
    │   ├── 📦 Models/
    │   └── ⚙️ Configuration/
    │
    │
    └── 🚀 MyCompany.MyProject.Web.Host/
        ├── ⚙️ appsettings.json
        ├── 🐳 Dockerfile
        ├── 📝 log4net.config
        └── 📄 MyCompany.MyProject.Web.Host.csproj
```

---

## 🏗️ Architecture Overview

```text
                    🌐 CLIENT
                       │
                       ▼
              🚀 Web.Host
                       │
                       ▼
              🌐 Web.Core
          Authentication / JWT
                       │
                       ▼
              ⚙️ Application
          AppService / DTO / Logic
                       │
                       ▼
                 🧠 Core
          Entity / Domain / Rules
                       │
                       ▼
          🗄️ EntityFrameworkCore
       DbContext / Repository / EF Core
                       │
                       ▼
                 🗃️ SQL Server
```

## 📦 Main Projects

| Project                 | Icon | Responsibility                         |
| ----------------------- | ---- | -------------------------------------- |
| **Core**                | 🧠   | Entity, domain rules, authorization    |
| **Application**         | ⚙️   | AppService, DTO, application logic     |
| **EntityFrameworkCore** | 🗄️  | DbContext, Repository, Migration, Seed |
| **Migrator**            | 🔄   | Database migration and initialization  |
| **Web.Core**            | 🌐   | Authentication, JWT, Controllers       |
| **Web.Host**            | 🚀   | ASP.NET Core application host          |

## 🎓 Education Management Modules

```text
🎓 Education Management System
│
├── 👨‍🎓 Student
│
├── 👨‍🏫 Teacher
│
├── 🏫 Class
│
├── 📚 Subject
│
├── 📅 Semester
│
├── 🎓 Grade Level
│
├── 👨‍🏫 Teaching Assignment
│
└── 📝 Grading
    ├── 📊 Grade Record
    ├── 📈 Academic Performance
    └── 🔒 Grade Lock
```

## 🔄 Application Flow

```text
👤 User
  │
  ▼
🌐 Controller
  │
  ▼
⚙️ AppService
  │
  ▼
🧠 Domain Entity
  │
  ▼
🗄️ Repository
  │
  ▼
💾 DbContext
  │
  ▼
🗃️ Database
```

## 🛠️ Technology

```text
💻 ASP.NET Core
⚡ .NET 9
🏗️ ASP.NET Boilerplate
🗄️ Entity Framework Core
🛢️ SQL Server
🔐 JWT Authentication
🗺️ AutoMapper
📦 Dependency Injection
🐳 Docker
🔄 Database Migration
```
