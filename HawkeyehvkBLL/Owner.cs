using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HawkeyehvkDB;

namespace HawkeyehvkBLL
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

        public List<Owner> listTheOwners()
        {
            OwnerDB ownDB= new OwnerDB();
        
            List<Owner> ownerList = new List<Owner>();
            foreach (DataRow row in ownDB.listOwnersDB().Tables["hvk_owner"].Rows)
            {

                ownerList.Add(fillBox(row));
            }
            return ownerList;
        }
        public List<Owner> getOwner(int ownerNum)
        {
            OwnerDB ownDB = new OwnerDB();

            List<Owner> ownerList = new List<Owner>();
            foreach (DataRow row in ownDB.listOwnersDB(ownerNum).Tables["hvk_owner"].Rows)
            {

                ownerList.Add(fillBox(row));
            }
            return ownerList;
        }

        private Owner fillBox(DataRow theRow)
        {
            Owner own = new Owner();
            try
            {
                own.ownerNumber = Convert.ToInt32(theRow["OWNER_NUMBER"].ToString());
                own.lastName = theRow["OWNER_LAST_NAME"].ToString();
                own.firstName = theRow["OWNER_FIRST_NAME"].ToString();
                own.address.street = theRow["OWNER_STREET"].ToString();
                own.address.city = theRow["OWNER_CITY"].ToString();
                own.address.province = theRow["OWNER_PROVINCE"].ToString();
                own.address.postalCode = theRow["OWNER_POSTAL_CODE"].ToString();
                own.phoneNumber = theRow["OWNER_PHONE"].ToString();
                own.email = theRow["OWNER_EMAIL"].ToString();
                own.emergencyFirstName = theRow["EMERGENCY_CONTACT_FIRST_NAME"].ToString();
                own.emergencyLastName = theRow["EMERGENCY_CONTACT_LAST_NAME"].ToString();
                own.emergencyPhone = theRow["EMERGENCY_CONTACT_PHONE"].ToString();
            }
            catch
            {
                Console.Write("Error");
            }
            return own;
        }

      
    }
}