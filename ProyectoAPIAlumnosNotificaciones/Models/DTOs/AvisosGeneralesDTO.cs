using ProyectoAPIAlumnosNotificaciones.Models.Entities;

namespace ProyectoAPIAlumnosNotificaciones.Models.DTOs
{
    public class AvisosGeneralesDTO
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = null!;

        public string Contenido { get; set; } = null!;

        public DateTime? FechaEmision { get; set; }

        public DateTime FechaCaducidad { get; set; }
    }
    public class CrearAvisoGeneralDTO
    {
        public string Titulo { get; set; } = null!;

        public string Contenido { get; set; } = null!;

        public DateTime? FechaEmision { get; set; }

        public DateTime FechaCaducidad { get; set; }
    }
    public class ActualizarAvisoGeneralDTO : CrearAvisoGeneralDTO
    {
        public int Id { get; set; }
    }
    public class EliminarAvisoGeneralDTO
    {
        public int Id { get; set; }
    }
}
