using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Formatting.Compact;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http.Features;
using Template.Application.Services;
using Template.Application;
using Template.Application.Utilities;
using Template.WebAPI.Utilities;
using Template.Infrastructure.EF;
using Template.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using Template.WebAPI.Hubs;
using Template.WebAPI.Utilities.middlewares;
using Microsoft.Extensions.Options;
using Template.ServiceRegister.ServiceRegister;
using Template.ServiceRegister.Connector;
using Template.Infrastructure.Loggers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger Template Solution", Version = "v1" });

//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
//                      Enter 'Bearer' [space] and then your token in the text input below.
//                      \r\n\r\nExample: 'Bearer 12345abcdef'",
//        Name = "Authorization",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "Bearer"
//    });

//    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
//                  {
//                    {
//                      new OpenApiSecurityScheme
//                      {
//                        Reference = new OpenApiReference
//                          {
//                            Type = ReferenceType.SecurityScheme,
//                            Id = "Bearer"
//                          },
//                          Scheme = "oauth2",
//                          Name = "Bearer",
//                          In = ParameterLocation.Header,
//                        },
//                        new List<string>()
//        }
//    });
//});



builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger Template Solution", Version = "v1" });

    // Define cookie-based authentication
    c.AddSecurityDefinition("accessToken", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Cookie, // Specify cookies for authentication
        Name = "accessToken", // Name of your authentication cookie
        Description = "HTTP-only cookie used for authentication"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "accessToken"
                }
            },
            Array.Empty<string>()
        }
    });
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    string issuer = builder.Configuration.GetValue<string>("Tokens:Issuer");
    string signingKey = builder.Configuration.GetValue<string>("Tokens:Key");
    byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);

    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidIssuer = issuer,

        ValidateAudience = false,
        ValidAudience = issuer,

        ValidateLifetime = false,
        ValidateIssuerSigningKey = false,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
    };

    // Intercept the token from a cookie
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            // Extract token from the cookie
            var token = context.HttpContext.Request.Cookies["accessToken"];
            if (!string.IsNullOrEmpty(token))
            {
                context.Token = token;
            }
            return Task.CompletedTask;
        }
    };
});

var logPath = builder.Environment.IsDevelopment()
                      ? Path.Combine("D:\\Projects\\.NET Core\\Monolitic - Template\\01.03.2025\\WebApi\\Template.WebAPI\\Logs", "logs.txt")
                      : Path.Combine(AppContext.BaseDirectory, "Logs", "log.txt");



var serlogger = new LoggerConfiguration()
             .ReadFrom.Configuration(builder.Configuration)
             .Enrich.WithThreadId()
             .WriteTo.File(logPath,
                           rollingInterval: RollingInterval.Infinite,
                           outputTemplate: "{Timestamp:MM/dd/yyyy H:mm:ss zzzz} {ThreadId} {Level} {SourceContext} {Message:lj}{NewLine}{Exception}")
             .CreateLogger();


//ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder
//   // .AddProvider(new ConsoleLoggerProvider(LogLevel.Information, ConsoleColor.Blue))
//    .AddFilter("Microsoft.EntityFrameworkCore", LogLevel.None)
//    .AddSimpleConsole(c => c.SingleLine = true)
//    .AddSerilog(serlogger)
//    .SetMinimumLevel(LogLevel.Debug));



//builder.Services.AddSingleton(loggerFactory);

//var client = await Connector.ConnectAsync(options => {
//        options.LogFactory = loggerFactory;
//        options.IsDevelopment = builder.Environment.IsDevelopment();
//        options.Configuration = builder.Configuration;

//}).ConfigureAwait(false);

//builder.Services.AddScoped<ITemplateClient>(provider => client);
//builder.Services.MigrationsContext(builder.Configuration);




builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.None);
    loggingBuilder.AddSerilog(serlogger);
});

builder.Services.AddSignalR();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);




builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 100 * 1024 * 1024; // 50 MB
});


builder.Services.AddMvc(options => options.Conventions.Add(new RouteConvention()));

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();




builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin() // Allow any origin or specify your origin
            .AllowAnyMethod()
            .AllowAnyHeader();
         //   .AllowCredentials(); // Allow credentials (cookies)
    });
});
var app = builder.Build();

//// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Template Solution v1");
     
    });
}

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();


app.UseHttpsRedirection();


app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();
app.MapHub<MyHub>("/MyHub");
app.Run();
