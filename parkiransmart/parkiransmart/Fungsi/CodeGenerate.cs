using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace parkiransmart.Fungsi
{
    //Kelas yang berisi fungsi untuk membuat kode (primary key tabel) baru
    class CodeGenerate
    {
        private string query;
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;

        public CodeGenerate()
        {
            connection = KoneksiDB.Koneksi.GetKoneksi();    //Koneksi ke database
        }

        public string NewCode(string table)
        {
            string code = "";
            string KD = "";
            int sto = 0;

            //Menyesuaikan huruf kode sesuai tabel
            if (table == "Spesifikasi_Kendaraan")
            {
                KD = "SP";
            }
            if (table == "Kendaraan")
            {
                KD = "KD";
            }
            if (table == "Record_Masuk")
            {
                KD = "RM";
            }
            if (table == "Lokasi")
            {
                KD = "LK";
            }
            if (table == "Status_Parkir")
            {
                KD = "PK";
            }
            if (table == "Record_Keluar")
            {
                KD = "RK";
            }
            if (table == "Pegawai")
            {
                KD = "PG";
            }
            if (table == "Presensi")
            {
                KD = "PR";
            }
            if (table == "Transaksi")
            {
                KD = "TR";
            }
            if (table == "Kartu")
            {
                KD = "KR";
            }

            try
            {
                query = "SELECT MAX(RIGHT(No_"+ table +", 3)) FROM "+ table; //mengambil nilai tertinggi dari kode (kode paling terakhir)
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = query;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    sto = int.Parse(reader.GetString(0).ToString());
                    if (sto < 10)                                           //membuat kode baru
                    {
                        code = KD + "00" + (sto + 1);
                    }
                    else
                    {
                        if (sto < 100)
                        {
                            code = KD + "0" + (sto + 1);
                        }
                        else
                        {
                            code = KD + (sto + 1);
                        }
                    }
                }
                connection.Close();
            }
            catch (SqlException se)
            {
                Console.WriteLine("ERROR " + se);   //error handling
            }

            return code;
        }

    }
}
