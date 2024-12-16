using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using LMS.BookInventory.Domain.Contracts;
using LMS.BookInventory.Infra.Database;
using LMS.BookInventory.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LMS.BookInventory.Infra;

/// <summary>
/// Static class containing dependencies for the infrastructure layer
/// </summary>
[ExcludeFromCodeCoverage]
public static class ConfigureServices
{
    private const string DbConnectionKey = "BookContext";

    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration, 
        IHostEnvironment environment)
    {
        services.AddAutoMapper(Assembly.GetEntryAssembly());
        
        services.AddTransient<IBookRepository, BookRepository>();

        if (environment.IsDevelopment() || environment.IsProduction())
        {
            var connectionString = configuration.GetConnectionString(DbConnectionKey);
            //services.AddSqlite<BookContext>(connectionString);
            // environment.ContentRootPath
            services.AddDbContext<BookContext>(
                db => {                    
                    db.UseSqlite(connectionString);                    
                });
        }
        else
        {
            /* if its not local dev or production, use in-memory database.
             this is for things like unit or contract testing */
            services.AddDbContext<BookContext>(db => db.UseInMemoryDatabase("TestBookContext"));
        }

        return services;
    }
}