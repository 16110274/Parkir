using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parkiransmart.Entity
{
    class EntEnter
    {
        //Enkapsulasi variabel yang digunakan untuk tampilan Enter

        string plat,lokasi;

        //set & get versi VS 2017
        public string Plat { get => plat; set => plat = value; }
        public string Lokasi { get => lokasi; set => lokasi = value; }
    }
}
