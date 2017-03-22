using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hawkeye_HVK
{
    public class Vet
    {
        public int vetNumber { get; set; }
        public string name { get; set; }

        public string phoneNumber { get; set; }

        public Address address { get; set; }

        public Vet()
        {
            this.vetNumber = -1;
            this.name = "";
            this.phoneNumber = "";
            this.address = new Address();
        }

        public Vet(int vetNumber, string name, string phone, Address address)
        {
            this.vetNumber = vetNumber;
            this.name = name;
            this.phoneNumber = phone;
            this.address = address;
        }
    }
}
