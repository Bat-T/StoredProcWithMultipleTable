using Microsoft.EntityFrameworkCore;
using StoredProcTest.Entities;
using StoredProcTest.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationContext>(opts =>
       opts.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));
builder.Services.AddScoped<StoredProcServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{ 
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapGet("/GetStoredProcData", async (ApplicationContext context) =>
{
    var spName = "MyCustomProcedure";
    var result =  await StoredProcServices.FindStudentsFromSql(context,spName);
    return result;
})
.WithName("GetStoredProcData");

app.MapGet("/GetStoredProcDataGeneric", async (ApplicationContext context,StoredProcServices services) =>
{
    var spName = "MyCustomProcedure";
    var result = await services.FindStudentsFromSqlGeneric(context, spName);
    return result;
})
.WithName("GetStoredProcDataGeneric");


app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


