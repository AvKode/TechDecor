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
            cmd.Parameters.AddWithValue("@stProd", mP.StaPro);
            cmd.Parameters.AddWithValue("@urlProd", mP.ImgProd);

            cmd.ExecuteNonQuery();

            con.MyDesConectarBD();
        }
        public void DeletarProd(int id)
        {
            MySqlCommand cmd = new MySqlCommand("call pcd_Deletar_Produto(@vCod)", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@vCod", id);
            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }

        public List<mProduto> BuscarProds()
        {
            List<mProduto> prodList = new List<mProduto>();

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

                prodList.Add(produto);
            }

            return prodList;
        }


        public List<vmCompraItensProd> BuscarProd()
        {
            List<vmCompraItensProd> ProdList = new List<vmCompraItensProd>();

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

        public void InserirAvaliacao(mAvaliacao mA)
        {
            MySqlCommand cmd = new MySqlCommand("call spInsertAvali(@dtAva,@notaAva,@cmAva,@fkUsu,@fkProd)", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@dtAva", mA.DtAvali);
            cmd.Parameters.AddWithValue("@notaAva", mA.notaAvali);
            cmd.Parameters.AddWithValue("@cmAva", mA.comentarioAvali);
            cmd.Parameters.AddWithValue("@fkUsu", mA.fkUsuário);
            cmd.Parameters.AddWithValue("@fkProd", mA.fkProduto);

            cmd.ExecuteNonQuery();

            con.MyDesConectarBD();
        }

        public List<mAvaliacao> selectMediaAvaliacao(int id)
        {
            List<mAvaliacao> AvaliList = new List<mAvaliacao>();

            MySqlCommand cmd = new MySqlCommand("call spSelectCalculaMedia(@id)", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@id", id);

            using (MySqlDataReader dr = cmd.ExecuteReader())
            {

                DataTable dt = new DataTable();
                dt.Load(dr);

                foreach (DataRow row in dt.Rows)
                {
                    AvaliList.Add(new mAvaliacao
                    {
                        DtAvali = Convert.ToDateTime(row["dataAva"]),
                        notaAvali = Convert.ToInt32(row["notaAva"]),
                        comentarioAvali = Convert.ToString(row["comentarioAva"]),
                        nomeUsu = Convert.ToString(row["nome_Usu"])
                    });
                }

                dr.NextResult();


                if (dr.Read())
                {
                    AvaliList.Add(new mAvaliacao
                    {
                        mediaNota = Convert.ToDouble(dr["media_notas"]),
                    });
                }
            }

            con.MyDesConectarBD();

            return AvaliList;
        }
        public void AtualizarProduto(mProduto produto, int id)
        {
            MySqlCommand cmd = new MySqlCommand("CALL sp_AltProduto(@idCategoria, @nmProduto, @dsProduto, @vlProduto, @stProduto, @urlProduto, @idProduto)", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@idCategoria", produto.IdfkCate);
            cmd.Parameters.AddWithValue("@nmProduto", produto.NmPro);
            cmd.Parameters.AddWithValue("@dsProduto", produto.DsPro);
            cmd.Parameters.AddWithValue("@vlProduto", produto.VlPro);
            cmd.Parameters.AddWithValue("@stProduto", produto.StaPro);
            cmd.Parameters.AddWithValue("@urlProduto", produto.ImgProd);
            cmd.Parameters.AddWithValue("@idProduto", id);

            cmd.ExecuteNonQuery();

            con.MyDesConectarBD();
        }

    }
}