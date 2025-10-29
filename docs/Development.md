# Development Guide

## English
### Prerequisites
- .NET 9 SDK

### Run projects
- Server: `Server/GrpcServerProject.GrpcService`
- Client: `Client/GrpcClientProject.WebApp`

### Add a new RPC
1. Update messages and service in `Protos/v{n}/` (prefer creating a new version for breaking changes).
2. gRPC host: extend the service class and implement the RPC.
3. Client: consume the generated stub and update application service/repository.

### Debugging
- Use Reflection with grpcui/BloomRPC to inspect services/methods.
- Error interceptor maps domain errors to `RpcException`.

### Deployment (high-level)
- Enable HTTPS with a valid certificate
- Tune resources (connection pools, max message size)
- Structured logging and monitoring

---

<div dir="rtl">

## فارسی
### پیش‌نیازها
- .NET 9 SDK

### اجرای پروژه‌ها
- Server: `Server/GrpcServerProject.GrpcService`
- Client: `Client/GrpcClientProject.WebApp`

### افزودن RPC جدید
1. پیام‌ها و سرویس را در `Protos/v{n}/` به‌روزرسانی کنید (ترجیحاً نسخه جدید بسازید).
2. هاست gRPC: کلاس سرویس را گسترش دهید و RPC را پیاده‌سازی کنید.
3. کلاینت: stub تولیدشده را مصرف کنید و سرویس اپلیکیشن/ریپازیتوری را به‌روزرسانی کنید.

### خطایابی
- از Reflection برای مشاهده سرویس‌ها و متدها استفاده کنید (grpcui، BloomRPC).
- اینترسپتور خطا در سرور، خطاهای دامنه را به `RpcException` تبدیل می‌کند.

### استقرار (High-level)
- فعال‌سازی HTTPS و گواهی معتبر
- تنظیم Resourceها (پول‌های کانکشن، max message size)
- نظارت و لاگ‌گیری ساختاریافته

</div>
