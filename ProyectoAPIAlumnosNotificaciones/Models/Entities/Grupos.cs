using System;
using System.Collections.Generic;

namespace ProyectoAPIAlumnosNotificaciones.Models.Entities;

public partial class Grupos
{
    public int Id { get; set; }

    public int IdMaestro { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Alumnos> Alumnos { get; set; } = new List<Alumnos>();

    public virtual Maestros IdMaestroNavigation { get; set; } = null!;
}
