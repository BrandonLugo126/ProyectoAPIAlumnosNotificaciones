using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoAPIAlumnosNotificaciones.Models.DTOs;
using ProyectoAPIAlumnosNotificaciones.Services;

namespace ProyectoAPIAlumnosNotificaciones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountAlumno : ControllerBase
    {
        private readonly AlumnoService alumnoService;
        public AccountAlumno(AlumnoService alumnoService)
        {
            this.alumnoService = alumnoService;
         }
        [HttpPost("login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            if (loginDTO == null)
                return BadRequest();
            var resultado = alumnoService.IniciarSesion(loginDTO);
            if (!resultado)
            {
                return Unauthorized();
            }
            return Ok();
        }
        [HttpPost("registrar")] 
        public IActionResult Registrar(AgregarAlumnoDTO dto)
        {
            alumnoService.Agregar(dto);
            return Ok(dto);
        }
    }
}
