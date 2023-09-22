using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PAKTD.Models
{
    public class mCompra
    {
        [DisplayName("Código da Compra")]
        public int IdCom { get; set; }

        //-----------------------------------------//

        [DisplayName("Cliente")]
        public string IdfkCli { get; set; }

        //-----------------------------------------//

        [DisplayName("Tipo de Pagamento")]
        public string IdfkPag { get; set; }

        //-----------------------------------------//

        [DisplayName("Valor Total")]
        [Required(ErrorMessage = "Verifique se preencheu corretamente!")]
        [Range(0, 9999, ErrorMessage = "Limite máximo de transferência!")]
        public double VlCom { get; set; }

        //-----------------------------------------//

        [DisplayName("Data da compra")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/aaaa}")]
        public DateTime? DtCom { get; set; }

        //-----------------------------------------//

        [DisplayName("Prazo do Serviço")]
        [Required(ErrorMessage = "Verifique se preencheu corretamente!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/aaaa}")]
        public DateTime? DtPrazo { get; set; }
    }
}