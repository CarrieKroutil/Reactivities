using Application.Activities;
using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions
{
    /// <summary>
    /// Extension classes and methods need to be static.
    /// Important to use the "this" keyword to denote what is being extended.
    /// 
    /// The Application Service Extensions class is intended to clean up the Program.cs code.
    /// </summary>
    public static class ApplicationServiceExensions
    {
        /// <summary>
        /// Add App
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            // Needs to match URL where requests are coming from, in our case, client-app aka our React application.
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    s => s.SetIsOriginAllowedToAllowWildcardSubdomains()
                    .WithOrigins(
                        "http://localhost:3000"
                    )
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                );
            });

            // Instruct which assembly the handlers live via typeof() pointing to any handler.
            services.AddMediatR(typeof(List.Handler));
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            return services;
        }
    }
}