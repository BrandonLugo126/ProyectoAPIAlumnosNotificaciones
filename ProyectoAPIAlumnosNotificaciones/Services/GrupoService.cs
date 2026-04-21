// Services/GruposService.cs
using AutoMapper;
using FluentValidation;
using ProyectoAPIAlumnosNotificaciones.Models.DTOs;
using ProyectoAPIAlumnosNotificaciones.Models.Entities;
using ProyectoAPIAlumnosNotificaciones.Repositories;

namespace ProyectoAPIAlumnosNotificaciones.Services
{
    public class GruposService
    {
        private readonly Repository<Grupos> _repository;
        private readonly IMapper _mapper;

        public GruposService(Repository<Grupos> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public GrupoDTO CrearGrupo(AgregarGrupoDTO dto)
        {
            var grupo = _mapper.Map<Grupos>(dto);
            _repository.Insert(grupo);
            return _mapper.Map<GrupoDTO>(grupo);
        }
        public List<GrupoDTO> GetAll()
        {
            var grupos = _repository.GetAll();
            return _mapper.Map<List<GrupoDTO>>(grupos);
        }

        public GrupoDTO GetById(int id)
        {
            var grupo = _repository.Get(id);
            return grupo != null ? _mapper.Map<GrupoDTO>(grupo) : null;
        }

        public bool ExisteGrupo(int id)
        {
            return _repository.Get(id) != null;
        }
    }
}