using Szoftverfejlesztés_dotnet_hw.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics;
using Szoftverfejlesztés_dotnet_hw.BLL.Dtos;
using Szoftverfejlesztés_dotnet_hw.BLL.Interfaces;
using Szoftverfejlesztés_dotnet_hw.BLL.Services;
using Szoftverfejlesztés_dotnet_hw.BLL.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks.Dataflow;
using Microsoft.OpenApi.Models;
using System.Reflection;
using NLog.Web;
using NLog;
using Microsoft.Extensions.Logging.ApplicationInsights;
using System.Linq;
using Szoftverfejlesztés_dotnet_hw;


var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore
builder.Services.AddEndpointsApiExplorer();






//openapi documentation
var info = new OpenApiInfo
{
    Title = "EventManager API",
    Version = "v1",
    Description = "A simple colaborative event manager ASP.NET Core Web API"
};
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", info);
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

//NLog
builder.Logging.ClearProviders();
builder.Host.UseNLog();

//applicationinsights
builder.Services.AddApplicationInsightsTelemetry();
builder.Logging.AddApplicationInsights(
        configureTelemetryConfiguration: (config) =>
            config.ConnectionString = builder.Configuration.GetSection("ApplicationInsights")["ConnectionString"],
            configureApplicationInsightsLoggerOptions: (options) => { }
    );

builder.Logging.AddFilter<ApplicationInsightsLoggerProvider>("AppLogs", Microsoft.Extensions.Logging.LogLevel.Trace);



//Add DbContext with resiliency
builder.Services.AddDbContext<AppDbContext>(
    o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),po => po.EnableRetryOnFailure()));

//health check
builder.Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>();

//Add AutoMapper
builder.Services.AddAutoMapper(typeof(WebApiProfile));

//Add ProblemDetails
builder.Services.AddProblemDetails(options =>
        options.CustomizeProblemDetails = context =>
        {
            if (context.HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error is EntityByIdNotFoundException ex)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                context.ProblemDetails.Title = "Invalid ID";
                context.ProblemDetails.Status = StatusCodes.Status404NotFound;
                context.ProblemDetails.Detail = $"No entity with ID {ex.Id}";
            }

            if (context.HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error is EntityByNameNotFoundException ex2)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                context.ProblemDetails.Title = "Invalid Name";
                context.ProblemDetails.Status = StatusCodes.Status404NotFound;
                context.ProblemDetails.Detail = $"No entity with Name {ex2.Name}";
            }
            if (context.HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error is UnauthorizedException ex3)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.ProblemDetails.Title = "Unauthenticated access";
                context.ProblemDetails.Status = StatusCodes.Status401Unauthorized;
                context.ProblemDetails.Detail = $"{ex3.Message}";
            }
        }
    );

//Add services
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IEventService, EventService>();
builder.Services.AddTransient<IGroupService, GroupService>();
builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddApplicationInsightsTelemetry();


var app = builder.Build();

//Update database on empty database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

//map health check
app.MapHealthChecks("/health");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(u =>
    {
        u.RouteTemplate = "swagger/{documentName}/swagger.json";
    });
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "swagger";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EventManager API V1");
    });
 }

app.UseAuthorization();

app.MapControllers();

app.Run();





