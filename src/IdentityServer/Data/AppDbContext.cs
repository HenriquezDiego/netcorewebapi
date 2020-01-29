using IdentityServer.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Modulo> Modulos { get; set; }
        public DbSet<UsuarioPermiso> UsuarioPermisos { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {

        }

        public AppDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}