using PAKTD.Models.AC;
using PAKTD.Models.MO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PAKTD.Controllers
{
    public class FuncionarioController : Controller
    {
        aFuncionario aF = new aFuncionario();
        public ActionResult cadastrarFuncionario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult cadastrarFuncionario(mUsuario mU)
        {

            if (ModelState.IsValid)
            {

                aF.InserirFuncionario(mU);

            }

            return RedirectToAction("listarFuncionario", "Funcionario");
        }

        public ActionResult listarFuncionario()
        {

            return View(aF.BuscarFunc());
        }
    }
}