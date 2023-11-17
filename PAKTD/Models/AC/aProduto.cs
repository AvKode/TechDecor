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
    public class aProduto
    {
        conexao con = new conexao();

        public void InserirProduto(mProduto mP)
        {
            MySqlCommand cmd = new MySqlCommand("call pcd_insertProduto(@idcat, @nmProd, @dsProd, @vlProd, @stProd, @urlProd)", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@idCat", mP.IdfkCate);
            cmd.Parameters.AddWithValue("@nmProd", mP.NmPro);
            cmd.Parameters.AddWithValue("@dsProd", mP.DsPro);
            cmd.Parameters.AddWithValue("@vlProd", mP.VlPro);
            cmd.Parameters.AddWithValue("@stProd", mP.StaPro) ;
            cmd.Parameters.AddWithValue("@urlProd", mP.ImgProd) ;

            cmd.ExecuteNonQuery();

            con.MyDesConectarBD();
        }

        public List<vmCompraItensProd> BuscarProd()
        {
            List<vmCompraItensProd> ProdList = new List<vmCompraItensProd> ();

            MySqlCommand cmd = new MySqlCommand("call sp_BuscarProduto()", con.MyConectarBD());

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            con.MyDesConectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                var produto = new mProduto
                {
                    IdPro = Convert.ToInt32(dr["id_Produto"]),
                    NmPro = Convert.ToString(dr["nm_Produto"]),
                    ImgProd = Convert.ToString(dr["url_produto"]),
                    VlPro = Convert.ToDouble(dr["vl_Produto"]),
                    DsPro = Convert.ToString(dr["ds_Produto"])
                };

                var compraItem = new vmCompraItensProd
                {
                    mP = produto,
                   
                    mC = new mCompra() 
                };

                ProdList.Add(compraItem);
            }

            return ProdList;
        }

        public List<mProduto> BuscarProdCod(int id)
        {
            List<mProduto> ProdList = new List<mProduto>();

            MySqlCommand cmd = new MySqlCommand("call pcd_Select_ProdutoPorCod(@id)", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            con.MyDesConectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                ProdList.Add(new mProduto
                {

                    IdPro = Convert.ToInt32(dr["id_Produto"]),
                    NmPro = Convert.ToString(dr["nm_Produto"]),
                    ImgProd = Convert.ToString(dr["url_produto"]),
                    VlPro = Convert.ToDouble(dr["vl_Produto"]),
                    DsPro = Convert.ToString(dr["ds_Produto"])


                }

                ); ;


            }

            return ProdList;
        }


        public List<mProduto> PesquisaProduto(String termoBuscado)
        {
            List<mProduto> ProdList = new List<mProduto>();

            MySqlCommand cmd = new MySqlCommand("call pcd_Select_ProdutoPorNome(@nome)", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@nome", termoBuscado);

            cmd.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            con.MyDesConectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                ProdList.Add(new mProduto
                {

                    IdPro = Convert.ToInt32(dr["id_Produto"]),
                    NmPro = Convert.ToString(dr["nm_Produto"]),
                    ImgProd = Convert.ToString(dr["url_produto"]),
                    VlPro = Convert.ToDouble(dr["vl_Produto"]),
                    DsPro = Convert.ToString(dr["ds_Produto"])


                }

                ); ;


            }

            return ProdList;
        }
    }
}