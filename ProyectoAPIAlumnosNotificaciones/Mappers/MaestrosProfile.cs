using AutoMapper;
using ProyectoAPIAlumnosNotificaciones.Models.DTOs;
using ProyectoAPIAlumnosNotificaciones.Models.Entities;

namespace ProyectoAPIAlumnosNotificaciones.Mappers
{
    public class MaestrosProfile : Profile
    {
        public MaestrosProfile()
        {
            CreateMap<Maestros, MaestroDTO>();
            CreateMap<AgregarMaestroDTO, Maestros>();
            CreateMap<EditarMaestroDTO, Maestros>();
            CreateMap<Maestros, EditarMaestroDTO>();
        }
    }
}
