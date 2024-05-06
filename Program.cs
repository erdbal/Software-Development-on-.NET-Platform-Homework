using Szoftverfejlesztés_dotnet_hw.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics;
using Szoftverfejlesztés_dotnet_hw.BLL.Dtos;
using Szoftverfejlesztés_dotnet_hw.BLL.Interfaces;
using Szoftverfejlesztés_dotnet_hw.BLL.Services;
using Szoftverfejlesztés_dotnet_hw.BLL.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks.Dataflow;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(WebApiProfile));

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


builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IEventService, EventService>();
builder.Services.AddTransient<IGroupService, GroupService>();
builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddTransient<ILoginService, LoginService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();