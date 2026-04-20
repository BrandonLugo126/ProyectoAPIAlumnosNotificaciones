using System;
using System.Collections.Generic;

namespace ProyectoAPIAlumnosNotificaciones.Models.Entities;

public partial class Alumnos
{
    public int Id { get; set; }

    public int IdGrupo { get; set; }

    public string Nombre { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public virtual ICollection<Avisospersonales> Avisospersonales { get; set; } = new List<Avisospersonales>();

    public virtual Grupos IdGrupoNavigation { get; set; } = null!;

    public virtual ICollection<LecturasAvisos> LecturasAvisos { get; set; } = new List<LecturasAvisos>();
}
