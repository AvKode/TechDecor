using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PAKTD.Models.MO
{
    public class vmCompraItensProd
    {

        public mProduto mP { get; set; }
        public mCompra mC { get; set; }
        public mCompra Carrinho { get; set; }
    }
}