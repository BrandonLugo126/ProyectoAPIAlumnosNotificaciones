using FluentValidation;
using ProyectoAPIAlumnosNotificaciones.Models.DTOs;
using ProyectoAPIAlumnosNotificaciones.Models.Entities;
using ProyectoAPIAlumnosNotificaciones.Repositories;

namespace ProyectoAPIAlumnosNotificaciones.Models.Validators
{
    public class AgregarAlumnoValidation : AbstractValidator<AgregarAlumnoDTO>
    {
        private readonly Repository<Alumnos> repository;

        public AgregarAlumnoValidation(Repository<Alumnos> repository)
        {
            this.repository = repository;

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre del alumno es requerido.")
                .MaximumLength(100).WithMessage("El nombre del alumno no puede tener más de 100 caracteres.")
                .MinimumLength(3).WithMessage("El nombre del alumno debe tener al menos 3 caracteres.");

            RuleFor(x => x.Contraseña)
                .NotEmpty().WithMessage("La contraseña es requerida.")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.")
                .MaximumLength(50).WithMessage("La contraseña no puede tener más de 50 caracteres.");

            RuleFor(x => x.IdGrupo)
                .GreaterThan(0).WithMessage("Debe indicar el grupo al que pertenece el alumno.");
        }
    }
}
