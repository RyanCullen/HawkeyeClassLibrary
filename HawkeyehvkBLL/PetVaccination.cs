using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hawkeye_HVK
{
    public class PetVaccination
    {
        public DateTime expirationDate { get; set; }

        public Vaccination vaccination { get; set; }

        public char isValidated { get; protected set; }

        public PetVaccination()
        {
            this.expirationDate = DateTime.MinValue;
            this.vaccination = new Vaccination();
            this.isValidated = 'F';
        }
        public PetVaccination(DateTime expires, Vaccination vaccination)
        {
            this.expirationDate = expires;
            this.vaccination = vaccination;
            this.isValidated = 'F';
        }

        public PetVaccination(DateTime expires, Vaccination vaccination, char isValidated)
        {
            this.expirationDate = expires;
            this.vaccination = vaccination;
            this.isValidated = isValidated;
        }

        public bool validate()
        {
            this.isValidated = 'T';
            return true;
        }
    }
}
