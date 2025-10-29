# Grpc Template Project

## English
A complete gRPC Server/Client template for .NET 9 featuring a gRPC server and a Razor Pages web client. It is designed for learning and quick starts with common patterns, best practices, and full documentation.

### Features
- gRPC Server with gRPC Reflection enabled
- Clean-ish layering: `Domain`, `Application`, `Infrastructure`
- InMemory database for quick, dependency-free runs
- Error handling interceptor on the server
- Sample Student service with RPC types:
  - Unary: `GetById`, `UpdateStudent`, `DeleteStudent`
  - Server streaming: `GetAll`
  - Bidirectional streaming: `CreateStudent`
- Optimized client config: gzip compression, message size limits, Retry Policy, HTTP/2 keep-alive
- HTTP endpoints to view `.proto` files

### Repository structure
```
Client/
  GrpcClientProject.WebApp/        # Razor Pages web app + gRPC client registration
  GrpcClientProject.Application/   # Client application services (use-cases)
  GrpcClientProject.Domain/        # Client-side domain contracts and models
  GrpcClientProject.Infrastructure/# protos and client repository
Server/
  GrpcServerProject.GrpcService/   # gRPC host + Reflection + Interceptor + Proto endpoints
  GrpcServerProject.Application/   # Application services (use-cases)
  GrpcServerProject.Domain/        # Domain models and contracts
  GrpcServerProject.Infrastructure/# EF InMemory, Repositories, Entities, Mappers
```

### gRPC API (StudentService v1)
`student.proto`:
- `GetAll(Empty) returns (stream StudentResponse)`
- `GetById(StudentId) returns (StudentResponse)`
- `CreateStudent(stream CreateStudentRequest) returns (stream StudentId)`
- `UpdateStudent(UpdateStudentRequest) returns (BoolValue)`
- `DeleteStudent(StudentId) returns (BoolValue)`

Proto browsing endpoints on server:
- `GET /protos`
- `GET /protos/v{version:int}/{protoName}` (download)
- `GET /protos/v{version:int}/{protoName}/view` (view text)

### Prerequisites
- .NET 9 SDK

### Run (Development)
1) Run the server:
```
cd Server/GrpcServerProject.GrpcService
dotnet run
```
Defaults to `https://localhost:7057` with Reflection enabled.

2) Run the client:
```
cd Client/GrpcClientProject.WebApp
dotnet run
```
The gRPC server address is read from `appsettings.json` section `Grpc:Address` (default: `https://localhost:7057`).

### Client notes
- Max send/receive size ≈ 5MB
- Gzip compression
- RetryPolicy for `Unavailable` and `DeadlineExceeded`
- HTTP/2 keep-alive with `SocketsHttpHandler`

### Development
- Contracts/messages are versioned under `Protos/v1`.
- To add a new RPC: update proto, regenerate, and implement in services.

### Useful paths
- Server: `Server/GrpcServerProject.GrpcService`
- Client: `Client/GrpcClientProject.WebApp`
- Proto: `Server/GrpcServerProject.GrpcService/Protos/v1/student.proto` and its client mirror

### License
MIT

---

<div dir="rtl">

## فارسی

یک تمپلیت کامل برای پیاده‌سازی معماری سرور–کلاینت مبتنی بر gRPC در .NET 9 که شامل یک سرویس gRPC (سمت سرور) و یک وب‌اپلیکیشن کلاینت (Razor Pages) است.  
این پروژه برای یادگیری و شروع سریع کار با gRPC طراحی شده و شامل الگوهای متداول، بهترین شیوه‌ها و مستندات کامل می‌باشد.

### ویژگی‌ها
- gRPC Server با قابلیت gRPC Reflection  
- ساختار Clean-ish با تفکیک لایه‌های `Domain`، `Application` و `Infrastructure`  
- استفاده از دیتابیس InMemory برای اجرای سریع و بدون وابستگی  
- اینترسپتور اختصاصی برای مدیریت خطاها در سمت سرور  
- سرویس نمونه دانشجو با انواع مختلف RPC:
  - Unary: `GetById`, `UpdateStudent`, `DeleteStudent`
  - Server streaming: `GetAll`
  - Bidirectional streaming: `CreateStudent`
- کلاینت با تنظیمات بهینه شامل فشرده‌سازی gzip، محدودیت اندازه پیام، Retry Policy و HTTP/2 keep-alive  
- endpointهای HTTP برای مشاهده محتوای فایل‌های `.proto`

### ساختار مخزن
```
Client/
  GrpcClientProject.WebApp/        # وب‌اپ Razor Pages + رجیستر کلاینت gRPC
  GrpcClientProject.Application/   # سرویس‌های کلاینتی (use-cases)
  GrpcClientProject.Domain/        # قراردادها و مدل‌های دامین سمت کلاینت
  GrpcClientProject.Infrastructure/# protoها و ریپازیتوری کلاینت
Server/
  GrpcServerProject.GrpcService/   # هاست gRPC + Reflection + Interceptor + Proto endpoints
  GrpcServerProject.Application/   # سرویس‌های اپلیکیشن (use-cases)
  GrpcServerProject.Domain/        # مدل‌ها و قراردادهای دامین
  GrpcServerProject.Infrastructure/# EF InMemory, Repositories, Entities, Mappers
```

### API gRPC (StudentService v1)
فایل `student.proto`:
- `GetAll(Empty) returns (stream StudentResponse)`
- `GetById(StudentId) returns (StudentResponse)`
- `CreateStudent(stream CreateStudentRequest) returns (stream StudentId)`
- `UpdateStudent(UpdateStudentRequest) returns (BoolValue)`
- `DeleteStudent(StudentId) returns (BoolValue)`

Endpointهای مشاهده فایل‌های proto در سرور:
- `GET /protos`
- `GET /protos/v{version:int}/{protoName}` (دانلود فایل)
- `GET /protos/v{version:int}/{protoName}/view` (نمایش محتوای فایل)

### پیش‌نیازها
- نصب .NET 9 SDK

### نحوه اجرا (Development)
1) اجرای سرور:
```
cd Server/GrpcServerProject.GrpcService
dotnet run
```
به‌صورت پیش‌فرض روی `https://localhost:7057` اجرا می‌شود و Reflection فعال است.

2) اجرای کلاینت:

```
cd Client/GrpcClientProject.WebApp
dotnet run
```
آدرس سرور gRPC از فایل `appsettings.json` بخش `Grpc:Address` خوانده می‌شود (پیش‌فرض: `https://localhost:7057`).

### نکات کلاینت
- حداکثر اندازه ارسال/دریافت پیام حدود ۵ مگابایت  
- فشرده‌سازی با Gzip  
- Retry Policy برای خطاهای `Unavailable` و `DeadlineExceeded`  
- فعال‌سازی HTTP/2 keep-alive با استفاده از `SocketsHttpHandler`

### توسعه
- قراردادها و پیام‌ها در پوشه `Protos/v1` نسخه‌گذاری شده‌اند.  
- برای افزودن RPC جدید: پیام‌ها را در proto اضافه کرده، کدهای تولیدی را به‌روزرسانی کنید و سپس در سرویس‌ها پیاده‌سازی نمایید.

### مسیرها
- سرور: `Server/GrpcServerProject.GrpcService`  
- کلاینت: `Client/GrpcClientProject.WebApp`  
- فایل پروتو: `Server/GrpcServerProject.GrpcService/Protos/v1/student.proto` و نسخه‌ی مشابه در کلاینت  

### License
MIT


</div>
