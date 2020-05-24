using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using WebApplication1.ViewModels;
using MySql.Data.MySqlClient;

namespace WebApplication1.Repos
{
    public class AtaskaituRepository
    {
        public List<KartinguAtaskaitaViewModel> getKartingai(DateTime ?nuo, DateTime? iki)
        {
            List<KartinguAtaskaitaViewModel> kartingai = new List<KartinguAtaskaitaViewModel>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT a.kodas,
                                       a.pagaminimo_data,
                                       m.pavadinimas AS modelis,
                                       mm.pavadinimas AS marke,
                                       mmm.pavadinimas AS grupe
                                       FROM " + @"kartingas a
                                        LEFT JOIN " + @"modelis m ON m.kodas = a.fk_MODELISkodas
                                        LEFT JOIN " + @"grupe mmm ON mmm.kodas = a.fk_GRUPEkodas
                                        LEFT JOIN " + @"marke mm ON mm.kodas = m.fk_MARKEkodas 
                                        where a.pagaminimo_data>=IFNULL(?nuo, a.pagaminimo_data) and a.pagaminimo_data<=IFNULL(?iki, a.pagaminimo_data)
                                        order by a.pagaminimo_data ASC";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?nuo", MySqlDbType.DateTime).Value = nuo;
            mySqlCommand.Parameters.Add("?iki", MySqlDbType.DateTime).Value = iki;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                kartingai.Add(new KartinguAtaskaitaViewModel
                {
                    kodas = Convert.ToInt32(item["kodas"]),
                    pagaminimo_data = Convert.ToDateTime(item["pagaminimo_data"]),
                    modelis = Convert.ToString(item["modelis"]),
                    marke = Convert.ToString(item["marke"]),
                    grupe = Convert.ToString(item["grupe"]),
                });
            }
            return kartingai;
        }

        public AtaskaitaViewModel getKartinguSkaiciu(DateTime ?nuo, DateTime ?iki)
        {
            AtaskaitaViewModel viso = new AtaskaitaViewModel();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"select count(a.kodas) as suma from kartingas a where a.pagaminimo_data>=IFNULL(?nuo, a.pagaminimo_data) and a.pagaminimo_data<=IFNULL(?iki, a.pagaminimo_data)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?nuo", MySqlDbType.DateTime).Value = nuo;
            mySqlCommand.Parameters.Add("?iki", MySqlDbType.DateTime).Value = iki;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                viso.suma = Convert.ToInt32(item["suma"] == System.DBNull.Value ? 0 : item["suma"]);
            }

            return viso;
        }
    }
}