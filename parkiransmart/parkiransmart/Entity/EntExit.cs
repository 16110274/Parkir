using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parkiransmart.Entity
{
    class EntExit
    {
        //Enkapsulasi variabel yang digunakan untuk tampilan Exit
        private string nostatus, noplat, lokasi, tglkeluar, waktukeluar, total, nama, id;

        //set & get versi VS 2017
        public string Nostatus { get => nostatus; set => nostatus = value; }
        public string Noplat { get => noplat; set => noplat = value; }
        public string Lokasi { get => lokasi; set => lokasi = value; }
        public string Tglkeluar { get => tglkeluar; set => tglkeluar = value; }
        public string Waktukeluar { get => waktukeluar; set => waktukeluar = value; }
        public string Total { get => total; set => total = value; }
        public string Nama { get => nama; set => nama = value; }
        public string Id { get => id; set => id = value; }
    }
}
