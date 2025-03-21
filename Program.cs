
using Microsoft.EntityFrameworkCore;
using System;
using TodoApi.Data;
using TodoApi.Repositories;
using TodoApi.Respositories;
using static System.Net.WebRequestMethods;

namespace TodoApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
			//Létrehozunk egy builder objektumot, amely az ASP.NET Core alkalmazás konfigurálására szolgál.
			var builder = WebApplication.CreateBuilder(args);

			//Adatbázis kapcsolat konfigurálása (SQLite)
			builder.Services.AddDbContext<TodoDbContext>(options =>
				options.UseSqlite("Data Source=todo.db"));
			//Ha SQL Servert akarsz használni, akkor így :
				//builder.Services.AddDbContext<TodoDbContext>(options =>
				//	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			//Ez biztosítja, hogy az API képes legyen kezelni az HTTP kéréseket (pl. GET, POST, PUT, DELETE).
			builder.Services.AddControllers();
			//Ez fontos a Swagger UI mûködéséhez, hogy az API végpontjait kilistázhassa.
			builder.Services.AddEndpointsApiExplorer();
			// Ennek köszönhetõen a Swagger UI segítségével vizuálisan kipróbálhatjuk az API-t a böngészõbõl.
			builder.Services.AddSwaggerGen();
			//Regisztráljuk az adatbázis réteget
			builder.Services.AddScoped<UserRepository>();
			builder.Services.AddScoped<TodoRepository>();

			// Itt történik meg az alkalmazás tényleges összeállítása és inicializálása.
			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			//Biztonsági okokból minden kérést titkosított kapcsolat(HTTPS) alá helyez, ha lehet.
			app.UseHttpsRedirection();
			//Engedélyezi a felhasználói hitelesítést és jogosultságkezelést.
			app.UseAuthorization();
			//Ez biztosítja, hogy az [Route] attribútummal megjelölt végpontok elérhetõk legyenek.
			app.MapControllers();

			app.Run();
		}
    }
}
