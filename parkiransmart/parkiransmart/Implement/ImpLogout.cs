using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace parkiransmart.Implement
{
    //Kelas implementasi fungsi2 tampilan Logout
    class ImpLogout : Interface.IntLogout
    {
        private string query;
        private Boolean status;
        private SqlConnection koneksi;

        public ImpLogout()
        {
            koneksi = KoneksiDB.Koneksi.GetKoneksi();   //koneksi database
        }

        public Boolean Logout(Entity.EntLogout e) //fungsi logout. hanya user yang sedang login (user yang terakhir login) yang bisa melakukan logout
        {
            query = "select top 1 Pegawai.nama from Pegawai join Presensi on Pegawai.No_Pegawai = Presensi.No_Pegawai order by Presensi.No_Presensi desc";

            koneksi.Open();

            SqlCommand command = koneksi.CreateCommand();
            command.CommandText = query;
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                if ((reader.GetString(0).ToString() == e.Password)) //pengecekan password. apakah password yang diinput = password milik pegawai yang sedang login
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
        
        public void UpdateKeluar(Entity.EntLogout e) //fungsi update data presensi
        {
            koneksi.Open();
            //update tabel presensi, waktu logout dengan waktu sekarang
            query = "Update Presensi set Waktu_Logout='" + DateTime.Now.ToString(@"MM/dd/yyyy HH") + ':' + DateTime.Now.ToString(@"mm") + ':' + DateTime.Now.ToString(@"ss") + "' where No_Presensi in (select top 1 Presensi.No_Presensi from Pegawai join Presensi on Pegawai.No_Pegawai = Presensi.No_Pegawai order by Presensi.No_Presensi desc)";

            SqlCommand command = koneksi.CreateCommand();
            command.CommandText = query;
            command.ExecuteNonQuery();

            koneksi.Close();
        }

    }
}