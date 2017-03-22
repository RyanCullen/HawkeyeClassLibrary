using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hawkeye_HVK
{
    public class Owner : User
    {
        public int ownerNumber { get; set; }

        public Address address { get; set; }

        public string phoneNumber { get; set; }

        public string email { get; set; }

        public string emergencyFirstName { get; set; }

        public string emergencyLastName { get; set; }

        public string emergencyPhone { get; set; }

        public Vet veterinarian { get; set; }

        public List<Pet> petList { get; protected set; }

        public List<Reservation> reservationList { get; protected set; }

        public Owner() : base()
        {
            base.role = 'O';
            this.ownerNumber = -1;
            this.address = new Address();
            this.phoneNumber = "";
            this.email = "";
            this.emergencyFirstName = "";
            this.emergencyLastName = "";
            this.emergencyPhone = "";
            this.veterinarian = new Vet();
            this.petList = new List<Pet>();
            this.reservationList = new List<Reservation>();
        }

        public Owner(string fName, string lName, int ownerNumber) : base(fName, lName)
        {
            this.ownerNumber = ownerNumber;
            base.role = 'O';
            this.address = new Address();
            this.phoneNumber = "";
            this.email = "";
            this.emergencyFirstName = "";
            this.emergencyLastName = "";
            this.emergencyPhone = "";
            this.veterinarian = new Vet();
            this.petList = new List<Pet>();
            this.reservationList = new List<Reservation>();
        }

        public Owner(string fName, string lName, int ownerNumber, Address address, string phone, string email, string emergencyFName, string emergencyLName, string emergencyPhone, Vet vet) : base(fName, lName)
        {
            this.role = 'O';
            this.ownerNumber = ownerNumber;
            this.address = address;
            this.phoneNumber = phone;
            this.email = email;
            this.emergencyFirstName = emergencyFName;
            this.emergencyLastName = emergencyLName;
            this.emergencyPhone = emergencyPhone;
            this.veterinarian = vet;
            this.petList = new List<Pet>();
            this.reservationList = new List<Reservation>();
        }

        public bool addPet(Pet pet)
        {
            this.petList.Add(pet);
            return true;
        }

        public bool removePet(Pet pet)
        {
            return this.petList.Remove(pet);
        }

        public bool addReservation(Reservation res)
        {
            this.reservationList.Add(res);
            if (!res.owner.Equals(this))
                res.owner = this;
            return true;
        }

        public bool removeReservation(Reservation res)
        {
            res.owner = new Owner();
            return this.reservationList.Remove(res);
        }
    }
}