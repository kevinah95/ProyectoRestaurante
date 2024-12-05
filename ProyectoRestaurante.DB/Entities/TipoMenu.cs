using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoRestaurante.DB.Entities;

public partial class TipoMenu
{
    [Key]
    public int IdTipoMenu { get; set; }

    public string DscTipoMenu { get; set; } = null!;

    [InverseProperty("IdTipoMenuNavigation")]
    public virtual ICollection<Menu> Menu { get; set; } = new List<Menu>();
}
