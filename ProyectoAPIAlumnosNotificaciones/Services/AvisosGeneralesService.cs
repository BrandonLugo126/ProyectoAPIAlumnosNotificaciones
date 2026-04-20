using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProyectoAPIAlumnosNotificaciones.Models.DTOs;
using ProyectoAPIAlumnosNotificaciones.Models.Entities;
using ProyectoAPIAlumnosNotificaciones.Repositories;

namespace ProyectoAPIAlumnosNotificaciones.Services
{
    public class AvisoGeneralService
    {
        private readonly Repository<Avisosgenerales> _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<CrearAvisoGeneralDTO> _validadorCrear;
        private readonly IValidator<ActualizarAvisoGeneralDTO> _validadorActualizar;

        public AvisoGeneralService(
            Repository<Avisosgenerales> repository,
            IMapper mapper,
            IValidator<CrearAvisoGeneralDTO> validadorCrear,
            IValidator<ActualizarAvisoGeneralDTO> validadorActualizar)
        {
            _repository = repository;
            _mapper = mapper;
            _validadorCrear = validadorCrear;
            _validadorActualizar = validadorActualizar;
        }

        public List<AvisosGeneralesDTO> GetAll()
        {
            var avisos = _repository.Query()
                .OrderByDescending(a => a.FechaEmision)
                .ToList();

            return _mapper.Map<List<AvisosGeneralesDTO>>(avisos);
        }

        public AvisosGeneralesDTO GetById(int id)
        {
            var aviso = _repository.Get(id);
            if (aviso != null)
            {
                return _mapper.Map<AvisosGeneralesDTO>(aviso);
            }
            else
            {
                throw new KeyNotFoundException($"No se encontró el aviso con ID {id}");
            }
        }

        public List<AvisosGeneralesDTO> GetAvisosVigentes()
        {
            var avisos = _repository.Query()
                .Where(a => a.FechaCaducidad > DateTime.Now)
                .OrderByDescending(a => a.FechaEmision)
                .ToList();

            return _mapper.Map<List<AvisosGeneralesDTO>>(avisos);
        }

        public void Crear(CrearAvisoGeneralDTO dto)
        {
            var result = _validadorCrear.Validate(dto);
            if (result.IsValid)
            {
                var entity = _mapper.Map<Avisosgenerales>(dto);

                if (entity.FechaEmision == null)
                    entity.FechaEmision = DateTime.Now;

                _repository.Insert(entity);
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }

        public void Actualizar(ActualizarAvisoGeneralDTO dto)
        {
            var result = _validadorActualizar.Validate(dto);
            if (result.IsValid)
            {
                var entity = _repository.Get(dto.Id);
                if (entity == null)
                {
                    throw new KeyNotFoundException();
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
                throw new KeyNotFoundException($"No se encontró el aviso con ID {id}");
            }
        }
    }
}