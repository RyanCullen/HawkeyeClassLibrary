﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HawkeyehvkDB;
using System.Data;

namespace HawkeyehvkBLL
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


        public List<Reservation> listReservations()
        {
            ReservationDB db = new ReservationDB();
            DataSet ds = db.listResevationsDB();
             
            return fillReservation(ds);
        }

        public List<Reservation> listReservations(int ownerNumber)
        {
            ReservationDB db = new ReservationDB();
            DataSet ds = db.listResevationsDB(ownerNumber);

            return fillReservation(ds);
        }

        public List<Reservation> listActiveReservations()
        {
            ReservationDB db = new ReservationDB();
            DataSet ds = db.listActiveReservationsDB(); 

            return fillReservation(ds);
        }


        public List<Reservation> listActiveReservations(int ownerNumber)
        {
            ReservationDB db = new ReservationDB();
            DataSet ds = db.listActiveReservationsDB(ownerNumber);

            return fillReservation(ds);
        }

        public List<Reservation> listUpcomingReservations(DateTime reservationDate)
        {
            ReservationDB db = new ReservationDB();
            DataSet ds = db.listUpcomingReservationsDB(reservationDate);

            return fillReservation(ds);
        }



        public List<Reservation> fillReservation(DataSet ds )
        {
            Reservation res = new Reservation();
            List<Reservation> resList = new List<Reservation>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {

                try
                {
                    if (i != 0 && (Convert.ToInt32(ds.Tables[0].Rows[i]["RESERVATION_NUMBER"]) == Convert.ToInt32(ds.Tables[0].Rows[i - 1]["RESERVATION_NUMBER"]))) {
                        res.petReservationList.Add(new PetReservation());
                        res.petReservationList[res.petReservationList.Count - 1].pet.petNumber = Convert.ToInt16(ds.Tables[0].Rows[i]["PET_NUMBER"].ToString());
                        // res.petReservationList[i].run.runNumber = Convert.ToInt16(ds.Tables[0].Rows[i]["RUN_RUN_NUMBER"].ToString());

                    }
                    else
                    {

                        //Retrieve pet info , owner # , reservation detail 
                        res.reservationNumber = Convert.ToInt32(ds.Tables[0].Rows[i]["RESERVATION_NUMBER"]);
                        res.startDate = DateTime.Parse(ds.Tables[0].Rows[i]["RESERVATION_START_DATE"].ToString());
                        res.endDate = DateTime.Parse(ds.Tables[0].Rows[i]["RESERVATION_END_DATE"].ToString());
                        res.petReservationList.Add(new PetReservation());
                        res.owner.ownerNumber = Convert.ToInt16(ds.Tables[0].Rows[i]["OWN_OWNER_NUMBER"].ToString());
                        res.petReservationList[res.petReservationList.Count - 1].pet.petNumber = Convert.ToInt16(ds.Tables[0].Rows[i]["PET_NUMBER"].ToString());
                        // res.petReservationList[i].run.runNumber = Convert.ToInt16(ds.Tables[0].Rows[i]["RUN_RUN_NUMBER"].ToString());
                        resList.Add(res);
                        res = new Reservation();


                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());

                }

            }
            return resList;


        }


        public List<Reservation> fillReservationDetail(DataSet ds)
        {
            Reservation res = new Reservation();
            List<Reservation> resList = new List<Reservation>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                try

                {

                    if (i != 0 && (Convert.ToInt32(ds.Tables[0].Rows[i]["RESERVATION_NUMBER"]) == Convert.ToInt32(ds.Tables[0].Rows[i - 1]["RESERVATION_NUMBER"])))
                    {
                        int tempIndex = res.petReservationList.Count - 1;
                        res.petReservationList.Add(new PetReservation());
                        res.petReservationList[tempIndex].pet.name = ds.Tables[0].Rows[i]["PET_NAME"].ToString();
                        res.petReservationList[tempIndex].pet.breed = ds.Tables[0].Rows[i]["pet_breed"].ToString();
                        res.petReservationList[tempIndex].pet.size = Convert.ToChar(ds.Tables[0].Rows[i]["dog_size"].ToString());
                        res.petReservationList[tempIndex].pet.isFixed = Convert.ToChar(ds.Tables[i].Rows[i]["pet_fixed"]);
                        res.petReservationList[tempIndex].pet.petNumber = Convert.ToInt16(ds.Tables[0].Rows[i]["PET_NAME"].ToString());
                        res.petReservationList[tempIndex].pet.notes = ds.Tables[0].Rows[i]["SPECIAL_NOTES"].ToString();
                        res.petReservationList[tempIndex].pet.gender = Convert.ToChar(ds.Tables[0].Rows[i]["PET_GENDER"].ToString());
                        res.petReservationList[tempIndex].run.runNumber = Convert.ToInt16(ds.Tables[0].Rows[i]["RUN_RUN_NUMBER"].ToString());
                    }
                    else
                    {
                        //Retrieve pet info , owner # , reservation detail 
                        res.reservationNumber = Convert.ToInt32(ds.Tables[0].Rows[i]["RESERVATION_NUMBER"]);
                        res.startDate = DateTime.Parse(ds.Tables[0].Rows[i]["RESERVATION_START_DATE"].ToString());
                        res.endDate = DateTime.Parse(ds.Tables[0].Rows[i]["RESERVATION_END_DATE"].ToString());
                        res.petReservationList.Add(new PetReservation());
                        res.owner.ownerNumber = Convert.ToInt16(ds.Tables[0].Rows[i]["OWN_OWNER_NUMBER"].ToString());
                        res.petReservationList[i].pet.name = ds.Tables[0].Rows[i]["PET_NAME"].ToString();
                        res.petReservationList[i].pet.breed = ds.Tables[0].Rows[i]["pet_breed"].ToString();
                        res.petReservationList[i].pet.size = Convert.ToChar(ds.Tables[0].Rows[i]["dog_size"].ToString());
                        res.petReservationList[i].pet.isFixed = Convert.ToChar(ds.Tables[i].Rows[i]["pet_fixed"]);
                        res.petReservationList[i].pet.petNumber = Convert.ToInt16(ds.Tables[0].Rows[i]["PET_NAME"].ToString());
                        res.petReservationList[i].pet.notes = ds.Tables[0].Rows[i]["SPECIAL_NOTES"].ToString();
                        res.petReservationList[i].pet.gender = Convert.ToChar(ds.Tables[0].Rows[i]["PET_GENDER"].ToString());
                        res.petReservationList[i].run.runNumber = Convert.ToInt16(ds.Tables[0].Rows[i]["RUN_RUN_NUMBER"].ToString());
                        resList.Add(res);
                        res = new Reservation();
                    }

                      

                }
                catch
                {
                    Console.Write("Error");
                }

            }
            return resList;


        }
        public List<Run> listAvailableRuns(DateTime start, DateTime end)
        {
            List<Run> runs = new List<Run>();
            ReservationDB db = new ReservationDB();
            foreach (DataRow row in db.listAvailableRunsDB(start, end).Tables["hvk_runsAvail"].Rows)
            {
                runs.Add(convertToRun(row));
            }
            return runs;
        }
        public ReservationCounts getReservationCounts(DateTime start, DateTime end)
        {
            RunDB db = new RunDB();
            return new ReservationCounts(db.getReservationCountsDB(start, end).Tables[0].Rows[0]);
        }

        private Run convertToRun(DataRow row)
        {
            Run run = new HawkeyehvkBLL.Run();
            run.runNumber = Convert.ToInt32(row[0]);
            if (((String)row[1]).ToUpper().Equals(("L")))
            {
                run.size = 'L';
            }
            else
            {
                run.size = 'R';
            }
            return run;
        }
        public int addReservation(int petNumber, DateTime startDate, DateTime endDate)
        {


            return 0;
        }

        public int addToReservation(int reservationNumber, int petNumber)
        {

            return 0;
        }

        public int cancelReservation(int reservationNumber)
        {

            return 0;
        }

        public int changeReservation(int reservationNumber, DateTime startDate, DateTime endDate)
        {

            return 0;
        }

        public int deleteDogFromReservation(int reservationNumber, int petNumber)
        {

            return 0;
        }

        public int checkVaccinations(int petNumber, DateTime byDate)
        {

            return 0;
        }

        public int checkRunAvailability(DateTime startDate, DateTime endDate, char runSize)
        {
            int count = -1;
           
            Reservation res = new Reservation();
            ReservationCounts resc = res.getReservationCounts(startDate, endDate);
            RunDB run = new RunDB();
            int totalRunsL = run.totalLargeRunsDB();
            int totalRunsR = run.totalRegularRunsDB();
            


            if (runSize == 'L') {
                if ((resc.numRegReservations - totalRunsR) > 0) 
                { // this will determine if the regular size runs have run out. in which case there may be large runs used for smaller dogs
                    count = (totalRunsL - (resc.numRegReservations - totalRunsR))-resc.numLargeReservations;// from total large runs take off the large runs already needed (on busiest day) and the overlap from small dogs in big runs
                }
                else { // otherwise just subtract the hights on a day from the total.
                    count = totalRunsL - resc.numLargeReservations;
                }
            }
            else { // if not large return the total number of runs 
                   // subtracting the highest number of reservations between the entered dates
                count =  totalRunsL+totalRunsR- resc.numTotalReservations;
            }

            return count;
        }

        public class ReservationCounts
        {
            public int numRegReservations { get; private set; }

            public int numLargeReservations { get; private set; }

            public int numTotalReservations { get; private set; }

            public ReservationCounts(DataRow row)
            {
                this.numRegReservations = Convert.ToInt32(row["REGULAR_RESERVATIONS"].ToString());
                this.numLargeReservations = Convert.ToInt32(row["LARGE_RESERVATIONS"].ToString());
                this.numTotalReservations = numRegReservations + numLargeReservations;
            }
        }

    }
}
