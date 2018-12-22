using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace parkiransmart.Implement
{
    //Kelas implementasi fungsi2 tampilan exit
    class ImpExit : Interface.IntExit
    {
        private string query;
        private SqlConnection connection;
        private SqlCommand command;

        public ImpExit()
        {
            connection = KoneksiDB.Koneksi.GetKoneksi();    //koneksi database
        }
        public DataSet CheckData(string st)     //fungsi menampilkan data (pengecekan data menggunakan kode status parkir)
        {
            DataSet ds = new DataSet();     //menyimpan hasil menggunakan dataset
            try
            {
                connection.Open();
                query = "select Kendaraan.Plat_No, Lokasi.Kode_Lokasi, Record_Masuk.Tanggal_Masuk, Record_Masuk.Jam_Masuk " +
                    "from Kendaraan, Record_Masuk, Lokasi, Status_Parkir " +
                    "where Kendaraan.No_Kendaraan = Record_Masuk.No_Kendaraan and Lokasi.No_Lokasi = Record_Masuk.No_Lokasi " +
                    "and Record_Masuk.No_Record_Masuk = Status_Parkir.No_Record_Masuk and Status_Parkir.No_Status_Parkir ='" + st + "'";
                command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.ExecuteNonQuery();
                adapter.Fill(ds, "E");      //mengisikan hasil query kedalam dataset
                connection.Close();
            }
            catch (SqlException err)
            {
                Console.WriteLine("ERROR" + err); //error handling
            }
            return ds;
        }
        public DataSet Petugas()    //fungsi mengambil nama & no pegawai yang sedang aktif (login)
        {
            DataSet ds = new DataSet(); //mengambil hasil menggunakan dataset
            try
            {
                connection.Open();
                query = "select top 1 Pegawai.Nama, Pegawai.No_Pegawai " +
                    "from Pegawai, Presensi " +
                    "where Pegawai.No_Pegawai = Presensi.No_Pegawai " +
                    "order by Presensi.No_Presensi desc";
                command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.ExecuteNonQuery();
                adapter.Fill(ds, "P");  //mengisikan hasil query kedalam dataset
                connection.Close();
            }
            catch (SqlException err)
            {
                Console.WriteLine("ERROR" + err); //error handling
            }
            return ds;
        }
        public void ProsesData(Entity.EntExit exit)     //fungsi memasukkan data pembayaran ke dalam database
        {
            Fungsi.CodeGenerate objcode = new Fungsi.CodeGenerate();    //objek fungsi pembuatan kode baru
            string newcode, sto = "";
            for (int i = 0; i < 2; i++)     //2 langkah pembayaran
            {
                switch (i)
                {
                    case 0:     //masukkan data ke tabel Record Keluar
                        newcode = objcode.NewCode("Record_Keluar");
                        query = "insert into Record_Keluar values('" + newcode + "', '" + exit.Id + "', '" + exit.Nostatus + "', '" + exit.Tglkeluar + "', '" + exit.Waktukeluar + "', '" + exit.Total + "')";
                        sto = newcode;
                        break;
                    case 1:     //masukkan data ke tabel transaksi  <--Denil lupa nambahin untuk insert di tabel Transaksi
                        newcode = objcode.NewCode("Transaksi");
                        query = "insert into Transaksi values ('" + newcode + "','" + sto + "','KR001')";
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
                    connection.Close();
                }
                catch (SqlException err)
                {
                    Console.WriteLine("ERROR" + err);   //error handling
                }
            }           
        }
        public void Ketersediaan(Entity.EntExit exit) //mengubah status lokasi parkir setelah pembayaran
        {
            connection.Open();
            query = "update Lokasi set Ketersediaan = 'Ya' where Kode_Lokasi = '" + exit.Lokasi + "'"; //--> bisa digabung dengan fungsi diatas sebenernya
            command = connection.CreateCommand();
            command.CommandText = query;
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}

