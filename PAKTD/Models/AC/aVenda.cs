using System;
using MySql.Data.MySqlClient;
using PAKTD.Models.MO;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PAKTD.Models.AC
{
    public class aVenda
    {

        conexao con = new conexao();

        public void InserirVenda(mCompra mV)
        {
            MySqlCommand cmd = new MySqlCommand("call sp_InserirCliUsu(@nmUsu, @seUsu, @nmCli, @emCli, @cpfCli, @dtCli, @noCasa, @coCasa, @cepCli, @foCli )", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@dataCompra", mV.DtCompra);
            cmd.Parameters.AddWithValue("@prazoServico", mV.DtReuniao);
            cmd.Parameters.AddWithValue("@idCliente", mV.IdfkCli);

            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }

    }
}