using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoRestaurante.DB.Entities;

public partial class Menu
{
    [Key]
    public int IdMenu { get; set; }

    public string DscMenu { get; set; } = null!;

    public int IdTipoMenu { get; set; }

    [Column(TypeName = "DECIMAL(19,2)")]
    public decimal Precio { get; set; }

    [ForeignKey("IdTipoMenu")]
    [InverseProperty("Menu")]
    public virtual TipoMenu? IdTipoMenuNavigation { get; set; } = null!;

    [InverseProperty("IdMenuNavigation")]
    public virtual ICollection<Reservacion> Reservacion { get; set; } = new List<Reservacion>();

    [InverseProperty("IdMenuNavigation")]
    public virtual ICollection<Valoracion> Valoracion { get; set; } = new List<Valoracion>();
}
