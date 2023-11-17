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

            mCliente mC = new mCliente();
            if (aCliUsu.LogarCliUsu(us) == true)
            {
               
                Session["senLog"] = us.SenhaUsu.ToString();
                if (us.TipoUsu == "A")
                {
                    Session["tU"] = "Administrador";
                    return RedirectToAction("Index", "Dashboard");
                }
                else if (us.TipoUsu == "F")
                {
                    Session["tU"] = "Funcionário";
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    Session["usuLog"] = mC.NmCli.ToString();
                   Session["tU"] = "Cliente";
                    return RedirectToAction("Index", "Home");
                   
                }
            }
            else
            {
                TempData["msgLogar"] = "Usuário não encontrado. Verifique usuário e senha";
                return RedirectToAction("Cadastrar", "ClienteUsuario");
            }
        }

     
        public ActionResult Sair()
        {

            Session.Abandon();
            return RedirectToAction("Index", "Home");

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

        public ActionResult excluirCliente(int id)
        {

            aCliUsu.DeletarCli(id);

            return RedirectToAction("consultarCliente", "ClienteUsuario");
        
        }

    
        
    }
}