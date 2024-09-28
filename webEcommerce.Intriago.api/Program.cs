using Microsoft.EntityFrameworkCore;
using WebApiPerson.Context;
using WebApiPerson.MQ;
using WebApiPerson.Services.Implementations;
using WebApiPerson.Services.Intefaces;
using WebApiPerson.Services.MQ;

var builder = WebApplication.CreateBuilder(args);

// Leer la configuración de RabbitMQ desde appsettings.json
builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("rabbitmq"));

builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ICustomersService, CustomersService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ISalesService, SalesService>();
builder.Services.AddTransient<ISaleDetailService, SaleDetailService>();


// Add services to the container.
// crear variable para la cadena de conexion
var connectionString = builder.Configuration.GetConnectionString("Connection");
//registrar servicio para la conexion
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(connectionString)
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//servicio para MQ
builder.Services.AddSingleton<RabbitMQService>();
builder.Services.AddHostedService<RabbitMQListenerService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
