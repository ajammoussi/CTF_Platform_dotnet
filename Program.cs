using CTF_Platform_dotnet.Mapping;
using CTF_Platform_dotnet.Repositories;
using CTF_Platform_dotnet.Services.EmailSender;
using Microsoft.EntityFrameworkCore;
using SendGrid;


var builder = WebApplication.CreateBuilder(args);


builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
using System.Text;
using CTF_Platform_dotnet.Auth;
using CTF_Platform_dotnet.Repositories;
using CTF_Platform_dotnet.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
using CTF_Platform_dotnet.Services.EmailSender;
using SendGrid;

var builder = WebApplication.CreateBuilder(args);

// Configuration of services
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<JwtSettings>>().Value);

builder.Services.AddDbContext<CTFContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//register repository so it can be injected into controllers
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IAdminService, AdminService>();

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
// Register the UserService
builder.Services.AddScoped<IUserService, UserService>();

// Register the TeamService
builder.Services.AddScoped<ITeamService, TeamService>();

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
    };
});

// Add authorization policies
builder.Services.AddAuthorization(options =>
{
    // Policy for participants
    options.AddPolicy("ParticipantOnly", policy =>
        policy.RequireClaim("Role", "Participant"));

    // Policy for admins
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireClaim("Role", "Admin"));

    // Policy for challenge creators
    options.AddPolicy("ChallengeCreatorOnly", policy =>
        policy.RequireClaim("Role", "ChallengeCreator"));
});

builder.Services.AddTransient<IEmailSender, SendGridEmailSender>();
builder.Services.AddSingleton<ISendGridClient>(sp =>
    new SendGridClient(builder.Configuration["SendGrid:ApiKey"])
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IEmailSender, SendGridEmailSender>();
builder.Services.AddSingleton<ISendGridClient>(sp =>
    new SendGridClient(builder.Configuration["SendGrid:ApiKey"])
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
