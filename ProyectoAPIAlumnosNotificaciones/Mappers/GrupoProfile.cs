using AutoMapper;
using ProyectoAPIAlumnosNotificaciones.Models.DTOs;
using ProyectoAPIAlumnosNotificaciones.Models.Entities;

namespace ProyectoAPIAlumnosNotificaciones.Mappers
{
    public class GrupoProfile : Profile
    {
        public GrupoProfile()
        {
            CreateMap<Grupos, GrupoDTO>();
            CreateMap<AgregarGrupoDTO, Grupos>();
            CreateMap<Grupos, EditarGrupoDTO>();
            CreateMap<EditarGrupoDTO, Grupos>();
        }
    }
}
