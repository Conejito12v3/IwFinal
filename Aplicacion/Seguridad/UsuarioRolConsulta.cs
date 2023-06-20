using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad;

public class UsuarioRolConsulta
{
    public class Ejecuta : IRequest<List<string>> 
    {
        public string UserName { get; set; }
    }

    public class Manejador : IRequestHandler<Ejecuta, List<string>>
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public Manejador(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager) 
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<List<string>> Handle(Ejecuta request, CancellationToken cancellationToken)
        {
            var usuario = await _userManager.FindByNameAsync(request.UserName);
            if (usuario == null) 
            {
                throw new Exception("El usuario no existe");
            }

            var resultado = await _userManager.GetRolesAsync(usuario);
            return new List<string>(resultado);
        }
    }
}