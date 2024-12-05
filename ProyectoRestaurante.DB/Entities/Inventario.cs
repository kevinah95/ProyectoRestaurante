using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoRestaurante.DB.Entities;

[Keyless]
public partial class Inventario
{
    public int IdIngrediente { get; set; }

    public int CantidadDisponible { get; set; }

    [Column(TypeName = "DECIMAL(19,2)")]
    public decimal PrecioUnidad { get; set; }

    [ForeignKey("IdIngrediente")]
    public virtual Ingrediente IdIngredienteNavigation { get; set; } = null!;
}
