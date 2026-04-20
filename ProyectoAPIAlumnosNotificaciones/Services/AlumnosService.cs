// Services/AlumnoService.cs
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProyectoAPIAlumnosNotificaciones.Models.DTOs;
using ProyectoAPIAlumnosNotificaciones.Models.Entities;
using ProyectoAPIAlumnosNotificaciones.Repositories;

namespace ProyectoAPIAlumnosNotificaciones.Services
{
    public class AlumnoService
    {
        private readonly Repository<Alumnos> _repository;
        private readonly GruposService _gruposService;
        private readonly IMapper _mapper;
        private readonly IValidator<AgregarAlumnoDTO> _validadorAgregar;
        private readonly IValidator<EditarAlumnoDTO> _validadorEditar;

        public AlumnoService(
            Repository<Alumnos> repository,
            IMapper mapper,
            IValidator<AgregarAlumnoDTO> validadorAgregar,
            IValidator<EditarAlumnoDTO> validadorEditar)
        {
            _repository = repository;
            _mapper = mapper;
            _validadorAgregar = validadorAgregar;
            _validadorEditar = validadorEditar;
        }

        public List<AlumnoDTO> GetAll()
        {
            var alumnos = _repository.GetAll();
            return _mapper.Map<List<AlumnoDTO>>(alumnos);
        }

        public AlumnoDetallesDTO GetById(int id)
        {
            var alumno = _repository.Query()
                .Include(a => a.Avisospersonales)
                .Include(a => a.IdGrupoNavigation)
                .FirstOrDefault(a => a.Id == id);

            if (alumno != null)
            {
                return _mapper.Map<AlumnoDetallesDTO>(alumno);
            }
            else
            {
                throw new KeyNotFoundException($"No se encontró el alumno con ID {id}");
            }
        }

        public List<AlumnoDTO> GetByGrupo(int idGrupo)
        {
            var alumnos = _repository.Query()
                .Where(a => a.IdGrupo == idGrupo)
                .ToList();

            return _mapper.Map<List<AlumnoDTO>>(alumnos);
        }

        public void Agregar(AgregarAlumnoDTO dto)
        {
            var result = _validadorAgregar.Validate(dto);
            if (result.IsValid)
            {
                if (_gruposService.GetById(dto.IdGrupo) == null)
                {
                    result.Errors.Add(new FluentValidation.Results.ValidationFailure("IdGrupo", "El grupo no existe"));
                    throw new ValidationException(result.Errors);
                }

                var entity = _mapper.Map<Alumnos>(dto);
                _repository.Insert(entity);
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }

        public void Editar(EditarAlumnoDTO dto)
        {
            var result = _validadorEditar.Validate(dto);
            if (result.IsValid)
            {
                var entity = _repository.Get(dto.Id);
                if (entity == null)
                {
                    throw new KeyNotFoundException($"No se encontró el alumno con ID {dto.Id}");
                }

                _mapper.Map(dto, entity);
                _repository.Update(entity);
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }

        public void Eliminar(int id)
        {
            var entity = _repository.Get(id);
            if (entity != null)
            {
                _repository.Delete(id);
            }
            else
            {
                throw new KeyNotFoundException($"No se encontró el alumno con ID {id}");
            }
        }

        public bool ExisteAlumno(int id)
        {
            return _repository.Get(id) != null;
        }
    }
}