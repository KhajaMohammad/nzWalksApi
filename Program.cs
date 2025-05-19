using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using nzWalksApi.Data;
using nzWalksApi.Mappings;
using nzWalksApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    // 1. Define the Swagger document with metadata
    // This sets up the basic information shown in the Swagger UI like title and version
    options.SwaggerDoc(
        "v1",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "NZ Walks API", // Display name of the API in Swagger UI
            Version = "v1", // Version of the API
        }
    );

    // 2. Define the security scheme for JWT Bearer authentication
    // This tells Swagger how the API expects clients to authenticate
    options.AddSecurityDefinition(
        JwtBearerDefaults.AuthenticationScheme,
        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Name = "Authorization", // Name of the header where the token is expected
            In = Microsoft.OpenApi.Models.ParameterLocation.Header, // Token is passed in the HTTP header
            Type = SecuritySchemeType.ApiKey, // Treated as an API key for Swagger purposes (even though it's a JWT)
            Scheme = JwtBearerDefaults.AuthenticationScheme, // Typically "Bearer", used by ASP.NET Core to identify the scheme
        }
    );

    // 3. Apply the security scheme globally to all API endpoints
    // This ensures that Swagger UI knows the API requires authentication using the defined scheme
    options.AddSecurityRequirement(
        new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme, // Refers to the scheme defined above
                        Id = JwtBearerDefaults.AuthenticationScheme, // Must match the scheme ID
                    },
                    Scheme = "Bearer", // This should match the actual scheme used in the app
                    Name = JwtBearerDefaults.AuthenticationScheme,
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                },
                new List<string>() // No specific scopes required for this API
            },
        }
    );
});

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder
    .Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks")
    .AddEntityFrameworkStores<NZWalksAuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
});

builder.Services.AddDbContext<NzWalksDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString"))
);

builder.Services.AddDbContext<NZWalksAuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksAuthConnectionString"))
);

builder.Services.AddScoped<IRegionRepository, SqlRegionRepository>();
builder.Services.AddScoped<IWalkRepository, SqlWalkRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            ),
        }
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
