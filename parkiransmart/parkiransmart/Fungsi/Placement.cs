using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace parkiransmart.Fungsi
{
    //Kelas yang berisi algoritma penempatan kendaraan secara otomatis
    class Placement
    {
        private string query;
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;

        public Placement()
        {
            connection = KoneksiDB.Koneksi.GetKoneksi();    //Koneksi ke database
        }

        //Lokasi berikutnya = kode lokasi terakhir yang digunakan + 1. bila sampai batas akhir, ulangi dari awal. Lewati jika telah terisi/terkunci
        public string NewPlace() //fungsi Lokasi Berikutnya
        {
            string loc = "LK";
            int sto = 0;
            query = "SELECT TOP 1 RIGHT(No_Lokasi,3) FROM Record_Masuk ORDER BY No_Record_Masuk DESC"; //Ambil kode lokasi yang terakhir digunakan
            try
            {
                if (connection.State == ConnectionState.Open)   //pengecekan jika koneksi masih terbuka, ditutup terlebih dahulu
                {
                    connection.Close();
                }
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = query;
                reader = command.ExecuteReader();
                reader.Read();
                sto = int.Parse(reader.GetString(0).ToString());
                //membuat kode lokasi berikutnya
                do
                {
                    sto++;
                    if (sto < 10)          
                    {
                        loc = "LK00" + sto;
                    }
                    else
                    {
                        if (sto <= 40) // batas akhir = LK040
                        {
                            loc = "LK0" + sto;
                        }
                        else
                        {
                            loc = "LK001"; // jika 40 atau lebih, kembali ke 001
                        }
                    }    
                }while (CheckLoc(loc)); //Ulangi hingga lokasi yang ditunjuk tersedia
                connection.Close();
            }
            catch (SqlException se)
            {
                Console.WriteLine("ERROR " + se);   //error handling
            }
            return loc;
        }
        public Boolean CheckLoc(string Loc)
        {
            Boolean status = true;  //set awal true karena logika true-false akan digunakan pada do while fungsi NewPlace
            string sto;
            query = "SELECT Ketersediaan FROM Lokasi WHERE No_Lokasi = '"+ Loc +"'";    //ambil data ketersediaan lokasi
            try
            {
                if (connection.State == ConnectionState.Open)   //pengecekan jika koneksi masih terbuka, ditutup terlebih dahulu
                {
                    connection.Close();
                }
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = query;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    sto = reader.GetString(0).ToString();
                    if (sto == "Ya")
                    {
                        status = false; //set benar = false karena akan digunakan sebagai terminator do while fungsi NewPlace
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
