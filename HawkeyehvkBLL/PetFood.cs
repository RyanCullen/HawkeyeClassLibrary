using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hawkeye_HVK
{
    public class PetFood
    {
        public string brand { get; set; }

        public string variety { get; set; }

        public PetFood()
        {
            this.brand = "";
            this.variety = "";
        }

        public PetFood(string brand, string variety)
        {
            this.brand = brand;
            this.variety = variety;
        }
    }
}
