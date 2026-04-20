using System;
using System.Collections.Generic;

namespace ProyectoAPIAlumnosNotificaciones.Models.Entities;

public partial class Avisosgenerales
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Contenido { get; set; } = null!;

    public DateTime? FechaEmision { get; set; }

    public DateTime FechaCaducidad { get; set; }
}
