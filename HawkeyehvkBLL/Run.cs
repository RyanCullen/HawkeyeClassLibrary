using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hawkeye_HVK
{
    public class Run
    {
        public int runNumber { get; set; }

        public char size { get; set; }

        public bool isCovered { get; set; }

        public char location { get; set; }

        public char status { get; set; }

        public Run()
        {
            this.runNumber = -1;
            this.size = 'S';
            this.isCovered = false;
            this.location = 'B';
            this.status = 'M'; //Maintenance
        }

        public Run(int number, char size, bool isCovered, char location, char status)
        {
            this.runNumber = number;
            this.size = size;
            this.isCovered = isCovered;
            this.location = location;
            this.status = status;
        }
    }
}
