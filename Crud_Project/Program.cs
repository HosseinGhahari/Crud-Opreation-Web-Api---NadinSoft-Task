using Crud_Application_Contracts.CQRS.Commands;
using Crud_Domain;
using Crud_Infrastructure.Repository;
using Crud_Opreation.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registers IProductService with ProductService.
// Enables dependency injection for ProductService.
builder.Services.AddTransient<IProductRepository, ProductRepository>();

// Configures the MainContext database connection using the DefaultConnection string.
builder.Services.AddDbContext<MainContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
