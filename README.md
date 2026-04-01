<div align="center">

# PP-ERP

**Enterprise Resource Planning System**  
**ระบบวางแผนทรัพยากรองค์กร**

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![Blazor](https://img.shields.io/badge/Blazor-Server-512BD4?logo=blazor)](https://blazor.net/)
[![EF Core](https://img.shields.io/badge/EF%20Core-9.0-512BD4)](https://docs.microsoft.com/ef/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-Express-CC2927?logo=microsoftsqlserver)](https://www.microsoft.com/sql-server)
[![Azure](https://img.shields.io/badge/Azure-Blob%20Storage-0078D4?logo=microsoftazure)](https://azure.microsoft.com/)

</div>

---

## Table of Contents / สารบัญ

- [English](#english)
  - [Project Overview](#project-overview)
  - [Tech Stack](#tech-stack)
  - [Architecture](#architecture)
  - [How to Run](#how-to-run)
  - [API Examples](#api-examples)
- [ภาษาไทย](#ภาษาไทย)
  - [ภาพรวมโปรเจค](#ภาพรวมโปรเจค)
  - [เทคโนโลยีที่ใช้](#เทคโนโลยีที่ใช้)
  - [สถาปัตยกรรมระบบ](#สถาปัตยกรรมระบบ)
  - [วิธีการรันโปรเจค](#วิธีการรันโปรเจค)
  - [ตัวอย่าง API](#ตัวอย่าง-api)

---

# English

## Project Overview

PP-ERP is a full-stack **Enterprise Resource Planning** system built with **.NET 8** following **Clean Architecture** principles. It manages core business operations including company/branch management, user administration, vendor records, and purchase orders.

The system consists of a **REST API backend** and a **Blazor Server frontend** that communicate over HTTP. File assets are stored in **Azure Blob Storage** with built-in image processing support.

**Current Features:**
- Company & Branch management
- System User management
- Flexible master data (FLEX / FLEX_ITEM) for dynamic lookup values
- Vendor management *(domain ready, UI in progress)*
- Purchase Order management *(domain ready, UI in progress)*
- Image upload with automatic resizing via Azure Blob Storage

---

## Tech Stack

| Layer | Technology |
|-------|-----------|
| Language | C# 12 / .NET 8 |
| Frontend | Blazor Server |
| UI Components | Syncfusion Blazor v26.2.9 (Bootstrap 5) |
| Backend | ASP.NET Core Web API |
| ORM | Entity Framework Core 9 |
| Database | SQL Server Express |
| CQRS / Mediator | MediatR v13 |
| File Storage | Azure Blob Storage (Azure.Storage.Blobs v12) |
| Image Processing | SixLabors.ImageSharp v3 |
| API Docs | Swagger / Swashbuckle |
| JSON | Newtonsoft.Json |

---

## Architecture

The solution follows **Clean Architecture** with 6 projects across 4 layers:

```
┌─────────────────────────────────────────────┐
│              Presentation Layer              │
│   PP-ERP.API  — REST API + Swagger          │
│   PP-ERP.WEB  — Blazor Server + Syncfusion  │
├─────────────────────────────────────────────┤
│            Infrastructure Layer             │
│   PP-ERP.Infrastructure                     │
│   (EF Core, SQL Server, Azure Blob,         │
│    Image Processing, UnitOfWork)            │
├─────────────────────────────────────────────┤
│             Application Layer               │
│   PP-ERP.Application — MediatR CQRS        │
│   PP-ERP.DTO         — Shared DTOs          │
├─────────────────────────────────────────────┤
│               Domain Layer                  │
│   PP-ERP.Domain — Entities only             │
└─────────────────────────────────────────────┘
```

**Dependency flow:**
```
Domain ← Application ← Infrastructure ← API
                                      ← WEB (HTTP only, no direct reference)
```

### Domain Entities

All entities follow UPPERCASE naming with standard audit fields (`CREATED_BY_ID`, `CREATED_DATE`, `LAST_UPDATE_ID`, `LAST_UPDATE_DATE`, `IS_ACTIVE`, `IS_DELETE`, `ROW_UN`).

| Entity | Description | Status |
|--------|-------------|--------|
| `COMPANY` | Company / Organization | ✅ Full stack |
| `BRANCH` | Company branches | ✅ Full stack |
| `SYS_USER` | System users | ✅ Full stack |
| `FLEX` | Dynamic master data categories | ✅ Full stack |
| `FLEX_ITEM` | Dynamic master data items | ✅ Full stack |
| `VENDOR` | Vendors / Suppliers | 🔧 Domain + DB only |
| `PURCHASE_ORDER` | Purchase orders | 🔧 Domain + DB only |

### CQRS Pattern (MediatR)

Each entity follows the handler naming convention:

```
Application/
└── Organization/
    └── {Entity}/
        ├── Queries/
        │   ├── QueryGetAll{Entity}/
        │   └── QueryGet{Entity}ById/
        └── Commands/
            ├── CommandCreate{Entity}/
            ├── CommandUpdate{Entity}/
            └── CommandDelete{Entity}/
```

### Soft Delete Pattern

Entities are **never physically deleted**. Delete operations set `IS_DELETE = true` and `IS_ACTIVE = false`. All queries filter with `WHERE IS_ACTIVE = 1 AND IS_DELETE = 0`.

### Reusable WEB Components

| Component | Description |
|-----------|-------------|
| `ListPage.razor` | Generic grid with paging (10/50), sorting, filtering |
| `InfoPage.razor` | Detail/edit page with PageMode (Create/View/Edit) |
| `InfoSection.razor` | Collapsible section wrapper for grouping form fields |

---

## How to Run

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server Express (instance: `localhost\SQLEXPRESS`)
- Azure Storage Account (for image uploads)

### 1. Clone the repository

```bash
git clone https://github.com/<your-username>/PP-ERP.git
cd PP-ERP
```

### 2. Configure the API

Edit `PP-ERP.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=PP_ERP;Trusted_Connection=True;"
  },
  "AzureStorage": {
    "ConnectionString": "<your-azure-storage-connection-string>",
    "PublicContainer": "public",
    "PrivateContainer": "private",
    "StorageUrl": "https://<account>.blob.core.windows.net",
    "CdnUrl": "",
    "UseCdn": false
  }
}
```

### 3. Apply database migrations

```bash
dotnet ef database update --project PP-ERP.Infrastructure --startup-project PP-ERP.API
```

### 4. Run the API

```bash
dotnet run --project PP-ERP.API
# API:     https://localhost:7260
# Swagger: https://localhost:7260/swagger
```

### 5. Run the Web app

Open a **new terminal**, then:

```bash
dotnet run --project PP-ERP.WEB
# Web: https://localhost:7276
```

> **Note:** The API must be running before starting the WEB app.

### Adding new migrations

```bash
dotnet ef migrations add <MigrationName> \
  --project PP-ERP.Infrastructure \
  --startup-project PP-ERP.API
```

---

## API Examples

Base URL: `https://localhost:7260`

All requests use the `PARAM_REST_REQUEST` wrapper body, and all responses are wrapped in `RESULT_REST_RESPONSE<T>`.

### Get all companies

```http
GET /api/company
```

**Response:**
```json
{
  "IS_SUCCESS": true,
  "STATUS_CODE": 200,
  "RESULT_CONTENT": "Success",
  "DATA": [
    {
      "COMPANY_ID": 1,
      "COMPANY_NAME": "PP Company Ltd.",
      "COMPANY_CODE": "PP001",
      "IS_ACTIVE": true,
      "IS_DELETE": false
    }
  ]
}
```

### Get company by ID

```http
GET /api/company/{id}
```

### Create a company

```http
POST /api/company
Content-Type: application/json

{
  "AUTO_SERIALIZE_OBJECT_REQUEST_BODY": {
    "COMPANY_NAME": "New Company Ltd.",
    "COMPANY_CODE": "NC001",
    "COMPANY_ADDRESS": "123 Main Street"
  }
}
```

### Update a company

```http
PUT /api/company/{id}
Content-Type: application/json

{
  "AUTO_SERIALIZE_OBJECT_REQUEST_BODY": {
    "COMPANY_NAME": "Updated Company Ltd.",
    "COMPANY_CODE": "NC001"
  }
}
```

### Delete a company (soft delete)

```http
DELETE /api/company/{id}
```

### Upload an image

```http
POST /api/upload/image
Content-Type: multipart/form-data

file=<image file>   (max 2 MB)
```

**Response:**
```json
{
  "IS_SUCCESS": true,
  "FILE_NAME": "abc123.jpg",
  "FILE_URL": "https://<storage>.blob.core.windows.net/public/abc123.jpg"
}
```

### Available Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/company` | List all companies |
| GET | `/api/company/{id}` | Get company by ID |
| POST | `/api/company` | Create company |
| PUT | `/api/company/{id}` | Update company |
| DELETE | `/api/company/{id}` | Delete company |
| GET | `/api/branch` | List all branches |
| GET | `/api/branch/{id}` | Get branch by ID |
| POST | `/api/branch` | Create branch |
| PUT | `/api/branch/{id}` | Update branch |
| DELETE | `/api/branch/{id}` | Delete branch |
| GET | `/api/user` | List all users |
| GET | `/api/user/{id}` | Get user by ID |
| POST | `/api/user` | Create user |
| PUT | `/api/user/{id}` | Update user |
| DELETE | `/api/user/{id}` | Delete user |
| POST | `/api/upload/image` | Upload image |

---

---

# ภาษาไทย

## ภาพรวมโปรเจค

PP-ERP คือระบบ **Enterprise Resource Planning (ERP)** แบบ full-stack ที่พัฒนาด้วย **.NET 8** โดยใช้หลักการ **Clean Architecture** รองรับการจัดการกระบวนการทางธุรกิจหลัก เช่น การจัดการบริษัทและสาขา, การดูแลผู้ใช้งาน, ข้อมูล Vendor และใบสั่งซื้อ

ระบบประกอบด้วย **REST API backend** และ **Blazor Server frontend** ที่สื่อสารกันผ่าน HTTP ไฟล์ต่าง ๆ จัดเก็บบน **Azure Blob Storage** พร้อมรองรับการประมวลผลภาพในตัว

**ฟีเจอร์ปัจจุบัน:**
- จัดการบริษัทและสาขา
- จัดการผู้ใช้งานในระบบ
- ข้อมูล Master Data แบบยืดหยุ่น (FLEX / FLEX_ITEM)
- จัดการข้อมูล Vendor *(Domain พร้อม, UI อยู่ระหว่างพัฒนา)*
- จัดการใบสั่งซื้อ *(Domain พร้อม, UI อยู่ระหว่างพัฒนา)*
- อัปโหลดรูปภาพพร้อม resize อัตโนมัติผ่าน Azure Blob Storage

---

## เทคโนโลยีที่ใช้

| Layer | เทคโนโลยี |
|-------|-----------|
| ภาษา | C# 12 / .NET 8 |
| Frontend | Blazor Server |
| UI Components | Syncfusion Blazor v26.2.9 (Bootstrap 5) |
| Backend | ASP.NET Core Web API |
| ORM | Entity Framework Core 9 |
| ฐานข้อมูล | SQL Server Express |
| CQRS / Mediator | MediatR v13 |
| File Storage | Azure Blob Storage (Azure.Storage.Blobs v12) |
| ประมวลผลภาพ | SixLabors.ImageSharp v3 |
| API Docs | Swagger / Swashbuckle |
| JSON | Newtonsoft.Json |

---

## สถาปัตยกรรมระบบ

Solution ใช้ **Clean Architecture** แบ่งออกเป็น 6 โปรเจคใน 4 Layer:

```
┌─────────────────────────────────────────────┐
│           Presentation Layer (UI)            │
│   PP-ERP.API  — REST API + Swagger          │
│   PP-ERP.WEB  — Blazor Server + Syncfusion  │
├─────────────────────────────────────────────┤
│           Infrastructure Layer              │
│   PP-ERP.Infrastructure                     │
│   (EF Core, SQL Server, Azure Blob,         │
│    Image Processing, UnitOfWork)            │
├─────────────────────────────────────────────┤
│            Application Layer                │
│   PP-ERP.Application — MediatR CQRS        │
│   PP-ERP.DTO         — Shared DTOs          │
├─────────────────────────────────────────────┤
│              Domain Layer                   │
│   PP-ERP.Domain — Entities เท่านั้น         │
└─────────────────────────────────────────────┘
```

**ทิศทาง Dependency:**
```
Domain ← Application ← Infrastructure ← API
                                      ← WEB (ผ่าน HTTP เท่านั้น, ไม่ reference โดยตรง)
```

### Domain Entities

Entity ทั้งหมดตั้งชื่อเป็น UPPERCASE และมี audit fields มาตรฐาน (`CREATED_BY_ID`, `CREATED_DATE`, `LAST_UPDATE_ID`, `LAST_UPDATE_DATE`, `IS_ACTIVE`, `IS_DELETE`, `ROW_UN`)

| Entity | คำอธิบาย | สถานะ |
|--------|----------|-------|
| `COMPANY` | บริษัท / องค์กร | ✅ ครบทุก Layer |
| `BRANCH` | สาขา | ✅ ครบทุก Layer |
| `SYS_USER` | ผู้ใช้งานระบบ | ✅ ครบทุก Layer |
| `FLEX` | หมวดหมู่ Master Data แบบยืดหยุ่น | ✅ ครบทุก Layer |
| `FLEX_ITEM` | รายการ Master Data แบบยืดหยุ่น | ✅ ครบทุก Layer |
| `VENDOR` | Vendor / ผู้จำหน่าย | 🔧 Domain + DB เท่านั้น |
| `PURCHASE_ORDER` | ใบสั่งซื้อ | 🔧 Domain + DB เท่านั้น |

### รูปแบบ CQRS (MediatR)

แต่ละ Entity ใช้ handler ตาม naming convention ดังนี้:

```
Application/
└── Organization/
    └── {Entity}/
        ├── Queries/
        │   ├── QueryGetAll{Entity}/
        │   └── QueryGet{Entity}ById/
        └── Commands/
            ├── CommandCreate{Entity}/
            ├── CommandUpdate{Entity}/
            └── CommandDelete{Entity}/
```

### Soft Delete Pattern

Entity ทุกตัว **ไม่มีการลบข้อมูลจริง** — การลบจะ set `IS_DELETE = true` และ `IS_ACTIVE = false` แทน Query ทั้งหมดกรองด้วย `WHERE IS_ACTIVE = 1 AND IS_DELETE = 0`

### WEB Components ที่ใช้ซ้ำได้

| Component | คำอธิบาย |
|-----------|----------|
| `ListPage.razor` | Grid component ทั่วไปพร้อม paging (10/50), sorting, filtering |
| `InfoPage.razor` | หน้าดูรายละเอียด/แก้ไข พร้อม PageMode (Create/View/Edit) |
| `InfoSection.razor` | Collapsible section สำหรับจัดกลุ่ม form fields |

---

## วิธีการรันโปรเจค

### ข้อกำหนดเบื้องต้น

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server Express (instance: `localhost\SQLEXPRESS`)
- Azure Storage Account (สำหรับอัปโหลดรูปภาพ)

### 1. Clone โปรเจค

```bash
git clone https://github.com/<your-username>/PP-ERP.git
cd PP-ERP
```

### 2. ตั้งค่า API

แก้ไขไฟล์ `PP-ERP.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=PP_ERP;Trusted_Connection=True;"
  },
  "AzureStorage": {
    "ConnectionString": "<azure-storage-connection-string>",
    "PublicContainer": "public",
    "PrivateContainer": "private",
    "StorageUrl": "https://<account>.blob.core.windows.net",
    "CdnUrl": "",
    "UseCdn": false
  }
}
```

### 3. สร้าง Database จาก Migrations

```bash
dotnet ef database update --project PP-ERP.Infrastructure --startup-project PP-ERP.API
```

### 4. รัน API

```bash
dotnet run --project PP-ERP.API
# API:     https://localhost:7260
# Swagger: https://localhost:7260/swagger
```

### 5. รัน Web App

เปิด **terminal ใหม่** แล้วรัน:

```bash
dotnet run --project PP-ERP.WEB
# Web: https://localhost:7276
```

> **หมายเหตุ:** ต้องรัน API ก่อนเสมอ ก่อนจะเปิด WEB app

### เพิ่ม Migration ใหม่

```bash
dotnet ef migrations add <ชื่อ Migration> \
  --project PP-ERP.Infrastructure \
  --startup-project PP-ERP.API
```

---

## ตัวอย่าง API

Base URL: `https://localhost:7260`

Request ทั้งหมดห่อด้วย `PARAM_REST_REQUEST` และ Response ทั้งหมดห่อด้วย `RESULT_REST_RESPONSE<T>`

### ดึงรายการบริษัททั้งหมด

```http
GET /api/company
```

**Response:**
```json
{
  "IS_SUCCESS": true,
  "STATUS_CODE": 200,
  "RESULT_CONTENT": "Success",
  "DATA": [
    {
      "COMPANY_ID": 1,
      "COMPANY_NAME": "PP Company Ltd.",
      "COMPANY_CODE": "PP001",
      "IS_ACTIVE": true,
      "IS_DELETE": false
    }
  ]
}
```

### ดึงบริษัทตาม ID

```http
GET /api/company/{id}
```

### สร้างบริษัทใหม่

```http
POST /api/company
Content-Type: application/json

{
  "AUTO_SERIALIZE_OBJECT_REQUEST_BODY": {
    "COMPANY_NAME": "บริษัท ใหม่ จำกัด",
    "COMPANY_CODE": "NC001",
    "COMPANY_ADDRESS": "123 ถนนสุขุมวิท"
  }
}
```

### แก้ไขข้อมูลบริษัท

```http
PUT /api/company/{id}
Content-Type: application/json

{
  "AUTO_SERIALIZE_OBJECT_REQUEST_BODY": {
    "COMPANY_NAME": "บริษัท ใหม่ จำกัด (แก้ไข)",
    "COMPANY_CODE": "NC001"
  }
}
```

### ลบบริษัท (Soft Delete)

```http
DELETE /api/company/{id}
```

### อัปโหลดรูปภาพ

```http
POST /api/upload/image
Content-Type: multipart/form-data

file=<ไฟล์รูปภาพ>   (ไม่เกิน 2 MB)
```

**Response:**
```json
{
  "IS_SUCCESS": true,
  "FILE_NAME": "abc123.jpg",
  "FILE_URL": "https://<storage>.blob.core.windows.net/public/abc123.jpg"
}
```

### สรุป Endpoint ทั้งหมด

| Method | Endpoint | คำอธิบาย |
|--------|----------|----------|
| GET | `/api/company` | ดูรายการบริษัททั้งหมด |
| GET | `/api/company/{id}` | ดูบริษัทตาม ID |
| POST | `/api/company` | สร้างบริษัทใหม่ |
| PUT | `/api/company/{id}` | แก้ไขบริษัท |
| DELETE | `/api/company/{id}` | ลบบริษัท |
| GET | `/api/branch` | ดูรายการสาขาทั้งหมด |
| GET | `/api/branch/{id}` | ดูสาขาตาม ID |
| POST | `/api/branch` | สร้างสาขาใหม่ |
| PUT | `/api/branch/{id}` | แก้ไขสาขา |
| DELETE | `/api/branch/{id}` | ลบสาขา |
| GET | `/api/user` | ดูรายการผู้ใช้งานทั้งหมด |
| GET | `/api/user/{id}` | ดูผู้ใช้งานตาม ID |
| POST | `/api/user` | สร้างผู้ใช้งานใหม่ |
| PUT | `/api/user/{id}` | แก้ไขผู้ใช้งาน |
| DELETE | `/api/user/{id}` | ลบผู้ใช้งาน |
| POST | `/api/upload/image` | อัปโหลดรูปภาพ |

---

<div align="center">

Built with .NET 8 Clean Architecture

</div>
