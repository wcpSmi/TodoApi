
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

			//Ez biztos�tja, hogy a React frontend (ami pl. http://localhost:3000-r�l fut) tudjon kapcsol�dni a https://localhost:7027 ASP.NET API-hoz.
			//Hozzunk l�tre egy AllowAll nev� CORS szab�lyt:
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowAll", policy =>
				{
					policy.WithOrigins(//Csak innen v�r k�r�st				   
						"http://192.168.1.8:3000",
								"http://localhost:3000",
								"http://192.168.1.8:3001",
								"http://localhost:3001"
								)
					//.AllowAnyOrigin()// B�rmilyen weboldalr�l enged�nk k�r�st (pl. localhost:3000)
						  .AllowAnyMethod()//GET, POST, PUT, DELETE stb. is mehet
						  .AllowAnyHeader();//Tetsz�leges HTTP fejl�ceket elfogadunk
				});
			});

			// Itt t�rt�nik meg az alkalmaz�s t�nyleges �ssze�ll�t�sa �s inicializ�l�sa.
			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			//Aktiv�ljuk a CORS enged�lyez�st
			app.UseCors("AllowAll");

			//Biztons�gi okokb�l minden k�r�st titkos�tott kapcsolat(HTTPS) al� helyez, ha lehet.
			//Tanul�si c�lb�l most kikapcsolom
			//app.UseHttpsRedirection();
			//Enged�lyezi a felhaszn�l�i hiteles�t�st �s jogosults�gkezel�st.
			app.UseAuthorization();
			//Ez biztos�tja, hogy az [Route] attrib�tummal megjel�lt v�gpontok el�rhet�k legyenek.
			app.MapControllers();

			app.Run();
		}
    }
}
