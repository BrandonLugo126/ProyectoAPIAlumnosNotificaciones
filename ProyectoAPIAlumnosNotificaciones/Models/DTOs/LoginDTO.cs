namespace ProyectoAPIAlumnosNotificaciones.Models.DTOs
{
    public class LoginDTO
    {
        public string Nombre { get; set; } = null!;
        public string Contraseña { get; set; } = null!;
        public string? Clave { get; set; }
    }
}
