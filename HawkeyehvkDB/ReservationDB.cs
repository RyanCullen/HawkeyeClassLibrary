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
    public class ReservationDB
    {

        public DataSet listResevation()
        {
            //return a list of reservation with it details 
            //Display : clerk or user edit reservation 
            //To be fixed : does not include sharing with pet name 
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT RES.RESERVATION_NUMBER, RES.RESERVATION_START_DATE, RES.RESERVATION_END_DATE, PRES.RUN_RUN_NUMBER, PET.PET_NAME, SERV.SERVICE_DESCRIPTION, RES_SERV.SERVICE_FREQUENCY, FOOD.FOOD_BRAND, 
             PFOOD.PET_FOOD_FREQUENCY, PFOOD.PET_FOOD_QUANTITY, MED.MEDICATION_NAME, MED.MEDICATION_DOSAGE, MED.MEDICATION_END_DATE, MED.MEDICATION_SPECIAL_INSTRUCT, PRES.PR_SHARING_WITH
             FROM   TEAMHAWKEYE.HVK_RESERVATION RES INNER JOIN
             TEAMHAWKEYE.HVK_PET_RESERVATION PRES ON RES.RESERVATION_NUMBER = PRES.RES_RESERVATION_NUMBER INNER JOIN
             TEAMHAWKEYE.HVK_PET PET ON PRES.PET_PET_NUMBER = PET.PET_NUMBER INNER JOIN
             TEAMHAWKEYE.HVK_PET_RESERVATION_SERVICE RES_SERV ON PRES.PET_RES_NUMBER = RES_SERV.PR_PET_RES_NUMBER INNER JOIN
             TEAMHAWKEYE.HVK_SERVICE SERV ON RES_SERV.SERV_SERVICE_NUMBER = SERV.SERVICE_NUMBER INNER JOIN
             TEAMHAWKEYE.HVK_PET_FOOD PFOOD ON PRES.PET_RES_NUMBER = PFOOD.PR_PET_RES_NUMBER INNER JOIN
             TEAMHAWKEYE.HVK_FOOD FOOD ON PFOOD.FOOD_FOOD_NUMBER = FOOD.FOOD_NUMBER INNER JOIN
             TEAMHAWKEYE.HVK_MEDICATION MED ON PRES.PET_RES_NUMBER = MED.PR_PET_RES_NUMBER";
            OracleCommand cmd = new OracleCommand(cmdStr, con);

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;
            DataSet ds = new DataSet("dsReservation");
            da.Fill(ds);

            return ds;
        }

        public DataSet listResevation(int ownerNumber)
        {
            //Overloaded return a list of reservation with it details base on owner number 
            //Display : clerk or user edit reservation 
            //To be fixed : does not include sharing with pet name 
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT RES.RESERVATION_NUMBER, RES.RESERVATION_START_DATE, RES.RESERVATION_END_DATE, PRES.RUN_RUN_NUMBER, PET.PET_NAME, SERV.SERVICE_DESCRIPTION, RES_SERV.SERVICE_FREQUENCY, FOOD.FOOD_BRAND, 
             PFOOD.PET_FOOD_FREQUENCY, PFOOD.PET_FOOD_QUANTITY, MED.MEDICATION_NAME, MED.MEDICATION_DOSAGE, MED.MEDICATION_END_DATE, MED.MEDICATION_SPECIAL_INSTRUCT, PRES.PR_SHARING_WITH
             FROM   TEAMHAWKEYE.HVK_RESERVATION RES INNER JOIN
             TEAMHAWKEYE.HVK_PET_RESERVATION PRES ON RES.RESERVATION_NUMBER = PRES.RES_RESERVATION_NUMBER INNER JOIN
             TEAMHAWKEYE.HVK_PET PET ON PRES.PET_PET_NUMBER = PET.PET_NUMBER INNER JOIN
             TEAMHAWKEYE.HVK_PET_RESERVATION_SERVICE RES_SERV ON PRES.PET_RES_NUMBER = RES_SERV.PR_PET_RES_NUMBER INNER JOIN
             TEAMHAWKEYE.HVK_SERVICE SERV ON RES_SERV.SERV_SERVICE_NUMBER = SERV.SERVICE_NUMBER INNER JOIN
             TEAMHAWKEYE.HVK_PET_FOOD PFOOD ON PRES.PET_RES_NUMBER = PFOOD.PR_PET_RES_NUMBER INNER JOIN
             TEAMHAWKEYE.HVK_FOOD FOOD ON PFOOD.FOOD_FOOD_NUMBER = FOOD.FOOD_NUMBER INNER JOIN
             TEAMHAWKEYE.HVK_MEDICATION MED ON PRES.PET_RES_NUMBER = MED.PR_PET_RES_NUMBER
             INNER JOIN TEAMHAWKEYE.HVK_PET.OWN_OWNER_NUMBER  = :OWNER_NUMBER";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("OWNER_NUMBER",ownerNumber);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;
            DataSet ds = new DataSet("dsReservation");
            

            return ds;
        }

    }
}
