
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
			//L�trehozunk egy builder objektumot, amely az ASP.NET Core alkalmaz�s konfigur�l�s�ra szolg�l.
			var builder = WebApplication.CreateBuilder(args);

			//Adatb�zis kapcsolat konfigur�l�sa (SQLite)
			builder.Services.AddDbContext<TodoDbContext>(options =>
				options.UseSqlite("Data Source=todo.db"));
			//Ha SQL Servert akarsz haszn�lni, akkor �gy :
				//builder.Services.AddDbContext<TodoDbContext>(options =>
				//	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			//Ez biztos�tja, hogy az API k�pes legyen kezelni az HTTP k�r�seket (pl. GET, POST, PUT, DELETE).
			builder.Services.AddControllers();
			//Ez fontos a Swagger UI m�k�d�s�hez, hogy az API v�gpontjait kilist�zhassa.
			builder.Services.AddEndpointsApiExplorer();
			// Ennek k�sz�nhet�en a Swagger UI seg�ts�g�vel vizu�lisan kipr�b�lhatjuk az API-t a b�ng�sz�b�l.
			builder.Services.AddSwaggerGen();
			//Regisztr�ljuk az adatb�zis r�teget
			builder.Services.AddScoped<UserRepository>();
			builder.Services.AddScoped<TodoRepository>();

			// Itt t�rt�nik meg az alkalmaz�s t�nyleges �ssze�ll�t�sa �s inicializ�l�sa.
			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			//Biztons�gi okokb�l minden k�r�st titkos�tott kapcsolat(HTTPS) al� helyez, ha lehet.
			app.UseHttpsRedirection();
			//Enged�lyezi a felhaszn�l�i hiteles�t�st �s jogosults�gkezel�st.
			app.UseAuthorization();
			//Ez biztos�tja, hogy az [Route] attrib�tummal megjel�lt v�gpontok el�rhet�k legyenek.
			app.MapControllers();

			app.Run();
		}
    }
}
