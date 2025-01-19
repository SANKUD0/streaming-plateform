using Authentification_backend.Models;
using Authentification_backend.Services.Security;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Authentification_backend.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [EnableCors("CorsPolicy")]
    public class LoginController : ControllerBase
    {
        private readonly SecurityServices _securityServices;

        public LoginController(SecurityServices securityServices)
        {
            _securityServices = securityServices;
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            // Chargement des données de l'utilisateur de la base de données
            // Vérification du mot de passe
            // Génération du token
            // Retourne le token
            if (loginModel != null)
            {
                if(_securityServices.IsDebuggerAttached())
                {
                    _securityServices.LogSuspiciousActivity("Débogueur détecté pendant la tentative de connexion.");
                    Environment.Exit(1);
                }
                if (_securityServices.VerifyLogin(loginModel.Username, loginModel.Password))
                {
                    Console.WriteLine($"Connexion réussie {loginModel.Username} ({DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss")})");
                    return Ok();
                }
            }
            return Unauthorized();
        }
    }
}
