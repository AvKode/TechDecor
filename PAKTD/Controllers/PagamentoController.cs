using PAKTD.Models.AC;
using PAKTD.Models.MO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace PAKTD.Controllers
{
    public class PagamentoController : Controller
    {
        aPagamento aP = new aPagamento();
        aProduto aProd = new aProduto();
        public ActionResult listarPagamento()
        {
            return View(aP.BuscarPag());
        }
        public ActionResult cadastrarPagamento()
        {
            return View();
        }

        [HttpPost]
        public ActionResult cadastrarPagamento(mPagamento mP)
        {

            if (ModelState.IsValid)
            {

                aP.InserirPag(mP);

            }

            return RedirectToAction("listarPagamento", "Pagamento");
        }

        public ActionResult GraficosVenda()
        {

            return View();
        }

        public ActionResult adicionarCarrinho(int id)
        {
            // instanciando a classe mCompra e jogando para a session do carrinho
            mCompra carrinho = Session["Carrinho"] != null ? (mCompra)Session["Carrinho"] : new mCompra();

         
            // buscando id do produto que será adicionado no carrinho
            var produto = aProd.BuscarProdCod(id);

           
            if (produto != null) // se produto diferente de nulo
            {
                var ItemsCompra = new mItensCompra // cria uma instância da classe mItensCompra
                {
                    ItemProdutoId = Guid.NewGuid(), 
                    IdfkPro = id, 
                    NmPro = produto[0].NmPro,
                    ImgProd = produto[0].ImgProd,
                    QuantidadeVenda = 1, // ou qualquer valor adequado para a quantidade
                    ValorProd = produto[0].VlPro // ou qualquer valor adequado para o valor do produto
                };

                // Procurando pelo produto no carrinho
                List<mItensCompra> x = carrinho.ItensCompra.FindAll(l => l.NmPro == ItemsCompra.NmPro);

                // verificando se o produto já está no carrinho
                if (x.Count != 0)
                {
                    return Content("<script language='JavaScript' type='text/javascript'>alert('Este produto já está no carrinho');location.href='/Home/Index';</script> ");

                }
                else 
                {
                    // Adicionando o item ao carrinho
                    carrinho.ItensCompra.Add(ItemsCompra);

                }

                Session["Carrinho"] = carrinho;
            }

            return RedirectToAction("Carrinho");
        }

        public ActionResult Carrinho()
        {
            mCompra carrinho = Session["Carrinho"] != null ? (mCompra)Session["Carrinho"] : new mCompra();

            // calculando o valor total do carrinho
            double valorTotal = carrinho.ItensCompra.Sum(item => item.QuantidadeVenda * item.ValorProd);

            carrinho.VlCom = valorTotal;

            ViewBag.ValorTotal = valorTotal;
            return View(carrinho);
        }

        public ActionResult RemoverCarrinho(Guid id)
        {

            mCompra carrinho = Session["Carrinho"] != null ? (mCompra)Session["Carrinho"] : new mCompra();
            var itemExclusao = carrinho.ItensCompra.FirstOrDefault(i => i.ItemProdutoId == id);

            carrinho.ItensCompra.Remove(itemExclusao);

            Session["Carrinho"] = carrinho;

            return RedirectToAction("Carrinho");
        }

        public ActionResult Checkout()
        {


            mCompra carrinho = Session["Carrinho"] != null ? (mCompra)Session["Carrinho"] : new mCompra();

            // calculando o valor total do carrinho
            double valorTotal = carrinho.ItensCompra.Sum(item => item.QuantidadeVenda * item.ValorProd);

            carrinho.VlCom = valorTotal;

            return View(carrinho);
        }

        public ActionResult FinalizarPedido()
        {

            mCompra mC = new mCompra();


            DateTime? data = DateTime.Now.ToLocalTime();
            mC.DtCompra = data;

            ViewBag.DataFormata = data?.ToString("dd/MM/yyyy");

            // verifica se a data existe
            if (data.HasValue)
            {
                // marca reunião para 14 dias da data da compra
                mC.DtReuniao = data.Value.AddDays(14);
            }
            



         
      





            return View();

        }
    }
}
