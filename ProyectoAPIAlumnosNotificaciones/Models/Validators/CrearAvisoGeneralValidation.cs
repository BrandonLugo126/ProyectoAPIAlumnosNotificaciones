using FluentValidation;
using ProyectoAPIAlumnosNotificaciones.Models.DTOs;

namespace ProyectoAPIAlumnosNotificaciones.Models.Validators
{
    public class CrearAvisoGeneralValidation : AbstractValidator<CrearAvisoGeneralDTO>
    {
        public CrearAvisoGeneralValidation()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("El título del aviso es requerido.")
                .MaximumLength(200).WithMessage("El título no puede tener más de 200 caracteres.")
                .MinimumLength(5).WithMessage("El título debe tener al menos 5 caracteres.");

            RuleFor(x => x.Contenido)
                .NotEmpty().WithMessage("El contenido del aviso es requerido.")
                .MaximumLength(2000).WithMessage("El contenido no puede tener más de 2000 caracteres.")
                .MinimumLength(10).WithMessage("El contenido debe tener al menos 10 caracteres.");

            RuleFor(x => x.FechaCaducidad)
                .GreaterThan(DateTime.Now).WithMessage("La fecha de caducidad debe ser futura.")
                .Must((dto, fechaCaducidad) =>
                    !dto.FechaEmision.HasValue || fechaCaducidad > dto.FechaEmision.Value)
                .WithMessage("La fecha de caducidad debe ser posterior a la fecha de emisión.");

            RuleFor(x => x.FechaEmision)
                .Must(fecha => !fecha.HasValue || fecha.Value <= DateTime.Now)
                .WithMessage("La fecha de emisión no puede ser futura.");
        }
    }
}
