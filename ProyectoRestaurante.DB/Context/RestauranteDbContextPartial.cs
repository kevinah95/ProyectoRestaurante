using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ProyectoRestaurante.DB.Entities;

namespace ProyectoRestaurante.DB.Context;

public partial class RestauranteDbContext
{
    protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            SqliteConnectionStringBuilder builder = new SqliteConnectionStringBuilder();
            
            builder.DataSource =
                "/Users/kevs/Documents/workshop/Curso-Dotnet/ProyectoRestaurante/identifier.sqlite";

            optionsBuilder.UseSqlite(builder.ConnectionString);
        }
    }
}
