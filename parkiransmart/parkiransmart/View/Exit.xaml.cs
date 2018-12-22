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
using System.Data.SqlClient;
using System.Data;

//Tampilan exit
namespace parkiransmart.View
{
    public partial class Exit : Window
    {
        private Implement.ImpExit impExit;
        private Entity.EntExit entExit;
        private DataSet dataExit;
        private Fungsi.TotalCost harga;

        public Exit()
        {
            entExit = new Entity.EntExit();
            impExit = new Implement.ImpExit();
            harga = new Fungsi.TotalCost();
            InitializeComponent();
            petugas(); //selalu tampilkan nama & id petugas yang sedang login

            //pengaturan tampilan
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            Left = 0;
            Top = 0;
            Width = 1366;
            Height = 768;

            //pengaturan jam pada bagian tengah atas tampilan
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

        }
        void petugas() //fungsi menampilkan nama & id petugas yang sedang login
        {
            dataExit = new DataSet();
            dataExit = impExit.Petugas();
            dgvExit.ItemsSource = dataExit.Tables["P"].DefaultView;
            dgvExit.SelectedIndex = 0;
            DataRowView dataRow = (DataRowView)dgvExit.SelectedItem;
            txtPetIsi.Text = dataRow.Row.ItemArray[0].ToString();
            txtID.Text = dataRow.Row.ItemArray[1].ToString();
        }
        void timer_Tick(object sender, EventArgs e) //fungsi jam
        {
            lblTime.Content = DateTime.Now.ToLongTimeString();
            //isi juga waktu saat ini ke label Keluar
            lblKIsi.Content = DateTime.Now.ToString(@"dd/MM/yyyy");
            lblKIsi1.Content = DateTime.Now.ToString(@"HH") + ':' + DateTime.Now.ToString(@"mm") + ':' + DateTime.Now.ToString(@"ss");
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            View.Enter i = new View.Enter();
            i.Show();
            this.Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            View.Control c = new View.Control();
            c.Show();
            this.Close();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            View.Monitor m = new View.Monitor();
            m.Show();
            this.Close();
        }

        private void btnCobakeluar_Click(object sender, RoutedEventArgs e)
        {
            View.Logout l = new View.Logout();
            l.Show();
            this.Close();
        }
        public void ShowData() //fungsi tampil data dari database
        {
            dataExit = new DataSet();   //variabel dataset penyimpan
            dataExit = impExit.CheckData(txtNoStat.Text); //panggil query fungsi tampil data
            dgvExit.ItemsSource = dataExit.Tables["E"].DefaultView; //masukkan dataset ke datagrid (datagrid tidak ditampilkan)
            dgvExit.SelectedIndex = 0;  //Pilih data teratas dari datagrid
            if (dgvExit.SelectedItem.ToString() == "System.Data.DataRowView")   //error handling bila query tidak mengembalikan hasil (null)
            {
                DataRowView dataRow = (DataRowView)dgvExit.SelectedItem;    //masukkan data terpilih ke datarow
                //tampilkan data dari datarow ke masing masing textbox & label
                txtPlatNoIsi.Text = dataRow.Row.ItemArray[0].ToString();
                txtLokasi.Text = dataRow.Row.ItemArray[1].ToString();
                lblMIsi.Content = dataRow.Row.ItemArray[2].ToString();
                lblMIsi1.Content = dataRow.Row.ItemArray[3].ToString();
                txtTotalIsi.Text = harga.total(lblMIsi.Content.ToString(), lblMIsi1.Content.ToString(), txtNoStat.Text); //Panggil fungsi hitung biaya.
            }
            else
            {
                MessageBox.Show("No Status Salah, Harap Masukkan Dengan Benar");
            }
        }

        private void btnProses_Click(object sender, RoutedEventArgs e) //button proses -> fungsi masukkan data keluar & transaksi
        {
            //masukkan data textbox & label ke variabel entitas
            entExit.Nostatus = txtNoStat.Text;
            entExit.Noplat = txtPlatNoIsi.Text;
            entExit.Lokasi = txtLokasi.Text;
            entExit.Total = txtTotalIsi.Text;
            entExit.Tglkeluar = lblKIsi.Content.ToString();
            entExit.Waktukeluar = lblKIsi1.Content.ToString();
            entExit.Total = txtTotalIsi.Text;
            entExit.Nama = txtPetIsi.Text;
            entExit.Id = txtID.Text;

            //panggil fungsi query memasukkan data ke database
            impExit.ProsesData(entExit);
            impExit.Ketersediaan(entExit);
            MessageBox.Show("Data Telah Disimpan");

            //set textbox & label ke awal
            txtNoStat.Text = "";
            txtPlatNoIsi.Text = "";
            txtLokasi.Text = "";
            txtTotalIsi.Text = "";
            lblKIsi.Content = "";
            lblKIsi1.Content = "";
            txtTotalIsi.Text = "";
            txtNoStat.Focus();
        }

        private void btnCek_Click(object sender, RoutedEventArgs e) //button pengecekan
        {
            ShowData(); //tampilkan data
        }
    }
}
