using FedGraph.Main;

var builder = WebApplication.CreateBuilder(args);

// HttpClient для создания запросов из графа
builder.Services.AddHttpClient();

// Add services to the container.

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

//Создаём наш граф
Application.graph = new Graph(Parsing.parse("config.json"));

// Запуск сервера
app.Run();

// Обёртка над объектом класса граф,чтобы он создавался только при запуске сервера, а не при каждом запросе к нему
class Application
{
    public static Graph graph { get; set; }
}

