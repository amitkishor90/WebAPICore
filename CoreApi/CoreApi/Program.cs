using CoreApi.DatabaseModels;
using CoreApi.InterfaceImplements;
using CoreApi.InterfacesWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CoreApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            #region add  DBConnection 
            //var connection = "Server=localhost;Database=CurdAPI;Trusted_Connection=True;TrustServerCertificate=True;";
            //builder.Services.AddDbContext<CurdApiContext>(options => options.UseSqlServer(connection)); // sql connection  add

            var provider = builder.Services.BuildServiceProvider();
            var config = provider.GetRequiredService<IConfiguration>();
            builder.Services.AddDbContext<CurdApiContext>(item => item.UseSqlServer(config.GetConnectionString("DBCS")));
            #endregion

            // Add Swagger
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = ".Net core api",
                    Version = "v1",
                    Description = "Api .NET Core Use Entity framework DataBase First  v1"
                });
                

            });

            builder.Services.AddScoped<IGenderMaster, GenderInterfaceImplements>();
            builder.Services.AddScoped<IDepartmentMaster, DepartmentInterfaceImplements>();
            builder.Services.AddScoped<IEmployeesMaster, EmployeeinterfaceImplements>();
            builder.Services.AddScoped<IadminInterface, AdminInterfaceImplements>();

            //================================================================================================// 

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //Enable Swagger UI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name v1");
            });
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}