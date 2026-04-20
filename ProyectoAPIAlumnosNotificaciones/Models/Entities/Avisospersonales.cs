using System;
using System.Collections.Generic;

namespace ProyectoAPIAlumnosNotificaciones.Models.Entities;

public partial class Avisospersonales
{
    public int Id { get; set; }
    public int IdMaestro { get; set; }

    public int IdAlumno { get; set; }

    public string Titulo { get; set; } = null!;

    public string Contenido { get; set; } = null!;

    public DateTime? FechaEnvio { get; set; }

    public virtual Alumnos IdAlumnoNavigation { get; set; } = null!;

    public virtual Maestros IdMaestroNavigation { get; set; } = null!;

    public virtual ICollection<LecturasAvisos> LecturasAvisos { get; set; } = new List<LecturasAvisos>();
}
