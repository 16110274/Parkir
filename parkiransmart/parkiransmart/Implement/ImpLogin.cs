using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace parkiransmart.Implement
{
    //Kelas implementasi fungsi2 tampilan Login
    class ImpLogin : Interface.IntLogin
    {
        private string query;
        private Boolean status;
        private SqlConnection koneksi;

        public ImpLogin()
        {
            koneksi = KoneksiDB.Koneksi.GetKoneksi();   //koneksi database
        }

        public Boolean Login(Entity.EntLogin e)     //fungsi login berdasarkan data di database. Kode = No Pegawai. Password = Nama Pegawai
        {
            query = "SELECT No_Pegawai, Nama FROM Pegawai WHERE No_Pegawai = '" + e.Kode + "' AND Nama = '" + e.Password + "'";
            koneksi.Open();
            SqlCommand command = koneksi.CreateCommand();
            command.CommandText = query;
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                if ((reader.GetString(0).ToString() == e.Kode) && (reader.GetString(1).ToString() == e.Password))   //pengecekan apakah kode & password ada di database
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            koneksi.Close();
            return status;
        }

        public void InputQuery(Entity.EntLogin e)   //fungsi memasukan data login ke tabel presensi
        {
            Fungsi.CodeGenerate objcode = new Fungsi.CodeGenerate();    //objek pembuatan kode baru
            string newcode = objcode.NewCode("Presensi");
            koneksi.Open();
            // catat pada presensi kode pegawai yang login dan waktu login (sekarang)
            query = "insert into Presensi values('" + newcode + "', '" + e.Kode + "', '" + DateTime.Now.ToString(@"MM/dd/yyyy HH")+':'+ DateTime.Now.ToString(@"mm") +':'+ DateTime.Now.ToString(@"ss") + "', '')";
            SqlCommand command = koneksi.CreateCommand();
            command.CommandText = query;
            command.ExecuteNonQuery();
            koneksi.Close();
        }



    }
}
