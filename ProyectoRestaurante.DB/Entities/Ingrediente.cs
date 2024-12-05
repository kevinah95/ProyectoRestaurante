using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoRestaurante.DB.Entities;

public partial class Ingrediente
{
    [Key]
    public int IdIngrediente { get; set; }

    public string DscIngrediente { get; set; } = null!;
}
