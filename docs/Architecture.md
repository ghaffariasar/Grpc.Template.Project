# Architecture

## English
### Layers
- Server.Domain: domain models, contracts, services
- Server.Application: use-cases (application services)
- Server.Infrastructure: EF Core InMemory, repositories, mappings
- GrpcService Host: gRPC host + Reflection + Interceptor + proto browsing endpoints

- Client.Domain: client models and contracts
- Client.Application: client services and use-cases
- Client.Infrastructure: proto files and client repository
- WebApp: UI (Razor Pages) and gRPC client registration

### Request flow
1. WebApp → gRPC Client → StudentService (RPC)
2. GrpcService → Application Service → Repository/Db (Infrastructure)
3. Response returns as proto messages.

### API versioning
- Protos live under `Protos/v1`; add `v2` for breaking changes.

---

<div dir="rtl">

## فارسی
### لایه‌ها
- Server.Domain: مدل‌ها، قراردادها، سرویس‌های دامنه
- Server.Application: پیاده‌سازی use-caseها (سرویس اپلیکیشن)
- Server.Infrastructure: EF Core InMemory، ریپازیتوری‌ها، مپینگ‌ها
- GrpcService Host: هاست gRPC + Reflection + Interceptor + endpoints مشاهده proto

- Client.Domain: مدل‌ها و قراردادهای کلاینت
- Client.Application: سرویس‌های کلاینتی و use-caseها
- Client.Infrastructure: فایل‌های proto و ریپازیتوری کلاینت
- WebApp: UI (Razor Pages) و رجیستر gRPC Client

### جریان درخواست
1. WebApp → gRPC Client → StudentService (RPC)
2. GrpcService → Application Service → Repository/Db (Infrastructure)
3. پاسخ به صورت پیام‌های پروتو بازگردانده می‌شود.

### نسخه‌گذاری API
- پروتوها در مسیر `Protos/v1` نگهداری می‌شوند. برای نسخه‌های جدید، پوشه‌ی `v2` اضافه کنید.

</div>
