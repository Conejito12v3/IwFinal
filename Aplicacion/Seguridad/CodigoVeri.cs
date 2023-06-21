using System.Net;
using System.Net.Mail;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad;

public class CodigoVeri 
{
    public class Ejecuta : IRequest {
        public string cuerpo { get; set; }
        public string correo { get; set; }
    }

    public class EjecutaValidacion : AbstractValidator<Ejecuta> {
        public EjecutaValidacion() {
            RuleFor(x => x.cuerpo).NotEmpty();
        }
    }

    public class Manejador : IRequestHandler<Ejecuta>
    {
        private readonly UserManager<Usuario> userManager;
        
        public Manejador(UserManager<Usuario> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
        {
            var usuario = await userManager.FindByEmailAsync(request.correo);

            if (usuario == null)
            {
                throw new Exception("No existe un usuario con este correo");
            }

            try
            {
                var cliente = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("iwproyectofinal@gmail.com", "pjgvxvdpcvwizyns")
                };

                var email = new MailMessage("iwproyectofinal@gmail.com", request.correo);
                email.Subject = "Codigo de verificacion";
                email.Body = request.cuerpo;
                email.IsBodyHtml = false;
                cliente.Send(email);

                cliente.Send(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw new Exception("No se pudo eliminar el rol");
            }

                return Unit.Value;
        }
    }
}