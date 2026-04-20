using AutoMapper;
using ProyectoAPIAlumnosNotificaciones.Models.DTOs;
using ProyectoAPIAlumnosNotificaciones.Models.Entities;

namespace ProyectoAPIAlumnosNotificaciones.Mappers
{
    public class AlumnosProfile: Profile
    {
        public AlumnosProfile() 
        {
            CreateMap<Alumnos, AlumnoDTO>();            
            CreateMap<AgregarAlumnoDTO, Alumnos>();
            CreateMap<AlumnoDetallesDTO, Alumnos>();
            CreateMap<EditarAlumnoDTO, Alumnos>();

        }
    }
}
