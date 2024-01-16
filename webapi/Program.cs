using MP7_progi.Models;
using System.Collections;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

//Init database - only used in special cases
//DatabaseFunctions.InitializeDB();

//Database tester - perform basic tests on tables
//string adTestData = "{\"Data\":[{\"species\":\"Pas\",\"namePet\":\"Marli\",\"dateHourMis\":\"01-01-2024\",\"location\":\"\",\"color\":\"\",\"age\":\"\"}]}";

string CommentTestData = "{\"Data\":[{\"userID\":\"18\",\"adID\":\"24\",\"photoCom\":\"\",\"textCom\":\"resii\",\"locCom\":\"Zadar\"}]}";

//Console.WriteLine(DatabaseFunctions.searchAd(adTestData));

//Console.WriteLine(DatabaseFunctions.postComment(CommentTestData));

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
