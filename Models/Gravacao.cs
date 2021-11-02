using System;
using System.Collections.Generic;

#nullable disable

namespace AudioTel.Models
{
    public partial class Gravacao
    {
        public int Id { get; set; }
        public int? IdBancoCliente { get; set; }
        public string Numero { get; set; }
        public string NomeDoArquivo { get; set; }
        public int? FileSize { get; set; }
        public string FilePath { get; set; }
        public string Ramal { get; set; }

        public virtual Bancocliente IdBancoClienteNavigation { get; set; }
    }
}
