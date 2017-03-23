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

        public DataSet listVaccinations()
        {
            VaccinationDB vaccDB = new VaccinationDB();
            DataSet vals = vaccDB.listVaccinations();
            return vals;
        }

        public DataSet listPetVaccinations(int petNum)
        {
            VaccinationDB vaccDB = new VaccinationDB();
            DataSet vals = vaccDB.listVaccinations(petNum);
            return vals;
        }

        public DataSet getVaccinations(int vacNum)
        {
            VaccinationDB vaccDB = new VaccinationDB();
            DataSet vals = vaccDB.getVaccination(vacNum);
            return vals;
        }

        public DataSet checkVaccinations(int petNum, int resNum)
        {
            VaccinationDB vaccDB = new VaccinationDB();
            DataSet vals = vaccDB.checkVaccinations(petNum, resNum);
            return vals;
        }

        public DataSet checkVaccinations(int petNum, DateTime byDate)
        {
            VaccinationDB vaccDB = new VaccinationDB();
            DataSet vals = vaccDB.checkVaccinations(petNum, byDate);
            return vals;
        }
    }
}
