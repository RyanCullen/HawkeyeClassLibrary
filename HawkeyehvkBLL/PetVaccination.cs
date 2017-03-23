using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HawkeyehvkDB;
using System.Data;

namespace HawkeyehvkBLL
{
    public class PetVaccination
    {
        public DateTime expirationDate { get; set; }

        public Vaccination vaccination { get; set; }

        public char isValidated { get; protected set; }

        public PetVaccination()
        {
            this.expirationDate = DateTime.MinValue;
            this.vaccination = new Vaccination();
            this.isValidated = 'F';
        }
        public PetVaccination(DateTime expires, Vaccination vaccination)
        {
            this.expirationDate = expires;
            this.vaccination = vaccination;
            this.isValidated = 'F';
        }

        public PetVaccination(DateTime expires, Vaccination vaccination, char isValidated)
        {
            this.expirationDate = expires;
            this.vaccination = vaccination;
            this.isValidated = isValidated;
        }

        public bool validate()
        {
            this.isValidated = 'T';
            return true;
        }

        public List<PetVaccination> checkVaccinations(int petNum, int resNum)
        {
            VaccinationDB vaccDB = new VaccinationDB();
            List<PetVaccination> petVaccList = new List<PetVaccination>();
            foreach(DataRow row in vaccDB.checkVaccinations(petNum, resNum).Tables["hvk_vaccination"].Rows)
            {
                petVaccList.Add(fillVaccination(row));
            }
            return petVaccList;
        }

        public List<PetVaccination> checkVaccinations(int petNum, DateTime byDate)
        {
            VaccinationDB vaccDB = new VaccinationDB();
            List<PetVaccination> petVaccList = new List<PetVaccination>();
            foreach (DataRow row in vaccDB.checkVaccinations(petNum, byDate).Tables["hvk_vaccination"].Rows)
            {
                petVaccList.Add(fillVaccination(row));
            }
            return petVaccList;
        }

        public List<PetVaccination> listPetVaccinations(int petNum)
        {
            VaccinationDB vaccDB = new VaccinationDB();
            List<PetVaccination> vaccList = new List<PetVaccination>();
            foreach (DataRow row in vaccDB.listVaccinations(petNum).Tables["hvk_vaccination"].Rows)
            {
                vaccList.Add(fillVaccination(row));
            }
            return vaccList;
        }

        public PetVaccination fillVaccination(DataRow row)
        {
            PetVaccination petVacc = new PetVaccination();

            petVacc.vaccination.vaccinationNumber = Convert.ToInt32(row["VACCINATION_NUMBER"].ToString());
            petVacc.vaccination.name = row["VACCINATION_NAME"].ToString();
            petVacc.expirationDate = Convert.ToDateTime(row["VACCINATION_EXPIRY_DATE"].ToString());
            petVacc.isValidated = Convert.ToChar(row["VACCINATION_CHECKED_STATUS"].ToString());

            return petVacc;

        }
    }
}
