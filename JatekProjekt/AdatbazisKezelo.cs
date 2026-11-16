using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace JatekProjekt
{

    public class AdatbazisKezelo
    {
        private readonly string connectionString;


        public AdatbazisKezelo()
        {

            this.connectionString = "server=localhost;database=jatekdb;uid=root;pwd=;port=3306;SslMode=none;";// Irjuk meg a connectionstringet a megfelelő adatbázisra hivatkozva
        }

   
        /// Beolvassa az összes karaktert az adatbázisból egy List<Karakter>-be.
       
        public List<Karakter> AdatokBetoltese()
        {
            var karakterek = new List<Karakter>();
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "Select * FROM karakterek ";

                    using (MySqlCommand parancs = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = parancs.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32("id");
                                string nev = reader.GetString("nev");
                                string osztaly = reader.GetString("osztaly");
                                int szint = reader.GetInt32("szint");
                                int arany = reader.GetInt32("arany");
                                string szerver = reader.GetString("szerver");

                                Karakter karakter = new Karakter(id, nev, osztaly, szint, arany, szerver);

                                karakterek.Add(karakter);
                            }
                        }
                    }

                }
            } catch (MySqlException ex)
            {
                Console.WriteLine("Adatbázis hiba: "+ ex.Message);
            }catch (Exception ex)
            {
                Console.WriteLine("Hiba történt: " + ex.Message);
            }

            return karakterek;
        }
    }
}
