using PAKTD.Models.AC;
using PAKTD.Models.MO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PAKTD.Controllers
{
    public class HomeController : Controller
    {

        aProduto aP = new aProduto();
        public ActionResult Index()
        {
            mCompra carrinho = Session["Carrinho"] != null ? (mCompra)Session["Carrinho"] : new mCompra();

        
          

            return View(aP.BuscarProd());
          
        }


        public ActionResult BuscaProduto()
        {


            return View();
        }

        [HttpPost]
        public ActionResult BuscaProduto(string txtPesquisa)
        {


            return View(aP.PesquisaProduto(txtPesquisa));
        }

    }  
}