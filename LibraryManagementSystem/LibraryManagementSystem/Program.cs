using LibraryManagementSystem.Entity;
using LibraryManagementSystem.Repository;
using LibraryManagementSystem.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
})
.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://localhost:44368",
        ValidAudience = "https://localhost:44368",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("SecretKey").Value))
    };
})
.AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IRentRepository, RentRepository>();
builder.Services.AddScoped<IRentService, RentService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddSingleton<IDynatraceLoggerService>(provider =>
    new DynatraceLoggerService(builder.Configuration.GetSection("DynatraceId").Value, builder.Configuration.GetSection("DynatraceToken").Value));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource
        .AddService(serviceName: "LibraryManagementSystemUOB", serviceVersion: "1.0.0")
        .AddTelemetrySdk())
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddOtlpExporter(otlpOptions =>
        {
            otlpOptions.Endpoint = new Uri($"https://{builder.Configuration.GetSection("DynatraceId").Value}.live.dynatrace.com/api/v2/otlp/v1/traces");
            otlpOptions.Headers = $"Authorization=Api-Token {builder.Configuration.GetSection("DynatraceToken").Value}";
        }))
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddOtlpExporter(otlpOptions =>
        {
            otlpOptions.Endpoint = new Uri($"https://{builder.Configuration.GetSection("DynatraceId").Value}.live.dynatrace.com/api/v2/otlp/v1/metrics");
            otlpOptions.Headers = $"Authorization=Api-Token {builder.Configuration.GetSection("DynatraceToken").Value}";
        }));

builder.Logging.AddOpenTelemetry(logging =>
{
    logging.IncludeFormattedMessage = true;
    logging.IncludeScopes = true;
    logging.ParseStateValues = true;
    logging.SetResourceBuilder(
        ResourceBuilder.CreateDefault()
            .AddService(serviceName: "LibraryManagementSystemUOB", serviceVersion: "1.0.0"));
    logging.AddOtlpExporter(otlpOptions =>
    {
        otlpOptions.Endpoint = new Uri($"https://{builder.Configuration.GetSection("DynatraceId").Value}.live.dynatrace.com/api/v2/otlp/v1/logs");
        otlpOptions.Headers = $"Authorization=Api-Token {builder.Configuration.GetSection("DynatraceToken").Value}";
    });
});

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "To include Bearer token authorization header --- Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();