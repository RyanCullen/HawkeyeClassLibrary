using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hawkeye_HVK
{
    public class Service
    {
        public int serviceNumber { get; set; }

        public string descripion { get; set; }

        public decimal smallRate { get; set; }

        public decimal mediumRate { get; set; }

        public decimal largeRate { get; set; }

        public Service()
        {
            this.serviceNumber = -1;
            this.descripion = "";
            this.smallRate = 0;
            this.mediumRate = 0;
            this.largeRate = 0;
        }

        public Service(int serviceNumber, string desc, decimal small, decimal medium, decimal large)
        {
            this.serviceNumber = serviceNumber;
            this.descripion = desc;
            this.smallRate = small;
            this.mediumRate = medium;
            this.largeRate = large;
        }
    }
}
