using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parkiransmart.Interface
{
    //Kelas deklarasi (Parent Class) fungsi2 tampilan login
    interface IntLogin
    {
        Boolean Login(Entity.EntLogin e);
        void InputQuery(Entity.EntLogin e);
    }
}
