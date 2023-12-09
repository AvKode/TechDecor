using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PAKTD.Models.MO
{
    public class mAvaliacao
    {

        [DisplayName("Data da Avaliação")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/aaaa}")]
        public DateTime? DtAvali { get; set; }
        
        [DisplayName("Nota da Avaliação")]
        public int notaAvali { get; set; }

        [DisplayName("Comentário da Avaliação")]
        public string comentarioAvali { get; set; }

        [DisplayName("Código Usuário")]
        public int fkUsuário { get; set; }

        [DisplayName("Código Produto")]
        public int fkProduto { get; set; }

        [DisplayName("Nome Usuário")]
        public string nomeUsu { get; set; }

        public double mediaNota { get; set; }
    }
}