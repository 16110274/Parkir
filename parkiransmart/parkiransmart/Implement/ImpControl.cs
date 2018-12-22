using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace parkiransmart.Implement
{
    //Kelas implementasi fungsi2 tampilan control
    class ImpControl : Interface.IntControl
    {
        private string query;
        private Boolean status;
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;

        public ImpControl()
        {
            connection = KoneksiDB.Koneksi.GetKoneksi();    //koneksi database
        }

        public Boolean InsertData(Entity.EntControl ctrl)   //fungsi menambah data baru
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
                        query = "INSERT into Kendaraan values('"+ newcode +"', '" + ctrl.Plat + "', 'SP001')";
                        sto = newcode;  //simpan kode untuk digunakan di query berikutnya
                        break;
                    case 1:         //masukkan data ke tabel Record_Masuk
                        newcode = objcode.NewCode("Record_Masuk");
                        query = "INSERT into Record_Masuk values('" + newcode + "', '" + sto + "', (SELECT No_Lokasi FROM Lokasi WHERE Kode_Lokasi = '"+ ctrl.Lokasi +"'),'" + DateTime.Now.ToString(@"MM/dd/yyyy") + "','" + DateTime.Now.ToString(@"HH") + ':' + DateTime.Now.ToString(@"mm") + ':' + DateTime.Now.ToString(@"ss") + "')";
                        sto = newcode;  //simpan kode untuk digunakan di query berikutnya
                        break;
                    case 2:         //masukkan data ke tabel Status_Parkir
                        newcode = objcode.NewCode("Status_Parkir");
                        query = "INSERT into Status_Parkir values('" + newcode + "', '" + sto + "','" + ctrl.Menginap + "','" + ctrl.Denda + "')";
                        break;
                    case 3:         //ubah data ketersediaan lokasi di tabel Lokasi
                        query = "UPDATE Lokasi set Ketersediaan = 'Tidak' WHERE Kode_Lokasi = '" + ctrl.Lokasi+ "'";
                        break;
                    default:
                        query = "";
                        break;
                }
                try           //Eksekusi tiap perintah query
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
        public Boolean UpdateData(Entity.EntControl ctrl)   //fungsi merubah data database
        {
            string tb, cl, val, key;
            status = false;
            for (int i = 0; i < 5; i++)     //5 langkah update data
            {
                switch (i)
                {
                    case 0:     //ubah data plat
                        tb = "Kendaraan";
                        cl = "Plat_No";
                        val = ctrl.Plat;
                        key = "No_Kendaraan";
                        break;
                    case 1:     //ubah data tanggal
                        tb = "Record_Masuk";
                        cl = "Tanggal_Masuk";
                        val = ctrl.Tanggal;
                        key = "No_Record_Masuk";
                        break;
                    case 2:     //ubah data jam
                        tb = "Record_Masuk";
                        cl = "Jam_Masuk";
                        val = ctrl.Waktu;
                        key = "No_Record_Masuk";
                        break;
                    case 3:     //ubah data menginap
                        tb = "Status_Parkir";
                        cl = "Menginap";
                        val = ctrl.Menginap;
                        key = "No_Status_Parkir";
                        break;
                    case 4:     //ubah data denda
                        tb = "Status_Parkir";
                        cl = "Denda";
                        val = ctrl.Denda;
                        key = "No_Status_Parkir";
                        break;
                    default:
                        tb = "";
                        cl = "";
                        val = "";
                        key = "";
                        break;
                }
                try     //eksekusi perintah sql
                {
                    if (connection.State == ConnectionState.Open)    //pengecekan jika koneksi masih terbuka, ditutup terlebih dahulu
                    {
                        connection.Close();
                    }
                    connection.Open();
                    query = "UPDATE " + tb + " set " + cl + " = '" + val +"' where " + key + " IN" +
                            "(SELECT top 1 " + tb + "." + key + " " +
                            "FROM Record_Masuk, Lokasi, Kendaraan, Status_Parkir " +
                            "WHERE Record_Masuk.No_Lokasi = Lokasi.No_Lokasi and Record_Masuk.No_Kendaraan = Kendaraan.No_Kendaraan " +
                            "and Record_Masuk.No_Record_Masuk = Status_Parkir.No_Record_Masuk and Lokasi.Kode_Lokasi = '" + ctrl.Lokasi +"' " +
                            "ORDER BY Record_Masuk.No_Record_Masuk DESC)";
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
        public Boolean DeleteData(Entity.EntControl ctrl)   //fungsi menghapus data di database
        {
            string st = ""; 
            string sto = "";
            status = false;
            for (int i = 0; i < 5; i++)     //5 langkah penghapusan data
            {
                switch (i)
                {
                    case 0:     //simpan terlebih dahulu No_Kendaraan yang akan dihapus
                        query = "SELECT TOP 1 Kendaraan.No_Kendaraan " +
                                "FROM Status_Parkir, Record_Masuk, Lokasi, Kendaraan " +
                                "WHERE Record_Masuk.No_Kendaraan = Kendaraan.No_Kendaraan and Record_Masuk.No_Lokasi = Lokasi.No_Lokasi and " +
                                "Record_Masuk.No_Record_Masuk = Status_Parkir.No_Record_Masuk and Lokasi.Kode_Lokasi = '" + ctrl.Lokasi + "' " +
                                "ORDER BY No_Status_Parkir DESC";                       
                        break;
                    case 1:     //ubah ketersediaan lokasi parkir
                        sto = st;   //simpan No_Kendaraan disini
                        query = "UPDATE Lokasi SET Ketersediaan = 'Ya' WHERE Kode_Lokasi = '" + ctrl.Lokasi + "'";
                        break;
                    case 2:     //hapus dari tabel Status_Parkir
                        query = "DELETE FROM Status_Parkir WHERE No_Status_Parkir = " +
                                "(SELECT TOP 1 Status_Parkir.No_Status_Parkir " +
                                "FROM Status_Parkir, Record_Masuk, Lokasi, Kendaraan " +
                                "WHERE Record_Masuk.No_Kendaraan = Kendaraan.No_Kendaraan and Record_Masuk.No_Lokasi = Lokasi.No_Lokasi and " +
                                "Record_Masuk.No_Record_Masuk = Status_Parkir.No_Record_Masuk and Lokasi.Kode_Lokasi = '" + ctrl.Lokasi + "' " +
                                "ORDER BY No_Status_Parkir DESC)";
                        break;
                    case 3:     //hapus dari tabel Record_Masuk
                        query = "DELETE FROM Record_Masuk WHERE No_Record_Masuk = " +
                                "(SELECT TOP 1 Record_Masuk.No_Record_Masuk " +
                                "FROM Record_Masuk, Lokasi, Kendaraan " +
                                "WHERE Record_Masuk.No_Kendaraan = Kendaraan.No_Kendaraan and Record_Masuk.No_Lokasi = Lokasi.No_Lokasi and " +
                                "Lokasi.Kode_Lokasi = '" + ctrl.Lokasi + "' " +
                                "ORDER BY No_Record_Masuk DESC)";
                        break;
                    case 4:     //hapus dari tabel Kendaraan (sesuai lokasi parkir yang telah tersimpan)
                        query = "DELETE FROM Kendaraan WHERE No_Kendaraan = '" + sto + "'";
                        break;
                    default:
                        query = "";
                        break;
                }
                try    //eksekusi perintah sql
                {
                    if (connection.State == ConnectionState.Open)       //pengecekan jika koneksi masih terbuka, ditutup terlebih dahulu
                    {
                        connection.Close();
                    }
                    connection.Open();
                    command = connection.CreateCommand();
                    command.CommandText = query;
                    if (query.Substring(0, 6) == "SELECT")      //khusus untuk perintah SELECT, baca hasil query
                    {
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            st = reader.GetString(0).ToString();    //digunakan untuk menyimpan No_Kendaraan
                        }
                    }
                    else
                    {
                        command.ExecuteNonQuery();      //perintah lain, eksekusi saja
                    }
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
        public DataSet SelectData(string code)  //perintah untuk menampilkan data dari database
        {
            DataSet ds = new DataSet();     //menyimpan hasil menggunakan dataset
            try
            {
                if (connection.State == ConnectionState.Open)   //pengecekan jika koneksi masih terbuka, ditutup terlebih dahulu
                {
                    connection.Close();
                }
                connection.Open();
                query = "SELECT L.Ketersediaan, K.Plat_No, RM.Tanggal_Masuk, RM.Jam_Masuk, SP.Menginap, SP.Denda " +
                        "FROM Record_Masuk AS RM,Lokasi AS L,Kendaraan AS K,Status_Parkir AS SP " +
                        "WHERE RM.No_Lokasi = L.No_Lokasi and RM.No_Kendaraan = K.No_Kendaraan and RM.No_Record_Masuk = SP.No_Record_Masuk and L.Kode_Lokasi = '"+ code +"' " +
                        "ORDER BY RM.No_Record_Masuk DESC";
                command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.ExecuteNonQuery();
                adapter.Fill(ds,"Control");     //mengisikan hasil query ke dalam dataset
                connection.Close();
            }
            catch (SqlException err)
            {
                Console.WriteLine("ERROR" + err); //error handling
            }
            return ds;
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
        public void Lock(string code)      // perintah untuk mengunci lokasi parkir (mengubah status lokasi parkir)
        {
            string val;
            Boolean ava = CheckAvailability(code);  //mengunakan fungsi pengecekan status parkir untuk mengetahui status saat ini
            if (ava == true)
            {
                val = "Tidak";
            }
            else
            {
                val = "Ya";
            }
            try
            {
                if (connection.State == ConnectionState.Open)   //pengecekan jika koneksi masih terbuka, ditutup terlebih dahulu
                {
                    connection.Close();
                }
                connection.Open();
                query = "update Lokasi set Ketersediaan = '"+ val +"' where Kode_Lokasi = '"+ code +"'";
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
}
