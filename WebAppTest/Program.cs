
using Microsoft.AspNetCore.Mvc;
using Sang.AspNetCore.CommonLibraries.Filter;

namespace WebAppTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddSingleton(new UnhandledExceptionFilterConfig
            {
                Status = 500,
                StatusCode = 500
            });
            builder.Services.AddControllers(opt =>
            {
                opt.Filters.Add<UnhandledExceptionFilter>();
            });
            

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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
