using Microsoft.EntityFrameworkCore;
using OrderService.Insfrastructure.DataBase;
using OrderService.RabbitMq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Ajout de la config de la bdd
builder.Services.AddDbContext<DefaultDbContext>(options =>
{
    //options.UseMySQL(builder.Configuration.GetConnectionString("DefaultContext"));
});


//Ajout du listener Rabbit MQ
builder.Services.AddHostedService<RabbitMqConsumer>();

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
