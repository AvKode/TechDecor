using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PAKTD.Models.MO
{
    public class mPlano
    {
        [DisplayName("Código")]
        public string IdCate { get; set; }

        //-----------------------------------------//

        [DisplayName("Plano")]
        [Required(ErrorMessage = "Verifique se preencheu corretamente!")]
        [StringLength(30, ErrorMessage = "Máximo de caracteres é 30!")]
        public string NmPlano { get; set; }

        //-----------------------------------------//

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "Verifique se preencheu corretamente!")]
        [StringLength(100, ErrorMessage = "Máximo de caracteres é 100!")]
        public string DsPlano { get; set; }

        //-----------------------------------------//

        [StringLength(254, ErrorMessage = "Máximo de caracteres é 254!")]
        public string ImgPlano { get; set; }

        [DisplayName("Código do Produto")]
        public int IdPro { get; set; }

        [DisplayName("Código do Plano")]
        public int IdPlano { get; set; }

        [DisplayName("Produto")]
        [Required(ErrorMessage = "Verifique se preencheu corretamente!")]
        [StringLength(40, ErrorMessage = "Máximo de caracteres é 40!")]
        public string NmPro { get; set; }

    }
}