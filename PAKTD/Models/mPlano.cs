using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PAKTD.Models
{
    public class mPlano
    {
        [DisplayName("Código")]
        public string IdCate { get; set; }

        //-----------------------------------------//

        [DisplayName("Plano")]
        [Required(ErrorMessage = "Verifique se preencheu corretamente!")]
        [StringLength(50, ErrorMessage = "Máximo de caracteres é 50!")]
        public string DsPlano { get; set; }
    }
}