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

            return View(aP.BuscarProd());
        }

        [HttpPost]
        public ActionResult detalhesProduto()
        {

            return View();
        }
        public ActionResult detalhesProduto(int id)
        {
           
            return View(aP.BuscarProdCod(id));
        }
    }
}