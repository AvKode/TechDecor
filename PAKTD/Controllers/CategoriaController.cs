using PAKTD.Models.AC;
using PAKTD.Models.MO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PAKTD.Controllers
{
    public class CategoriaController : Controller
    {

        aCategoria aC = new aCategoria();

        public ActionResult ListarCategoria()
        {


            return View(aC.BuscarCat());
        }

        public ActionResult CadastrarCategoria()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CadastrarCategoria(mCategoria mC)
        {
            if (ModelState.IsValid)
            {
                aC.InserirCat(mC);
            }
            return RedirectToAction("ListarCategoria", "Categoria");
        }

        public ActionResult ExcluirCategoria(int id)
        {

            aC.DeletarCat(id);

            return RedirectToAction("ListarCategoria", "Categoria");
        }

    }
}