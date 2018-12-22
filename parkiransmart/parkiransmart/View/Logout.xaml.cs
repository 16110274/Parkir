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

//Tampilan logout
namespace parkiransmart.View
{
    public partial class Logout : Window
    {
        private Boolean status;
        private Entity.EntLogout logOut;
        private Implement.ImpLogout impLogout;
        public Logout()
        {
            impLogout = new Implement.ImpLogout();
            logOut = new Entity.EntLogout();

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

            Left = 0;
            Top = 0;
            Width = 1366;
            Height = 768;
        }

        private void btnKeluar_Click(object sender, RoutedEventArgs e)  //button logout
        {
            {
                logOut.Password = txtPwd.Password;  //masukkan password ke entitas
                status = impLogout.Logout(logOut);  //jalankan fungsi logout
                if (status == true)
                {
                    impLogout.UpdateKeluar(logOut);
                    MainWindow li = new MainWindow();
                    MessageBox.Show("Terima kasih");
                    this.Close();
                    li.Show();
                }
                else
                {
                    MessageBox.Show("Maaf Username/Password Salah !");
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)  //button batal & kembali ke tampilan monitor
        {
            this.Hide();
            View.Monitor s = new View.Monitor();
            s.Show();
            this.Close();
        }
    }
}
