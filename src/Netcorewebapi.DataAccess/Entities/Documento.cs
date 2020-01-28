namespace NetcorewebApi.DataAccess.Entities
{
    public class Documento
    {

        public int DocumentoId { get; set; }
        public string Prop { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
    }
}
