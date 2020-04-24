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
    public class KartingaiRepository
    {
        public List<KartingaiListViewModel> getKarts()
        {
            List<KartingaiListViewModel> kartingai = new List<KartingaiListViewModel>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT a.kodas, 
                                       a.busena,
                                       m.pavadinimas AS modelis,
                                       mm.pavadinimas AS marke,
                                       mmm.pavadinimas AS grupe
                                       FROM " + @"kartingas a
                                        LEFT JOIN " + @"modelis m ON m.kodas = a.fk_MODELISkodas
                                        LEFT JOIN " + @"grupe mmm ON mmm.kodas = a.fk_GRUPEkodas
                                        LEFT JOIN " + @"marke mm ON mm.kodas = m.fk_MARKEkodas;";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                kartingai.Add(new KartingaiListViewModel
                {
                    kodas = Convert.ToInt32(item["kodas"]),
                    busena = Convert.ToString(item["busena"]),
                    modelis = Convert.ToString(item["modelis"]),
                    marke = Convert.ToString(item["marke"]),
                    grupe = Convert.ToString(item["grupe"])
                });
            }

            return kartingai;
        }

        public bool addKart(KartingaiEditViewModel kartingasEditViewModel)
        {
            int kodas = getNewId();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO `" + @"kartingas`
                                    (
                                    `kodas`,
                                    `pagaminimo_data`,
                                    `rida`,
                                    `verte`,
                                    `vietu_skaicius`,
                                    `busena`,
                                    `fk_GRUPEkodas`,
                                    `fk_MODELISkodas`) 
                                    VALUES (
                                    ?kodas,
                                    ?pag_data,
                                    ?rida,
                                    ?verte,
                                    ?viet_sk,
                                    ?busena,
                                    ?fk_grupe,
                                    ?fk_mod)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?kodas", MySqlDbType.VarChar).Value = kodas;
            kartingasEditViewModel.kodas = kodas;
            mySqlCommand.Parameters.Add("?pag_data", MySqlDbType.Date).Value = kartingasEditViewModel.pagaminimoData.ToString("yyyy-MM-dd");
            mySqlCommand.Parameters.Add("?rida", MySqlDbType.Int32).Value = kartingasEditViewModel.rida;
            mySqlCommand.Parameters.Add("?viet_sk", MySqlDbType.Int32).Value = kartingasEditViewModel.vietuSkaicius;
            mySqlCommand.Parameters.Add("?verte", MySqlDbType.Decimal).Value = kartingasEditViewModel.verte;
            mySqlCommand.Parameters.Add("?busena", MySqlDbType.VarChar).Value = kartingasEditViewModel.busena;
            mySqlCommand.Parameters.Add("?fk_grupe", MySqlDbType.Int32).Value = kartingasEditViewModel.fk_grupe;
            mySqlCommand.Parameters.Add("?fk_mod", MySqlDbType.Int32).Value = kartingasEditViewModel.fk_modelis;
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
                string sqlquery = @"SELECT MAX(kodas) FROM kartingas";
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

        public bool updateKart(KartingaiEditViewModel autoEditViewModel)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE `" + @"kartingas` SET
                                    `pagaminimo_data` = ?pag_data,
                                    `rida` = ?rida,
                                    `verte` = ?verte,
                                    `vietu_skaicius` = ?viet_sk,
                                    `busena` = ?busena,
                                    `fk_GRUPEkodas` = ?fk_grupe,
                                    `fk_MODELISkodas` = ?fk_mod
                                    WHERE kodas=" + autoEditViewModel.kodas;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pag_data", MySqlDbType.Date).Value = autoEditViewModel.pagaminimoData.ToString("yyyy-MM-dd");
            mySqlCommand.Parameters.Add("?rida", MySqlDbType.Int32).Value = autoEditViewModel.rida;
            mySqlCommand.Parameters.Add("?viet_sk", MySqlDbType.Int32).Value = autoEditViewModel.vietuSkaicius;
            mySqlCommand.Parameters.Add("?verte", MySqlDbType.Decimal).Value = autoEditViewModel.verte;      
            mySqlCommand.Parameters.Add("?busena", MySqlDbType.VarChar).Value = autoEditViewModel.busena;
            mySqlCommand.Parameters.Add("?fk_grupe", MySqlDbType.Int32).Value = autoEditViewModel.fk_grupe;
            mySqlCommand.Parameters.Add("?fk_mod", MySqlDbType.Int32).Value = autoEditViewModel.fk_modelis;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }


        public KartingaiEditViewModel getKart(int kodas)
        {
            KartingaiEditViewModel autoEditViewModel = new KartingaiEditViewModel();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT a.kodas, 
                                       a.pagaminimo_data,
                                       a.rida,
                                       a.verte,
                                       a.vietu_skaicius,
                                       a.busena,
                                       a.fk_GRUPEkodas,
                                       a.fk_MODELISkodas
                                       FROM " + @"kartingas a
                                       WHERE a.kodas= " + kodas;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                autoEditViewModel.kodas = Convert.ToInt32(item["kodas"]);
                autoEditViewModel.pagaminimoData = Convert.ToDateTime(item["pagaminimo_data"]);
                autoEditViewModel.rida = Convert.ToInt32(item["rida"]);
                autoEditViewModel.vietuSkaicius = Convert.ToInt32(item["vietu_skaicius"]);
                autoEditViewModel.verte = Convert.ToDecimal(item["verte"]);
                autoEditViewModel.busena = Convert.ToString(item["busena"]);
                autoEditViewModel.fk_grupe = Convert.ToInt32(item["fk_GRUPEkodas"]);
                autoEditViewModel.fk_modelis = Convert.ToInt32(item["fk_MODELISkodas"]);
            }

            return autoEditViewModel;
        }

        public int getKartingasSutarciuCount(int kodas)
        {
            int naudota = 0;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(kodas) as kiekis from sutartis where fk_KARTINGASkodas=" + kodas;
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

        public void deleteKartingas(int kodas)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM kartingas where kodas=?kodas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?kodas", MySqlDbType.Int32).Value = kodas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}