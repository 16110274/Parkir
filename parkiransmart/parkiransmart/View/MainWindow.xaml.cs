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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Data.SqlClient;

//Tampilan login
namespace parkiransmart
{
    public partial class MainWindow : Window
    {
        private Boolean status;
        private Entity.EntLogin login;
        private Implement.ImpLogin impLogin;

        public MainWindow()
        {
            impLogin = new Implement.ImpLogin();
            login = new Entity.EntLogin();

            //cek status koneksi
            SqlConnection koneksi = KoneksiDB.Koneksi.GetKoneksi();
            try
            {
                koneksi.Open();
                Console.WriteLine("Koneksi Sukses");
            }
            catch (Exception e)
            {
                Console.WriteLine("Koneksi Gagal \n" + e);
            }

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

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e) //button login
        {
            //masukkan text dari textbox ke entitas
            login.Kode = txtUsername.Text;
            login.Password = txtPassword.Password;
            status = impLogin.Login(login);     //panggil fungsi login
            if (status == true)
            {
                impLogin.InputQuery(login);     //jalankan fungsi input presensi
                this.Hide();
                View.Enter s = new View.Enter();    //tampilkan tampilan enter
                s.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Maaf Username/Password Salah !");
                //set ulang textbox
                txtUsername.Text = "";
                txtPassword.Password = "";
                txtUsername.Focus();
            }
        }
    }
}
