using FedGraph.Main;

var builder = WebApplication.CreateBuilder(args);

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

Config config = Parsing.parse("config2.json");
Graph graph = new Graph(config);

//graph.printMatrix();
graph.dijksra(5, 6);
//graph.printPathes();

app.Run(async (context) => await context.Response.WriteAsync("Hello METANIT.COM"));

app.Run();


