using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Data;

//tampilan Monitor
namespace parkiransmart.View
{
    public partial class Monitor : Window
    {
        private Implement.ImpMonitor impMonitor;
        private DataSet dataMonitor;
        public Monitor()
        {
            impMonitor = new Implement.ImpMonitor();

            InitializeComponent();

            //pengaturan ukuran tampilan
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            Left = 0;
            Top = 0;
            Width = 1366;
            Height = 768;

            //pengaturan jam
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e) //fungsi jam
        {
            lblTime.Content = DateTime.Now.ToLongTimeString();
        }

        private void btnControl_Click(object sender, RoutedEventArgs e)
        {
            View.Control c = new View.Control();
            c.Show();
            this.Close();
        }

        private void btnBayar_Click(object sender, RoutedEventArgs e)
        {
            View.Exit b = new View.Exit();
            b.Show();
            this.Close();
        }

        private void btnInput_Click(object sender, RoutedEventArgs e)
        {
            View.Enter i = new View.Enter();
            i.Show();
            this.Close();
        }

        private void btnCobakeluar_Click(object sender, RoutedEventArgs e)
        {

            View.Logout l = new View.Logout();
            l.Show();
            this.Close();
        }

        /*private void buttonExecute_Click(object sender, RoutedEventArgs e) //button eksekusi
        {
            //masukkan teks pada richtextbox ke variabel
            string Query = new TextRange(richtextboxQuery.Document.ContentStart, richtextboxQuery.Document.ContentEnd).Text;
            dataMonitor = new DataSet();    //dataset penerima hasil query
            //query yang dikirim = SELECT + apa yang tertulis pada richtextbox
            dataMonitor = impMonitor.SelectData("SELECT " + Query); //panggil fungsi
            if(dataMonitor.Tables.Count > 0)    //tampilkan jika fungsi mengambelikan hasil (minimal 1 tabel)
            {
                dgMonitor.ItemsSource = dataMonitor.Tables["Monitor"].DefaultView; //tampilkan dataset pada datagrid
            }
            else
            {
                MessageBox.Show("Query SQL Salah !");
            }
            
        }*/

        private void buttonDaftarLogin_Click(object sender, RoutedEventArgs e)
        {
            string Query = "Select * from Daftar_Login";
            dataMonitor = new DataSet();    //dataset penerima hasil query
            dataMonitor = impMonitor.SelectData(Query); //panggil fungsi
            if (dataMonitor.Tables.Count > 0)    //tampilkan jika fungsi mengambelikan hasil (minimal 1 tabel)
            {
                dgMonitor.ItemsSource = dataMonitor.Tables["Monitor"].DefaultView; //tampilkan dataset pada datagrid
            }
            else
            {
                MessageBox.Show("Data Tidak Ada !");
            }
        }

        private void buttonKendaraanMasuk_Click(object sender, RoutedEventArgs e)
        {
            string Query = "Select * from Kendaraan_Masuk";
            dataMonitor = new DataSet();    //dataset penerima hasil query
            dataMonitor = impMonitor.SelectData(Query); //panggil fungsi
            if (dataMonitor.Tables.Count > 0)    //tampilkan jika fungsi mengambelikan hasil (minimal 1 tabel)
            {
                dgMonitor.ItemsSource = dataMonitor.Tables["Monitor"].DefaultView; //tampilkan dataset pada datagrid
            }
            else
            {
                MessageBox.Show("Data Tidak Ada !");
            }
        }

        private void buttonKendaraanTerparkir_Click(object sender, RoutedEventArgs e)
        {
            string Query = "Select * from Kendaraan_Terparkir";
            dataMonitor = new DataSet();    //dataset penerima hasil query
            dataMonitor = impMonitor.SelectData(Query); //panggil fungsi
            if (dataMonitor.Tables.Count > 0)    //tampilkan jika fungsi mengambelikan hasil (minimal 1 tabel)
            {
                dgMonitor.ItemsSource = dataMonitor.Tables["Monitor"].DefaultView; //tampilkan dataset pada datagrid
            }
            else
            {
                MessageBox.Show("Data Tidak Ada !");
            }
        }

        private void buttonKendaraanKeluar_Click(object sender, RoutedEventArgs e)
        {
            string Query = "Select * from Kendaraan_Keluar";
            dataMonitor = new DataSet();    //dataset penerima hasil query
            dataMonitor = impMonitor.SelectData(Query); //panggil fungsi
            if (dataMonitor.Tables.Count > 0)    //tampilkan jika fungsi mengambelikan hasil (minimal 1 tabel)
            {
                dgMonitor.ItemsSource = dataMonitor.Tables["Monitor"].DefaultView; //tampilkan dataset pada datagrid
            }
            else
            {
                MessageBox.Show("Data Tidak Ada !");
            }
        }

        private void buttonPembayaran_Click(object sender, RoutedEventArgs e)
        {
            string Query = "Select * from Pembayaran";
            dataMonitor = new DataSet();    //dataset penerima hasil query
            dataMonitor = impMonitor.SelectData(Query); //panggil fungsi
            if (dataMonitor.Tables.Count > 0)    //tampilkan jika fungsi mengambelikan hasil (minimal 1 tabel)
            {
                dgMonitor.ItemsSource = dataMonitor.Tables["Monitor"].DefaultView; //tampilkan dataset pada datagrid
            }
            else
            {
                MessageBox.Show("Data Tidak Ada !");
            }
        }
    }
}
