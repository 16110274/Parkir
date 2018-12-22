using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parkiransmart.Entity
{
    class EntLogin
    {
        //Enkapsulasi variabel yang digunakan untuk tampilan Login
        private string kode, password;

        //set & get versi VS 2017
        public string Kode { get => kode; set => kode = value; }
        public string Password { get => password; set => password = value; }
    }
}
