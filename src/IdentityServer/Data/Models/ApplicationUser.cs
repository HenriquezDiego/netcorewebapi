
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}