using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
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
        public bool LogarCliUsu(mUsuario us)
        {
            MySqlCommand cmd = new MySqlCommand("call pcd_Logar(@nmUsu, @seUsu)", con.MyConectarBD());
            cmd.Parameters.Add("@nmUsu", MySqlDbType.VarChar).Value = us.NomeUsu;
            cmd.Parameters.Add("@seUsu", MySqlDbType.VarChar).Value = us.SenhaUsu;
            dr = cmd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    us.NomeUsu = dr["nome_Usu"].ToString();
                    us.SenhaUsu = dr["senha_Usu"].ToString();
                    us.TipoUsu = dr["tipo_Usu"].ToString();
                }
                con.MyDesConectarBD();
                return true;
            }
            else
            {
                con.MyDesConectarBD();
                return false;
            }
        }
        public void BuscarCli(mCliente cl)
        {
            mUsuario us = new mUsuario();
            MySqlCommand cmd = new MySqlCommand("", con.MyConectarBD());
            cmd.Parameters.Add("@idCli", MySqlDbType.VarChar).Value = cl.IdCli;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cl.NmCli = dr[1].ToString();
                cl.DtNascCli = DateTime.Parse(dr[2].ToString());
                cl.EmailCli = dr[3].ToString();       
                cl.FoneCli = dr[4].ToString();
                cl.CpfCli = dr[5].ToString();
                cl.CepCli = dr[6].ToString();
                cl.NoCasa = dr[7].ToString();
                cl.ComCasa = dr[8].ToString();
                us.NomeUsu = dr[9].ToString();
                us.SenhaUsu = dr[10].ToString();
                us.TipoUsu = dr[11].ToString();
            }
            con.MyDesConectarBD();
        }
        public void AlterarCli(mCliente cl, mUsuario us)
        {
            MySqlCommand cmd = new MySqlCommand("", con.MyConectarBD());
            cmd.Parameters.Add("@idCli", MySqlDbType.Int16).Value = cl.IdCli;
            cmd.Parameters.Add("@nmCli", MySqlDbType.VarChar).Value = cl.NmCli;
            cmd.Parameters.Add("@dtCli", MySqlDbType.Date).Value = cl.DtNascCli;
            cmd.Parameters.Add("@emCli", MySqlDbType.VarChar).Value = cl.EmailCli;
            cmd.Parameters.Add("@foCli", MySqlDbType.VarChar).Value = cl.FoneCli;
            cmd.Parameters.Add("@cpfCli", MySqlDbType.VarChar).Value = cl.CpfCli;
            cmd.Parameters.Add("@cepCli", MySqlDbType.VarChar).Value = cl.CepCli;
            cmd.Parameters.Add("@noCasa", MySqlDbType.VarChar).Value = cl.NoCasa;
            cmd.Parameters.Add("@coCasa", MySqlDbType.VarChar).Value = cl.ComCasa;
            cmd.Parameters.Add("@idUsu", MySqlDbType.Int16).Value = us.IdUsu;
            cmd.Parameters.Add("@nmUsu", MySqlDbType.VarChar).Value = us.NomeUsu;
            cmd.Parameters.Add("@seUsu", MySqlDbType.VarChar).Value = us.SenhaUsu;
            cmd.Parameters.Add("@tpUsu", MySqlDbType.VarChar).Value = us.TipoUsu;
            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }
        public void Deletar(mCliente cl)
        {
            MySqlCommand cmd = new MySqlCommand("", con.MyConectarBD());
            cmd.Parameters.Add("@idCli", MySqlDbType.VarChar).Value = cl.IdCli;
            cmd.Parameters.Add("@idUsu", MySqlDbType.VarChar).Value = cl.IdfkUsu;
            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }
    }
}