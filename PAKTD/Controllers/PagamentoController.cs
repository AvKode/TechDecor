using MySql.Data.MySqlClient;
using PAKTD.Models;
using PAKTD.Models.AC;
using PAKTD.Models.MO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
        aClienteUsuario aCliUsu = new aClienteUsuario();
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

            return RedirectToAction("Index", "Home");
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

            return RedirectToAction("Index", "Home");
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
                mC.DtServico = data.Value.AddDays(14);
            }

            Session["Carrinho"] = null;


            int idCliente = Convert.ToInt32(Session["usuId"]);
            mC.IdfkCli = idCliente;
            mC.IdfkPag = 2;

            mC.VlCom = 435.90;

            aP.InserirCompra(mC);

            TempData["DtServico"] = mC.DtServico;
            return RedirectToAction("gerarBoleto", "Pagamento");

        }

        public ActionResult DadosVendasPorMes()
        {
            List<object> dadosVendas = new List<object>();
            conexao con = new conexao();
            using (MySqlCommand cmd = new MySqlCommand("call sp_SelecionaVendaPorMes()", con.MyConectarBD()))
            {
                

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int mes = dr.GetInt32("MesDaCompra");
                        int numeroDeVendas = dr.GetInt32("NumeroDeVendas");
                        decimal totalDeVendas = dr.GetDecimal("TotalDeVendas");

                        dadosVendas.Add(new { Mes = mes, NumeroDeVendas = numeroDeVendas, TotalDeVendas = totalDeVendas });
                    }
                }
            }

            string jsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(dadosVendas);

            return Content(jsonResult, "application/json");
        }

        public ActionResult DadosVendasPorPagamento()
        {
            List<object> dadosVendasPag = new List<object>();
            conexao con = new conexao();
            using (MySqlCommand cmd = new MySqlCommand("call spNumeroVendaPorPag()", con.MyConectarBD()))
            {
                
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        

                        string pagamento = dr.GetString("ds_Pagamento");
                        int numeroDeVendas = dr.GetInt32("NumeroDeVendas");
                        

                        dadosVendasPag.Add(new { Pagamento = pagamento, NumeroDeVendas = numeroDeVendas });
                    }
                }
            }

            string jsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(dadosVendasPag);

            return Content(jsonResult, "application/json");
        }

        public async Task<ActionResult> gerarBoleto()
        {
            int id = Convert.ToInt32(Session["usuId"]);
            if (Session["usuId"] == null || string.IsNullOrWhiteSpace(Session["usuId"].ToString()))
            {
                return RedirectToAction("Logar", "ClienteUsuario");
            }

            var cliente = aCliUsu.BuscarDadosClientes(id).FirstOrDefault();
            // Obtenha os valores dos campos do formulário
            var nomeCliente = cliente.NmCli;
            var CPF = cliente.CpfCli;
            var email = cliente.EmailCli.ToLower();
            var CEP = cliente.CepCli;
            var numeroCasa = cliente.NoCasa;
           
            var valor = 7890;
            var estado = "São Paulo";
            var cidade = "São Paulo";

        
            // Faça a consulta de CEP
            var resultadoConsulta = ConsultaCEP(CEP);
            var Localidade = resultadoConsulta.localidade;
            var rua = resultadoConsulta.logradouro;
            var UF = resultadoConsulta.uf;


            // Defina o token e a URL do PagSeguro
            const string TOKEN = "6493FF5B80FC44C8BE99A8930A82E352";
            const string URL = "https://sandbox.api.pagseguro.com/charges";

            // Crie o objeto de dados a ser enviado
            var data = new
            {
                reference_id = "ex-00001",
                description = "Pagamento com boleto - Blade Enclave",
                amount = new { value = valor, currency = "BRL" },
                payment_method = new
                {
                    type = "BOLETO",
                    boleto = new
                    {
                        due_date = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd"),
                        instruction_lines = new { line_1 = "Pagamento processado para DESC Fatura", line_2 = "Via PagSeguro" },
                        holder = new { name = nomeCliente, tax_id = CPF, email = email, address = new { country = "Brasil", region = estado, region_code = UF, city = cidade, postal_code = CEP, street = rua, number = numeroCasa, locality = Localidade } }
                    }
                },
                notification_urls = new[] { "https://meusite.com/notificacoes" }
            };

            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, URL);
                request.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                request.Headers.Add("Authorization", "Bearer " + TOKEN);

               
                var jsonRequest = await request.Content.ReadAsStringAsync();

              
                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var linkPagamento = (await response.Content.ReadAsAsync<dynamic>()).links[0].href;
                    return RedirectToAction("compraFinalizada", new { linkPagamento });
                }

             
                var errorResponse = new
                {
                    ErrorMessage = "Erro na solicitação",
                    JsonRequest = jsonRequest,
                    ResponseContent = await response.Content.ReadAsStringAsync()
                };

                return Content(Newtonsoft.Json.JsonConvert.SerializeObject(errorResponse), "application/json");
            }
        }

        public dynamic ConsultaCEP(string CEP)
        {
            var urlCEP = $"https://viacep.com.br/ws/{CEP}/json/";

            using (var httpClient = new HttpClient())
            {
                var resultadoConsultaJson = httpClient.GetStringAsync(urlCEP).Result;
                var resultadoConsulta = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(resultadoConsultaJson);
                return resultadoConsulta;
            }
        }

        public ActionResult compraFinalizada(string linkPagamento)
        {

            DateTime? dtServico = TempData["DtServico"] as DateTime?;

            ViewBag.DtServico = dtServico?.ToString("dd/MM/yyyy");

            if (linkPagamento != "")
            {
                ViewBag.MensagemBoleto = "o seu boleto foi gerado clique no link abaixo para abrir";
                ViewBag.LinkPagamento = linkPagamento;

            }
            return View();
        }

    }
}

