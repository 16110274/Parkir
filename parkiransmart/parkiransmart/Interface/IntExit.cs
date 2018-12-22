using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace parkiransmart.Interface
{
    //Kelas deklarasi (Parent Class) fungsi2 tampilan exit
    interface IntExit
    {
        DataSet CheckData(string st);
        DataSet Petugas();
        void ProsesData(Entity.EntExit exit);
        void Ketersediaan(Entity.EntExit exit);
    }
}
