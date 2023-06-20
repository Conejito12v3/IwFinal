using Aplicacion.Contratos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad;

public class UsuarioActual
{
    public class Ejecuta : IRequest<UsuarioData> { }

    public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IJwtGenerador _jwtGenerador;
        private readonly IUsuarioSesion _usuarioSesion;
        public Manejador(UserManager<Usuario> userManager, IJwtGenerador jwtGenerador, IUsuarioSesion usuarioSesion)
        {
            _userManager = userManager;
            _jwtGenerador = jwtGenerador;
            _usuarioSesion = usuarioSesion;
        }

        public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
        {
            var usuario = await _userManager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion());

            var resultadoRoles = await _userManager.GetRolesAsync(usuario);
            var listaRoles = new List<string>(resultadoRoles);

            foreach(var rol in listaRoles)
            {
                Console.WriteLine("Rol: " + rol);
            }

            return new UsuarioData {
                NombreCompleto = usuario.NombreCompleto,
                UserName = usuario.UserName,
                Token = _jwtGenerador.CrearToken(usuario, listaRoles),
                Email = usuario.Email,
                Roles = listaRoles
            };
        }
    }
}