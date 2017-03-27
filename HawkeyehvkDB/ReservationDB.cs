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
             PFOOD.PET_FOOD_FREQUENCY, PFOOD.PET_FOOD_QUANTITY, MED.MEDICATION_NAME, MED.MEDICATION_DOSAGE, MED.MEDICATION_END_DATE, MED.MEDICATION_SPECIAL_INSTRUCT, PRES.PR_SHARING_WITH, PET.PET_NUMBER, PET.PET_GENDER, 
             PET.PET_FIXED, PET.PET_BREED, PET.PET_BIRTHDATE, PET.DOG_SIZE, PET.SPECIAL_NOTES
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
             PFOOD.PET_FOOD_FREQUENCY, PFOOD.PET_FOOD_QUANTITY, MED.MEDICATION_NAME, MED.MEDICATION_DOSAGE, MED.MEDICATION_END_DATE, MED.MEDICATION_SPECIAL_INSTRUCT, PRES.PR_SHARING_WITH, PET.PET_NUMBER, PET.PET_GENDER, 
             PET.PET_FIXED, PET.PET_BREED, PET.PET_BIRTHDATE, PET.DOG_SIZE, PET.SPECIAL_NOTES
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
            da.Fill(ds);
            return ds;
        }


        public DataSet listActiveReservations()
        {
            //List all active reservation 
            //Display : clerk Home page 
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT RES.RESERVATION_NUMBER, OWN.OWNER_LAST_NAME, OWN.OWNER_FIRST_NAME, PRES.PET_PET_NUMBER, PET.PET_NAME, PRES.RUN_RUN_NUMBER, RES.RESERVATION_START_DATE, RES.RESERVATION_END_DATE
             FROM   TEAMHAWKEYE.HVK_RESERVATION RES INNER JOIN
             TEAMHAWKEYE.HVK_PET_RESERVATION PRES ON RES.RESERVATION_NUMBER = PRES.RES_RESERVATION_NUMBER INNER JOIN
             TEAMHAWKEYE.HVK_PET PET ON PRES.PET_PET_NUMBER = PET.PET_NUMBER INNER JOIN
             TEAMHAWKEYE.HVK_OWNER OWN ON PET.OWN_OWNER_NUMBER = OWN.OWNER_NUMBER
             WHERE (RES.RESERVATION_START_DATE <= SYSDATE) AND (RES.RESERVATION_END_DATE > SYSDATE) AND (PRES.RUN_RUN_NUMBER IS NOT NULL)";

            OracleCommand cmd = new OracleCommand(cmdStr, con);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;
            DataSet ds = new DataSet("dsActiveReservation");
            da.Fill(ds);
            return ds;

        }


        public DataSet listActiveReservations(int ownerNumber)
        {
            //Overloaded List all active reservation base on owner number 
            //Display : clerk or user Home page 
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT RES.RESERVATION_NUMBER, OWN.OWNER_LAST_NAME, OWN.OWNER_FIRST_NAME, PRES.PET_PET_NUMBER, PET.PET_NAME, PRES.RUN_RUN_NUMBER, RES.RESERVATION_START_DATE, RES.RESERVATION_END_DATE
             FROM   TEAMHAWKEYE.HVK_RESERVATION RES INNER JOIN
             TEAMHAWKEYE.HVK_PET_RESERVATION PRES ON RES.RESERVATION_NUMBER = PRES.RES_RESERVATION_NUMBER INNER JOIN
             TEAMHAWKEYE.HVK_PET PET ON PRES.PET_PET_NUMBER = PET.PET_NUMBER INNER JOIN
             TEAMHAWKEYE.HVK_OWNER OWN ON PET.OWN_OWNER_NUMBER = OWN.OWNER_NUMBER
             WHERE (RES.RESERVATION_START_DATE <= SYSDATE) AND (RES.RESERVATION_END_DATE > SYSDATE) AND (PRES.RUN_RUN_NUMBER IS NOT NULL)
             AND PET.OWN_OWNER_NUMBER = :OWNER_NUMBER ";

            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("OWNER_NUMBER", ownerNumber);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;
            DataSet ds = new DataSet("dsActiveReservation");
            da.Fill(ds);
            return ds;

        }


        public DataSet listUpcomingReservation(DateTime reservationDate)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT TEAMHAWKEYE.HVK_RESERVATION.RESERVATION_NUMBER,
  TEAMHAWKEYE.HVK_RESERVATION.RESERVATION_START_DATE,
  TEAMHAWKEYE.HVK_RESERVATION.RESERVATION_END_DATE,
  TEAMHAWKEYE.HVK_PET.PET_NUMBER,
  TEAMHAWKEYE.HVK_RUN.RUN_NUMBER,
  TEAMHAWKEYE.HVK_OWNER.OWNER_NUMBER
FROM TEAMHAWKEYE.HVK_OWNER
INNER JOIN TEAMHAWKEYE.HVK_PET
ON TEAMHAWKEYE.HVK_OWNER.OWNER_NUMBER = TEAMHAWKEYE.HVK_PET.OWN_OWNER_NUMBER,
  TEAMHAWKEYE.HVK_RESERVATION,
  TEAMHAWKEYE.HVK_RUN
WHERE TEAMHAWKEYE.HVK_RESERVATION.RESERVATION_START_DATE >= :DateParameter";

            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("DateParameter", reservationDate);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;
            DataSet ds = new DataSet("dsActiveReservation");
            da.Fill(ds);
            return ds;
        }

        public int addReservation(int petNum, DateTime startDate, DateTime endDate)
        {
            int result = 0;
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"INSERT INTO HVK_RESERVATION
                                (
                                    RESERVATION_NUMBER, 
                                    RESERVATION_START_DATE,
                                    RESERVATION_END_DATE
                                )
                                VALUES
                                (
                                    HVK_RESERVATION_SEQ.NEXTVAL,
                                    :start,
                                    :end,
                                )";
            string cmdAddPetRes = @"INSERT INTO HVK_PET_RESERVATION
                                (
                                    PET_RES_NUMBER,
                                    PET_PET_NUMBER,
                                    RES_RESERVATION_NUMBER
                                )
                                VALUES
                                (
                                    HVK_PET_RES_SEQ.NEXTVAL,
                                    :petNum,
                                    HVK_RESERVATION_SEQ.CURRVAL
                                )";

            string cmdAddService = @"INSERT INTO HVK_PET_RESERVATION
                                (
                                    SERVICE_FREQUENCY,
                                    PR_PET_RES_NUMBER,
                                    SERV_SERVICE_NUMBER
                                )
                                VALUES
                                (
                                    NULL,
                                    HVK_PET_RES_SEQ.CURRVAL,
                                    1
                                )";

            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("start", startDate);
            cmd.Parameters.Add("end", endDate);

            OracleCommand cmd2 = new OracleCommand(cmdAddPetRes, con);
            cmd2.Parameters.Add("petNum", cmdAddPetRes);
            OracleCommand cmd3 = new OracleCommand(cmdAddService, con);
            
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.InsertCommand = cmd;
            OracleDataAdapter da2 = new OracleDataAdapter(cmd2);
            da2.InsertCommand = cmd2;
            OracleDataAdapter da3 = new OracleDataAdapter(cmd3);
            da3.InsertCommand = cmd3;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                cmd3.ExecuteNonQuery();
            }
            catch
            {
                result = -1;
            }
            finally
            {
                con.Close();
            }
            return result;
        }


        public int changeReservation(int resNum, DateTime startDate, DateTime endDate)
        {
            int result = 0;
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"INSERT INTO HVK_RESERVATION
                                (
                                    RESERVATION_NUMBER, 
                                    RESERVATION_START_DATE,
                                    RESERVATION_END_DATE
                                )
                                VALUES
                                (
                                    HVK_RESERVATION_SEQ.NEXTVAL,
                                    :start,
                                    :end,
                                )";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("start", startDate);
            cmd.Parameters.Add("end", endDate);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.InsertCommand = cmd;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                result = -1;
            }
            finally
            {
                con.Close(); 
            }
            return result;
        }
    }
}
