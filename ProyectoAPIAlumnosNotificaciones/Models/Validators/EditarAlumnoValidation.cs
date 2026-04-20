using FluentValidation;
using ProyectoAPIAlumnosNotificaciones.Models.DTOs;

namespace ProyectoAPIAlumnosNotificaciones.Models.Validators
{
    public class EditarAlumnoValidation:AbstractValidator<EditarAlumnoDTO>
    {
        public EditarAlumnoValidation()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El ID del alumno es requerido.");

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre del alumno es requerido.")
                .MaximumLength(100).WithMessage("El nombre del alumno no puede tener más de 100 caracteres.")
                .MinimumLength(3).WithMessage("El nombre del alumno debe tener al menos 3 caracteres.");

            RuleFor(x => x.IdGrupo)
                .GreaterThan(0).WithMessage("Debe indicar el grupo al que pertenece el alumno.");
        }
    }
}
