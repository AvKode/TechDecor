using MySql.Data.MySqlClient;
using PAKTD.Models.MO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PAKTD.Models.AC
{
    public class aPagamento
    {

        conexao con = new conexao();
        public List<mPagamento> BuscarPag()
        {
            List<mPagamento> pagList = new List<mPagamento>();

            MySqlCommand cmd = new MySqlCommand("call spMostrarPgto()", con.MyConectarBD());

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            con.MyDesConectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                pagList.Add(new mPagamento
                {

                    IdPagamento = Convert.ToString(dr["id_Pagamento"]),
                    DsPagamento = Convert.ToString(dr["ds_Pagamento"]),

                }

                );


            }

            return pagList;
        }

        public void InserirPag(mPagamento mP)
        {
            MySqlCommand cmd = new MySqlCommand("call sp_insPagamento(@dsPagamento)", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@dsPagamento", mP.DsPagamento);
      

            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }

        public void InserirCompra(mCompra mC)
        {
            MySqlCommand cmd = new MySqlCommand("call InserirCarrinho(@fkCliente,@fkPagamento, @valorCompra,@dataCompra,@dataServico)", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@fkCliente", mC.IdfkCli);
            cmd.Parameters.AddWithValue("@fkPagamento", mC.IdfkPag);
            cmd.Parameters.AddWithValue("@valorCompra", mC.VlCom);
            cmd.Parameters.AddWithValue("@dataCompra", mC.DtCompra);
            cmd.Parameters.AddWithValue("@dataServico", mC.DtServico);
            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }


    }
}