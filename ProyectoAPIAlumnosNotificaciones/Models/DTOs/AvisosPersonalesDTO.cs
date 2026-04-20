namespace ProyectoAPIAlumnosNotificaciones.Models.DTOs
{
    public class AvsisosPersonalesDTO
    {
        public int Id { get; set; }
        public int IdMaestro { get; set; } //No se si sea necesaria pero la pondre para saber quien la mando

        public int IdAlumno { get; set; }

        public string Titulo { get; set; } = null!;

        public string Contenido { get; set; } = null!;

        public DateTime? FechaEnvio { get; set; }
    }
    public class CrearAvisoPersonalDTO
    {
        public int IdMaestro { get; set; } //No se si sea necesaria pero la pondre para saber quien la mando

        public List<int> IdAlumnos { get; set; } = new();

        public string Titulo { get; set; } = null!;

        public string Contenido { get; set; } = null!;

        public DateTime? FechaEnvio { get; set; }
    }
    public class EditarAvisoPersonalDTO {
        public int Id { get; set; } 
        public List<int> IdAlumnos { get; set; } = new();

        public string Titulo { get; set; } = null!;

        public string Contenido { get; set; } = null!;

        public DateTime? FechaEnvio { get; set; }
    }
  
}
