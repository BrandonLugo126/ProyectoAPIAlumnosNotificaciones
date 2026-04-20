// Services/MaestroService.cs
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProyectoAPIAlumnosNotificaciones.Models.DTOs;
using ProyectoAPIAlumnosNotificaciones.Models.Entities;
using ProyectoAPIAlumnosNotificaciones.Repositories;

namespace ProyectoAPIAlumnosNotificaciones.Services
{
    public class MaestroService
    {
        private readonly Repository<Maestros> _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<AgregarMaestroDTO> _validadorAgregar;
        private readonly IValidator<EditarMaestroDTO> _validadorEditar;

        public MaestroService(
            Repository<Maestros> repository,
            IMapper mapper,
            IValidator<AgregarMaestroDTO> validadorAgregar,
            IValidator<EditarMaestroDTO> validadorEditar)
        {
            _repository = repository;
            _mapper = mapper;
            _validadorAgregar = validadorAgregar;
            _validadorEditar = validadorEditar;
        }

        public List<MaestroDTO> GetAll()
        {
            var maestros = _repository.GetAll();
            return _mapper.Map<List<MaestroDTO>>(maestros);
        }

        public MaestroDTO GetById(int id)
        {
            var maestro = _repository.Get(id);
            if (maestro != null)
            {
                return _mapper.Map<MaestroDTO>(maestro);
            }
            else
            {
                throw new KeyNotFoundException($"No se encontró el maestro con ID {id}");
            }
        }

        public MaestroDTO GetByClave(string clave)
        {
            var maestro = _repository.Query()
                .FirstOrDefault(m => m.Clave == clave);

            return maestro != null ? _mapper.Map<MaestroDTO>(maestro) : null;
        }

        public void Agregar(AgregarMaestroDTO dto)
        {
            var result = _validadorAgregar.Validate(dto);
            if (result.IsValid)
            {
                var entity = _mapper.Map<Maestros>(dto);
                _repository.Insert(entity);
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }

        public void Editar(EditarMaestroDTO dto)
        {
            var result = _validadorEditar.Validate(dto);
            if (result.IsValid)
            {
                var entity = _repository.Get(dto.Id);
                if (entity == null)
                {
                    throw new KeyNotFoundException($"No se encontró el maestro con ID {dto.Id}");
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
                throw new KeyNotFoundException();
            }
        }
    }
}