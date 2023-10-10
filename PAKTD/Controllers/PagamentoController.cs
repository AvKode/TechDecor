using PAKTD.Models.AC;
using PAKTD.Models.MO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PAKTD.Controllers
{
    public class PagamentoController : Controller
    {
        aPagamento aP = new aPagamento();
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

    }
}