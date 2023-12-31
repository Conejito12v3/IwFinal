using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Seguridad;

public class RolLista
{
    public class Ejecuta : IRequest<List<IdentityRole>> { }

    public class Manejador : IRequestHandler<Ejecuta, List<IdentityRole>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public Manejador(RoleManager<IdentityRole> roleManager) {
            _roleManager = roleManager;
        }

        public async Task<List<IdentityRole>> Handle(Ejecuta request, CancellationToken cancellationToken)
        {
            return await _roleManager.Roles.ToListAsync();
        }
    }
}