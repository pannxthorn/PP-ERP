# ===== Stage 1: Build =====
# ดึง .NET SDK image มาเพื่อ build project
# คิดว่าเป็นการ "ยืมเครื่องที่ลง .NET SDK แล้ว" มาใช้ build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# ตั้ง working directory ใน container (เหมือน cd /app)
WORKDIR /app

# ---- Copy .csproj ก่อน เพื่อ restore dependencies ----
# ทำแยกจาก source code เพราะ Docker จะ cache layer นี้ไว้
# ถ้า .csproj ไม่เปลี่ยน = ไม่ต้อง restore ใหม่ = build เร็วขึ้น
COPY PP-ERP.Domain/PP-ERP.Domain.csproj PP-ERP.Domain/
COPY PP-ERP.DTO/PP-ERP.DTO.csproj PP-ERP.DTO/
COPY PP-ERP.Application/PP-ERP.Application.csproj PP-ERP.Application/
COPY PP-ERP.Infrastructure/PP-ERP.Infrastructure.csproj PP-ERP.Infrastructure/
COPY PP-ERP.API/PP-ERP.API.csproj PP-ERP.API/

# restore NuGet packages (ดึง libraries ที่ project ใช้)
RUN dotnet restore PP-ERP.API/PP-ERP.API.csproj

# ---- Copy source code ทั้งหมดแล้ว build ----
COPY . .

# publish = build + รวมไฟล์ทั้งหมดให้พร้อม deploy
# -c Release = build แบบ optimized
# -o /app/publish = output ไปไว้ที่ /app/publish
RUN dotnet publish PP-ERP.API/PP-ERP.API.csproj -c Release -o /app/publish

# ===== Stage 2: Runtime =====
# ใช้ image เล็กกว่า (ASP.NET runtime เท่านั้น ไม่มี SDK)
# image นี้เล็กกว่า SDK ~5 เท่า เพราะไม่ต้อง build แล้ว
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

# copy ไฟล์ที่ publish แล้วจาก stage 1 มาใช้
COPY --from=build /app/publish .

# เปิด port 8080 (บอก Docker ว่า app จะใช้ port นี้)
EXPOSE 8080

# คำสั่งที่จะ run เมื่อ container เริ่มทำงาน
ENTRYPOINT ["dotnet", "PP-ERP.API.dll"]
