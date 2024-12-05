using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProyectoRestaurante.DB.Entities;

namespace ProyectoRestaurante.DB.Context;

public partial class RestauranteDbContext : DbContext
{
    public RestauranteDbContext()
    {
    }

    public RestauranteDbContext(DbContextOptions<RestauranteDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Clientes> Clientes { get; set; }

    public virtual DbSet<Ingrediente> Ingrediente { get; set; }

    public virtual DbSet<Inventario> Inventario { get; set; }

    public virtual DbSet<Menu> Menu { get; set; }

    public virtual DbSet<Mesas> Mesas { get; set; }

    public virtual DbSet<Reservacion> Reservacion { get; set; }

    public virtual DbSet<TipoMenu> TipoMenu { get; set; }

    public virtual DbSet<Valoracion> Valoracion { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Clientes>(entity =>
        {
            entity.Property(e => e.IdCliente).ValueGeneratedNever();
        });

        modelBuilder.Entity<Ingrediente>(entity =>
        {
            entity.Property(e => e.IdIngrediente).ValueGeneratedNever();
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasOne(d => d.IdIngredienteNavigation).WithMany().OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.Property(e => e.IdMenu).ValueGeneratedNever();

            entity.HasOne(d => d.IdTipoMenuNavigation).WithMany(p => p.Menu).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Mesas>(entity =>
        {
            entity.Property(e => e.IdMesa).ValueGeneratedNever();
        });

        modelBuilder.Entity<Reservacion>(entity =>
        {
            entity.Property(e => e.NumReservacion).ValueGeneratedNever();

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Reservacion).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.Reservacion).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdMesaNavigation).WithMany(p => p.Reservacion).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<TipoMenu>(entity =>
        {
            entity.Property(e => e.IdTipoMenu).ValueGeneratedNever();
        });

        modelBuilder.Entity<Valoracion>(entity =>
        {
            entity.Property(e => e.IdValoracion).ValueGeneratedNever();

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Valoracion).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.Valoracion).OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
