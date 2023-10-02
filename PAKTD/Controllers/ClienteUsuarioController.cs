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
            aCliUsu.LogarCliUsu(us);
            if (us.NomeUsu != null && us.SenhaUsu != null)
            {
                Session["usuLog"] = us.NomeUsu.ToString();
                Session["senLog"] = us.SenhaUsu.ToString();
                if (us.TipoUsu == "A")
                {
                    Session["tA"] = us.TipoUsu.ToString();
                    return RedirectToAction("Index", "Admin");
                }
                else if (us.TipoUsu == "F")
                {
                    Session["tA"] = us.TipoUsu.ToString();
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    Session["tC"] = us.TipoUsu.ToString();
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
            try
            {
                aCliUsu.InserirCliUsu(cl, us);
                Session["usuLog"] = us.NomeUsu.ToString();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception erro)
            {
                msg = "Ocorreu um erro ao Inserir!" + erro.Message;
                return View();
            }
        }
    }
}