namespace ProyectoAPIAlumnosNotificaciones.Models.DTOs
{
    public class MaestroDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Correo { get; set; }

        public string Clave { get; set; } = null!;
    }

    public class AgregarMaestroDTO
    {
        public string Nombre { get; set; } = null!;
        public string? Correo { get; set; }
        public string Clave { get; set; } = null!;
        public string Contraseña { get; set; } = null!;
    }
    public class EditarMaestroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Correo { get; set; }
        public string Contraseña { get; set; } = null!;
    }
}
