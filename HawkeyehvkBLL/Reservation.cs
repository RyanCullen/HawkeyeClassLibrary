using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hawkeye_HVK
{
    public class Reservation
    {
        public int reservationNumber { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public List<Discount> discountList { get; protected set; }

        public List<PetReservation> petReservationList { get; protected set; }

        private Owner own { get; set; }

        public Owner owner
        {
            get { return own; }
            set
            {
                own = value;
                if (!own.reservationList.Contains(this))
                    own.addReservation(this);
            }
        }

        public Reservation()
        {
            this.reservationNumber = -1;
            this.startDate = DateTime.MinValue;
            this.endDate = DateTime.MaxValue;
            this.discountList = new List<Discount>();
            this.petReservationList = new List<PetReservation>();
            this.owner = new Owner();
        }

        public Reservation(int resNumber, DateTime start, DateTime end)
        {
            this.reservationNumber = resNumber;
            this.startDate = start;
            this.endDate = end;
            this.discountList = new List<Discount>();
            this.petReservationList = new List<PetReservation>();
            this.owner = new Owner();
        }

        public Reservation(int resNumber, DateTime start, DateTime end, Owner owner)
        {
            this.reservationNumber = resNumber;
            this.startDate = start;
            this.endDate = end;
            this.discountList = new List<Discount>();
            this.petReservationList = new List<PetReservation>();
            this.owner = owner;
        }

        public bool addDiscount(Discount discount)
        {
            this.discountList.Add(discount);
            return true;
        }

        public bool removeDiscount(Discount discount)
        {
            return this.discountList.Remove(discount);
        }

        public bool addPetReservation(PetReservation petRes)
        {
            this.petReservationList.Add(petRes);
            if (!petRes.reservation.Equals(this))
                petRes.reservation = this;        
            return true;
        }

        public bool removePetReservation(PetReservation petRes)
        {
            petRes.reservation = new Reservation();
            return this.petReservationList.Remove(petRes);
        }
    }
}
