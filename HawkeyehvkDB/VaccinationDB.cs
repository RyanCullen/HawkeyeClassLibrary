using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HawkeyehvkDB
{
    class VaccinationDB
    {
        public DataSet listVaccinations()
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = "Select vaccination_name from HVK_vaccination";
            OracleCommand cmd = new OracleCommand(cmdStr, con);

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

            DataSet ds = new DataSet("vaccDataSet");
            da.Fill(ds, "hvk_vaccination");
            return ds;
        }

        public DataSet listVaccinations(int petNum)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = "Select V.vaccination_name, PV.vaccination_expiry_date, PV.vaccination_checked_status from HVK_vaccination V, HVK_pet_vaccination PV where PV.pet_pet_number = :pet and PV.vacc_vaccination_number = V.vaccination_number";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("pet", petNum);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

            DataSet ds = new DataSet("vaccDataSet");
            da.Fill(ds, "hvk_vaccination");
            return ds;
        }

        public DataSet checkVaccinations(int petNum, int resNum)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT V.VACCINATION_NAME
                                FROM HVK_VACCINATION V,
                                  HVK_PET_VACCINATION PV,
                                  HVK_PET P,
                                  HVK_RESERVATION R
                                WHERE R.RESERVATION_NUMBER       = :res
                                AND P.PET_NUMBER                 = :pet
                                AND P.PET_NUMBER                 = PV.PET_PET_NUMBER
                                AND (PV.VACCINATION_EXPIRY_DATE  < R.RESERVATION_END_DATE
                                OR PV.VACCINATION_CHECKED_STATUS = 'N')
                                AND PV.VACC_VACCINATION_NUMBER   = V.VACCINATION_NUMBER
                                UNION
                                SELECT V.VACCINATION_NAME FROM HVK_VACCINATION V WHERE V.VACCINATION_NAME
                                NOT IN
                                (SELECT V.VACCINATION_NAME
                                FROM HVK_VACCINATION V,
                                  HVK_PET_VACCINATION PV,
                                  HVK_PET P,
                                  HVK_RESERVATION R
                                WHERE R.RESERVATION_NUMBER       = :res
                                AND P.PET_NUMBER                 = :pet
                                AND P.PET_NUMBER                 = PV.PET_PET_NUMBER
                                AND (PV.VACCINATION_EXPIRY_DATE  < R.RESERVATION_END_DATE
                                OR PV.VACCINATION_CHECKED_STATUS = 'Y' OR PV.VACCINATION_CHECKED_STATUS = 'N')
                                AND PV.VACC_VACCINATION_NUMBER   = V.VACCINATION_NUMBER)";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("pet", petNum);
            cmd.Parameters.Add("res", resNum);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

            DataSet ds = new DataSet("vaccDataSet");
            da.Fill(ds, "hvk_vaccination");
            return ds;
        }

        public DataSet getVaccination(int vaccNum)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = "Select vaccination_name from HVK_vaccination where vaccination_number = :vacc";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("vacc", vaccNum);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

            DataSet ds = new DataSet("vaccDataSet");
            da.Fill(ds, "hvk_vaccination");
            return ds;
        }
    }
}
