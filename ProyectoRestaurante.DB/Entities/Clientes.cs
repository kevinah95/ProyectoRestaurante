using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoRestaurante.DB.Entities;

public partial class Clientes
{
    [Key]
    public int IdCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string Ap1 { get; set; } = null!;

    public string Ap2 { get; set; } = null!;

    public string NumTelefono { get; set; } = null!;

    public string CorreoElectronico { get; set; } = null!;

    [InverseProperty("IdClienteNavigation")]
    public virtual ICollection<Reservacion> Reservacion { get; set; } = new List<Reservacion>();

    [InverseProperty("IdClienteNavigation")]
    public virtual ICollection<Valoracion> Valoracion { get; set; } = new List<Valoracion>();
}
