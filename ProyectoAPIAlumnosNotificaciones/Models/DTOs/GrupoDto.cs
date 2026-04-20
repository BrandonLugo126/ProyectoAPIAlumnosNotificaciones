namespace ProyectoAPIAlumnosNotificaciones.Models.DTOs
{
    public class GrupoDTO
    {
        public int Id { get; set; }

        public int IdMaestro { get; set; }

        public string Nombre { get; set; } = null!;

    }
   public class AgregarGrupoDTO
    {
        public int IdMaestro { get; set; }
        public string Nombre { get; set; } = null!;
    }
     public class GrupoDetallesDTO
    {
        public int Id { get; set; }
        public int IdMaestro { get; set; }
        public string Nombre { get; set; } = null!;
        public List<AlumnoDTO> Alumnos { get; set; } = new List<AlumnoDTO>();
    }
     public class EditarGrupoDTO:GrupoDetallesDTO
    {
    }
  
}
