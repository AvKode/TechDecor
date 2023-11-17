using Google.Protobuf.WellKnownTypes;
using PAKTD.Models.AC;
using PAKTD.Models.MO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PAKTD.Controllers
{
    public class CompraController : Controller
    {

        aProduto aP = new aProduto();

        public ActionResult adicionarCarrinho(int id)
        {
            mCompra carrinho = Session["Carrinho"] != null ? (mCompra)Session["Carrinho"] : new mCompra();
            var produto = aP.BuscarProdCod(id);

            if (produto != null)
            {
                var ItemsCompra = new mItensCompra
                {
                    ItemProdutoId = Guid.NewGuid(),
                    IdfkPro = id,
                    NmPro = produto[0].NmPro,
                    ImgProd = produto[0].ImgProd
                };

                // Adicione o item ao carrinho
                carrinho.ItensCompra.Add(ItemsCompra);

                Session["Carrinho"] = carrinho;
            }

            return RedirectToAction("Carrinho");
        }

        public ActionResult RemoverCarrinho(Guid id)
        {

            mCompra carrinho = Session["Carrinho"] != null ? (mCompra)Session["Carrinho"] : new mCompra();
            var itemExclusao = carrinho.ItensCompra.FirstOrDefault(i => i.ItemProdutoId == id);

            carrinho.ItensCompra.Remove(itemExclusao);

            Session["Carrinho"] = carrinho;

            return RedirectToAction("Carrinho");
        }

        public ActionResult Carrinho()
        {

              mCompra carrinho = Session["Carrinho"] != null ? (mCompra)Session["Carrinho"] : new mCompra();

            return View(carrinho);
        }

    }
}