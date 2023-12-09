using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Web.Mvc;
using PAKTD.Models.MO;

namespace PAKTD.Models.AC
{
    public class aClienteUsuario
    {
        conexao con = new conexao();
        MySqlDataReader dr;

        //---   Colocar o nome das Procedures nos métodos   ---//

        public void InserirCliUsu(mCliente cl, mUsuario us)
        {
            MySqlCommand cmd = new MySqlCommand("call sp_InserirCliUsu(@nmUsu, @seUsu, @nmCli, @emCli, @cpfCli, @dtCli, @noCasa, @coCasa, @cepCli, @foCli )", con.MyConectarBD()); 
            cmd.Parameters.Add("@nmUsu", MySqlDbType.VarChar).Value = us.NomeUsu;
            cmd.Parameters.Add("@seUsu", MySqlDbType.VarChar).Value = us.SenhaUsu;
            cmd.Parameters.Add("@nmCli", MySqlDbType.VarChar).Value = cl.NmCli;
            cmd.Parameters.Add("@dtCli", MySqlDbType.Date).Value = cl.DtNascCli;
            cmd.Parameters.Add("@emCli", MySqlDbType.VarChar).Value = cl.EmailCli;
            cmd.Parameters.Add("@foCli", MySqlDbType.VarChar).Value = cl.FoneCli;
            cmd.Parameters.Add("@cpfCli", MySqlDbType.VarChar).Value = cl.CpfCli;
            cmd.Parameters.Add("@cepCli", MySqlDbType.VarChar).Value = cl.CepCli;
            cmd.Parameters.Add("@noCasa", MySqlDbType.VarChar).Value = cl.NoCasa;
            cmd.Parameters.Add("@coCasa", MySqlDbType.VarChar).Value = cl.ComCasa;
            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }
        public bool LogarCliUsu(mUsuario us, out mCliente mC)
        {
            mC = new mCliente();
            MySqlCommand cmd = new MySqlCommand("call pcd_Logar(@nmUsu, @seUsu)", con.MyConectarBD());
            cmd.Parameters.Add("@nmUsu", MySqlDbType.VarChar).Value = us.NomeUsu;
            cmd.Parameters.Add("@seUsu", MySqlDbType.VarChar).Value = us.SenhaUsu;

            using (MySqlDataReader dr = cmd.ExecuteReader())
            {
             
                if (dr.Read())
                {
                    int idClienteIndex;
                    try
                    {
                        idClienteIndex = dr.GetOrdinal("id");
                    }
                    catch (IndexOutOfRangeException)
                    {
                        idClienteIndex = -1;
                    }

                    if (idClienteIndex >= 0)
                    {
                        mC.IdCli = Convert.ToInt32(dr["id"]);
                        us.IdUsu = Convert.ToInt32(dr["id_Usu"]);
                    }

                    us.TipoUsu = dr["tipo_Usu"].ToString();
                    con.MyDesConectarBD();
                    return true;
                }
                else
                {
                    con.MyDesConectarBD();
                    return false;
                }
            }
        }






        public List<mCliente> BuscarCli()
        {

            List<mCliente> cliList = new List<mCliente>();

            MySqlCommand cmd = new MySqlCommand("call pcd_Select_Cliente()", con.MyConectarBD());

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            con.MyDesConectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                cliList.Add(new mCliente
                {

                   IdCli = Convert.ToInt32(dr["id_Cliente"]),
                   NmCli = Convert.ToString(dr["nome_Cli"]),
                   EmailCli = Convert.ToString(dr["email_Cli"]),
                   FoneCli = Convert.ToString(dr["fone_Cli"]),
                   CpfCli = Convert.ToString(dr["cpf_Cli"]),
                   CepCli = Convert.ToString(dr["cep_Cli"])
                }

                );


            }

            return cliList;
        }
       

        public void DeletarCli(int id)
        {
            MySqlCommand cmd = new MySqlCommand("call pcd_Deletar_Cliente(@vCod)", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@vCod", id);
            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }

        public List<mCliente> BuscarDadosClientes(int id)
        {
            List<mCliente> ClienteList = new List<mCliente>();

            MySqlCommand cmd = new MySqlCommand("call spDadosCliente(@id)", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            con.MyDesConectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                ClienteList.Add(new mCliente
                {

                    NmCli = dr["nome_Cli"].ToString(),
                    EmailCli = dr["email_Cli"].ToString(),
                    CpfCli = dr["cpf_Cli"].ToString(),
                    CepCli = dr["Cep_Cli"].ToString(),
                    NoCasa = dr["no_log_Cli"].ToString(),
                    ComCasa = dr["complemento_Cli"].ToString(),
                }

                ) ; 


            }

            return ClienteList;
        }

        public void AtualizarCliente(mCliente mC, int id)
        {

            MySqlCommand cmd = new MySqlCommand("call sp_UpdateCliente(@id, @nome, @email,@cpf,@dtNasc,@noLogra,@complementoCli,@cep,@telefone)", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nome", mC.NmCli);
            cmd.Parameters.AddWithValue("@email", mC.EmailCli);
            cmd.Parameters.AddWithValue("@cpf", mC.CpfCli);
            cmd.Parameters.AddWithValue("@dtNasc", mC.DtNascCli) ;
            cmd.Parameters.AddWithValue("@noLogra", mC.NoCasa);
            cmd.Parameters.AddWithValue("@complementoCli", mC.ComCasa) ;
            cmd.Parameters.AddWithValue("@cep", mC.CepCli);
            cmd.Parameters.AddWithValue("@telefone", mC.FoneCli);

            cmd.ExecuteNonQuery();

            con.MyDesConectarBD();

        }
    }
}