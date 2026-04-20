using System;
using System.Collections.Generic;

namespace ProyectoAPIAlumnosNotificaciones.Models.Entities;

public partial class LecturasAvisos
{
    public int Id { get; set; }

    public int IdAvisoPersonal { get; set; }

    public int IdAlumno { get; set; }

    public DateTime? FechaLectura { get; set; }

    public virtual Alumnos IdAlumnoNavigation { get; set; } = null!;

    public virtual Avisospersonales IdAvisoPersonalNavigation { get; set; } = null!;
}
