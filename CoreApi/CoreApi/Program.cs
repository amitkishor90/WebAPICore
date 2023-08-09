using CoreApi.DatabaseModels;
using CoreApi.InterfaceImplements;
using CoreApi.InterfacesWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text;
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

            #region Swagger
            //var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
            //builder.Services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(x =>
            //{
            //    x.RequireHttpsMetadata = false;
            //    x.SaveToken = false;
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(key),
            //        ValidateIssuer = false,
            //        ValidateAudience = false,
            //    };
            //});

            builder.Services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                     {
                         {
                               new OpenApiSecurityScheme
                                 {
                                     Reference = new OpenApiReference
                                     {
                                         Type = ReferenceType.SecurityScheme,
                                         Id = "Bearer"
                                     }
                                 },
                                 new string[] {}
                         }
                     });

            });
            #endregion
            // Add Swagger


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