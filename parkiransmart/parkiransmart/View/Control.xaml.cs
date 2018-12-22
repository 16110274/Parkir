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
using System.Data.SqlClient;

//Tampilan Control
namespace parkiransmart.View
{
    public partial class Control : Window
    {
        private Entity.EntControl control;
        private Implement.ImpControl impControl;
        private Boolean result;
        private DataSet datacontrol;

        public Control()
        {
            control = new Entity.EntControl();
            impControl = new Implement.ImpControl();

            InitializeComponent();
            //pengaturan ukuran tampilan
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

            CheckColour();
        }

        void timer_Tick(object sender, EventArgs e) //fungsi jam
        {
            lblTime.Content = DateTime.Now.ToLongTimeString();
        }

        private void btnInput_Click(object sender, RoutedEventArgs e) //button tab input
        {
            View.Enter i = new View.Enter();
            i.Show();
            this.Close();
        }

        private void btnBayar_Click(object sender, RoutedEventArgs e) //button tab bayar
        {
            View.Exit b = new View.Exit();
            b.Show();
            this.Close();
        }

        private void btnMonitoring_Click(object sender, RoutedEventArgs e) //button tab monitor
        {
            View.Monitor m = new View.Monitor();
            m.Show();
            this.Close();
        }

        private void btnCobakeluar_Click(object sender, RoutedEventArgs e) //button tab logout
        {
            View.Logout l = new View.Logout();
            l.Show();
            this.Close();
        }

        private void buttonbaru_Click(object sender, RoutedEventArgs e) //button input data baru
        {
            InputData(); //input data textbox ke variabel entitas
            result = impControl.InsertData(control);    //panggil fungsi input data
            if (result == true)
            {
                MessageBox.Show("Data Baru Sukses");
            }
            else
            {
                MessageBox.Show("Data Baru Gagal");
            }
            ShowData(labelLokasi.Content.ToString());   //update tampilan data
            CheckColour();  //update warna lokasi
        }

        private void buttonUbah_Click(object sender, RoutedEventArgs e) //button ubah data
        {
            InputData();    //input data textbox ke variabel entitas
            control.Tanggal = DateTime.Parse(textBoxTglMasuk.Text).ToString(@"MM/dd/yyyy"); //swap date & month <- disesuaikan dengan setting regional komputer
            result = impControl.UpdateData(control);    //panggil fungsi update data
            if (result == true)
            {
                MessageBox.Show("Update Sukses");
            }
            else
            {
                MessageBox.Show("Update Gagal");
            }
            ShowData(labelLokasi.Content.ToString());   //update tampilan data
            CheckColour();  //update warna lokasi
        }

        private void buttonhapus_Click(object sender, RoutedEventArgs e) //button hapus data
        {
            InputData();    //input data textbox ke variabel entitas
            result = impControl.DeleteData(control);    //panggil fungsi delete data
            if (result == true)
            {
                MessageBox.Show("Hapus Data Sukses");
            }
            else
            {
                MessageBox.Show("Hapus Data Gagal");
            }
            ShowData(labelLokasi.Content.ToString());   //update tampilan data
            CheckColour();  //update warna lokasi
        }

        private void buttonkunci_Click(object sender, RoutedEventArgs e)    //button kunci lokasi
        {
            impControl.Lock(labelLokasi.Content.ToString());    //panggil fungsi kunci lokasi

            ShowData(labelLokasi.Content.ToString());   //update tampilan data
            CheckColour();  //update warna lokasi
        }

        public void InputData() //fungsi input data textbox ke variabel entitas
        {
            control.Lokasi = labelLokasi.Content.ToString();
            control.Plat = textBoxPlatNo.Text;
            control.Tanggal = textBoxTglMasuk.Text;
            control.Waktu = textBoxJamMasuk.Text;
            if (checkBoxMenginap.IsChecked == true) //penyesuain checkbox
            {
                control.Menginap = "Ya";
            }
            else
            {
                control.Menginap = "Tidak";
            }
            if (checkBoxDenda.IsChecked == true) //penyesuain checkbox
            {
                control.Denda = "Ya";
            }
            else
            {
                control.Denda = "Tidak";
            }
        }

        public void ShowData(string L)  //fungsi menampilkan data dari database ke textbox
        {
            //set awal textbox & chackbox
            labelLokasi.Content = L;
            textBoxPlatNo.Text = "";
            textBoxTglMasuk.Text = "";
            textBoxJamMasuk.Text = "";
            textBoxTglMasuk.IsEnabled = false;
            textBoxJamMasuk.IsEnabled = false;
            checkBoxMenginap.IsChecked = false;
            checkBoxDenda.IsChecked = false;

            datacontrol = new DataSet();        //variabel dataset penyimpan
            datacontrol = impControl.SelectData(L);     //panggil query fungsi tampil data
            dgTest.ItemsSource = datacontrol.Tables["Control"].DefaultView; //masukkan dataset ke datagrid (datagrid tidak ditampilkan)
            dgTest.SelectedIndex = 0;   //Pilih data teratas dari datagrid
            if (dgTest.SelectedItem.ToString() == "System.Data.DataRowView")    //error handling bila query tidak mengembalikan hasil (null)
            {
                DataRowView dataRow = (DataRowView)dgTest.SelectedItem; //masukkan data terpilih ke datarow
                if (dataRow.Row.ItemArray[0].ToString() == "Tidak")     //tampilkan hanya jika lokasi tersebut terisi
                {
                    //tampilkan data dari datarow ke masing masing textbox
                    textBoxPlatNo.Text = dataRow.Row.ItemArray[1].ToString();
                    textBoxTglMasuk.IsEnabled = true;
                    textBoxTglMasuk.Text = (dataRow.Row.ItemArray[2].ToString()).Substring(0, 10); //potong string, hanya data tanggal saja
                    textBoxJamMasuk.IsEnabled = true;
                    textBoxJamMasuk.Text = dataRow.Row.ItemArray[3].ToString();
                    if (dataRow.Row.ItemArray[4].ToString() == "Ya") //penyesuain checkbox
                    {
                        checkBoxMenginap.IsChecked = true;
                    }
                    if (dataRow.Row.ItemArray[5].ToString() == "Ya") //penyesuain checkbox
                    {
                        checkBoxDenda.IsChecked = true;
                    }

                    //fungsi pembanding dengan CCTV
                    if (textBoxCCTV.Text == textBoxPlatNo.Text)
                    {
                        labelCCTV.Foreground = Brushes.Black;
                    }
                    else
                    {
                        labelCCTV.Foreground = Brushes.Red;
                    }

                }
            }
        }

        private void txtCCTVChanged(object sender, TextChangedEventArgs args)
        {
            if (textBoxCCTV.Text == textBoxPlatNo.Text)
            {
                labelCCTV.Foreground = Brushes.Black;
            }
            else
            {
                labelCCTV.Foreground = Brushes.Red;
            }
        }

        private void CheckColour() //fungsi pengecekan warna berdasarkan status lokasi parkir
        {
            //tiap button lokasi dicek dan diberikan warna satu persatu
            result = impControl.CheckAvailability("A1"); //panggil fungsi pengecekan status lokasi parkir
            if (result == false)
            {
                buttonA1.Background = Brushes.Red;
            }
            else
            {
                buttonA1.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("A2");
            if (result == false)
            {
                buttonA2.Background = Brushes.Red;
            }
            else
            {
                buttonA2.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("A3");
            if (result == false)
            {
                buttonA3.Background = Brushes.Red;
            }
            else
            {
                buttonA3.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("A4");
            if (result == false)
            {
                buttonA4.Background = Brushes.Red;
            }
            else
            {
                buttonA4.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("A5");
            if (result == false)
            {
                buttonA5.Background = Brushes.Red;
            }
            else
            {
                buttonA5.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("A6");
            if (result == false)
            {
                buttonA6.Background = Brushes.Red;
            }
            else
            {
                buttonA6.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("A7");
            if (result == false)
            {
                buttonA7.Background = Brushes.Red;
            }
            else
            {
                buttonA7.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("A8");
            if (result == false)
            {
                buttonA8.Background = Brushes.Red;
            }
            else
            {
                buttonA8.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("A9");
            if (result == false)
            {
                buttonA9.Background = Brushes.Red;
            }
            else
            {
                buttonA9.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("B1");
            if (result == false)
            {
                buttonB1.Background = Brushes.Red;
            }
            else
            {
                buttonB1.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("B2");
            if (result == false)
            {
                buttonB2.Background = Brushes.Red;
            }
            else
            {
                buttonB2.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("B3");
            if (result == false)
            {
                buttonB3.Background = Brushes.Red;
            }
            else
            {
                buttonB3.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("B4");
            if (result == false)
            {
                buttonB4.Background = Brushes.Red;
            }
            else
            {
                buttonB4.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("B5");
            if (result == false)
            {
                buttonB5.Background = Brushes.Red;
            }
            else
            {
                buttonB5.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("B6");
            if (result == false)
            {
                buttonB6.Background = Brushes.Red;
            }
            else
            {
                buttonB6.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("B7");
            if (result == false)
            {
                buttonB7.Background = Brushes.Red;
            }
            else
            {
                buttonB7.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("B8");
            if (result == false)
            {
                buttonB8.Background = Brushes.Red;
            }
            else
            {
                buttonB8.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("B9");
            if (result == false)
            {
                buttonB9.Background = Brushes.Red;
            }
            else
            {
                buttonB9.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("C1");
            if (result == false)
            {
                buttonC1.Background = Brushes.Red;
            }
            else
            {
                buttonC1.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("C2");
            if (result == false)
            {
                buttonC2.Background = Brushes.Red;
            }
            else
            {
                buttonC2.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("C3");
            if (result == false)
            {
                buttonC3.Background = Brushes.Red;
            }
            else
            {
                buttonC3.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("C4");
            if (result == false)
            {
                buttonC4.Background = Brushes.Red;
            }
            else
            {
                buttonC4.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("C5");
            if (result == false)
            {
                buttonC5.Background = Brushes.Red;
            }
            else
            {
                buttonC5.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("C6");
            if (result == false)
            {
                buttonC6.Background = Brushes.Red;
            }
            else
            {
                buttonC6.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("C7");
            if (result == false)
            {
                buttonC7.Background = Brushes.Red;
            }
            else
            {
                buttonC7.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("C8");
            if (result == false)
            {
                buttonC8.Background = Brushes.Red;
            }
            else
            {
                buttonC8.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("C9");
            if (result == false)
            {
                buttonC9.Background = Brushes.Red;
            }
            else
            {
                buttonC9.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("D1");
            if (result == false)
            {
                buttonD1.Background = Brushes.Red;
            }
            else
            {
                buttonD1.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("D2");
            if (result == false)
            {
                buttonD2.Background = Brushes.Red;
            }
            else
            {
                buttonD2.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("D3");
            if (result == false)
            {
                buttonD3.Background = Brushes.Red;
            }
            else
            {
                buttonD3.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("D4");
            if (result == false)
            {
                buttonD4.Background = Brushes.Red;
            }
            else
            {
                buttonD4.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("D5");
            if (result == false)
            {
                buttonD5.Background = Brushes.Red;
            }
            else
            {
                buttonD5.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("D6");
            if (result == false)
            {
                buttonD6.Background = Brushes.Red;
            }
            else
            {
                buttonD6.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("D7");
            if (result == false)
            {
                buttonD7.Background = Brushes.Red;
            }
            else
            {
                buttonD7.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("D8");
            if (result == false)
            {
                buttonD8.Background = Brushes.Red;
            }
            else
            {
                buttonD8.Background = Brushes.Lime;
            }

            result = impControl.CheckAvailability("D9");
            if (result == false)
            {
                buttonD9.Background = Brushes.Red;
            }
            else
            {
                buttonD9.Background = Brushes.Lime;
            }
        }

        //Kumpulan fungsi ketika button lokasi parkir ditekan -> menampilkan data pada lokasi parkir tersebut
        private void buttonA1_Click(object sender, RoutedEventArgs e)
        {
            ShowData("A1"); //panggil fungsi tampil data
        }

        private void buttonA2_Click(object sender, RoutedEventArgs e)
        {
            ShowData("A2");
        }

        private void buttonA3_Click(object sender, RoutedEventArgs e)
        {
            ShowData("A3");
        }

        private void buttonA4_Click(object sender, RoutedEventArgs e)
        {
            ShowData("A4");
        }

        private void buttonA5_Click(object sender, RoutedEventArgs e)
        {
            ShowData("A5");
        }

        private void buttonA6_Click(object sender, RoutedEventArgs e)
        {
            ShowData("A6");
        }

        private void buttonA7_Click(object sender, RoutedEventArgs e)
        {
            ShowData("A7");
        }

        private void buttonA8_Click(object sender, RoutedEventArgs e)
        {
            ShowData("A8");
        }

        private void buttonA9_Click(object sender, RoutedEventArgs e)
        {
            ShowData("A9");
        }

        private void buttonB1_Click(object sender, RoutedEventArgs e)
        {
            ShowData("B1");
        }

        private void buttonB2_Click(object sender, RoutedEventArgs e)
        {
            ShowData("B2");
        }

        private void buttonB3_Click(object sender, RoutedEventArgs e)
        {
            ShowData("B3");
        }

        private void buttonB4_Click(object sender, RoutedEventArgs e)
        {
            ShowData("B4");
        }

        private void buttonB5_Click(object sender, RoutedEventArgs e)
        {
            ShowData("B5");
        }

        private void buttonB6_Click(object sender, RoutedEventArgs e)
        {
            ShowData("B6");
        }

        private void buttonB7_Click(object sender, RoutedEventArgs e)
        {
            ShowData("B7");
        }

        private void buttonB8_Click(object sender, RoutedEventArgs e)
        {
            ShowData("B8");
        }

        private void buttonB9_Click(object sender, RoutedEventArgs e)
        {
            ShowData("B9");
        }

        private void buttonC1_Click(object sender, RoutedEventArgs e)
        {
            ShowData("C1");
        }

        private void buttonC2_Click(object sender, RoutedEventArgs e)
        {
            ShowData("C2");
        }

        private void buttonC3_Click(object sender, RoutedEventArgs e)
        {
            ShowData("C3");
        }

        private void buttonC4_Click(object sender, RoutedEventArgs e)
        {
            ShowData("C4");
        }

        private void buttonC5_Click(object sender, RoutedEventArgs e)
        {
            ShowData("C5");
        }

        private void buttonC6_Click(object sender, RoutedEventArgs e)
        {
            ShowData("C6");
        }

        private void buttonC7_Click(object sender, RoutedEventArgs e)
        {
            ShowData("C7");
        }

        private void buttonC8_Click(object sender, RoutedEventArgs e)
        {
            ShowData("C8");
        }

        private void buttonC9_Click(object sender, RoutedEventArgs e)
        {
            ShowData("C9");
        }

        private void buttonD1_Click(object sender, RoutedEventArgs e)
        {
            ShowData("D1");
        }

        private void buttonD2_Click(object sender, RoutedEventArgs e)
        {
            ShowData("D2");
        }

        private void buttonD3_Click(object sender, RoutedEventArgs e)
        {
            ShowData("D3");
        }

        private void buttonD4_Click(object sender, RoutedEventArgs e)
        {
            ShowData("D4");
        }

        private void buttonD5_Click(object sender, RoutedEventArgs e)
        {
            ShowData("D5");
        }

        private void buttonD6_Click(object sender, RoutedEventArgs e)
        {
            ShowData("D6");
        }

        private void buttonD7_Click(object sender, RoutedEventArgs e)
        {
            ShowData("D7");
        }

        private void buttonD8_Click(object sender, RoutedEventArgs e)
        {
            ShowData("D8");
        }

        private void buttonD9_Click(object sender, RoutedEventArgs e)
        {
            ShowData("D9");
        }
    }
}
