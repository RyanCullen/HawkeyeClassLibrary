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
        public static int updatePetVaccinationChecked(char isChecked, int vacNumber, int petNumber) {
            Search search = new Search();
            if (!search.validatePetNumber(petNumber)) {
                return -2;
            }
            if (!search.validateVaccNumber(vacNumber)) {
                return -3;
            }
            
            return VaccinationDB.updatePetVaccinationCheckedDB(isChecked,vacNumber,petNumber);
        }
        public static int updatePetVaccinationExpiry(DateTime expiryDate, int vacNumber, int petNumber) {
            Search search = new Search();
            if (!search.validatePetNumber(petNumber))
            {
                return -2;
            }
            if (!search.validateVaccNumber(vacNumber))
            {
                return -3;
            }
            return VaccinationDB.updatePetVaccinationExpiryDB(expiryDate,vacNumber,petNumber);
        }
        public static int addPetVaccination(DateTime expiryDate, int vacNumber, int petNumber) {
            Search search = new Search();
            if (!search.validatePetNumber(petNumber))
            {
                return -2;
            }
            if (!search.validateVaccNumber(vacNumber))
            {
                return -3;
            }
            return VaccinationDB.addPetVaccinationDB(expiryDate,vacNumber,petNumber);
        }

    }
}
