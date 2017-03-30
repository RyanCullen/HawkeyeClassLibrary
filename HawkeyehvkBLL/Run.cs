using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HawkeyehvkDB;
using System.Data;

namespace HawkeyehvkBLL
{
    public class Run
    {
        public int runNumber { get; set; }

        public char size { get; set; }

        public bool isCovered { get; set; }

        public char location { get; set; }

        public char status { get; set; }

        public Run()
        {
            this.runNumber = -1;
            this.size = 'S';
            this.isCovered = false;
            this.location = 'B';
            this.status = 'M'; //Maintenance
        }

        public Run(int number, char size, bool isCovered, char location, char status)
        {
            this.runNumber = number;
            this.size = size;
            this.isCovered = isCovered;
            this.location = location;
            this.status = status;
        }
        public ReservationCounts getReservationCounts(DateTime start, DateTime end) {
            RunDB db = new RunDB();
            return new ReservationCounts(db.getReservationCountsDB(start, end).Tables[0].Rows[0]);
        }
        public int checkRunAvailability(DateTime startDate, DateTime endDate, char runSize) {
            if (startDate > endDate)
            {
                return -1;
            }
           
            int count = -1;

            Run run = new Run();
            ReservationCounts resc = run.getReservationCounts(startDate, endDate);
            RunDB rundb = new RunDB();
            int totalRunsL = rundb.totalLargeRunsDB();
            int totalRunsR = rundb.totalRegularRunsDB();

            

            if (runSize == 'L') {
                if ((resc.numRegReservations - totalRunsR) > 0) { // this will determine if the regular size runs have run out. in which case there may be large runs used for smaller dogs
                    count = (totalRunsL - (resc.numRegReservations - totalRunsR)) - resc.numLargeReservations;// from total large runs take off the large runs already needed (on busiest day) and the overlap from small dogs in big runs
                }
                else { // otherwise just subtract the hights on a day from the total.
                    count = totalRunsL - resc.numLargeReservations;
                }
            }
            else { // if not large return the total number of runs 
                   // subtracting the highest number of reservations between the entered dates
                count = totalRunsL + totalRunsR - resc.numTotalReservations;
            }

            return count;
        }

        public class ReservationCounts {
            public int numRegReservations { get; private set; }

            public int numLargeReservations { get; private set; }

            public int numTotalReservations { get; private set; }

            public ReservationCounts(DataRow row) {
                this.numRegReservations = Convert.ToInt32(row["REGULAR_RESERVATIONS"].ToString());
                this.numLargeReservations = Convert.ToInt32(row["LARGE_RESERVATIONS"].ToString());
                this.numTotalReservations = numRegReservations + numLargeReservations;
            }
        }
    }
}
