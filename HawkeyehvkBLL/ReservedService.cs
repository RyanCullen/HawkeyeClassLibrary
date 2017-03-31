using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HawkeyehvkDB;
using System.Data;

namespace HawkeyehvkBLL
{
    public class ReservedService
    {
        public int frequency { get; set; }

        public Service service { get; set; }

        public ReservedService()
        {
            this.frequency = 0;
            this.service = new Service();
        }

        public ReservedService(int frequency, Service service)
        {
            this.frequency = frequency;
            this.service = service;
        }

        public static int addReservedService(int petResNum, int serviceNum) {
            ReservedServiceDB db = new ReservedServiceDB();
            if (addReservedService(petResNum, serviceNum) != 0) {
                return 1;
            }
            return 0;
        }

        public static int deleteReservedService(int petResNum, int serviceNum) {
            ReservedServiceDB db = new ReservedServiceDB();
            if (deleteReservedService(petResNum, serviceNum) != 0) {
                return 1;
            }
            return 0;
        }

        public void listReservedService(int petResNum)
        {

        }

        private ReservedService fillReservedService(DataRow row)
        {

        }
    }
}
