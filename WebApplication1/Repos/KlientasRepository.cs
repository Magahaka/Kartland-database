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
    public class KlientasRepository
    {
        public List<Klientas> getKlientai()
        {
            List<Klientas> klientai = new List<Klientas>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from klientas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                klientai.Add(new Klientas
                {
                    asmensKodas = Convert.ToString(item["asmens_kodas"]),
                    vardas = Convert.ToString(item["vardas"]),
                    pavarde = Convert.ToString(item["pavarde"]),
                    gimimoData = Convert.ToDateTime(item["gimimo_data"]),
                    telefonas = Convert.ToString(item["telefonas"]),
                    epastas = Convert.ToString(item["el_pastas"]),
                    id = Convert.ToInt32(item["id_KLIENTAS"])
                });
            }
            return klientai;
        }

        public bool addKlientas(Klientas klientas)
        {
            int kodas = getNewId();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO klientas(asmens_kodas,vardas,pavarde,gimimo_data,telefonas,el_pastas,slapyvardis,id_KLIENTAS)VALUES(?asmkod,?vardas,?pavarde,?gimdata,?tel,?email,?slapyv,?id_klientas);";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?asmkod", MySqlDbType.VarChar).Value = klientas.asmensKodas;
            mySqlCommand.Parameters.Add("?vardas", MySqlDbType.VarChar).Value = klientas.vardas;
            mySqlCommand.Parameters.Add("?pavarde", MySqlDbType.VarChar).Value = klientas.pavarde;
            mySqlCommand.Parameters.Add("?gimdata", MySqlDbType.Date).Value = klientas.gimimoData;
            mySqlCommand.Parameters.Add("?tel", MySqlDbType.VarChar).Value = klientas.telefonas;
            mySqlCommand.Parameters.Add("?email", MySqlDbType.VarChar).Value = klientas.epastas;
            mySqlCommand.Parameters.Add("?slapyv", MySqlDbType.VarChar).Value = klientas.slapyvardis;
            mySqlCommand.Parameters.Add("?id_klientas", MySqlDbType.Int32).Value = kodas;
            klientas.id = kodas;
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
                string sqlquery = @"SELECT MAX(id_KLIENTAS) FROM klientas";
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

        public Klientas getKlientas(string asmkodas)
        {
            Klientas klientas = new Klientas();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from klientas where asmens_kodas=?asmkodas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?asmkodas", MySqlDbType.VarChar).Value = asmkodas;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                klientas.asmensKodas = Convert.ToString(item["asmens_kodas"]);
                klientas.vardas = Convert.ToString(item["vardas"]);
                klientas.pavarde = Convert.ToString(item["pavarde"]);
                klientas.gimimoData = Convert.ToDateTime(item["gimimo_data"]);
                klientas.telefonas = Convert.ToString(item["telefonas"]);
                klientas.epastas = Convert.ToString(item["el_pastas"]);
                klientas.id = Convert.ToInt32(item["id_KLIENTAS"]);
            }
            return klientas;
        }

        public bool updateKlientas(Klientas klientas)
        {

            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"UPDATE klientas a SET a.vardas=?vardas, a.pavarde=?pavarde, a.gimimo_data=?gimdata, a.telefonas=?tel, a.epastas=?email WHERE a.id_KLIENTAS=?kodas";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?asmkod", MySqlDbType.VarChar).Value = klientas.asmensKodas;
                mySqlCommand.Parameters.Add("?vardas", MySqlDbType.VarChar).Value = klientas.vardas;
                mySqlCommand.Parameters.Add("?pavarde", MySqlDbType.VarChar).Value = klientas.pavarde;
                mySqlCommand.Parameters.Add("?gimdata", MySqlDbType.Date).Value = klientas.gimimoData;
                mySqlCommand.Parameters.Add("?tel", MySqlDbType.VarChar).Value = klientas.telefonas;
                mySqlCommand.Parameters.Add("?email", MySqlDbType.VarChar).Value = klientas.epastas;
                mySqlConnection.Open();
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int getKlientasSutarciuCount(string id)
        {
            int naudota = 0;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(kodas) as kiekis from sutartis where fk_KLIENTASid_KLIENTAS=" + id;
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

        public void deleteKlientas(string id)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM klientas where asmens_kodas=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.VarChar).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}