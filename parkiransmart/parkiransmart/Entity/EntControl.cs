using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parkiransmart.Entity
{
    class EntControl
    {
        //Enkapsulasi variabel yang digunakan untuk tampilan Control
        private string lokasi, plat, tanggal, waktu, menginap, denda;

        //set & get versi VS 2017
        public string Lokasi { get => lokasi; set => lokasi = value; }
        public string Plat { get => plat; set => plat = value; }
        public string Tanggal { get => tanggal; set => tanggal = value; }
        public string Waktu { get => waktu; set => waktu = value; }
        public string Menginap { get => menginap; set => menginap = value; }
        public string Denda { get => denda; set => denda = value; }
    }
}
