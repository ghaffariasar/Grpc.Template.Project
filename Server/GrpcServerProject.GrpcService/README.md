# gRPC Server Project

## English
.NET 9 gRPC host with Reflection and an error-handling interceptor. Uses an InMemory database for a simple sample.

### Run
```
dotnet run
```
Default: `https://localhost:7057`

### DI
- `IStudentRepository` ← `StudentRepository`
- `IStudentService` ← `GrpcServerProject.Application.Services.StudentService`
- `DataContext` ← `UseInMemoryDatabase("Grpc_DB")`

### Reflection & Proto browsing
- Reflection enabled (useful for grpcui/BloomRPC)
- HTTP endpoints:
  - `GET /protos`
  - `GET /protos/v{version:int}/{protoName}`
  - `GET /protos/v{version:int}/{protoName}/view`

### RPCs (StudentService)
- `GetAll(Empty) returns (stream StudentResponse)`
- `GetById(StudentId) returns (StudentResponse)`
- `CreateStudent(stream CreateStudentRequest) returns (stream StudentId)`
- `UpdateStudent(UpdateStudentRequest) returns (BoolValue)`
- `DeleteStudent(StudentId) returns (BoolValue)`

### Notes
- Domain errors mapped to `RpcException` with proper status (e.g., `NotFound`).
- For real DB, switch to `UseSqlServer`/`UseNpgsql` and add migrations.

---

<div dir="rtl">

## فارسی
سرور gRPC مبتنی بر .NET 9 با Reflection و Interceptor مدیریت خطا. همچنین دیتابیس بصورت InMemory  پیاده سازی شده است.

### اجرای محلی
```
dotnet run
```
پیش‌فرض: `https://localhost:7057`

### DI و سرویس‌ها
- `IStudentRepository` ← `StudentRepository`
- `IStudentService` ← `GrpcServerProject.Application.Services.StudentService`
- `DataContext` ← `UseInMemoryDatabase("Grpc_DB")`

### Reflection و مشاهده پروتو
- Reflection فعال است (برای ابزارهایی مثل grpcui / BloomRPC مفید است)
- HTTP endpoints:
  - `GET /protos`
  - `GET /protos/v{version:int}/{protoName}`
  - `GET /protos/v{version:int}/{protoName}/view`

### RPCها (StudentService)
- `GetAll(Empty) returns (stream StudentResponse)`
- `GetById(StudentId) returns (StudentResponse)`
- `CreateStudent(stream CreateStudentRequest) returns (stream StudentId)`
- `UpdateStudent(UpdateStudentRequest) returns (BoolValue)`
- `DeleteStudent(StudentId) returns (BoolValue)`

### نکات
- خطاهای دامنه به `RpcException` با کد مناسب تبدیل می‌شوند (نمونه: `NotFound`).
- برای دیتابیس واقعی، `UseSqlServer`/`UseNpgsql` و migrationها را جایگزین کنید.

</div>
