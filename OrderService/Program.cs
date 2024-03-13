using Microsoft.EntityFrameworkCore;
using NSwag;
using OrderService.Insfrastructure.DataBase;
using OrderService.RabbitMq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApiDocument(options =>
{
    options.PostProcess = document =>
    {
        document.Info = new OpenApiInfo
        {
            Version = "v1",
            Title = "API Commandes",
            Description = "API des commandes"
        };
    };
});

//Ajout de la config de la bdd
builder.Services.AddDbContext<DefaultDbContext>(options =>
{
    options.UseMySQL(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
});


//Ajout du listener Rabbit MQ
builder.Services.AddHostedService<RabbitMqConsumer>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //Add OpenAPI 3.0 document serving middleware
    //Available at: http://localhost:<port>/swagger/v1/swagger.json
    app.UseOpenApi();

    //   Add web UIs to interact with the document
    //Available at: http://localhost:<port>/swagger
    app.UseSwaggerUi3();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Welcome to Order API Demo ! :)");

app.UseRouting();

//app.UseGrpcWeb();

app.Run();
