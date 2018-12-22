using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parkiransmart.Interface
{
    //Kelas deklarasi (Parent Class) fungsi2 tampilan logout
    interface IntLogout
    {
        Boolean Logout(Entity.EntLogout e);
        void UpdateKeluar(Entity.EntLogout e);
    }
}
