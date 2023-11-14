using MP7_progi.Models;
using System.Collections;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

//Init database - only used in special cases
//DatabaseFunctions.InitializeDB();

//Database tester - perform basic tests on tables

//**testing data**

List<ArrayList> lista = new List<ArrayList>() ;
ArrayList red1 = new ArrayList();
ArrayList red2 = new ArrayList();
red1.Add(100);
red1.Add("jozic");
red1.Add("zg");
red1.Add(13);
red1.Add("5");


red2.Add(122);
red2.Add("peric");
red2.Add("vz");
red2.Add(22);
red2.Add("2");


lista.Add(red1);
lista.Add(red2);

//DatabaseFunctions.insert(new User(),lista); **testing**
DatabaseFunctions.databaseTester(new User());
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
