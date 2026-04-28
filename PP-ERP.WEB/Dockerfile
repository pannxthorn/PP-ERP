# ===== Stage 1: Build =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# copy .csproj แล้ว restore (WEB ใช้แค่ DTO เท่านั้น ไม่ได้ reference Infrastructure/Application)
COPY PP-ERP.DTO/PP-ERP.DTO.csproj PP-ERP.DTO/
COPY PP-ERP.WEB/PP-ERP.WEB.csproj PP-ERP.WEB/

RUN dotnet restore PP-ERP.WEB/PP-ERP.WEB.csproj

# copy source code แล้ว publish
COPY . .

RUN dotnet publish PP-ERP.WEB/PP-ERP.WEB.csproj -c Release -o /app/publish

# ===== Stage 2: Runtime =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "PP-ERP.WEB.dll"]
