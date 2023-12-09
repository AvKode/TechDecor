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
        aPlano aPL = new aPlano();
        public ActionResult Index()
        {
            mCompra carrinho = Session["Carrinho"] != null ? (mCompra)Session["Carrinho"] : new mCompra();
            List<vmCompraItensProd> listaDeProdutos = aP.BuscarProd();
            List<mPlano> listaPlanos = aPL.BuscarPlanos();
            List <vmCompraItensProd> listaDeProdutosECarrinho = listaDeProdutos
              .Select(p => new vmCompraItensProd { mP = p.mP, Carrinho = carrinho,mPL = listaPlanos })
              .ToList();


            double valorTotal = carrinho.ItensCompra.Sum(item => item.QuantidadeVenda * item.ValorProd);

            carrinho.VlCom = valorTotal;

            return View(listaDeProdutosECarrinho);
        }






        public ActionResult BuscaProduto()
        {


            return View();
        }

        [HttpGet]
        public ActionResult BuscaProduto(string txtPesquisa)
        {


            return View(aP.PesquisaProduto(txtPesquisa));
        }
       
    }  
}