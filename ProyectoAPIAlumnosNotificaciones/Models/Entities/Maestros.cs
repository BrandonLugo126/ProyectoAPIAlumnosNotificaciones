using System;
using System.Collections.Generic;

namespace ProyectoAPIAlumnosNotificaciones.Models.Entities;

public partial class Maestros
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Correo { get; set; }

    public string Clave { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public virtual ICollection<Avisospersonales> Avisospersonales { get; set; } = new List<Avisospersonales>();

    public virtual Grupos? Grupos { get; set; }
}
