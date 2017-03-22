using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hawkeye_HVK
{
    public class Vaccination
    {
        public int vaccinationNumber { get; set; }
        public string name { get; set; }

        public Vaccination()
        {
            this.vaccinationNumber = -1;
            this.name = "";
        }

        public Vaccination(int vaccinationNumber, string name)
        {
            this.vaccinationNumber = vaccinationNumber;
            this.name = name;
        }
    }
}
