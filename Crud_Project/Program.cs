using Crud_Application.Services;
using Crud_Application_Contracts.CQRS.Commands;
using Crud_Domain;
using Crud_Infrastructure.Repository;
using Crud_Opreation.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// this code sets up Swagger to handle API key-based
// authentication (OAuth2) and ensures that all API
// endpoints require the “Authorization” header with the token.
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    option.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddSwaggerGen();

// Registers IProductService with ProductService.
// Enables dependency injection for ProductService.
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IAuthService, AuthService>();

// Configures the MainContext database connection using the DefaultConnection string.
builder.Services.AddDbContext<MainContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity API endpoints, customize password requirements,
// use Entity Framework for storing user data, and add default token providers.
builder.Services.AddIdentity<IdentityUser,IdentityRole>(option =>
{
    option.Password.RequiredLength = 5;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireUppercase = false;
    option.Password.RequireDigit = false;

}).AddEntityFrameworkStores<MainContext>().AddDefaultTokenProviders();


// This code configures the authentication scheme for the application to use
// JWT (Json Web Tokens).It sets the default authentication and challenge schemes to use JWT.
// Then, it adds the JWT bearer authentication middleware to the pipeline with specific token
// validation parameters.These parameters include validating the actor, issuer, audience, and
// the issuer signing key.It also requires the token to have an expiration time. The valid issuer,
// audience, and signing key are retrieved from the application's configuration.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option => 
{
    option.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        RequireExpirationTime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                (builder.Configuration.GetSection("Jwt:Key").Value))
    };
});


// This method call adds MediatR-related services to the DI container.
// It allows you to use MediatR for handling commands and queries.
builder.Services.AddMediatR(a => a.RegisterServicesFromAssembly(typeof(CreateProductCommand.Command).Assembly));


var app = builder.Build();

// Creates or ensures the existence of the MainContext database.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<MainContext>();
    context.Database.EnsureCreated();
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.MapIdentityApi<IdentityUser>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
