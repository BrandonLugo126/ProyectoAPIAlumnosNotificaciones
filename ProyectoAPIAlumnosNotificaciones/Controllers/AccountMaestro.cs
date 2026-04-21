using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoAPIAlumnosNotificaciones.Models.DTOs;
using ProyectoAPIAlumnosNotificaciones.Services;

namespace ProyectoAPIAlumnosNotificaciones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountMaestro : ControllerBase
    {
        private readonly MaestroService service;
        private readonly GruposService gruposService;
        public AccountMaestro(MaestroService service, GruposService gruposService)
        {
            this.service = service;
            this.gruposService = gruposService;
        }
        [HttpPost("login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            if (loginDTO == null)
                return BadRequest();
            var resultado = service.IniciarSesion(loginDTO);
            if (!resultado)
            {
                return Unauthorized();
            }
            return Ok();
        }
        [HttpPost("registrar")]
        public IActionResult Registrar(AgregarMaestroDTO dto)
        {
            service.Agregar(dto);
            return Ok(dto);
        }

        [HttpPost("creargrupo")]
        public IActionResult CrearGrupo(AgregarGrupoDTO dto)
        {
            gruposService.CrearGrupo(dto); 
            return Ok(dto);
        }
    }
}
