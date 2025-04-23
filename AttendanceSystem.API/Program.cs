using AttendanceSystem.Business;
using AttendanceSystem.Services.Interfaces;
using AttendanceSystem.Services;
using AttendanceSystem.Repository.Interfaces;
using AttendanceSystem.Data.DomainEntities;
using Microsoft.EntityFrameworkCore;
using AttendanceSystem.Data;
using AttendanceSystem.Repository;
using AttendanceSystem.Business.Interfaces;
using AttendanceSystem.Business.AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc;
using AttendanceSystem.Shared;
using FluentValidation.AspNetCore;
using FluentValidation;
using AttendanceSystem.Shared.Validators;
using AttendanceSystem.Shared.DTOs;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("./Settings/logsettings.json", optional: true, reloadOnChange: true);
builder.Configuration.AddJsonFile("./Settings/securitysettings.json", optional: true, reloadOnChange: true);
builder.Configuration.AddJsonFile("./Settings/appsettings.json", optional: true, reloadOnChange: true);
builder.Configuration.AddJsonFile("./Settings/databasesettings.json", optional: true, reloadOnChange: true);
var logger = new LoggerConfiguration()
.ReadFrom.Configuration(builder.Configuration)
.Enrich.FromLogContext()
.CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApiVersioning(option =>
{
    option.DefaultApiVersion = new ApiVersion(1, 0);
    option.AssumeDefaultVersionWhenUnspecified = true;
    option.ReportApiVersions = true;
});
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme,
        new OpenApiSecurityScheme
        {
            Name = Constants.Authorization,
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = JwtBearerDefaults.AuthenticationScheme
        });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
        {
            Reference= new OpenApiReference
            {
                Type= ReferenceType.SecurityScheme,
                Id= JwtBearerDefaults.AuthenticationScheme
            },
            Scheme= Constants.Oauth2,
            Name= JwtBearerDefaults.AuthenticationScheme,
            In=ParameterLocation.Header
        },
        new List<string>()
    }
});
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration[Constants.JwtIssuer],
        ValidAudience = builder.Configuration[Constants.JwtAudience],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration[Constants.JwtKey]))
    });

//Add Validator Services 
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<LoginRequestDto>, LoginRequestValidator>();
builder.Services.AddScoped<IValidator<RegisterDto>, RegistorRequestValidator>();
builder.Services.AddScoped<IValidator<EmployeeDto>, EmployeeValidator>();
builder.Services.AddScoped<IValidator<AttendanceDto>, AttendanceValidator>();

//Add Automapper profile
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

//Add Db Contexts 
builder.Services.AddEntityFrameworkSqlite().AddDbContext<DataContext>();
builder.Services.AddEntityFrameworkSqlite().AddDbContext<IdentityDataContext>();
builder.Services.AddIdentityCore<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>(Constants.ProjectName)
                .AddEntityFrameworkStores<IdentityDataContext>()
                .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = Boolean.Parse(builder.Configuration[Constants.RequireDigit]);
    options.Password.RequireLowercase = Boolean.Parse(builder.Configuration[Constants.RequireLowercase]);
    options.Password.RequireNonAlphanumeric = Boolean.Parse(builder.Configuration[Constants.RequireNonAlphanumeric]);
    options.Password.RequireUppercase = Boolean.Parse(builder.Configuration[Constants.RequireUppercase]);
    options.Password.RequiredLength = int.Parse(builder.Configuration[Constants.RequiredLength]);
    options.Password.RequiredUniqueChars = int.Parse(builder.Configuration[Constants.RequiredUniqueChars]);
});

//Add Repository
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IRepository<Attendance>, Repository<Attendance>>();
builder.Services.AddScoped<IRepository<Employee>, Repository<Employee>>();
builder.Services.AddScoped<ICustomRepository, CustomRepository>();

//Add Business layer 
builder.Services.AddTransient<IAttendanceBusiness, AttendanceBusiness>();
builder.Services.AddTransient<IEmployeeBusiness, EmployeeBusiness>();
builder.Services.AddTransient<IReportBusiness, ReportBusiness>();

//Add Service layer 
builder.Services.AddTransient<IAttendanceServices, AttendanceServices>();
builder.Services.AddTransient<IEmployeeServices, EmployeeServices>();
builder.Services.AddTransient<IReportServices, ReportServices>();

var app = builder.Build();

var versionDescriptionProvider = app.Services.CreateScope().ServiceProvider.GetService<IApiVersionDescriptionProvider>();
using (var db = app.Services.CreateScope().ServiceProvider.GetService<DataContext>())
{
    db.Database.EnsureCreated();
    db.Database.Migrate();
}
using (var db = app.Services.CreateScope().ServiceProvider.GetService<IdentityDataContext>())
{
    db.Database.EnsureCreated();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0);
    app.UseSwaggerUI(options =>
    {
        foreach (var description in versionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
