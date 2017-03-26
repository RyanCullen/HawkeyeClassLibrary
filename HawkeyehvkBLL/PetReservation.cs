using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HawkeyehvkDB;
using System.Data;

namespace HawkeyehvkBLL
{
    public class PetReservation
    {
        public int petResNumber { get; set; }

        private Pet thePet { get; set; }

        public Pet pet
        {
            get { return thePet; }
            set
            {
                thePet = value;
                if (!thePet.petReservationList.Contains(this))
                    thePet.addPetReservation(this);
            }
        }

        public List<Discount> discountList { get; protected set; }

        public Run run { get; set; }

        public List<Medication> medicationList { get; protected set; }

        public List<ReservedService> serviceList { get; protected set; }

        public PetFood food { get; set; }

        public int foodFrequency { get; set; }

        public string foodQuantity { get; set; }

        private Reservation res;

        public Reservation reservation
        {
            get { return res; }
            set
            {
                res = value;
                if (!res.petReservationList.Contains(this))
                    res.addPetReservation(this);
            }
        }

        public PetReservation()
        {
            this.petResNumber = -1;
            this.pet = new Pet();
            this.discountList = new List<Discount>();
            this.run = new Run();
            this.medicationList = new List<Medication>();
            this.serviceList = new List<ReservedService>();
            this.food = new PetFood();
            this.foodFrequency = 0;
            this.foodQuantity = "";
            this.reservation = new Reservation();
        }

        public PetReservation(int petResNum, Pet pet, Run run, PetFood food, int foodFrequency)
        {
            this.petResNumber = petResNum;
            this.pet = pet;
            this.discountList = new List<Discount>();
            this.run = run;
            this.medicationList = new List<Medication>();
            this.serviceList = new List<ReservedService>();
            this.food = food;
            this.foodFrequency = foodFrequency;
            this.foodQuantity = "";
            this.reservation = new Reservation();
        }

        public PetReservation(int petResNum, Pet pet, Run run, PetFood food, int foodFrequency, string foodQuantity)
        {
            this.petResNumber = petResNum;
            this.pet = pet;
            this.discountList = new List<Discount>();
            this.run = run;
            this.medicationList = new List<Medication>();
            this.serviceList = new List<ReservedService>();
            this.food = food;
            this.foodFrequency = foodFrequency;
            this.foodQuantity = foodQuantity;
            this.reservation = new Reservation();
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

        public bool addMedication(Medication med)
        {
            this.medicationList.Add(med);
            return true;
        }

        public bool removeMedication(Medication med)
        {
            return this.medicationList.Remove(med);
        }

        public bool addService(ReservedService service)
        {
            this.serviceList.Add(service);
            return true;
        }

        public bool removeService(ReservedService service)
        {
            return this.serviceList.Remove(service);
        }

        public List<Run> listAvailableRuns(DateTime start, DateTime end) {
            List<Run> runs = new List<Run>();
            PetReservationDB db = new PetReservationDB();
            foreach (DataRow row in db.listAvailableRunsDB(start, end).Tables["hvk_runsAvail"].Rows)
            {
                runs.Add(convertToRun(row));
            }
                return runs;
        }
        public int NumberOfRunsAvailable(DateTime start, DateTime end, char dogSize) {
            // gives the number of available runs for this stay
            int count=-1;
            PetReservationDB db = new PetReservationDB();
            dogSize = Char.ToUpper(dogSize);
            foreach (DataRow row in db.NumberOfRunsAvailableDB(start, end).Tables["hvk_numRuns"].Rows)
            {
                if (dogSize == Convert.ToString(row["DOG_SIZE"]).ToUpper()[0]) {
                    count = Convert.ToInt32(row[1]);
                }
            }
            return count;
        }
        private Run convertToRun(DataRow row) {
            Run run = new HawkeyehvkBLL.Run();
            run.runNumber = Convert.ToInt32(row[0]);
            if (((String)row[1]).ToUpper().Equals(("L")))
            {
                run.size = 'L';
            }
            else {
                run.size = 'R';
            }
            return run;
        }





    }
}
