using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PAKTD.Models
{
    public class mPagamento
    {
        [DisplayName("Código")]
        public string IdPagamento { get; set; }

        //-----------------------------------------//

        [DisplayName("Tipo de Pagamento")]
        [Required(ErrorMessage = "Verifique se preencheu corretamente!")]
        [StringLength(30, ErrorMessage = "Máximo de caracteres é 30!")]
        public string DsPagamento { get; set; }
    }
}