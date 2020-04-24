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
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO klientas(asmens_kodas,vardas,pavarde,gimimo_data,telefonas,el_pastas,slapyvardis)VALUES(?asmkod,?vardas,?pavarde,?gimdata,?tel,?email,?slapyv);";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?asmkod", MySqlDbType.VarChar).Value = klientas.asmensKodas;
            mySqlCommand.Parameters.Add("?vardas", MySqlDbType.VarChar).Value = klientas.vardas;
            mySqlCommand.Parameters.Add("?pavarde", MySqlDbType.VarChar).Value = klientas.pavarde;
            mySqlCommand.Parameters.Add("?gimdata", MySqlDbType.Date).Value = klientas.gimimoData;
            mySqlCommand.Parameters.Add("?tel", MySqlDbType.VarChar).Value = klientas.telefonas;
            mySqlCommand.Parameters.Add("?email", MySqlDbType.VarChar).Value = klientas.epastas;
            mySqlCommand.Parameters.Add("?slapyv", MySqlDbType.VarChar).Value = klientas.slapyvardis;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
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
    }
}