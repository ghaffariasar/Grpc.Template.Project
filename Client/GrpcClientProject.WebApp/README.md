# GrpcClientProject.WebApp

## English
Razor Pages web app that connects to the gRPC server to perform Student operations.

### Run
```
dotnet run
```
Server address is set in `appsettings.json` → `Grpc:Address` (default: `https://localhost:7057`).

### gRPC Client registration (highlights)
- Max send/receive size ≈ 5MB
- `gzip` compression
- `RetryPolicy` for transient errors (`Unavailable`, `DeadlineExceeded`)
- HTTP/2 keep-alive via `SocketsHttpHandler`

### Available RPCs (StudentService)
- `GetAll` (Server streaming)
- `GetById` (Unary)
- `CreateStudent` (Bidirectional streaming)
- `UpdateStudent` (Unary)
- `DeleteStudent` (Unary)

### Development
- Application service and repository under `Application` and `Infrastructure`
- Models and contracts in `Client/GrpcClientProject.Domain`

---

<div dir="rtl">

## فارسی
وب‌اپ Razor Pages که به سرور gRPC متصل می‌شود و عملیات مربوط به آبجکت دانشجو را انجام می‌دهد.

### اجرای محلی
```
dotnet run
```
آدرس سرور gRPC در `appsettings.json` بخش `Grpc:Address` تنظیم می‌شود (پیش‌فرض: `https://localhost:7057`).

### رجیستر کلاینت gRPC (خلاصه تنظیمات)
- Max send/receive size ≈ 5MB
- فشرده‌سازی `gzip`
- `RetryPolicy` برای خطاهای موقتی (`Unavailable`, `DeadlineExceeded`)
- HTTP/2 keep-alive با `SocketsHttpHandler`

### RPCهای در دسترس (StudentService)
- `GetAll` (Server streaming)
- `GetById` (Unary)
- `CreateStudent` (Bidirectional streaming)
- `UpdateStudent` (Unary)
- `DeleteStudent` (Unary)

### توسعه
- سرویس اپلیکیشن و ریپازیتوری در پوشه‌های `Application` و `Infrastructure` قرار دارند.
- مدل‌ها و قراردادها در `Client/GrpcClientProject.Domain` هستند.

</div>
