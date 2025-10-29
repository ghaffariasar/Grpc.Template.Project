# API Reference (StudentService v1)

## English
Proto: `Protos/v1/student.proto`

### Messages
- StudentResponse { Id, StudentNumber, FirstName, LastName, Description, repeated PhoneNumbers }
- StudentId { Id }
- CreateStudentRequest { StudentNumber, FirstName, LastName, Description, repeated PhoneNumbers }
- UpdateStudentRequest { Id, FirstName, LastName, Description }

### RPCs
- GetAll(Empty) → stream StudentResponse
  - Server streaming: streams all students.
- GetById(StudentId) → StudentResponse
  - Unary: returns a student by Id. If not found: `NOT_FOUND`.
- CreateStudent(stream CreateStudentRequest) → stream StudentId
  - Bidirectional streaming: responds with Id per request.
- UpdateStudent(UpdateStudentRequest) → BoolValue
  - Unary: success/failure.
- DeleteStudent(StudentId) → BoolValue
  - Unary: success/failure.

### Proto via HTTP
- GET /protos
- GET /protos/v{version:int}/{protoName}
- GET /protos/v{version:int}/{protoName}/view

---

<div dir="rtl">

## فارسی
Proto: `Protos/v1/student.proto`

### Messages
- StudentResponse { Id, StudentNumber, FirstName, LastName, Description, repeated PhoneNumbers }
- StudentId { Id }
- CreateStudentRequest { StudentNumber, FirstName, LastName, Description, repeated PhoneNumbers }
- UpdateStudentRequest { Id, FirstName, LastName, Description }

### RPCها
- GetAll(Empty) → stream StudentResponse
  - Server streaming: همه دانشجویان را استریم می‌کند.
- GetById(StudentId) → StudentResponse
  - Unary: بر اساس Id، یک دانشجو را برمی‌گرداند. در صورت عدم وجود: `NOT_FOUND`.
- CreateStudent(stream CreateStudentRequest) → stream StudentId
  - Bidirectional streaming: برای هر درخواست، Id ایجادشده را برمی‌گرداند.
- UpdateStudent(UpdateStudentRequest) → BoolValue
  - Unary: نتیجه موفق/ناموفق.
- DeleteStudent(StudentId) → BoolValue
  - Unary: نتیجه موفق/ناموفق.

### مشاهده proto از طریق HTTP
- GET /protos
- GET /protos/v{version:int}/{protoName}
- GET /protos/v{version:int}/{protoName}/view

</div>
