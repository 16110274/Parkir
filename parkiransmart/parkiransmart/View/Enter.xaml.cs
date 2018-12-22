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

//Tampilan Enter
namespace parkiransmart.View
{
    public partial class Enter : Window
    {
        private Entity.EntEnter enter;
        private Implement.ImpEnter impEnter;
        private Boolean result;
        public Enter()
        {
            enter = new Entity.EntEnter();
            impEnter = new Implement.ImpEnter();
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            Left = 0;
            Top = 0;
            Width = 1366;
            Height = 768;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            CheckColour();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            lblTime.Content = DateTime.Now.ToLongTimeString();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            View.Exit b = new View.Exit();
            b.Show();
            this.Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button2_Click_1(object sender, RoutedEventArgs e)
        {
            View.Control c = new View.Control();
            c.Show();
            this.Close();
        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            View.Exit b = new View.Exit();
            b.Show();
            this.Close();

        }

        private void btnMonitoring_Click(object sender, RoutedEventArgs e)
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

        private void button3_Click(object sender, RoutedEventArgs e) //Button Generate
        {
            //input data ke entitas
            textBox.Text=impEnter.ShowLoc();
            enter.Lokasi = textBox.Text;
            enter.Plat = txtPlat.Text;
            result = impEnter.KendaraanMasuk(enter);   //panggil fungsi input data
            if (result == true)
            {
                MessageBox.Show("Data Baru Sukses");
            }
            else
            {
                MessageBox.Show("Data Baru Gagal");
            }
            CheckColour(); //update warna lokasi
        }

        private void CheckColour() //fungsi pengecekan warna berdasarkan status lokasi parkir
        {
            //tiap button lokasi dicek dan diberikan warna satu persatu
            result = impEnter.CheckAvailability("A1"); //panggil fungsi pengecekan status lokasi parkir
            if (result == false)
            {
                buttonA1.Background = Brushes.Red;
            }
            else
            {
                buttonA1.Background = Brushes.Lime;
            }
            if (textBox.Text == "A1") //ubah warna jadi kuning untuk lokasi ter-generate
            {
                buttonA1.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("A2");
            if (result == false)
            {
                buttonA2.Background = Brushes.Red;
            }
            else
            {
                buttonA2.Background = Brushes.Lime;
            }
            if (textBox.Text == "A2")
            {
                buttonA2.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("A3");
            if (result == false)
            {
                buttonA3.Background = Brushes.Red;
            }
            else
            {
                buttonA3.Background = Brushes.Lime;
            }
            if (textBox.Text == "A3")
            {
                buttonA3.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("A4");
            if (result == false)
            {
                buttonA4.Background = Brushes.Red;
            }
            else
            {
                buttonA4.Background = Brushes.Lime;
            }
            if (textBox.Text == "A4")
            {
                buttonA4.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("A5");
            if (result == false)
            {
                buttonA5.Background = Brushes.Red;
            }
            else
            {
                buttonA5.Background = Brushes.Lime;
            }
            if (textBox.Text == "A5")
            {
                buttonA5.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("A6");
            if (result == false)
            {
                buttonA6.Background = Brushes.Red;
            }
            else
            {
                buttonA6.Background = Brushes.Lime;
            }
            if (textBox.Text == "A6")
            {
                buttonA6.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("A7");
            if (result == false)
            {
                buttonA7.Background = Brushes.Red;
            }
            else
            {
                buttonA7.Background = Brushes.Lime;
            }
            if (textBox.Text == "A7")
            {
                buttonA7.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("A8");
            if (result == false)
            {
                buttonA8.Background = Brushes.Red;
            }
            else
            {
                buttonA8.Background = Brushes.Lime;
            }
            if (textBox.Text == "A8")
            {
                buttonA8.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("A9");
            if (result == false)
            {
                buttonA9.Background = Brushes.Red;
            }
            else
            {
                buttonA9.Background = Brushes.Lime;
            }
            if (textBox.Text == "A9")
            {
                buttonA9.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("B1");
            if (result == false)
            {
                buttonB1.Background = Brushes.Red;
            }
            else
            {
                buttonB1.Background = Brushes.Lime;
            }
            if (textBox.Text == "B1")
            {
                buttonB1.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("B2");
            if (result == false)
            {
                buttonB2.Background = Brushes.Red;
            }
            else
            {
                buttonB2.Background = Brushes.Lime;
            }
            if (textBox.Text == "B2")
            {
                buttonB2.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("B3");
            if (result == false)
            {
                buttonB3.Background = Brushes.Red;
            }
            else
            {
                buttonB3.Background = Brushes.Lime;
            }
            if (textBox.Text == "B3")
            {
                buttonB3.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("B4");
            if (result == false)
            {
                buttonB4.Background = Brushes.Red;
            }
            else
            {
                buttonB4.Background = Brushes.Lime;
            }
            if (textBox.Text == "B4")
            {
                buttonB4.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("B5");
            if (result == false)
            {
                buttonB5.Background = Brushes.Red;
            }
            else
            {
                buttonB5.Background = Brushes.Lime;
            }
            if (textBox.Text == "B5")
            {
                buttonB5.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("B6");
            if (result == false)
            {
                buttonB6.Background = Brushes.Red;
            }
            else
            {
                buttonB6.Background = Brushes.Lime;
            }
            if (textBox.Text == "B6")
            {
                buttonB6.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("B7");
            if (result == false)
            {
                buttonB7.Background = Brushes.Red;
            }
            else
            {
                buttonB7.Background = Brushes.Lime;
            }
            if (textBox.Text == "B7")
            {
                buttonB7.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("B8");
            if (result == false)
            {
                buttonB8.Background = Brushes.Red;
            }
            else
            {
                buttonB8.Background = Brushes.Lime;
            }
            if (textBox.Text == "B8")
            {
                buttonB8.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("B9");
            if (result == false)
            {
                buttonB9.Background = Brushes.Red;
            }
            else
            {
                buttonB9.Background = Brushes.Lime;
            }
            if (textBox.Text == "B9")
            {
                buttonB9.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("C1");
            if (result == false)
            {
                buttonC1.Background = Brushes.Red;
            }
            else
            {
                buttonC1.Background = Brushes.Lime;
            }
            if (textBox.Text == "C1")
            {
                buttonC1.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("C2");
            if (result == false)
            {
                buttonC2.Background = Brushes.Red;
            }
            else
            {
                buttonC2.Background = Brushes.Lime;
            }
            if (textBox.Text == "C2")
            {
                buttonC2.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("C3");
            if (result == false)
            {
                buttonC3.Background = Brushes.Red;
            }
            else
            {
                buttonC3.Background = Brushes.Lime;
            }
            if (textBox.Text == "C3")
            {
                buttonC3.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("C4");
            if (result == false)
            {
                buttonC4.Background = Brushes.Red;
            }
            else
            {
                buttonC4.Background = Brushes.Lime;
            }
            if (textBox.Text == "C4")
            {
                buttonC4.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("C5");
            if (result == false)
            {
                buttonC5.Background = Brushes.Red;
            }
            else
            {
                buttonC5.Background = Brushes.Lime;
            }
            if (textBox.Text == "C5")
            {
                buttonC5.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("C6");
            if (result == false)
            {
                buttonC6.Background = Brushes.Red;
            }
            else
            {
                buttonC6.Background = Brushes.Lime;
            }
            if (textBox.Text == "C6")
            {
                buttonC6.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("C7");
            if (result == false)
            {
                buttonC7.Background = Brushes.Red;
            }
            else
            {
                buttonC7.Background = Brushes.Lime;
            }
            if (textBox.Text == "C7")
            {
                buttonC7.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("C8");
            if (result == false)
            {
                buttonC8.Background = Brushes.Red;
            }
            else
            {
                buttonC8.Background = Brushes.Lime;
            }
            if (textBox.Text == "C8")
            {
                buttonC8.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("C9");
            if (result == false)
            {
                buttonC9.Background = Brushes.Red;
            }
            else
            {
                buttonC9.Background = Brushes.Lime;
            }
            if (textBox.Text == "C9")
            {
                buttonC9.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("D1");
            if (result == false)
            {
                buttonD1.Background = Brushes.Red;
            }
            else
            {
                buttonD1.Background = Brushes.Lime;
            }
            if (textBox.Text == "D1")
            {
                buttonD1.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("D2");
            if (result == false)
            {
                buttonD2.Background = Brushes.Red;
            }
            else
            {
                buttonD2.Background = Brushes.Lime;
            }
            if (textBox.Text == "D2")
            {
                buttonD2.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("D3");
            if (result == false)
            {
                buttonD3.Background = Brushes.Red;
            }
            else
            {
                buttonD3.Background = Brushes.Lime;
            }
            if (textBox.Text == "D3")
            {
                buttonD3.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("D4");
            if (result == false)
            {
                buttonD4.Background = Brushes.Red;
            }
            else
            {
                buttonD4.Background = Brushes.Lime;
            }
            if (textBox.Text == "D4")
            {
                buttonD4.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("D5");
            if (result == false)
            {
                buttonD5.Background = Brushes.Red;
            }
            else
            {
                buttonD5.Background = Brushes.Lime;
            }
            if (textBox.Text == "D5")
            {
                buttonD5.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("D6");
            if (result == false)
            {
                buttonD6.Background = Brushes.Red;
            }
            else
            {
                buttonD6.Background = Brushes.Lime;
            }
            if (textBox.Text == "D6")
            {
                buttonD6.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("D7");
            if (result == false)
            {
                buttonD7.Background = Brushes.Red;
            }
            else
            {
                buttonD7.Background = Brushes.Lime;
            }
            if (textBox.Text == "D7")
            {
                buttonD7.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("D8");
            if (result == false)
            {
                buttonD8.Background = Brushes.Red;
            }
            else
            {
                buttonD8.Background = Brushes.Lime;
            }
            if (textBox.Text == "D8")
            {
                buttonD8.Background = Brushes.Yellow;
            }

            result = impEnter.CheckAvailability("D9");
            if (result == false)
            {
                buttonD9.Background = Brushes.Red;
            }
            else
            {
                buttonD9.Background = Brushes.Lime;
            }
            if (textBox.Text == "D9")
            {
                buttonD9.Background = Brushes.Yellow;
            }
        }
    }
}
