using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoRestaurante.DB.Entities;

public partial class Mesas
{
    [Key]
    public int IdMesa { get; set; }

    public string DscMesa { get; set; } = null!;

    [InverseProperty("IdMesaNavigation")]
    public virtual ICollection<Reservacion> Reservacion { get; set; } = new List<Reservacion>();
}
