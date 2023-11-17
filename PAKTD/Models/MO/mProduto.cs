using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PAKTD.Models.MO
{
    public class mProduto
    {
        [DisplayName("Código do Produto")]
        public int IdPro { get; set; }

        //-----------------------------------------//

        [DisplayName("Categoria")]
        public string IdfkCate { get; set; }

        //-----------------------------------------//

        [DisplayName("Produto")]
        [Required(ErrorMessage = "Verifique se preencheu corretamente!")]
        [StringLength(40, ErrorMessage = "Máximo de caracteres é 40!")]
        public string NmPro { get; set; }

        //-----------------------------------------//

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "Verifique se preencheu corretamente!")]
        [StringLength(150, ErrorMessage = "Máximo de caracteres é 150!")]
        [DataType(DataType.MultilineText)]
        public string DsPro { get; set; }

        //-----------------------------------------//

        [DisplayName("Valor")]
        [Required(ErrorMessage = "Verifique se preencheu corretamente!")]
        [Range(0, 99999999, ErrorMessage = "Limite máximo de transferência!")]
        [DataType(DataType.Currency)]
        public double VlPro { get; set; }

        //-----------------------------------------//

        [DisplayName("Status")]
        [DataType(DataType.Text)]
        public bool StaPro { get; set; }

        //-----------------------------------------//

        [StringLength(254, ErrorMessage = "Máximo de caracteres é 254!")]
        public string ImgProd{ get; set; }



    }
}