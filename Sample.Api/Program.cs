using Microsoft.EntityFrameworkCore;
using QuickActions.Api;
using Sample.Common.Models;

namespace Sample.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection")));
            }, contextLifetime: ServiceLifetime.Transient);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddTransient<AppDbContext>();
            builder.Services.AddTransient<DbContext, AppDbContext>();
            builder.Services.AddTransient<CrudRepository<User>>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

            app.MapControllers();

            app.Run();
        }
    }
}