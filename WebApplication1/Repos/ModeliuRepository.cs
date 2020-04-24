using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication1.ViewModels;
using MySql.Data.MySqlClient;

namespace WebApplication1.Repos
{
    public class ModeliuRepository
    {
        public List<ModelisViewModel> getModeliai()
        {
            List<ModelisViewModel> modelisViewModels = new List<ModelisViewModel>();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT m.kodas, m.pavadinimas, mm.pavadinimas AS marke 
                                FROM " + @"modelis m
                                LEFT JOIN " + @"marke mm ON mm.kodas=m.fk_MARKEkodas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                modelisViewModels.Add(new ModelisViewModel
                {
                    kodas = Convert.ToInt32(item["kodas"]),
                    pavadinimas = Convert.ToString(item["pavadinimas"]),
                    marke = Convert.ToString(item["marke"])
                });
            }

            return modelisViewModels;
        }

        public ModelisEditViewModel getModelis(int kodas)
        {
            ModelisEditViewModel modelis = new ModelisEditViewModel();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT m.* 
                                FROM " + @"modelis m WHERE m.kodas=" + kodas;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                modelis.kodas = Convert.ToInt32(item["kodas"]);
                modelis.pavadinimas = Convert.ToString(item["pavadinimas"]);
                modelis.fk_marke = Convert.ToInt32(item["fk_MARKEkodas"]);
            }

            return modelis;
        }

        public bool updateModelis(ModelisEditViewModel modelis)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE modelis a SET a.pavadinimas=?pavadinimas, a.fk_MARKEkodas=?marke WHERE a.kodas=?kodas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?kodas", MySqlDbType.VarChar).Value = modelis.kodas;
            mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.VarChar).Value = modelis.pavadinimas;
            mySqlCommand.Parameters.Add("?marke", MySqlDbType.VarChar).Value = modelis.fk_marke;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public bool addModelis(ModelisEditViewModel modelis)
        {
            int kodas = getNewId();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO modelis(kodas,pavadinimas,fk_MARKEkodas)VALUES(?kodas,?pavadinimas,?marke)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?kodas", MySqlDbType.VarChar).Value = kodas;
            modelis.kodas = kodas;
            mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.VarChar).Value = modelis.pavadinimas;
            mySqlCommand.Parameters.Add("?marke", MySqlDbType.VarChar).Value = modelis.fk_marke;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public int getNewId()
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"SELECT MAX(kodas) FROM modelis";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlConnection.Open();
                int id = (int)mySqlCommand.ExecuteScalar();
                mySqlConnection.Close();
                return id + 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int getModelisCount(int kodas)
        {
            int naudota = 0;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(kodas) as kiekis from kartingas where fk_MODELISkodas=" + kodas;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                naudota = Convert.ToInt32(item["kiekis"] == DBNull.Value ? 0 : item["kiekis"]);
            }
            return naudota;
        }

        public void deleteModelis(int kodas)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM modelis where kodas=?kodas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?kodas", MySqlDbType.Int32).Value = kodas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }


    }
}