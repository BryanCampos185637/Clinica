
using System;

namespace Models
{
    public class Documento
    {
        public Guid DocumentoId { get; set; }
        public string ExtensionDocumento { get; set; }
        public byte[] ContenidoDocumento { get; set; }
        public string NombreDocumento { get; set; }
        public Guid ObjetoReferencia { get; set; }
    }
}
