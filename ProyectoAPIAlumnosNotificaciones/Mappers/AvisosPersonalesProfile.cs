using AutoMapper;
using ProyectoAPIAlumnosNotificaciones.Models.DTOs;
using ProyectoAPIAlumnosNotificaciones.Models.Entities;

namespace ProyectoAPIAlumnosNotificaciones.Mappers
{
    public class AvisosPersonalesProfile : Profile
    {
        public AvisosPersonalesProfile()
        {
            CreateMap<Avisospersonales, AvsisosPersonalesDTO>();
            CreateMap<CrearAvisoPersonalDTO, Avisospersonales>();
            CreateMap<EditarAvisoPersonalDTO, Avisospersonales>();
            CreateMap<Avisospersonales, EditarAvisoPersonalDTO>();
        }
    }
}
