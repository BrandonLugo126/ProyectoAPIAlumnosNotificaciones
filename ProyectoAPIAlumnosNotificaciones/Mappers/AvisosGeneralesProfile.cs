using AutoMapper;
using ProyectoAPIAlumnosNotificaciones.Models.DTOs;
using ProyectoAPIAlumnosNotificaciones.Models.Entities;

namespace ProyectoAPIAlumnosNotificaciones.Mappers
{
    public class AvisosGeneralesProfile : Profile
    {
        public AvisosGeneralesProfile()
        {
            CreateMap<Avisosgenerales, AvisosGeneralesDTO>();
            CreateMap<ActualizarAvisoGeneralDTO, Avisosgenerales>();
            CreateMap<CrearAvisoGeneralDTO, Avisosgenerales>();
            CreateMap<Avisosgenerales, ActualizarAvisoGeneralDTO>();


        }
    }
}
