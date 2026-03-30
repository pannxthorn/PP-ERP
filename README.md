# PP-ERP

ระบบ ERP พัฒนาด้วย .NET 8 ตามแนวทาง Clean Architecture ประกอบด้วย Blazor Server สำหรับหน้าเว็บ และ REST API สำหรับ Backend

## สถาปัตยกรรม (Architecture)

โปรเจกต์แบ่งเป็น 6 โปรเจกต์ย่อย ใน 4 เลเยอร์:

```
Domain (ไม่มี dependency)
  ^
Application (Domain, DTO, MediatR)
  ^
Infrastructure (Application, DTO, EF Core SqlServer)
  ^
API (Infrastructure)        WEB (DTO, Syncfusion, Newtonsoft.Json)
```

> WEB **ไม่ได้**อ้างอิง Infrastructure โดยตรง — สื่อสารกับ API ผ่าน HTTP

### รายละเอียดแต่ละเลเยอร์

| เลเยอร์ | โปรเจกต์ | หน้าที่ |
|---------|----------|--------|
| **Domain** | `PP-ERP.Domain` | Entity เท่านั้น ไม่มี dependency ภายนอก |
| **Application** | `PP-ERP.Application` | Business logic ผ่าน MediatR (CQRS pattern) |
| **DTO** | `PP-ERP.DTO` | Data Transfer Objects ที่ใช้ร่วมกันระหว่าง API และ WEB |
| **Infrastructure** | `PP-ERP.Infrastructure` | EF Core 9, SQL Server, Repository, Unit of Work |
| **API** | `PP-ERP.API` | REST API พร้อม Swagger |
| **WEB** | `PP-ERP.WEB` | Blazor Server + Syncfusion v26.2.9 (Bootstrap 5) |

## เทคโนโลยีที่ใช้

- **.NET 8** — Framework หลัก
- **Blazor Server** — หน้าเว็บแบบ Interactive Server-Side Rendering
- **Syncfusion Blazor v26.2.9** — UI Components (Grid, Form, Spinner ฯลฯ)
- **MediatR v13** — CQRS Pattern สำหรับ Query/Command
- **Entity Framework Core 9** — ORM สำหรับจัดการฐานข้อมูล
- **SQL Server Express** — ฐานข้อมูล
- **Swashbuckle** — Swagger/OpenAPI สำหรับเอกสาร API

## วิธี Build และ Run

```bash
# Build ทั้ง solution
dotnet build PP-ERP.sln

# รัน Blazor WEB (หน้าเว็บหลัก)
dotnet run --project PP-ERP.WEB

# รัน API (https://localhost:7260, Swagger อยู่ที่ /swagger)
dotnet run --project PP-ERP.API
```

> ต้องรัน API ก่อน แล้วค่อยรัน WEB เพราะ WEB เรียก API ผ่าน HTTP

## ฐานข้อมูล

- **SQL Server** (`localhost\SQLEXPRESS`), ชื่อฐานข้อมูล: `PP_ERP`
- Connection string อยู่ใน `PP-ERP.API/appsettings.json` (ใช้ Windows Authentication)
- Migration อยู่ใน `PP-ERP.Infrastructure/Migrations/`

### คำสั่ง EF Core Migration

```bash
# สร้าง migration ใหม่
dotnet ef migrations add <ชื่อMigration> --project PP-ERP.Infrastructure --startup-project PP-ERP.API

# อัปเดตฐานข้อมูล
dotnet ef database update --project PP-ERP.Infrastructure --startup-project PP-ERP.API
```

## โครงสร้างโค้ดสำคัญ

### Entity (Domain)

Entity ใช้ชื่อแบบ **UPPERCASE** เช่น `COMPANY`, `BRANCH`, `SYS_USER`, `FLEX`, `FLEX_ITEM`

ทุก Entity มี Audit Fields ร่วมกัน:
- `CREATED_BY_ID`, `CREATED_DATE` — ผู้สร้างและวันที่สร้าง
- `LAST_UPDATE_ID`, `LAST_UPDATE_DATE` — ผู้แก้ไขล่าสุดและวันที่แก้ไข
- `IS_ACTIVE`, `IS_DELETE` — สถานะ
- `ROW_UN` (GUID) — Row unique identifier

### DTO

ตั้งชื่อตามแบบแผน:
- `RESULT_{ENTITY}_DTO` — สำหรับ Response
- `PARAM_{ENTITY}_DTO` — สำหรับ Request

จัดโฟลเดอร์ตาม Entity: `PP-ERP.DTO/{Entity}/Results/` และ `Params/`

### MediatR (CQRS)

- **Query**: `QueryGetAll{Entity}`, `QueryGet{Entity}ById`
- **Command**: `CommandCreate{Entity}`, `CommandUpdate{Entity}`, `CommandDelete{Entity}`
- แต่ละ Handler อยู่ในโฟลเดอร์ของตัวเอง: `Organization/{Entity}/Queries/{Operation}/` หรือ `Commands/{Operation}/`

### WEB Service Layer

WEB มี Service Layer ของตัวเอง สำหรับเรียก API:
- `BaseService` — Abstract base class พร้อม method Get/Post/Put/Delete
- Entity Service (เช่น `CompanyService`, `BranchService`) สืบทอดจาก `BaseService`
- URL ของ API ตั้งค่าใน `PP-ERP.WEB/appsettings.json` (`ApiBaseUrl`)

### Reusable Blazor Components

- `ListPage.razor` — Generic component สำหรับหน้า Grid (รองรับ Paging, Sorting, Filtering)
- `InfoPage.razor` — Generic component สำหรับหน้ารายละเอียด/แก้ไข

## DI Registration

แต่ละเลเยอร์มี Extension Method สำหรับลงทะเบียน Service:

```csharp
services.AddApplication();                  // MediatR handlers
services.AddInfrastructure(configuration);  // DbContext + UnitOfWork
services.AddWebServices();                  // RestCommon + Auto-discover BaseService subclasses
```

## ขั้นตอนเพิ่ม Entity ใหม่ (ครบ end-to-end)

1. สร้าง Entity class ใน `PP-ERP.Domain/Entities/` (ชื่อ UPPERCASE, ใส่ audit fields)
2. สร้าง DTO ใน `PP-ERP.DTO/{Entity}/Results/` และ `Params/`
3. สร้าง EF Configuration ใน `PP-ERP.Infrastructure/Configuration/`
4. เพิ่ม DbSet ใน `ApplicationDbContext`
5. เพิ่ม Repository property ใน `IUnitOfWork` และ `UnitOfWork`
6. สร้าง MediatR Queries/Commands ใน `PP-ERP.Application/Organization/{Entity}/`
7. สร้าง API Controller ใน `PP-ERP.API/Controllers/`
8. สร้าง WEB Service ใน `PP-ERP.WEB/Services/{Entity}/` (สืบทอด `BaseService`)
9. สร้าง Blazor Pages ใน `PP-ERP.WEB/Components/Pages/{Entity}/` (ใช้ `ListPage` และ `InfoPage`)
10. สร้าง EF Migration
