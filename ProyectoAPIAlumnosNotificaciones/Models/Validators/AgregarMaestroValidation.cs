using FluentValidation;
using ProyectoAPIAlumnosNotificaciones.Models.DTOs;
using ProyectoAPIAlumnosNotificaciones.Models.Entities;
using ProyectoAPIAlumnosNotificaciones.Repositories;

namespace ProyectoAPIAlumnosNotificaciones.Models.Validators
{
    public class AgregarMaestroValidation: AbstractValidator<AgregarMaestroDTO>
    {
        private readonly Repository<Maestros> repository;

        public AgregarMaestroValidation(Repository<Maestros> repository)
        {
            this.repository = repository;

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre del maestro es requerido.")
                .MaximumLength(100).WithMessage("El nombre del maestro no puede tener más de 100 caracteres.")
                .MinimumLength(3).WithMessage("El nombre del maestro debe tener al menos 3 caracteres.");

            RuleFor(x => x.Correo)
                .EmailAddress().WithMessage("Debe proporcionar un correo electrónico válido.")
                .MaximumLength(150).WithMessage("El correo no puede tener más de 150 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.Correo));

           

            RuleFor(x => x.Contraseña)
                .NotEmpty().WithMessage("La contraseña es requerida.")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.")
                .MaximumLength(50).WithMessage("La contraseña no puede tener más de 50 caracteres.");
        }
       
    }
}
