using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HawkeyehvkBLL
{
    public class Pet
    {
        public int petNumber { get; set; }

        public string name { get; set; }

        public char gender { get; set; }

        public char isFixed { get; set; }

        public string breed { get; set; }

        public DateTime birthday { get; set; }

        public char size { get; set; }

        public string notes { get; set; }

        public List<PetVaccination> vaccinationList { get; protected set; }

        public List<PetReservation> petReservationList { get; protected set; }

        public Pet()
        {
            this.petNumber = -1;
            this.name = "";
            this.gender = 'M';
            this.isFixed = 'F';
            this.breed = "";
            this.birthday = DateTime.MinValue;
            this.size = 'S';
            this.notes = "";
            this.vaccinationList = new List<PetVaccination>();
            this.petReservationList = new List<PetReservation>();
        }

        public Pet(int petNum, string name, char gender, char isFixed)
        {
            this.petNumber = petNum;
            this.name = name;
            this.gender = gender;
            this.isFixed = isFixed;
            this.breed = "";
            this.birthday = DateTime.MinValue;
            this.size = 'S';
            this.notes = "";
            this.vaccinationList = new List<PetVaccination>();
            this.petReservationList = new List<PetReservation>();
        }

        public Pet(int petNum, string name, char gender, char isFixed, string breed, DateTime bday, char size, string notes)
        {
            this.petNumber = petNum;
            this.name = name;
            this.gender = gender;
            this.isFixed = isFixed;
            this.breed = breed;
            this.birthday = bday;
            this.size = size;
            this.notes = notes;
            this.vaccinationList = new List<PetVaccination>();
            this.petReservationList = new List<PetReservation>();
        }

        public bool addPetReservation(PetReservation petRes)
        {
            this.petReservationList.Add(petRes);
            if (!petRes.pet.Equals(this))
                petRes.pet = this;
            return true;
        }

        public bool removePetReservation(PetReservation petRes)
        {
            petRes.pet = new Pet();
            return this.petReservationList.Remove(petRes);
        }

        public bool addVaccination(PetVaccination petVaccination)
        {
            this.vaccinationList.Add(petVaccination);
            return true;
        }

        public bool removeVaccination(PetVaccination petVaccination)
        {
            return this.vaccinationList.Remove(petVaccination);
        }
    }
}
