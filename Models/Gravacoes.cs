using System;
using System.Collections.Generic;

#nullable disable

namespace AudioTel.Models
{
    public partial class Gravacoes
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public string NomeDoArquivo { get; set; }
        public string Ramal { get; set; }
    }
}
