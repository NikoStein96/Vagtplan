using Microsoft.EntityFrameworkCore;
using Vagtplan.Data;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.OpenApi.Models;
using OfficeOpenXml;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Vagtplan.Interfaces.Repositories;
using Vagtplan.Repositories;
using Microsoft.Office.Interop.Excel;
using Vagtplan.Interfaces;
using Vagtplan.UOF;
using Vagtplan.Interfaces.Services;
using Vagtplan.Services;

namespace Vagtplan
{
    public class Program
    {
        public static void Main(string[] args)
        {


            var credentials = GoogleCredential.FromFile("keys.json");
            var firebaseApp = FirebaseApp.Create(new AppOptions
            {
                Credential = credentials,
            });

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            builder.Services.AddControllers(); 



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
            });
            builder.Services.AddDbContext<ShiftPlannerContext>(Options => { Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); } );
            builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<IEmployeeService, EmployeeService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseSwagger();
            app.UseSwaggerUI();


            app.UseCors();
            //app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseMiddleware<FirebaseAuthenticationMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}