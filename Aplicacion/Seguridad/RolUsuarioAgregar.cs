using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad;

public class RolUsuarioAgregar
{ 
    public class Ejecuta : IRequest {
        public string Username { get; set; }
        public string RolNombre { get; set; }
    }
    
    public class EjecutaValidacion : AbstractValidator<Ejecuta> {
        public EjecutaValidacion() {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.RolNombre).NotEmpty();
        }
    }
    
    public class Manejador : IRequestHandler<Ejecuta>
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public Manejador(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager) {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByNameAsync(request.RolNombre);
            if (role == null) {
                throw new Exception("El rol no existe");
            }

            var usuario = await _userManager.FindByNameAsync(request.Username);
            if (usuario == null) {
                throw new Exception("El usuario no existe");
            }

            var resultado = await _userManager.AddToRoleAsync(usuario, request.RolNombre);
            if (resultado.Succeeded) {
                return Unit.Value;
            }

            throw new Exception("No se pudo agregar el rol al usuario");
        }
    }
}