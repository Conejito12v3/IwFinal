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

namespace Aplicacion.Seguridad;

public class CambiarPassword 
{
    public class Ejecuta : IRequest<Unit>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class EjecutaValidacion : AbstractValidator<Ejecuta>
    {
        public EjecutaValidacion()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }

    public class Manejador : IRequestHandler<Ejecuta>
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

        public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
        {
            var usuarioIden = await _userManager.FindByEmailAsync(request.Email);
            if (usuarioIden == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No existe un usuario con este username" });
            }

            usuarioIden.PasswordHash = _passwordHasher.HashPassword(usuarioIden, request.Password);

            var resultado = await _userManager.UpdateAsync(usuarioIden);

            if (resultado.Succeeded)
            {
                return Unit.Value;
            }

            throw new Exception("No se pudo actualizar el usuario");
        }
    }
}