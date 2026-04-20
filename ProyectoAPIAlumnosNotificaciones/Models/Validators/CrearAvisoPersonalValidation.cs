using FluentValidation;
using ProyectoAPIAlumnosNotificaciones.Models.DTOs;
using ProyectoAPIAlumnosNotificaciones.Models.Entities;
using ProyectoAPIAlumnosNotificaciones.Repositories;

public class CrearAvisoPersonalValidation : AbstractValidator<CrearAvisoPersonalDTO>
{
    private readonly Repository<Maestros> _maestroRepository;
    private readonly Repository<Alumnos> _alumnoRepository;

    public CrearAvisoPersonalValidation(
        Repository<Maestros> maestroRepository,
        Repository<Alumnos> alumnoRepository)
    {
        _maestroRepository = maestroRepository;
        _alumnoRepository = alumnoRepository;

        RuleFor(x => x.IdMaestro)
            .GreaterThan(0).WithMessage("El ID del maestro es requerido.")
            .Must(IdMaestroExiste).WithMessage("El maestro especificado no existe.");

        RuleFor(x => x.IdAlumnos)
            .NotNull().WithMessage("Debe especificar al menos un alumno.")
            .Must(ids => ids != null && ids.Count > 0).WithMessage("Debe especificar al menos un alumno.")
            .Must(ids => ids != null && ids.Distinct().Count() == ids.Count).WithMessage("No puede haber IDs de alumnos duplicados.")
            .Must(IdsAlumnosExisten).WithMessage("Uno o más alumnos no existen.");

        RuleFor(x => x.Titulo)
            .NotEmpty().WithMessage("El título del aviso es requerido.")
            .MaximumLength(200).WithMessage("El título no puede tener más de 200 caracteres.")
            .MinimumLength(5).WithMessage("El título debe tener al menos 5 caracteres.");

        RuleFor(x => x.Contenido)
            .NotEmpty().WithMessage("El contenido del aviso es requerido.")
            .MaximumLength(2000).WithMessage("El contenido no puede tener más de 2000 caracteres.")
            .MinimumLength(10).WithMessage("El contenido debe tener al menos 10 caracteres.");

        RuleFor(x => x.FechaEnvio)
            .Must(fecha => !fecha.HasValue || fecha.Value <= DateTime.Now)
            .WithMessage("La fecha de envío no puede ser futura.");
    }

    private bool IdMaestroExiste(int idMaestro)
    {
        return _maestroRepository.GetAll().Any(m => m.Id == idMaestro);
    }

    private bool IdsAlumnosExisten(List<int> idsAlumnos)
    {
        if (idsAlumnos == null || !idsAlumnos.Any())
            return false;

        var alumnosExistentes = _alumnoRepository.GetAll()
            .Where(a => idsAlumnos.Contains(a.Id))
            .Select(a => a.Id)
            .ToList();

        return alumnosExistentes.Count == idsAlumnos.Count;
    }
}