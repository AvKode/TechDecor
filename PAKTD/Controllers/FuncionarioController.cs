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


        public ActionResult alterarFuncionario(int id)
        {
        
            return View(aF.BuscarFuncionarioCod(id));

        }
        [HttpPost]
        public ActionResult alterarFuncionario(mUsuario mU, int id)
        {
            aF.AtualizarFuncionario(mU,id);
            return RedirectToAction("listarFuncionario", "Funcionario");
        }
        public ActionResult ExcluirFuncionario(int id)
        {

            aF.DeletarFunc(id);

            return RedirectToAction("listarFuncionario", "Funcionario");
        }
    }
}