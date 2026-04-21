using FluentValidation;
using ProyectoAPIAlumnosNotificaciones.Mappers;
using ProyectoAPIAlumnosNotificaciones.Models.DTOs;
using ProyectoAPIAlumnosNotificaciones.Models.Entities;
using ProyectoAPIAlumnosNotificaciones.Models.Validators;
using ProyectoAPIAlumnosNotificaciones.Repositories;
using ProyectoAPIAlumnosNotificaciones.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NotificacionesApiContext>();

builder.Services.AddScoped(typeof(Repository<>), typeof(Repository<>));
builder.Services.AddScoped<MaestroService>();
builder.Services.AddScoped<AlumnoService>();
builder.Services.AddScoped<AvisoGeneralService>();
builder.Services.AddScoped<AvisoPersonalService>();
builder.Services.AddScoped<GruposService>();
builder.Services.AddAutoMapper(cfg => {
    cfg.AddProfile<AvisosGeneralesProfile>();
    cfg.AddProfile<AlumnosProfile>();
    cfg.AddProfile<AvisosPersonalesProfile>();
    cfg.AddProfile<MaestrosProfile>();
    cfg.AddProfile<GrupoProfile>();
});


builder.Services.AddScoped<IValidator<ActualizarAvisoGeneralDTO>, ActualizarAvisoGeneralValidation>();
builder.Services.AddScoped<IValidator<EditarAvisoPersonalDTO>, ActualizarAvisoPersonalValidation>();
builder.Services.AddScoped<IValidator<AgregarAlumnoDTO>, AgregarAlumnoValidation>();
builder.Services.AddScoped<IValidator<AgregarMaestroDTO>, AgregarMaestroValidation>();
builder.Services.AddScoped<IValidator<CrearAvisoGeneralDTO>, CrearAvisoGeneralValidation>();
builder.Services.AddScoped<IValidator<CrearAvisoPersonalDTO>, CrearAvisoPersonalValidation>();
builder.Services.AddScoped<IValidator<EditarAlumnoDTO>, EditarAlumnoValidation>();
builder.Services.AddScoped<IValidator<EditarMaestroDTO>, EditarMaestroValidation>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<NotificacionesApiContext>();
    db.Database.EnsureCreated();
}

app.Run();