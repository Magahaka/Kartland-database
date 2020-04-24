using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using MySql.Data.MySqlClient;

namespace WebApplication1.Repos
{
    public class MarkeRepository
    {
        public List<Marke> getMarkes()
        {
            List<Marke> markes = new List<Marke>();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT m.kodas, m.pavadinimas FROM marke m";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                markes.Add(new Marke
                {        
                    pavadinimas = Convert.ToString(item["pavadinimas"]),
                    kodas = Convert.ToInt32(item["kodas"]),
                });
            }

            return markes;
        }

        public Marke getMarke(int kodas)
        {
            Marke marke = new Marke();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT m.kodas, m.pavadinimas 
                                FROM marke m WHERE m.kodas=" + kodas;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {

                marke.kodas = Convert.ToInt32(item["kodas"]);
                marke.pavadinimas = Convert.ToString(item["pavadinimas"]);
            }

            return marke;
        }

        public bool updateMarke(Marke marke)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE marke a SET a.pavadinimas=?pavadinimas WHERE a.kodas=?kodas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?kodas", MySqlDbType.VarChar).Value = marke.kodas;
            mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.VarChar).Value = marke.pavadinimas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public bool addMarke(Marke marke)
        {
            int kodas = getNewId();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO marke(kodas,pavadinimas)VALUES(?kodas,?pavadinimas)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.VarChar).Value = marke.pavadinimas;
            mySqlCommand.Parameters.Add("?kodas", MySqlDbType.VarChar).Value = kodas;
            marke.kodas = kodas;
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
                string sqlquery = @"SELECT MAX(kodas) FROM marke";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlConnection.Open();
                int id = (int)mySqlCommand.ExecuteScalar();
                mySqlConnection.Close();
                return id+1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int getMarkeCount(int kodas)
        {
            int naudota = 0;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(kodas) as kiekis from modelis where fk_MARKEkodas=" + kodas;
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

        public void deleteMarke(int kodas)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM marke where kodas=?kodas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?kodas", MySqlDbType.Int32).Value = kodas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}