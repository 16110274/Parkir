using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parkiransmart.Interface
{
    //Kelas deklarasi (Parent Class) fungsi2 tampilan enter
    interface IntEnter
    {
        string ShowLoc();
        Boolean CheckAvailability(string code);
        Boolean KendaraanMasuk(Entity.EntEnter enter);
    }
}
