using MySql.Data.MySqlClient;
using PAKTD.Models.AC;
using PAKTD.Models.MO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PAKTD.Controllers
{
    public class ProdutoController : Controller
    {

        aProduto aP = new aProduto();

        public ActionResult CadastrarProduto()
        {



            return View();
        }

        [HttpPost]
        public ActionResult CadastrarProduto(mProduto mP, HttpPostedFileBase fotoProduto)
        {

            //Pegando a Imagem dos Produtos

            string arquivo = Path.GetFileName(fotoProduto.FileName);

            string file2 = "/Imagens - Produtos/" + arquivo;

            string _path = Path.Combine(Server.MapPath("~/Imagens - Produtos/"), arquivo);

            fotoProduto.SaveAs(_path);

            //Salvando no banco

            mP.ImgProd = file2;

            aP.InserirProduto(mP);



            return RedirectToAction("consultarProduto", "Produto");
        }


        public ActionResult consultarProduto()
        {

            return View(aP.BuscarProds());
        }

        [HttpPost]
        public ActionResult detalhesProduto()
        {

            return View();
        }
        public ActionResult detalhesProduto(int id)
        {
            aProduto aP = new aProduto();
            var model = new vmAvaliProd
            {
                mP = aP.BuscarProdCod(id).FirstOrDefault(),
                mA = aP.selectMediaAvaliacao(id).ToList()
            };

            return View(new List<vmAvaliProd> { model });
        }


        public ActionResult avaliacaoProduto(int idProd)
        {
            ViewBag.IdProduto = idProd;
            return View();
        }

        [HttpPost]

        public ActionResult InserirAvaliacao(int nota, string comentario, int idProd)
        {
            mAvaliacao mA = new mAvaliacao();
            DateTime? data = DateTime.Now.ToLocalTime();
            mA.DtAvali = data;

            mA.comentarioAvali = comentario;
            mA.notaAvali = nota;
            mA.fkProduto = idProd;

            int idUsuario = Convert.ToInt32(Session["usuarioId"]);
            mA.fkUsuário = idUsuario;

            aP.InserirAvaliacao(mA);

            string urlDetalhesProduto = $"/Produto/detalhesProduto/{idProd}";
            return RedirectToAction("DetalhesProduto", "Produto", new { id = idProd });
        }

        public ActionResult AlterarProduto(int id)
        {
            var produtos = aP.BuscarProdCod(id);
            var produto = produtos.FirstOrDefault(p => p.IdPro == id);

            return View(produto);
        }

        [HttpPost]
        public ActionResult AlterarProduto(mProduto produto, int id)
        {
            aP.AtualizarProduto(produto, id);
            return RedirectToAction("ListarProdutos", "Produto");
        }

        public ActionResult ExcluirProduto(int id)
        {

            aP.DeletarProd(id);

            return RedirectToAction("consultarProduto", "Produto");
        }
    }
}