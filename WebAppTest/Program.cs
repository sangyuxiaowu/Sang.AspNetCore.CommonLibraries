using Sang.AspNetCore.CommonLibraries;

namespace WebAppTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddUnhandledExceptionFilter(config =>
            {
                config.Status = 500;
                config.StatusCode = 500;
            });

            builder.Services.AddModelValidationExceptionFilter(config =>
            {
                config.Status = 400;
                config.StatusCode = 400;
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
