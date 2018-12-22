using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace parkiransmart.Implement
{
    //Kelas implementasi fungsi2 tampilan enter
    class ImpEnter : Interface.IntEnter
    {
        private string query;
        private Boolean status;
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;

        public ImpEnter()
        {
            connection = KoneksiDB.Koneksi.GetKoneksi();    //koneksi database
        }

        public String ShowLoc()   
        {
            Fungsi.Placement objplacement = new Fungsi.Placement();
            string code = objplacement.NewPlace();
            string sto ="";
            try
            {
                connection.Open();
                query = "SELECT Kode_Lokasi FROM Lokasi WHERE No_Lokasi = '" + code + "'";
                command = connection.CreateCommand();
                command.CommandText = query;
                reader = command.ExecuteReader();

                while (reader.Read())       //membaca hasil query
                {
                    sto = reader.GetString(0).ToString();
                }
                connection.Close();
            }
            catch (SqlException err)
            {
                Console.WriteLine("ERROR" + err);   //error handling
            }
            return sto;
        }

        public Boolean CheckAvailability(string code)   //perintah pengecekan status lokasi parkir
        {
            string sto;
            try
            {
                if (connection.State == ConnectionState.Open)   //pengecekan jika koneksi masih terbuka, ditutup terlebih dahulu
                {
                    connection.Close();
                }
                connection.Open();
                query = "SELECT Ketersediaan FROM Lokasi WHERE Kode_Lokasi = '" + code + "'";
                command = connection.CreateCommand();
                command.CommandText = query;
                reader = command.ExecuteReader();

                while (reader.Read())       //membaca hasil query
                {
                    sto = reader.GetString(0).ToString();
                    if (sto == "Ya")
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                }
                connection.Close();
            }
            catch (SqlException err)
            {
                Console.WriteLine("ERROR" + err);   //error handling
            }
            return status;
        }

        public Boolean KendaraanMasuk(Entity.EntEnter enter)     //fungsi memasukkan data baru ke database
        {
            Fungsi.CodeGenerate objcode = new Fungsi.CodeGenerate();    //objek fungsi pembuatan kode baru
            string newcode, sto = "";
            status = false;
            for (int i = 0; i < 4; i++)     //4 langkah penambahan data baru
            {
                switch (i)
                {
                    case 0:         //masukkan data ke tabel Kendaraan
                        newcode = objcode.NewCode("Kendaraan");
                        query = "INSERT into Kendaraan values('" + newcode + "', '" + enter.Plat + "', 'SP001')";
                        sto = newcode;  //simpan kode untuk digunakan di query berikutnya
                        break;
                    case 1:         //masukkan data ke tabel Record_Masuk
                        newcode = objcode.NewCode("Record_Masuk");
                        query = "INSERT into Record_Masuk values('" + newcode + "', '" + sto + "', (SELECT No_Lokasi FROM Lokasi WHERE Kode_Lokasi = '" + enter.Lokasi + "'),'" + DateTime.Now.ToString(@"MM/dd/yyyy") + "','" + DateTime.Now.ToString(@"HH") + ':' + DateTime.Now.ToString(@"mm") + ':' + DateTime.Now.ToString(@"ss") + "')";
                        sto = newcode;  //simpan kode untuk digunakan di query berikutnya
                        break;
                    case 2:         //masukkan data ke tabel Status_Parkir
                        newcode = objcode.NewCode("Status_Parkir");
                        query = "INSERT into Status_Parkir values('" + newcode + "', '" + sto + "','Tidak','Tidak')";
                        break;
                    case 3:         //ubah data ketersediaan lokasi di tabel Lokasi
                        query = "UPDATE Lokasi set Ketersediaan = 'Tidak' WHERE Kode_Lokasi = '" + enter.Lokasi + "'";
                        break;
                    default:
                        query = "";
                        break;
                }
                try
                {
                    if (connection.State == ConnectionState.Open)   //pengecekan jika koneksi masih terbuka, ditutup terlebih dahulu
                    {
                        connection.Close();
                    }
                    connection.Open();
                    command = connection.CreateCommand();
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                    status = true;
                    connection.Close();
                }
                catch (SqlException err)
                {
                    Console.WriteLine("ERROR" + err);   //error handling
                }
            }
            return status;
        }
    }
}
