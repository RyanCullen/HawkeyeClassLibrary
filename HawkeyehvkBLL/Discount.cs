using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hawkeye_HVK
{
    public class Discount
    {

        public char type { get; set; }

        public int discountNumber { get; set; }

        public string description { get; set; }

        public decimal percentage { get; set; }

        public Discount()
        {
            this.discountNumber = -1;
            this.description = "";
            this.percentage = 0;
            this.type = 'D';
        }

        public Discount(int code, string desc, decimal percentage, char type)
        {
            this.discountNumber = code;
            this.description = desc;
            this.percentage = percentage;
            this.type = type;
        }
    }
}
