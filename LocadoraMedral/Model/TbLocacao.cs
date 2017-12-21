using System;
using System.Collections.Generic;

namespace LocadoraMedral.Model
{
    public partial class TbLocacao
    {
        public int TbClienteId { get; set; }
        public int TbFilmeId { get; set; }
        public DateTime DataLocacao { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public int Id { get; set; }

        public TbCliente TbCliente { get; set; }
        public TbFilme TbFilme { get; set; }
    }
}
