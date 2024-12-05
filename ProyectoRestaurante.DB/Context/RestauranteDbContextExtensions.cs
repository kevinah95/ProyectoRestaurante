using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProyectoRestaurante.DB.Entities;

namespace ProyectoRestaurante.DB.Context;

public static class RestauranteDbContextExtensions
{
    public static IServiceCollection AddRestauranteContext(this IServiceCollection services, string? connectionString = null)
    {
        if (connectionString == null)
        {
            SqliteConnectionStringBuilder builder = new SqliteConnectionStringBuilder();
            //Path.Combine(AppContext.BaseDirectory, "Data", "Northwind.db");
            builder.DataSource =
                "../identifier.sqlite";
            
            connectionString = builder.ConnectionString;
        }

        services.AddDbContext<RestauranteDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });

        return services;
    }
}
