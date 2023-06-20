using System;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Identity;

namespace Persistencia;

public class DataPrueba 
{
    public static async Task InsertarData(EMContext context, UserManager<Usuario> usuarioManager)
    {
        if (!usuarioManager.Users.Any())
        {
            var usuarios = new Usuario{
                NombreCompleto = "Sebastian Rivera",
                UserName = "sebas",
                Email = "sriverasc12@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };
            Console.WriteLine("Creando usuario");
            await usuarioManager.CreateAsync(usuarios, "Password123$");
        }
    }    
}