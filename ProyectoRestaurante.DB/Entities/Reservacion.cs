using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoRestaurante.DB.Entities;

public partial class Reservacion
{
    [Key]
    public int NumReservacion { get; set; }

    public int IdCliente { get; set; }

    public int IdMesa { get; set; }

    public int IdMenu { get; set; }

    public int Cantidad { get; set; }

    [Column(TypeName = "DATETIME")]
    public DateTime FecReserva { get; set; }

    [ForeignKey("IdCliente")]
    [InverseProperty("Reservacion")]
    public virtual Clientes? IdClienteNavigation { get; set; } = null!;

    [ForeignKey("IdMenu")]
    [InverseProperty("Reservacion")]
    public virtual Menu? IdMenuNavigation { get; set; } = null!;

    [ForeignKey("IdMesa")]
    [InverseProperty("Reservacion")]
    public virtual Mesas? IdMesaNavigation { get; set; } = null!;
}
