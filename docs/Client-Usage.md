# Client Usage

## English
### Configure server address
`Client/GrpcClientProject.WebApp/appsettings.json`
```json
{
  "Grpc": {
    "Address": "https://localhost:7057"
  }
}
```

### Client registration in Program.cs (highlights)
- Message size limits, gzip compression
- RetryPolicy for transient errors
- HTTP/2 keep-alive

### Call patterns
- Unary: `GetById`, `UpdateStudent`, `DeleteStudent`
- Server streaming: `GetAll` → read via `ResponseStream.ReadAllAsync()` on client
- Bidirectional streaming: `CreateStudent` → write and read concurrently

Note: For large payloads, use compression and tune message size limits as configured.

---

<div dir="rtl">

## فارسی
### تنظیم آدرس سرور
`Client/GrpcClientProject.WebApp/appsettings.json`
```json
{
  "Grpc": {
    "Address": "https://localhost:7057"
  }
}
```

### رجیستر کلاینت در Program.cs (خلاصه)
- محدودیت سایز پیام‌ها، فشرده‌سازی gzip
- RetryPolicy برای خطاهای موقتی
- HTTP/2 keep-alive

### نمونه الگوهای فراخوانی
- Unary: `GetById`, `UpdateStudent`, `DeleteStudent`
- Server streaming: `GetAll` → خواندن با `ResponseStream.ReadAllAsync()` در سمت کلاینت
- Bidirectional streaming: `CreateStudent` → نوشتن و خواندن همزمان در stream

نکته: برای عملیات هاس سنگین، از فشرده‌سازی و محدودیت سایز پیام‌ها مطابق تنظیمات استفاده کنید.

</div>
