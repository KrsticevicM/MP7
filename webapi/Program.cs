using MP7_progi.Models;
using System.Collections;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

//Init database - only used in special cases
//DatabaseFunctions.InitializeDB();

//Database tester - perform basic tests on tables

//**testing data**

List<Object> red1 = new List<Object>();

red1.Add(102);
red1.Add("jozic3");
red1.Add("zg");
red1.Add("0123312313");
red1.Add("323");


//int c = DatabaseFunctions.insert(new User(), red1);
//Console.WriteLine(c);
//DatabaseFunctions.databaseTester(new Ad());
DatabaseFunctions.getNextAvailableID(new User());
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

app.Run();
