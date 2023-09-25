using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PAKTD.Models
{
    public class mCategoria
    {
        [DisplayName("Código")]
        public string IdCate { get; set; }

        //-----------------------------------------//

        [DisplayName("Categoria")]
        [Required(ErrorMessage = "Verifique se preencheu corretamente!")]
        [StringLength(20, ErrorMessage = "Máximo de caracteres é 20!")]
        public string DsCate { get; set; }
    }
    
}