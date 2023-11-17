using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PAKTD.Models.MO
{
    public class mItensCompra
    {

        public Guid ItemProdutoId { get; set; }

        public int IdIco { get; set; }

        //-----------------------------------------//

        public int IdfkPro { get; set; }

        //-----------------------------------------//

        public string IdfkCom { get; set; }

      

        [DisplayName("Produto")]
        [Required(ErrorMessage = "Verifique se preencheu corretamente!")]
        [StringLength(40, ErrorMessage = "Máximo de caracteres é 40!")]
        public string NmPro { get; set; }

        [StringLength(254, ErrorMessage = "Máximo de caracteres é 254!")]
        public string ImgProd { get; set; }

        public int QuantidadeVenda { get; set; }
     
        public int ValorTotal { get; set; }
    }
}