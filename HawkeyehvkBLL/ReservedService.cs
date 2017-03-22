using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hawkeye_HVK
{
    public class ReservedService
    {
        public int frequency { get; set; }

        public Service service { get; set; }

        public ReservedService()
        {
            this.frequency = 0;
            this.service = new Service();
        }

        public ReservedService(int frequency, Service service)
        {
            this.frequency = frequency;
            this.service = service;
        }
    }
}
