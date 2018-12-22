using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace parkiransmart.Fungsi
{
    //Kelas yang berisi algoritma perhitungan total biaya parkir
    class TotalCost
    {
        private string query;
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;
        public TotalCost()
        {
            connection = KoneksiDB.Koneksi.GetKoneksi();    //Koneksi ke database
        }

        public string total(string tglmasuk, string jammasuk, string nostatus)
        {
            int hasil = 0;
            DateTime join = DateTime.Parse(tglmasuk.Substring(0,10) + " " + jammasuk);
            TimeSpan selisih = DateTime.Now - join;

            //biaya menginap = 50000 per hari
            hasil = hasil + (50000*selisih.Days);

            //biaya parkir: 2 Jam pertama = 4000, 1 Jam berikutnya 2000, maks 5 Jam
            if (selisih.Hours >= 5)
            {
                hasil = hasil + 10000;
            }
            else if (selisih.Hours == 4)
            {
                hasil = hasil + 8000;
            }
            else if (selisih.Hours == 3)
            {
                hasil = hasil + 6000;
            }
            else
            {
                hasil = hasil + 4000;
            }

            //denda = 100000
            if (CheckDenda(nostatus))
            {
                hasil = hasil + 100000;
            }
            return hasil.ToString();
        }
        public Boolean CheckDenda(string nostat)
        {
            Boolean status = false;  
            string sto;
            query = "SELECT Denda FROM Status_Parkir WHERE No_Status_Parkir = '" + nostat + "'";    //ambil data denda
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = query;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    sto = reader.GetString(0).ToString();
                    if (sto == "Ya")
                    {
                        status = true;
                    }
                }
                connection.Close();
            }
            catch (SqlException se)
            {
                Console.WriteLine("ERROR " + se);   //error handling
            }
            return status;
        }
    }
}
