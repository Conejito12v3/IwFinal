using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad;

public class RolNuevo
{
    public class Ejecuta : IRequest {
        public string Nombre { get; set; }
    }

    public class ValidacionEjecuta : AbstractValidator<Ejecuta> {
        public ValidacionEjecuta() {
            RuleFor(x => x.Nombre).NotEmpty();
        }
    }

    public class Manejador : IRequestHandler<Ejecuta>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public Manejador(RoleManager<IdentityRole> roleManager) {
            _roleManager = roleManager;
        }

        public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByNameAsync(request.Nombre);
            if (role != null) {
                throw new Exception("El rol ya existe");
            }

            var resultado = await _roleManager.CreateAsync(new IdentityRole(request.Nombre));
            if (resultado.Succeeded) {
                return Unit.Value;
            }

            throw new Exception("No se pudo crear el rol");
        }
    }
}