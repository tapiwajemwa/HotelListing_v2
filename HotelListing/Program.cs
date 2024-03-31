using Serilog;
using Serilog.Events;
using System.Reflection;

namespace HotelListing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(
                path: "C:\\Users\\tjemwa\\Documents\\Development\\Workspace\\HotelListing\\logs\\logs-.txt",
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fffd zzz} [{Level:u3}] {Message:lj} {NewLine}{Exception}",
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: LogEventLevel.Information

                ).CreateLogger();
            
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Host.UseSerilog();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); 
            }
            app.UseSwagger();
            app.UseSwaggerUI();


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            try
            {
                Log.Information("Application is starting");
                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal("Application Failed to start");
            }
            finally
            {
                Log.CloseAndFlush();
            }
            
        }
    }
}