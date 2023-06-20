using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Aplicacion.ManejadorError;
using Aplicacion.Seguridad;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;
namespace Aplicacion.Productor;

public class UsuarioActualizar 
{
    public class Ejecuta : IRequest<UsuarioData>
    {
        public Guid Id { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
    }

    public class EjecutarValidacion : AbstractValidator<Ejecuta>
    {
        public EjecutarValidacion()
        {
            RuleFor(x => x.NombreCompleto).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Username).NotEmpty();
        }
    }

    public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
    {
        private readonly EMContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly IJwtGenerador _jwtGenerador;
        private readonly IPasswordHasher<Usuario> _passwordHasher;
        
        public Manejador(EMContext context, UserManager<Usuario> userManager, IJwtGenerador jwtGenerador, IPasswordHasher<Usuario> passwordHasher)
        {
            _context = context;
            _userManager = userManager;
            _jwtGenerador = jwtGenerador;
            _passwordHasher = passwordHasher;
        }

        public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
        {
            var usuarioIden = await _userManager.FindByNameAsync(request.Username);
            if (usuarioIden == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe un usuario con este username" });
            }

            var usuarioEmail = await _context.Users.Where(x => x.Email == request.Email && x.UserName != request.Username).AnyAsync();
            if (usuarioEmail)
            {
                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Este email pertenece a otro usuario" });
            }

            usuarioIden.NombreCompleto = request.NombreCompleto;
            usuarioIden.PasswordHash = _passwordHasher.HashPassword(usuarioIden, request.Password);
            usuarioIden.Email = request.Email;

            var resultado = await _userManager.UpdateAsync(usuarioIden);
            var resultadoRoles = await _userManager.GetRolesAsync(usuarioIden);
            var listaRoles = new List<string>(resultadoRoles);

            if (resultado.Succeeded)
            {
                return new UsuarioData {
                    NombreCompleto = usuarioIden.NombreCompleto,
                    Token = _jwtGenerador.CrearToken(usuarioIden, listaRoles),
                    Email = usuarioIden.Email,
                    UserName = usuarioIden.UserName,
                    
                };
            }

            throw new Exception("No se pudo actualizar el usuario");
        }
    }
}