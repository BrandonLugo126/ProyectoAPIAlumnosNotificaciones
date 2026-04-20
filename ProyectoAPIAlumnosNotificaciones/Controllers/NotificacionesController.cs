using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoAPIAlumnosNotificaciones.Models.DTOs;
using ProyectoAPIAlumnosNotificaciones.Models.Entities;
using ProyectoAPIAlumnosNotificaciones.Services;

namespace ProyectoAPIAlumnosNotificaciones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionesController : ControllerBase
    {
        private readonly AvisoGeneralService serviceGeneral;
        private readonly AvisoPersonalService servicePersonal;
        public NotificacionesController(AvisoGeneralService serviceGeneral, AvisoPersonalService servicePersonal)
        {
            this.serviceGeneral = serviceGeneral;
            this.servicePersonal = servicePersonal;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var avisosGenerales = serviceGeneral.GetAll();
            var avisosPersonales = servicePersonal.GetAll();

            var allAvisos = new
            {
                Generales = avisosGenerales,
                Personales = avisosPersonales
            };

            return Ok(allAvisos);
        }

        [HttpGet("{id}")]
        public IActionResult GetForGrupo(int id)
        {
            var avisosGenerales = serviceGeneral.GetById(id);
            return Ok(avisosGenerales);
        }

        [HttpGet("alumno/{idAlumno}")]
        public IActionResult GetForAlumno(int idAlumno)
        {
            var avisosPersonales = servicePersonal.GetByAlumno(idAlumno);
            return Ok(avisosPersonales);
        }

        [HttpPost]
        public IActionResult CrearAvisoGeneral(CrearAvisoGeneralDTO avisoGeneral)
        {
            try
            {
                serviceGeneral.Crear(avisoGeneral);
                return Ok("Aviso general creado exitosamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el aviso general: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult CrearAvisoPersonal(CrearAvisoPersonalDTO avisoPersonal)
        {
            try
            {
                servicePersonal.CrearAviso(avisoPersonal);
                return Ok("Aviso personal creado exitosamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el aviso personal: {ex.Message}");
            }
        }
    }
}
