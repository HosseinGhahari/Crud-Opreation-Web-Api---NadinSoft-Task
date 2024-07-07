using Crud_Application_Contracts.CQRS.Commands;
using Crud_Domain;
using Crud_Infrastructure.Repository;
using Crud_Opreation.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

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


// Registers IProductService with ProductService.
// Enables dependency injection for ProductService.
builder.Services.AddTransient<IProductRepository, ProductRepository>();

// Configures the MainContext database connection using the DefaultConnection string.
builder.Services.AddDbContext<MainContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity API endpoints, customize password requirements,
// use Entity Framework for storing user data, and add default token providers.
builder.Services.AddIdentityApiEndpoints<IdentityUser>(option =>
{
    option.Password.RequiredLength = 5;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireUppercase = false;
    option.Password.RequireDigit = false;

}).AddEntityFrameworkStores<MainContext>().AddDefaultTokenProviders();


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

app.MapIdentityApi<IdentityUser>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
