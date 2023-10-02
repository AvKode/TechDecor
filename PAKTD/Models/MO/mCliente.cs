using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PAKTD.Models.MO
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
        [StringLength(14, MinimumLength = 10, ErrorMessage = "Verifique se preencheu corretamente!")]
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


        //-----------------------------------------//

        [DisplayName("Número da Casa")]
        [StringLength(5, ErrorMessage = "Máximo de caracteres é 5!")]
        public string NoCasa { get; set; }

        //-----------------------------------------//

        [DisplayName("Complemento")]
        [StringLength(60, ErrorMessage = "Máximo de caracteres é 60!")]
        public string ComCasa { get; set; }
    }
}