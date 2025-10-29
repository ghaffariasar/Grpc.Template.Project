# Troubleshooting & FAQ

## English
### Client cannot connect
- Check `Grpc:Address` (correct HTTP/HTTPS)
- Ensure server runs on the same port

### `UNAVAILABLE` or `DEADLINE_EXCEEDED`
- RetryPolicy is enabled; adjust `MaxAttempts`/backoff if needed
- Check network/firewall

### `NOT_FOUND` on GetById
- No data with that Id in InMemory DB; create first or list all

### Message too large
- Increase `MaxSendMessageSize`/`MaxReceiveMessageSize` or reduce payload

### View proto
- Use `/protos`, `/protos/v1/student`, `/protos/v1/student/view`

---

<div dir="rtl">

## فارسی
### کلاینت وصل نمی‌شود
- آدرس `Grpc:Address` را بررسی کنید (HTTPS/HTTP درست باشد)
- مطمئن شوید سرور روی همان پورت در حال اجراست

### خطای `UNAVAILABLE` یا `DEADLINE_EXCEEDED`
- RetryPolicy فعال است؛ مقدار `MaxAttempts` یا Backoff را در صورت نیاز تنظیم کنید
- بررسی شبکه/فایروال

### خطای `NOT_FOUND` در GetById
- داده با آن Id در دیتابیس InMemory وجود ندارد؛ ابتدا ایجاد کنید یا لیست بگیرید

### سایز پیام زیاد است
- مقدار `MaxSendMessageSize`/`MaxReceiveMessageSize` را افزایش دهید یا payload را کوچک کنید

### مشاهده پروتو
- از مسیرهای `/protos`، `/protos/v1/student` و `/protos/v1/student/view` استفاده کنید

</div>
