using Microsoft.AspNetCore.Http.HttpResults;
using Sodium;
using System.Threading;
using System.Diagnostics;
using System.Net;

namespace Authentification_backend.Services.Security
{
    public class SecurityServices
    {
        private static Dictionary<string, bool> _verifiedUserConnection = new Dictionary<string, bool> { { "username", false }, { "password", false } };

        public bool IsDebuggerAttached()
        {
            return Debugger.IsAttached;
        }

        public bool VerifyLogin(string username, string password)
        {
            _verifiedUserConnection["username"] = false;
            _verifiedUserConnection["password"] = false;
            if (_verifiedUserConnection.ContainsKey(username) && _verifiedUserConnection[username] == false)
            {
                if (/*username == "admin" && password == "admin"*/)
                {
                    _verifiedUserConnection[username] = true;
                    return true;
                }
            }
            
        }

        public void LogSuspiciousActivity(string message)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[ALERTE] Activité suspecte détectée !");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Message : {message}");
            Console.WriteLine($"Heure : {DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss")}");
            Console.WriteLine($"Adresse IP : {GetLocalIpAddress()}");
            Console.ResetColor();
        }

        private static string GetLocalIpAddress()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
                return "Aucune adresse IP IPv4 trouvée.";
            }
            catch (Exception ex)
            {
                return $"Erreur lors de la récupération de l'adresse IP : {ex.Message}";
            }
        }
    }
}
