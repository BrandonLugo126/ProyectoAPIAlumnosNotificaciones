
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using ProyectoAPIAlumnosNotificaciones.Models.DTOs;
using ProyectoAPIAlumnosNotificaciones.Models.Entities;
using ProyectoAPIAlumnosNotificaciones.Repositories;
using ProyectoAPIAlumnosNotificaciones.Services;

public class AvisoPersonalService
{
    private readonly Repository<Avisospersonales> _repository;
    private readonly AlumnoService _alumnoService;
    private readonly MaestroService _maestroService;
    private readonly IMapper _mapper;
    private readonly IValidator<CrearAvisoPersonalDTO> _validadorCrear;
    private readonly IValidator<EditarAvisoPersonalDTO> _validadorEditar;

    public AvisoPersonalService(
        Repository<Avisospersonales> repository,
        AlumnoService alumnoService,
        MaestroService maestroService,
        IMapper mapper,
        IValidator<CrearAvisoPersonalDTO> validadorCrear,
        IValidator<EditarAvisoPersonalDTO> validadorEditar)
    {
        _repository = repository;
        _alumnoService = alumnoService;
        _maestroService = maestroService;
        _mapper = mapper;
        _validadorCrear = validadorCrear;
        _validadorEditar = validadorEditar;
    }

    public List<AvsisosPersonalesDTO> CrearAviso(CrearAvisoPersonalDTO dto)
    {
        var result = _validadorCrear.Validate(dto);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }

        try
        {
            _maestroService.GetById(dto.IdMaestro);
        }
        catch (KeyNotFoundException)
        {
            result.Errors.Add(new ValidationFailure("IdMaestro", "El maestro no existe"));
            throw new ValidationException(result.Errors);
        }

        var avisosCreados = new List<AvsisosPersonalesDTO>();
        var fechaEnvio = dto.FechaEnvio ?? DateTime.Now;

        foreach (var idAlumno in dto.IdAlumnos)
        {
            var aviso = new Avisospersonales
            {
                IdMaestro = dto.IdMaestro,
                IdAlumno = idAlumno,
                Titulo = dto.Titulo,
                Contenido = dto.Contenido,
                FechaEnvio = fechaEnvio
            };

            _repository.Insert(aviso);
            avisosCreados.Add(_mapper.Map<AvsisosPersonalesDTO>(aviso));
        }

        return avisosCreados;
    }

    public List<AvsisosPersonalesDTO> EditarAviso(EditarAvisoPersonalDTO dto)
    {
        var result = _validadorEditar.Validate(dto);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }

        var avisoOriginal = _repository.Get(dto.Id);
        if (avisoOriginal == null)
        {
            throw new KeyNotFoundException($"No se encontró el aviso personal con ID {dto.Id}");
        }

        var avisosActualizados = new List<AvsisosPersonalesDTO>();
        var fechaEnvio = dto.FechaEnvio ?? DateTime.Now;

        var alumnosActuales = _repository.Query()
            .Where(a => a.Id == dto.Id)
            .Select(a => a.IdAlumno)
            .ToList();

        var alumnosEliminar = alumnosActuales.Except(dto.IdAlumnos).ToList();

        var alumnosAgregar = dto.IdAlumnos.Except(alumnosActuales).ToList();

        if (alumnosEliminar.Any() || alumnosAgregar.Any())
        {
            foreach (var idAlumno in alumnosEliminar)
            {
                var avisoAEliminar = _repository.Query()
                    .FirstOrDefault(a => a.Id == dto.Id && a.IdAlumno == idAlumno);

                if (avisoAEliminar != null)
                {
                    _repository.Delete(avisoAEliminar.Id);
                }
            }

            foreach (var idAlumno in alumnosAgregar)
            {
                var nuevoAviso = new Avisospersonales
                {
                    IdMaestro = avisoOriginal.IdMaestro,
                    IdAlumno = idAlumno,
                    Titulo = dto.Titulo,
                    Contenido = dto.Contenido,
                    FechaEnvio = fechaEnvio
                };

                _repository.Insert(nuevoAviso);
                avisosActualizados.Add(_mapper.Map<AvsisosPersonalesDTO>(nuevoAviso));
            }

            var avisosMantenidos = _repository.GetAll()
                .Where(a => a.Id == dto.Id && dto.IdAlumnos.Contains(a.IdAlumno))
                .ToList();

            foreach (var aviso in avisosMantenidos)
            {
                aviso.Titulo = dto.Titulo;
                aviso.Contenido = dto.Contenido;
                aviso.FechaEnvio = fechaEnvio;
                _repository.Update(aviso);
                avisosActualizados.Add(_mapper.Map<AvsisosPersonalesDTO>(aviso));
            }
        }
        else
        {
            var avisos = _repository.GetAll()
                .Where(a => a.Id == dto.Id)
                .ToList();

            foreach (var aviso in avisos)
            {
                aviso.Titulo = dto.Titulo;
                aviso.Contenido = dto.Contenido;
                aviso.FechaEnvio = fechaEnvio;
                _repository.Update(aviso);
                avisosActualizados.Add(_mapper.Map<AvsisosPersonalesDTO>(aviso));
            }
        }

        return avisosActualizados;
    }

    //public void EditarAvisoUnico(EditarAvisoPersonalDTO dto)
    //{
    //    var result = _validadorEditar.Validate(dto);
    //    if (result.IsValid)
    //    {
    //        var entity = _repository.Get(dto.Id);
    //        if (entity == null)
    //        {
    //            throw new KeyNotFoundException($"No se encontró el aviso personal con ID {dto.Id}");
    //        }

    //        if (!_alumnoService.ExisteAlumno(dto.Id))
    //        {
    //            result.Errors.Add(new ValidationFailure("IdAlumno", "El alumno no existe"));
    //            throw new ValidationException(result.Errors);
    //        }

    //        _mapper.Map(dto, entity);
    //        _repository.Update(entity);
    //    }
    //    else
    //    {
    //        throw new ValidationException(result.Errors);
    //    }
    //}

    public void EliminarAvisoDeAlumno(int idAviso, int idAlumno)
    {
        var aviso = _repository.GetAll()
            .FirstOrDefault(a => a.Id == idAviso && a.IdAlumno == idAlumno);

        if (aviso != null)
        {
            _repository.Delete(aviso.Id);
        }
        else
        {
            throw new KeyNotFoundException($"No se encontró el aviso con ID {idAviso} para el alumno {idAlumno}");
        }
    }

    public void EliminarAvisosPorTitulo(int idMaestro, string titulo)
    {
        var avisos = _repository.GetAll()
            .Where(a => a.IdMaestro == idMaestro && a.Titulo == titulo)
            .ToList();

        foreach (var aviso in avisos)
        {
            _repository.Delete(aviso.Id);
        }
    }

    public List<AvsisosPersonalesDTO> GetAll()
    {
        var avisos = _repository.GetAll()
            .OrderByDescending(a => a.FechaEnvio)
            .ToList();

        return _mapper.Map<List<AvsisosPersonalesDTO>>(avisos);
    }

    public List<AvsisosPersonalesDTO> GetByAlumno(int idAlumno)
    {
        var avisos = _repository.GetAll()
            .Where(a => a.IdAlumno == idAlumno)
            .OrderByDescending(a => a.FechaEnvio)
            .ToList();

        return _mapper.Map<List<AvsisosPersonalesDTO>>(avisos);
    }

    public List<AvsisosPersonalesDTO> GetByMaestro(int idMaestro)
    {
        var avisos = _repository.GetAll()
            .Where(a => a.IdMaestro == idMaestro)
            .OrderByDescending(a => a.FechaEnvio)
            .ToList();

        return _mapper.Map<List<AvsisosPersonalesDTO>>(avisos);
    }

    public void Eliminar(int id)
    {
        var avisos = _repository.GetAll()
            .Where(a => a.Id == id)
            .ToList();

        if (avisos.Any())
        {
            foreach (var aviso in avisos)
            {
                _repository.Delete(aviso.Id);
            }
        }
        else
        {
            throw new KeyNotFoundException($"No se encontró el aviso personal con ID {id}");
        }
    }
}