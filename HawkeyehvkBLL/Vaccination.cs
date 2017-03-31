using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HawkeyehvkDB;

namespace HawkeyehvkBLL
{
    public class Vaccination
    {
        public int vaccinationNumber { get; set; }
        public string name { get; set; }

        public Vaccination()
        {
            this.vaccinationNumber = -1;
            this.name = "";
        }

        public Vaccination(int vaccinationNumber, string name)
        {
            this.vaccinationNumber = vaccinationNumber;
            this.name = name;
        }

        public DataSet listAllVaccinations()
        {
            VaccinationDB vaccDB = new VaccinationDB();
            DataSet vals = vaccDB.listVaccinationsDB();
            return vals;
        }
      
        public DataSet getVaccinations(int vacNum)
        {
            VaccinationDB vaccDB = new VaccinationDB();
            DataSet vals = vaccDB.getVaccinationDB(vacNum);
            return vals;
        }


    }
}
