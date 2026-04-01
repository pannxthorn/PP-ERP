# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build & Run Commands

```bash
# Build entire solution
dotnet build PP-ERP.sln

# Run the API project (https://localhost:7260, Swagger at /swagger in dev)
dotnet run --project PP-ERP.API

# Run the Blazor WEB app (https://localhost:7276)
dotnet run --project PP-ERP.WEB

# EF Core migrations (run from solution root, startup project is API)
dotnet ef migrations add <MigrationName> --project PP-ERP.Infrastructure --startup-project PP-ERP.API
dotnet ef database update --project PP-ERP.Infrastructure --startup-project PP-ERP.API
```

No test project exists yet.

**Important:** API must be running before WEB — the WEB project calls the API over HTTP.

## Architecture

Clean Architecture .NET 8 solution with 6 projects across 4 layers:

**Domain** (`PP-ERP.Domain`) — Entities only, no dependencies. Entities use UPPERCASE naming (COMPANY, BRANCH, SYS_USER, FLEX, FLEX_ITEM, VENDOR, PURCHASE_ORDER) with common audit fields: `CREATED_BY_ID`, `CREATED_DATE`, `LAST_UPDATE_ID`, `LAST_UPDATE_DATE`, `IS_ACTIVE`, `IS_DELETE`, `ROW_UN` (GUID).

> **In-progress:** VENDOR and PURCHASE_ORDER have domain entities and EF configs but are **not yet wired up** — no DTOs, no IUnitOfWork repository properties, no MediatR handlers, no API controllers, and no WEB pages.

**Application** (`PP-ERP.Application`) — Business logic via MediatR v13 (CQRS pattern). Contains:
- `Repositories/IRepositories.cs` — Generic repository interface with async CRUD, predicate queries, includes, AsNoTracking, AsSplitQuery support
- `UnitOfWork/IUnitOfWork.cs` — Exposes typed repository properties (`Company`, `Branch`, `Flex`, `FlexItem`, `User`) plus transaction management
- `Organization/{Entity}/Queries|Commands/` — MediatR handlers organized by domain area then by operation type
- `Interfaces/IBlobStorageService.cs` — File storage abstraction (upload/delete/list, public & private containers, CDN support)
- `Upload/Commands/CommandUploadImage` — MediatR command that chains image resize → blob upload; exposed at `api/upload/image`

No MediatR pipeline behaviors are registered (no validation, logging, or transaction behaviors).

**Infrastructure** (`PP-ERP.Infrastructure`) — EF Core 9 with SQL Server. Contains:
- `ApplicationDbContext.cs` — DbContext with `ApplyConfigurationsFromAssembly`
- `Configuration/` — Fluent API entity configs (`IEntityTypeConfiguration<T>`), foreign keys use `DeleteBehavior.Restrict`
- `Repositories/Repositories.cs` — Generic repository implementation (AsNoTracking and SplitQuery enabled by default)
- `Persistence/UnitOfWork.cs` — UoW with lazy-loaded repositories and nested transaction support
- `DependencyInjection.cs` — Registers DbContext (scoped), `IUnitOfWork`, `IBlobStorageService` (Azure SDK), and `IImageProcessingService` (SixLabors.ImageSharp)
- `Options/AzureStorageOptions.cs` — Bound from `"AzureStorage"` config section: `ConnectionString`, `PublicContainer`, `PrivateContainer`, `StorageUrl`, `CdnUrl`, `UseCdn`
- `Services/BlobStorageService.cs` — Uploads to Azure Blob Storage with content-type mapping and cache-control headers
- `Services/ImageProcessingService.cs` — Resizes images to quality levels ("signature"=90, "profile"=85, "general"=80) using Lanczos3 resampling; orientation-aware sizing

**Presentation** — Two startup projects:
- `PP-ERP.API` — REST API with Swagger (Swashbuckle). Controllers follow standard CRUD pattern at `api/{entity}`. Controllers use MediatR `ISender` to dispatch queries/commands.
- `PP-ERP.WEB` — Blazor Server app with Syncfusion components (v26.2.9, Bootstrap 5 theme). Calls the API via `HttpClient`. Reusable generic components: `ListPage.razor` (grid pages) and `InfoPage.razor` (detail/edit pages).

**DTO** (`PP-ERP.DTO`) — Shared data transfer objects. Convention: `RESULT_{ENTITY}_DTO` for responses, `PARAM_{ENTITY}_DTO` for requests. Organized by entity in subfolders (`Company/Results/`, `Company/Params/`). Also contains `Enums/PageMode.cs` (Create, View, Edit).

## Dependency Flow

```
Domain (no deps)
  ^
Application (Domain, DTO, MediatR)
  ^
Infrastructure (Application, DTO, EF Core SqlServer)
  ^
API (Infrastructure)        WEB (DTO, Syncfusion, Newtonsoft.Json)
```

Note: WEB does **not** reference Infrastructure or Application — it communicates with the API over HTTP.

## DI Registration Pattern

Each layer exposes an `AddX()` extension method called in startup:
- `services.AddApplication()` — registers MediatR handlers from Application assembly
- `services.AddInfrastructure(configuration)` — registers DbContext and UnitOfWork
- `services.AddWebServices()` — registers `RestCommon` and auto-discovers all services inheriting `BaseService` via reflection

## WEB Service Layer

The WEB project has its own service abstraction for API calls:
- `Services/Base/RestCommon.cs` — HTTP client wrapper using `IHttpClientFactory`, JSON via Newtonsoft.Json
- `Services/Base/BaseService.cs` — Abstract base with typed Get/Post/Put/Delete methods; all entity services inherit from this
- Entity services (e.g. `CompanyService`, `BranchService`, `UserService`, `UploadService`) provide strongly-typed API calls
- `UploadService` handles multipart form-data uploads with a 2 MB max file size limit
- API base URL configured in `PP-ERP.WEB/appsettings.json` (`ApiBaseUrl`: `https://localhost:7260`)

## WEB Reusable Components

Located in `PP-ERP.WEB/Components/Layout/`:
- **`ListPage.razor`** — Generic grid component (`TValue` type param). Wraps Syncfusion `SfGrid` with paging (10/50), sorting, filtering. Columns defined via `ChildContent` RenderFragment.
- **`InfoPage.razor`** — Generic detail/edit component. Manages `PageMode` (Create/View/Edit), button states (Save/Edit/Delete/Back), and `IsReadOnly` binding. Uses `ChildContent`, `TabContent`, `StatusContent` RenderFragments.
- **`InfoSection.razor`** — Collapsible section wrapper for grouping form fields.

## WEB Page Routing Convention

Entity pages use these route patterns in `Components/Pages/{Entity}/`:
- List: `/{entity}` (e.g. `/company`)
- Create: `/{entity}/create`
- View: `/{entity}/{Id}`
- Edit: `/{entity}/{Id}/edit`

## REST API Wrapper DTOs

All API communication uses wrapper types from `PP-ERP.DTO/BaseDTO/`:
- `PARAM_REST_REQUEST` — Wraps request payloads; includes `ACCESS_TOKEN` and `AUTO_SERIALIZE_OBJECT_REQUEST_BODY`
- `RESULT_REST_RESPONSE<T>` — Wraps API responses with `IS_SUCCESS`, `STATUS_CODE`, `RESULT_CONTENT`, `DATA`
- `BASE_AZURE_BLOB` — File upload response: `FILE_NAME`, `FILE_URL`, `IS_SUCCESS`

## Soft Delete Pattern

All entities use soft deletion — never physically delete rows:
- Delete handlers set `IS_DELETE = true` and `IS_ACTIVE = false`
- All queries filter with `x => x.IS_ACTIVE && !x.IS_DELETE`

## Database

- SQL Server (`localhost\SQLEXPRESS`), database: `PP_ERP`
- Connection string in `PP-ERP.API/appsettings.json` (Windows auth / Trusted_Connection)
- Migrations in `PP-ERP.Infrastructure/Migrations/`

## Adding a New Entity (end-to-end)

1. Entity class in `PP-ERP.Domain/Entities/` (UPPERCASE naming, include audit fields)
2. DTOs in `PP-ERP.DTO/{Entity}/Results/` and `Params/` (use `[SetsRequiredMembers]` on PARAM constructors)
3. EF config in `PP-ERP.Infrastructure/Configuration/` (`IEntityTypeConfiguration<T>`, `DeleteBehavior.Restrict` for FKs)
4. DbSet in `ApplicationDbContext`
5. Repository property in `IUnitOfWork` and `UnitOfWork` (lazy-loaded pattern: `_field ??= new Repositories<T>(_context)`)
6. MediatR queries/commands in `PP-ERP.Application/Organization/{Entity}/`
7. API controller in `PP-ERP.API/Controllers/` (inject `IMediator`, standard CRUD at `api/{entity}`)
8. WEB service in `PP-ERP.WEB/Services/{Entity}/` (inheriting `BaseService` — auto-registered by reflection)
9. Blazor pages in `PP-ERP.WEB/Components/Pages/{Entity}/` (use `ListPage` and `InfoPage` generic components)
10. Add EF migration

## MediatR Handler Naming Convention

- Queries: `QueryGetAll{Entity}`, `QueryGet{Entity}ById` — returns `IEnumerable<RESULT_DTO>` or single `RESULT_DTO`
- Commands: `CommandCreate{Entity}`, `CommandUpdate{Entity}`, `CommandDelete{Entity}`
- Each handler lives in its own folder: `Organization/{Entity}/Queries/{Operation}/` or `Commands/{Operation}/`
