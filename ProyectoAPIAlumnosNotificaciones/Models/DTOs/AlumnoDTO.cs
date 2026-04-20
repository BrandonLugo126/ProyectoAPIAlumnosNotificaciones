using ProyectoAPIAlumnosNotificaciones.Models.Entities;

namespace ProyectoAPIAlumnosNotificaciones.Models.DTOs
{
    public class AlumnoDTO
    {
        public int Id { get; set; }

        public int IdGrupo { get; set; }

        public string Nombre { get; set; } = null!;

    }

    public class AgregarAlumnoDTO
    {
        public string Nombre { get; set; } = null!;
        public string Contraseña { get; set; } = null!;
        public int IdGrupo { get; set; }
    }

    public class AlumnoDetallesDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdGrupo { get; set; }
         public List<AvsisosPersonalesDTO> AvisosPersonales { get; set; } = new List<AvsisosPersonalesDTO>();
    }

    public class EditarAlumnoDTO:AlumnoDetallesDTO
    {
    }
}
