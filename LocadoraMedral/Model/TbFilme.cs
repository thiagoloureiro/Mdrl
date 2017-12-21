using System;
using System.Collections.Generic;

namespace LocadoraMedral.Model
{
    public partial class TbFilme
    {
        public TbFilme()
        {
            TbLocacao = new HashSet<TbLocacao>();
        }

        public int Id { get; set; }
        public string Filme { get; set; }
        public string Genero { get; set; }

        public ICollection<TbLocacao> TbLocacao { get; set; }
    }
}
