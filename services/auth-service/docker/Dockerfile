# ── Stage 1: base ──────────────────────────────────────────
# ใช้ image ที่มีแค่ runtime (aspnet) ไม่มี SDK
# เบากว่า SDK image มาก, ใช้เป็น "แม่แบบ" ของ image สุดท้าย
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

# ── Stage 2: build ─────────────────────────────────────────
# ใช้ SDK image (มี dotnet build, restore, publish)
# stage นี้จะถูกทิ้งหลัง build เสร็จ ไม่ติดไปใน image จริง
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
# Copy เฉพาะ .csproj ก่อน แล้วค่อย restore
# เหตุผล: Docker cache — ถ้า .csproj ไม่เปลี่ยน จะใช้ cache layer เดิม
# ไม่ต้อง restore packages ใหม่ทุกครั้งที่ build = เร็วขึ้นมาก
COPY ["src/API/API.csproj", "src/API/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
RUN dotnet restore "src/API/API.csproj"
# หลัง restore แล้วค่อย copy โค้ดทั้งหมด แล้ว publish
COPY src/ ./src/
RUN dotnet publish "src/API/API.csproj" -c Release -o /app/publish

# ── Stage 3: final ─────────────────────────────────────────
# เริ่มจาก base (runtime เท่านั้น)
# แล้ว copy เฉพาะผลลัพธ์จาก build stage มา
# SDK และ source code ไม่ติดมาด้วย → image เล็ก + ปลอดภัย
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]

# สรุป flow
# source code + SDK  →  dotnet publish  →  /app/publish (binary เท่านั้น)
#                                               ↓
#                               runtime image + binary = Docker image สุดท้าย
# image สุดท้ายจะมีแค่ runtime + ไฟล์ที่ compile แล้ว ไม่มี source code หรือ SDK ติดไปด้วย