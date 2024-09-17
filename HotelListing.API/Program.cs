
using Serilog;

namespace HotelListing.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            #region CORS
            //Used to faciltate "Cross Origin Resource Sharing"
            //Accessed by clients on different servers
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", b => b.AllowAnyHeader()
                                                    .AllowAnyOrigin()
                                                    .AllowAnyMethod());
            });
            #endregion

            builder.Host.UseSerilog((context, loggerConfig) =>
            {
                loggerConfig.WriteTo.Console().ReadFrom.Configuration(context.Configuration);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");////////////////////////////

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
