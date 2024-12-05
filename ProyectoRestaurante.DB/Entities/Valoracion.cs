using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoRestaurante.DB.Entities;

public partial class Valoracion
{
    [Key]
    public int IdValoracion { get; set; }

    public int IdCliente { get; set; }

    public int IdMenu { get; set; }

    public string? Comentario { get; set; }

    public int Calificacion { get; set; }

    [Column(TypeName = "DATETIME")]
    public DateTime Fecha { get; set; }

    [ForeignKey("IdCliente")]
    [InverseProperty("Valoracion")]
    public virtual Clientes? IdClienteNavigation { get; set; } = null!;

    [ForeignKey("IdMenu")]
    [InverseProperty("Valoracion")]
    public virtual Menu? IdMenuNavigation { get; set; } = null!;
}
