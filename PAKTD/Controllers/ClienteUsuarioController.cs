using PAKTD.Models.AC;
using PAKTD.Models.MO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace PAKTD.Controllers
{
    public class ClienteUsuarioController : Controller
    {
        aClienteUsuario aCliUsu = new aClienteUsuario();
        public static string msg;
        // GET: ClienteUsuario
        public ActionResult Logar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Logar(mUsuario us)
        {
            if (aCliUsu.LogarCliUsu(us) == true)
            {
                Session["usuLog"] = us.NomeUsu.ToString();
                Session["senLog"] = us.SenhaUsu.ToString();
                if (us.TipoUsu == "A")
                {
                    Session["tU"] = "Administrador";
                    return RedirectToAction("Index", "Home");
                }
                else if (us.TipoUsu == "F")
                {
                    Session["tU"] = "Funcionário";
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    Session["tU"] = "Cliente";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["msgLogar"] = "Usuário não encontrado. Verifique usuário e senha";
                return View();
            }
        }
        public ActionResult Cadastrar()
        {
            return View();  
        }
        [HttpPost]
        public ActionResult Cadastrar(mCliente cl, mUsuario us)
        {

                aCliUsu.InserirCliUsu(cl, us);
                Session["usuLog"] = us.NomeUsu.ToString();
                return RedirectToAction("Index", "Home");

        }

        public ActionResult consultarCliente(mCliente cl)
        {


            return View(aCliUsu.BuscarCli());
        }
    }
}