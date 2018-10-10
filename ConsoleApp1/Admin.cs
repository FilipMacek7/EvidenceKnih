using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Admin : User
    {
        new public string Name = "admin";
        public string Pass = "admin";
    }
}
