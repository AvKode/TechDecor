using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PAKTD.Models
{
    public class mUsuario
    {
        [DisplayName("Código do Usuário")]
        public int IdUsu { get; set; }

        //----------------------------------//

        [DisplayName("Usuário")]
        [Required(ErrorMessage = "Verifique se preencheu corretamente!")]
        [StringLength(30, ErrorMessage = "Máximo de caracteres é 30!")]
        public string NomeUsu { get; set; }

        //----------------------------------//

        [DisplayName("Senha")]
        [Required(ErrorMessage = "Verifique se preencheu corretamente!")]
        [StringLength(20, ErrorMessage = "Máximo de caracteres é 20!")]
        public string SenhaUsu { get; set; }

        //----------------------------------//

        [DisplayName("Tipo de Usuário")]
        public string TipoUsu { get; set; }

        //----------------------------------//

        [DisplayName("Confirmar Senha")]
        [Compare("senhaUsu", ErrorMessage = "As senhas não são iguais!")]
        public string ConfsenhaUsu { get; set; }
    }
}