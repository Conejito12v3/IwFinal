using System.Net;
using Aplicacion.Contratos;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Seguridad;

public class Registrar
{
    public class Ejecuta : IRequest<UsuarioData>
    {
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? UserName { get; set; }
    }

    public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
    {
        private readonly EMContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly IJwtGenerador _jwtGenerador;
        public Manejador(EMContext context, UserManager<Usuario> userManager, IJwtGenerador jwtGenerador)
        {
            _context = context;
            _userManager = userManager;
            _jwtGenerador = jwtGenerador;
        }

        public class EjecutaValidador : AbstractValidator<Ejecuta>
        {
            public EjecutaValidador()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.UserName).NotEmpty();
            }
        }

        public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
        {
            var existe = await _context.Users.Where(x => x.Email == request.Email).AnyAsync();
            if(existe)
            {
                throw new Exception("El email ingresado ya existe");
            }

            var existeUserName = await _context.Users.Where(x => x.UserName == request.UserName).AnyAsync();
            if(existeUserName)
            {
                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "El username ingresado ya existe" });
            }

            var usuario = new Usuario {
                NombreCompleto = request.Nombre,
                Email = request.Email,
                UserName = request.UserName
            };

            var result = await _userManager.CreateAsync(usuario, request.Password);
            var result2 = await _userManager.AddToRoleAsync(usuario, "Visitante");
            if (result.Succeeded && result2.Succeeded)
            {
                return new UsuarioData {
                    NombreCompleto = usuario.NombreCompleto,
                    Token = _jwtGenerador.CrearToken(usuario, null),
                    UserName = usuario.UserName,
                    Email = usuario.Email
                };
            }

            throw new Exception("No se pudo agregar al nuevo usuario");
        }
    }
}