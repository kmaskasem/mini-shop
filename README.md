# 🧋 Mini-Shop: Full-Stack POS System

ระบบจัดการร้านขายชานมแบบครบวงจร (POS) ที่สร้างขึ้นเพื่อเป็นพื้นที่เรียนรู้ **Microservices, Clean Architecture และ Domain-Driven Design (DDD)** ในระดับ Production-grade

> **หมายเหตุ:** นี่เป็นโปรเจกต์ส่วนตัวที่สร้างขึ้นเพื่อการเรียนรู้ เป้าหมายไม่ใช่แค่การสร้างโปรแกรมที่ใช้งานได้จริง แต่คือการ**ฝึกฝน**ออกแบบสถาปัตยกรรม Backend เพื่อแก้ปัญหาทางธุรกิจที่มีความซับซ้อน

---

## 🏗️ สถาปัตยกรรมของระบบ
ระบบถูกออกแบบเป็น Microservices ที่สื่อสารกันหลักๆ ผ่าน **gRPC** เพื่อประสิทธิภาพที่สูง

* **Client:** React (TypeScript)
* **API Gateway:** จุดรับข้อมูลเข้าและจัดการ JWT Auth
* **Auth Service:** C# .NET 8 (Clean Architecture)
* **Order & Inventory:** Go (Gin Framework)
* **Analytics:** Python (FastAPI)
* **Data Layer:** PostgreSQL (Transactional), MongoDB (Documents), Redis (Queue/Cache)

---

## 🛠️ เทคโนโลยีและรูปแบบที่ใช้ (Tech Stack)

| เลเยอร์ | เทคโนโลยี |
| :--- | :--- |
| **Auth Service** | C# .NET 8, EF Core, Clean Architecture |
| **Order Service** | Go, Gin (อยู่ในระหว่างพัฒนา) |
| **Analytics** | Python, FastAPI |
| **Messaging** | gRPC (ระหว่าง Service), Redis (คิวงาน) |
| **Search** | Elasticsearch |
| **Infrastructure**| Docker, Docker Compose |

---

## 📁 โครงสร้างโปรเจกต์ (ปัจจุบัน)
> **หมายเหตุ:** โครงสร้างนี้เป็นเพียงร่างเริ่มต้น (Draft) อาจมีการเปลี่ยนแปลงหรือจัดกลุ่มใหม่ตามความเหมาะสมเมื่อมีการเพิ่ม Service หรือปรับปรุงสถาปัตยกรรมในอนาคต

```bash
mini-shop/
├── frontend/              # React POS & Admin Dashboard
├── services/
│   ├── auth-service/      # C# .NET 8 — Clean Architecture
│   │   ├── Domain/        # Entities, Value Objects
│   │   ├── Application/   # Use Cases, Ports
│   │   ├── Infrastructure/# EF Core, JWT
│   │   └── API/           # Controllers
│   ├── order-service/     # Go + Gin
│   └── analytics-service/ # Python FastAPI
├── infra/                 # ไฟล์ Docker Compose
└── proto/                 # ไฟล์นิยาม gRPC (Shared)
```

## 🚀 Getting Started
#### สิ่งที่ต้องมี
* Docker & Docker Compose

#### ขั้นตอนการรัน
```bash
# 1. Clone โปรเจกต์
git clone https://github.com/kmaskasem/mini-shop.git
cd mini-shop

# 2. ตั้งค่า Environment
cp .env.example .env
# แก้ไขค่าความลับต่างๆ ในไฟล์ .env

# 3. รัน Infrastructure
docker compose -f infra/docker-compose.yml up -d

# 4. Auth Service      (กำลังพัฒนา)

# 5. Order Service     (อยู่ในแผน)

# 6. Inventory Service (อยู่ในแผน)

# 7. Analytics Service (อยู่ในแผน)
```
