using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace WebApplication1.Repos
{
    public class GrupeRepository
    {
        public List<Grupe> getGrupes()
        {
            List<Grupe> grupes = new List<Grupe>();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT a.kodas, a.pavadinimas FROM grupe a";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                grupes.Add(new Grupe
                {
                    kodas = Convert.ToInt32(item["kodas"]),
                    pavadinimas = Convert.ToString(item["pavadinimas"])
                });
            }

            return grupes;
        }
    }
}