using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenCMDSdc
{
    class adatok
    {
        public string elso { get; set; }
        public string kihivott { get; set; }
        public adatok(string sor)
        {
            string[] tomb = sor.Split(',');
            elso = tomb[0];
            kihivott = tomb[1];
        }
    }
}
