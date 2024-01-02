using MP7_progi.Models;
using System.Collections;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

//Init database - only used in special cases
//DatabaseFunctions.InitializeDB();

//Database tester - perform basic tests on tables
DatabaseFunctions.getShelterData();

DatabaseFunctions.getCommentData(28);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

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
