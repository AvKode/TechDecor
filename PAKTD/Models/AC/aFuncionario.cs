using MySql.Data.MySqlClient;
using PAKTD.Models.MO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PAKTD.Models.AC
{
    public class aFuncionario
    {

        conexao con = new conexao();
        public List<mUsuario> BuscarFunc()
        {
            List<mUsuario> funcList = new List<mUsuario>();
           
            MySqlCommand cmd = new MySqlCommand("call pcd_selectFuncionario('F')", con.MyConectarBD());

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            con.MyDesConectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                funcList.Add(new mUsuario
                {

                    IdUsu = Convert.ToInt16(dr["id_Usu"]),
                    NomeUsu = Convert.ToString(dr["nome_Usu"]),
                    
                }

                );


            }

            return funcList;
        }

        public void InserirFuncionario(mUsuario mU)
        {
            MySqlCommand cmd = new MySqlCommand("call pcd_insertFuncionario(@nmFunc, @seFunc,'F')", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@nmFunc", mU.NomeUsu);
            cmd.Parameters.AddWithValue("@seFunc", mU.SenhaUsu);
         
            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }

        public void DeletarFunc(int id)
        {
            MySqlCommand cmd = new MySqlCommand("call pcd_Deletar_Usuario(@vCod)", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@vCod", id);
            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }

        public void AtualizarFuncionario(mUsuario mU, int id)
        {

            MySqlCommand cmd = new MySqlCommand("call sp_UpdateUsuario(@id, @nome, @senha)", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nome", mU.NomeUsu);
            cmd.Parameters.AddWithValue("@senha", mU.SenhaUsu);

            cmd.ExecuteNonQuery();

            con.MyDesConectarBD();
           
        }

      

        public List<mUsuario> BuscarFuncionarioCod(int id)
        {
            List<mUsuario> funcList = new List<mUsuario>();

            MySqlCommand cmd = new MySqlCommand("call pcd_Select_UsuarioPorCod(@id) ", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@id", id);

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            con.MyDesConectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                funcList.Add(new mUsuario
                {

                    IdUsu = Convert.ToInt16(dr["id_Usu"]),
                    NomeUsu = Convert.ToString(dr["nome_Usu"]),

                }

                );


            }

            return funcList;
        }

    }
}