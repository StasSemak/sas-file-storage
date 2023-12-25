using BussinessLogic;
using BussinessLogic.Interfaces;
using BussinessLogic.Services;
using DataLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connStr = builder.Configuration.GetConnectionString("LocalDb");
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connStr));

            string dir;
            if (builder.Environment.IsDevelopment())
            {
                dir = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory().ToString()).FullName,
                    "BussinessLogic", "Files");
            }
            else dir = Path.Combine(Directory.GetCurrentDirectory(), "Files");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            Constants.FilesFolderPath = dir;
            Constants.IsDevelopment = builder.Environment.IsDevelopment();

            builder.Services.AddScoped<IFileService, FileService>();

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

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(dir),
                RequestPath = "/files"
            });

            app.UseHttpsRedirection();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}