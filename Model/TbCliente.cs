﻿using System.Collections.Generic;

namespace Model
{
    public class TbCliente
    {
        public TbCliente()
        {
            TbLocacao = new HashSet<TbLocacao>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Cpf { get; set; }

        public ICollection<TbLocacao> TbLocacao { get; set; }
    }
}