using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOTServer
{
    public class CommandPackage
    {
        public string Method { get; set; }
        public int FirstNumber { get; set; }
        public int SecondNumber { get; set; }
        public int Result { get; set; }
        public string ErrorMessage { get; set; }
    }
}
