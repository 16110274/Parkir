using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace parkiransmart.Interface
{
    //Kelas deklarasi (Parent Class) fungsi2 tampilan control
    interface IntControl
    {
        Boolean InsertData(Entity.EntControl ctrl);
        Boolean UpdateData(Entity.EntControl ctrl);
        Boolean DeleteData(Entity.EntControl ctrl);
        DataSet SelectData(string code);
        Boolean CheckAvailability(string code);
        void Lock(string code);
    }
}
