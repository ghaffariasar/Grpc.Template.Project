using Grpc.Net.Client;
using Grpc.Net.Client.Configuration;
using GrpcClientProject.Domain.Repositories;
using GrpcClientProject.Domain.Services;
using GrpcClientProject.Infrastructure.Repositories;
using System.IO.Compression;
using Grpc.Net.Compression;
using Microsoft.Extensions.Options;
using StudentService = GrpcClientProject.Application.Services.StudentService;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();



builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.Configure<GrpcClientOptions>(builder.Configuration.GetSection("Grpc"));

builder.Services.AddGrpcClient<GrpcClientProject.Infrastructure.Protos.v1.StudentService.StudentServiceClient>((sp, options) =>
{
    var grpcOpts = sp.GetRequiredService<IOptions<GrpcClientOptions>>().Value;
    options.Address = new Uri(string.IsNullOrWhiteSpace(grpcOpts.Address) ? "https://localhost:7057" : grpcOpts.Address);

    #region تنظیمات پرفورمنس

    options.ChannelOptionsActions.Add(channel =>
    {
        // حداکثر اندازه پیام
        channel.MaxSendMessageSize = 5_242_880;
        channel.MaxReceiveMessageSize = 5_242_880;

        // فشرده‌سازی gzip
        channel.CompressionProviders = new ICompressionProvider[]
        {
            new GzipCompressionProvider(CompressionLevel.Optimal)
        };

        // پالیسی Retry در صورت خطاهای موقتی
        channel.ServiceConfig = new ServiceConfig
        {
            MethodConfigs =
            {
                new MethodConfig
                {
                    Names = { MethodName.Default },
                    RetryPolicy = new RetryPolicy
                    {
                        MaxAttempts = 3,
                        InitialBackoff = TimeSpan.FromSeconds(1),
                        MaxBackoff = TimeSpan.FromSeconds(5),
                        BackoffMultiplier = 1.5,
                        RetryableStatusCodes =
                        {
                            Grpc.Core.StatusCode.Unavailable,
                            Grpc.Core.StatusCode.DeadlineExceeded
                        }
                    }
                }
            }
        };
    });

    #endregion

}).ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
{
    #region تنظیمات پرفورمنس

    EnableMultipleHttp2Connections = true,

    // مدیریت کانکشن‌ها
    PooledConnectionIdleTimeout = TimeSpan.FromMinutes(5),
    PooledConnectionLifetime = TimeSpan.FromMinutes(10),

    // پینگ برای حفظ ارتباط زنده
    KeepAlivePingDelay = TimeSpan.FromSeconds(30),
    KeepAlivePingTimeout = TimeSpan.FromSeconds(15),
    KeepAlivePingPolicy = HttpKeepAlivePingPolicy.Always

    #endregion

});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();

app.Run();


public class GrpcClientOptions
{
    public string Address { get; set; } = string.Empty;
}