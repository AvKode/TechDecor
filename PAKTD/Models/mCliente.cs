using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PAKTD.Models
{
    public class mCliente
    {
        [DisplayName("Código do Cliente")]
        public int IdCli { get; set; }

        //-----------------------------------------//

        [DisplayName("Código do Usuário")]
        public int IdfkUsu { get; set; }

        //-----------------------------------------//

        [DisplayName("Nome")]
        [Required(ErrorMessage = "Verifique se preencheu corretamente!")]
        [StringLength(80, ErrorMessage = "Máximo de caracteres é 80!")]
        public string NmCli { get; set; }

        //-----------------------------------------//

        [DisplayName("Data de Nascimento")]
        [Required(ErrorMessage = "Verifique se preencheu corretamente!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/aaaa}")]
        public DateTime? DtNascCli { get; set; }

        //-----------------------------------------//

        [DisplayName("E-mail")]
        [Required(ErrorMessage = "Verifique se preencheu corretamente!")]
        [StringLength(80, ErrorMessage = "Máximo de caracteres é 80!")]
        public string EmailCli { get; set; }

        //-----------------------------------------//

        [DisplayName("Telefone")]
        [Required(ErrorMessage = "Verifique se preencheu corretamente!")]
        [StringLength(11, MinimumLength = 9, ErrorMessage = "Verifique se preencheu corretamente!")]
        public string FoneCli { get; set; }

        //-----------------------------------------//

        [DisplayName("CPF")]
        [Required(ErrorMessage = "Verifique se preencheu corretamente!")]
        [StringLength(11, ErrorMessage = "Máximo de caracteres é 11!")]
        public string CpfCli { get; set; }

        //-----------------------------------------//

        [DisplayName("CEP")]
        [StringLength(8, ErrorMessage = "Máximo de caracteres é 8!")]
        public string CepCli { get; set; }
    }
}