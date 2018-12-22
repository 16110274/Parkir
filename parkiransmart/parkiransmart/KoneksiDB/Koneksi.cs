using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace parkiransmart.KoneksiDB
{
    //Kelas fungsi koneksi database
    //Menghubungkan Project dengan server database
    class Koneksi
    {
        public static System.Data.SqlClient.SqlConnection GetKoneksi()
        {
            System.Data.SqlClient.SqlConnection koneksi = new System.Data.SqlClient.SqlConnection();
            koneksi.ConnectionString = "Data Source = ASUS-K401UQ;"+
                "Initial Catalog = Parkir; Integrated Security = True;";
            return koneksi;
        }
    }
}