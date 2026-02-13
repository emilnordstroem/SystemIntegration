using Data.TodoAPI;
using Microsoft.EntityFrameworkCore;
using Lektion3TodoAPI.Models;
using Scalar.AspNetCore;

namespace Lektion3TodoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<TodoContext>(option => option.UseInMemoryDatabase("TodoList"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseAuthorization();


            app.MapControllers();
            app.MapScalarApiReference();

            app.Run();
        }
    }
}
