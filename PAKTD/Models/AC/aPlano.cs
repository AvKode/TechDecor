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
    public class aPlano
    {
        conexao con = new conexao();
        public List<mPlano> BuscarPlanos()
        {
            List<mPlano> PlanoList = new List<mPlano>();

            MySqlCommand cmd = new MySqlCommand("call pcd_selectPlano()", con.MyConectarBD());


            cmd.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            con.MyDesConectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                PlanoList.Add(new mPlano
                {
                    ImgPlano = Convert.ToString(dr["url_Produto"]),
                    DsPlano = Convert.ToString(dr["ds_Planos"]),
                    NmPro = Convert.ToString(dr["nm_Produto"]),
                    NmPlano = Convert.ToString(dr["nm_Planos"]),
                    IdPro = Convert.ToInt32(dr["id_produto"]),
                    IdPlano = Convert.ToInt32(dr["id_plano"])
                }

                );


            }

            return PlanoList;
        }

    }
}