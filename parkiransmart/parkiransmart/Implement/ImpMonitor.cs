using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace parkiransmart.Implement
{
    //Kelas implementasi fungsi2 tampilan Monitor
    class ImpMonitor : Interface.IntMonitor
    {
        private SqlConnection connection;
        private SqlCommand command;

        public ImpMonitor()
        {
            connection = KoneksiDB.Koneksi.GetKoneksi();    //koneksi database
        }
        public DataSet SelectData(string query) //menampilkan data query yang diinput ke dalam datagrid
        {
            DataSet ds = new DataSet();
            try
            {
                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.ExecuteNonQuery();
                adapter.Fill(ds, "Monitor");
                connection.Close();
            }
            catch (SqlException err)
            {
                Console.WriteLine("ERROR" + err);
            }
            return ds;
        }
    }
}
