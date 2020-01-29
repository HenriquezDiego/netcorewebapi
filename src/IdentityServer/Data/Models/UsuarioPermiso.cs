namespace IdentityServer.Data.Models
{
    public class UsuarioPermiso
    {
        public int UsuarioPermisoId { get; set; }
        public bool Ver { get; set; }
        public bool Editar { get; set; }
        public bool Eliminar { get; set; }
        public int ModuloId { get; set; }
        public Modulo Modulo { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
