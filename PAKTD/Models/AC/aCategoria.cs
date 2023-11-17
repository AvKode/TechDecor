using MySql.Data.MySqlClient;
using PAKTD.Models.MO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;

namespace PAKTD.Models.AC
{
    public class aCategoria
    {
        conexao con = new conexao();
        public List<mCategoria> BuscarCat()
        {
            List<mCategoria> catList = new List<mCategoria>();

            MySqlCommand cmd = new MySqlCommand("call sp_ListarCategoria()", con.MyConectarBD());

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            con.MyDesConectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                catList.Add(new mCategoria
                {

                   IdCate = Convert.ToString(dr["id_Categoria"]),
                   NmCate = Convert.ToString(dr["nm_Categoria"]),
                   dsCate = Convert.ToString(dr["ds_Categoria"]),

                }

                );


            }

            return catList;
        }

        public void InserirCat(mCategoria mC)
        {
            MySqlCommand cmd = new MySqlCommand("call  pcd_insertCategoria(@nmCat, @dsCat)", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@nmCat", mC.NmCate);
            cmd.Parameters.AddWithValue("@dsCat", mC.dsCate);

            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }

        public void DeletarCat(int id)
        {
            MySqlCommand cmd = new MySqlCommand("call pcd_Deletar_Categoria(@vCod)", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@vCod", id);
            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }

    }
}