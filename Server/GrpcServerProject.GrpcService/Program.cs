using GrpcServerProject.Application.Services;
using GrpcServerProject.Domain.Repositories;
using GrpcServerProject.Domain.Services;
using GrpcServerProject.GrpcService.Infrastructure;
using GrpcServerProject.GrpcService.Infrastructure.Interceptors;
using GrpcServerProject.GrpcService.Services;
using GrpcServerProject.Infrastructure;
using GrpcServerProject.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();


builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseInMemoryDatabase("Grpc_DB");
});


builder.Services.AddSingleton<ProtoFileProvider>();
builder.Services.AddGrpcReflection();



// Add services to the container.
builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = true;
    options.Interceptors.Add<ExceptionInterceptor>();
});







var app = builder.Build();

app.MapGrpcReflectionService();


app.MapGrpcService<StudentGrpcService>();

app.MapGet("/protos", (ProtoFileProvider protoFileProvider) =>
{
    return Results.Ok(protoFileProvider.GetAll());
});
app.MapGet("/protos/v{version:int}/{protoName}", (ProtoFileProvider protoFileProvider, int version, string protoName) =>
{
    var filePath = protoFileProvider.GetPath(version, protoName);
    if (string.IsNullOrEmpty(filePath))
        return Results.NotFound();
    return Results.File(filePath);
});

app.MapGet("/protos/v{version:int}/{protoName}/view", async (ProtoFileProvider protoFileProvider, int version, string protoName) =>
{
    var fileContent = await protoFileProvider.GetContent(version, protoName);
    if (string.IsNullOrEmpty(fileContent))
        return Results.NotFound();
    return Results.Text(fileContent);
});




app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
