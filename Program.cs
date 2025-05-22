using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using nzWalksApi.Data;
using nzWalksApi.Mappings;
using nzWalksApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(); // Registers MVC controllers in the dependency injection container

builder.Services.AddHttpContextAccessor(); // Registers the IHttpContextAccessor service for accessing the current HTTP context

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); // Adds the endpoints API explorer for discovering API endpoints

// Configuring Swagger for API documentation
builder.Services.AddSwaggerGen(options =>
{
    // 1. Define the Swagger document with metadata
    // This sets up the basic information shown in the Swagger UI like title and version
    options.SwaggerDoc(
        "v1", // API version
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "NZ Walks API", // Display name of the API in Swagger UI
            Version = "v1", // Version of the API
        }
    );

    // 2. Define the security scheme for JWT Bearer authentication
    // This tells Swagger how the API expects clients to authenticate
    options.AddSecurityDefinition(
        JwtBearerDefaults.AuthenticationScheme, // Defines the name of the authentication scheme
        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Name = "Authorization", // Name of the header where the token is expected
            In = Microsoft.OpenApi.Models.ParameterLocation.Header, // Token is passed in the HTTP header
            Type = SecuritySchemeType.ApiKey, // JWT treated as an API key for Swagger documentation
            Scheme = JwtBearerDefaults.AuthenticationScheme, // Defines the authentication scheme used in the app (Bearer token)
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
                        Id = JwtBearerDefaults.AuthenticationScheme, // Match the scheme ID defined earlier
                    },
                    Scheme = "Bearer", // This should match the scheme used in the app (Bearer token)
                    Name = JwtBearerDefaults.AuthenticationScheme,
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header, // Indicates the token should be sent in the HTTP header
                },
                new List<string>() // No specific scopes are required for this API
            },
        }
    );
});

// Configure AutoMapper for object-to-object mapping
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder
    .Services.AddIdentityCore<IdentityUser>() // Adds IdentityCore services for user management
    .AddRoles<IdentityRole>() // Adds support for user roles
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks") // Configures token provider for identity
    .AddEntityFrameworkStores<NZWalksAuthDbContext>() // Configures EF Core to use NZWalksAuthDbContext for Identity
    .AddDefaultTokenProviders(); // Adds default token providers for account-related functionality

// Configure password settings for Identity
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredUniqueChars = 1; // At least 1 unique character in the password
    options.Password.RequireDigit = false; // Password does not require a digit
    options.Password.RequireLowercase = false; // Password does not require a lowercase letter
    options.Password.RequireUppercase = false; // Password does not require an uppercase letter
    options.Password.RequiredLength = 6; // Minimum password length is 6 characters
    options.Password.RequireNonAlphanumeric = false; // Password does not require non-alphanumeric characters
});

// Configure databases with connection strings from the configuration
builder.Services.AddDbContext<NzWalksDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString")) // Connection string for main application database
);

builder.Services.AddDbContext<NZWalksAuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksAuthConnectionString")) // Connection string for authentication database
);

// Register repositories for dependency injection
builder.Services.AddScoped<IRegionRepository, SqlRegionRepository>(); // Scoped service for region repository
builder.Services.AddScoped<IWalkRepository, SqlWalkRepository>(); // Scoped service for walk repository
builder.Services.AddScoped<ITokenRepository, TokenRepository>(); // Scoped service for token repository

builder.Services.AddScoped<IImageRepository, LocalImageRepository>(); // Scoped service for image repository

// Configure JWT Bearer Authentication
builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // Adds JWT authentication scheme
    .AddJwtBearer(options =>
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, // Validates the issuer of the JWT token
            ValidateAudience = true, // Validates the audience of the JWT token
            ValidateLifetime = true, // Validates the token's expiration time
            ValidateIssuerSigningKey = true, // Validates the issuer signing key
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // Set issuer from configuration
            ValidAudience = builder.Configuration["Jwt:Audience"], // Set audience from configuration
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]) // Set signing key for JWT from configuration
            ),
        }
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enables Swagger in development environment
    app.UseSwaggerUI(); // Enables Swagger UI in development environment
}

app.UseHttpsRedirection(); // Redirects HTTP requests to HTTPS

app.UseAuthentication(); // Enables authentication middleware for token validation

app.UseAuthorization(); // Enables authorization middleware for role and policy-based access control

// Serve static files from the "Images" directory
app.UseStaticFiles(
    new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "Images") // Serve files from the "Images" directory in the project root
        ),
        RequestPath = "/Images", // URL path where images will be accessible
        // Example: localhost:/images will redirect to the file provider path
    }
);

app.MapControllers(); // Maps incoming requests to the appropriate controller actions

app.Run(); // Runs the application
