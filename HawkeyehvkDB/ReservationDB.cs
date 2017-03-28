﻿using Oracle.ManagedDataAccess.Client;
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

        public DataSet listResevationsDB()
        {
            //return a list of reservation with it details 
            //Display : clerk or user edit reservation 
            //To be fixed : does not include sharing with pet name 
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT RES.RESERVATION_NUMBER,
                              RES.RESERVATION_START_DATE,
                              RES.RESERVATION_END_DATE,
                              PRES.RUN_RUN_NUMBER,
                              PET.PET_NAME,
                              SERV.SERVICE_DESCRIPTION,
                              RES_SERV.SERVICE_FREQUENCY,
                              PET.PET_NUMBER,
                              PET.PET_GENDER,
                              PET.PET_FIXED,
                              PET.PET_BREED,
                              PET.PET_BIRTHDATE,
                              PET.DOG_SIZE,
                              PET.SPECIAL_NOTES,
                              PET.OWN_OWNER_NUMBER
                            FROM TEAMHAWKEYE.HVK_RESERVATION RES
                            INNER JOIN TEAMHAWKEYE.HVK_PET_RESERVATION PRES
                            ON RES.RESERVATION_NUMBER = PRES.RES_RESERVATION_NUMBER
                            INNER JOIN TEAMHAWKEYE.HVK_PET PET
                            ON PRES.PET_PET_NUMBER = PET.PET_NUMBER
                            INNER JOIN TEAMHAWKEYE.HVK_PET_RESERVATION_SERVICE RES_SERV
                            ON PRES.PET_RES_NUMBER = RES_SERV.PR_PET_RES_NUMBER
                            INNER JOIN TEAMHAWKEYE.HVK_SERVICE SERV
                            ON RES_SERV.SERV_SERVICE_NUMBER = SERV.SERVICE_NUMBER
                            ORDER BY RES.RESERVATION_NUMBER  ";
            OracleCommand cmd = new OracleCommand(cmdStr, con);

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;
            DataSet ds = new DataSet("dsReservation");
            da.Fill(ds);

            return ds;
        }

        public DataSet listResevationsDB(int ownerNumber)
        {
            //Overloaded return a list of reservation with it details base on owner number 
            //Display : clerk or user edit reservation 
            //To be fixed : does not include sharing with pet name 
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT RES.RESERVATION_NUMBER,
                              RES.RESERVATION_START_DATE,
                              RES.RESERVATION_END_DATE,
                              PRES.RUN_RUN_NUMBER,
                              PET.PET_NAME,
                              SERV.SERVICE_DESCRIPTION,
                              RES_SERV.SERVICE_FREQUENCY,
                              PET.PET_NUMBER,
                              PET.PET_GENDER,
                              PET.PET_FIXED,
                              PET.PET_BREED,
                              PET.PET_BIRTHDATE,
                              PET.DOG_SIZE,
                              PET.SPECIAL_NOTES,
                              PET.OWN_OWNER_NUMBER
                            FROM TEAMHAWKEYE.HVK_RESERVATION RES
                            INNER JOIN TEAMHAWKEYE.HVK_PET_RESERVATION PRES
                            ON RES.RESERVATION_NUMBER = PRES.RES_RESERVATION_NUMBER
                            INNER JOIN TEAMHAWKEYE.HVK_PET PET
                            ON PRES.PET_PET_NUMBER = PET.PET_NUMBER
                            INNER JOIN TEAMHAWKEYE.HVK_PET_RESERVATION_SERVICE RES_SERV
                            ON PRES.PET_RES_NUMBER = RES_SERV.PR_PET_RES_NUMBER
                            INNER JOIN TEAMHAWKEYE.HVK_SERVICE SERV
                            ON RES_SERV.SERV_SERVICE_NUMBER = SERV.SERVICE_NUMBER
                            WHERE  PET.OWN_OWNER_NUMBER = :OwnerNum
                            ORDER BY RES.RESERVATION_NUMBER";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("OwnerNum", ownerNumber);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;
            DataSet ds = new DataSet("dsReservation");
            da.Fill(ds);
            return ds;
        }


        public DataSet listActiveReservationsDB()
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
             WHERE (RES.RESERVATION_START_DATE <= SYSDATE) AND (RES.RESERVATION_END_DATE > SYSDATE) AND (PRES.RUN_RUN_NUMBER IS NOT NULL)
            ORDER BY RES.RESERVATION_NUMBER";

            OracleCommand cmd = new OracleCommand(cmdStr, con);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;
            DataSet ds = new DataSet("dsActiveReservation");
            da.Fill(ds);
            return ds;

        }


        public DataSet listActiveReservationsDB(int ownerNumber)
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
             AND PET.OWN_OWNER_NUMBER = :OWNER_NUMBER 
                ORDER BY RES.RESERVATION_NUMBER";

            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("OWNER_NUMBER", ownerNumber);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;
            DataSet ds = new DataSet("dsActiveReservation");
            da.Fill(ds);
            return ds;

        }


        public DataSet listUpcomingReservationsDB(DateTime reservationDate)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT RES.RESERVATION_NUMBER, RES.RESERVATION_START_DATE, RES.RESERVATION_END_DATE, PET.PET_NUMBER,PRES.RUN_RUN_NUMBER, O.OWNER_NUMBER, PET.OWN_OWNER_NUMBER
                FROM   TEAMHAWKEYE.HVK_PET_RESERVATION PRES INNER JOIN
             TEAMHAWKEYE.HVK_RESERVATION RES ON PRES.RES_RESERVATION_NUMBER = RES.RESERVATION_NUMBER INNER JOIN
             TEAMHAWKEYE.HVK_PET PET ON PRES.PET_PET_NUMBER = PET.PET_NUMBER INNER JOIN
             TEAMHAWKEYE.HVK_OWNER O ON PET.OWN_OWNER_NUMBER = O.OWNER_NUMBER 
                WHERE (RES.RESERVATION_START_DATE >= :DateParameter)";

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
        public DataSet listAvailableRunsDB(DateTime start, DateTime end)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT r.run_number, r.run_size FROM hvk_run r 
              MINUS
               (SELECT UNIQUE r.run_number,
                r.run_size
              FROM hvk_run r
            JOIN hvk_pet_reservation pr
                ON pr.run_run_number = r.run_number
            JOIN HVK_RESERVATION res
            ON PR.RES_RESERVATION_NUMBER = RES.RESERVATION_NUMBER
              AND (RES.RESERVATION_START_DATE >= :START_DATE 
            AND RES.RESERVATION_START_DATE <= :END_DATE)
            OR (RES.RESERVATION_END_DATE >= :START_DATE 
            AND RES.RESERVATION_END_DATE <= :END_DATE)
            OR (RES.RESERVATION_START_DATE <= :START_DATE
            AND RES.RESERVATION_END_DATE >= :END_DATE)
             )";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("START_DATE", start);
            cmd.Parameters.Add("END_DATE", end);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;
            DataSet ds = new DataSet("AvailableRuns");
            da.Fill(ds, "hvk_runsAvail");
            return ds;
        }

        //error checking required 
        public int addToReservationDB(int resNumber , int petNumber) {



            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);

            string cmdSelect = @" SELECT RUN_RUN_NUMBER
FROM   TEAMHAWKEYE.HVK_PET_RESERVATION
WHERE RES_RESERVATION_NUMBER = :ResNumber";

            string cmdStr = @"INSERT
INTO TEAMHAWKEYE.HVK_PET_RESERVATION
  (
    PET_RES_NUMBER,
    PET_PET_NUMBER,
    RES_RESERVATION_NUMBER,
    RUN_RUN_NUMBER,
    PR_SHARING_WITH
  )
  VALUES
  (
    HVK_PET_RES_SEQ.NEXTVAL,
    :PET_PET_NUMBER,
    :RES_RESERVATION_NUMBER,
    NULL,
    NULL
  )";






            OracleCommand cmd = new OracleCommand(cmdSelect, con);
            OracleCommand cmd2 = new OracleCommand(cmdStr, con);

            cmd.Parameters.Add("ResNumber", resNumber);
            OracleDataAdapter da1 = new OracleDataAdapter(cmd);
            da1.SelectCommand = cmd;




            try
            {
                con.Open();
            //if(cmd.ExecuteScalar() != null )
            //    int runNumber = cmd.ExecuteScalar().ToString();
                cmd2.Parameters.Add("PET_PET_NUMBER", petNumber);
                cmd2.Parameters.Add("RES_RESERVATION_NUMBER", resNumber);
                //cmd2.Parameters.Add("runNumber", (cmd.ExecuteScalar() is DBNull) ? null : cmd.ExecuteScalar());
            

            OracleDataAdapter da = new OracleDataAdapter(cmd2);
            da.InsertCommand = cmd2;
            cmd.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            }
            catch
            {
                return -1;
            }
            finally
            {
                con.Close();

            }

            return 1;


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
        public static int deleteDogFromReservationDB(int reservationNumber, int dogNumber) {
            // before using make sure the following scripts are run
            //Delete from hvk_pet_reservation_discount;
            //delete from hvk_pet_food;
            // update sharing with to null
            int result = 0;
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"Delete From hvk_pet_reservation_service
                            where PR_PET_RES_NUMBER= (select pet_res_number 
                                                      from hvk_pet_reservation 
                                                      where RES_RESERVATION_NUMBER = :RESNUM
                                                      and pet_pet_number = :PETNUM )";
            string cmdStr2 = @"Delete from hvk_pet_reservation
                                where RES_RESERVATION_NUMBER = :RESNUM
                                and pet_pet_number = :PETNUM";
            string cmdStr3 = @"delete from hvk_reservation where reservation_number in
                                (select reservation_number from hvk_reservation 
                                where reservation_number not in (select res_reservation_number 
                                                                 from hvk_pet_reservation))";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("PETNUM", dogNumber);
            cmd.Parameters.Add("RESNUM", reservationNumber);
            OracleCommand cmd2 = new OracleCommand(cmdStr2, con);
            cmd2.Parameters.Add("PETNUM", dogNumber);
            cmd2.Parameters.Add("RESNUM", reservationNumber);
            OracleCommand cmd3 = new OracleCommand(cmdStr3, con);

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.DeleteCommand = cmd;
            OracleDataAdapter da2 = new OracleDataAdapter(cmd2);
            da2.DeleteCommand = cmd2;
            OracleDataAdapter da3 = new OracleDataAdapter(cmd3);
            da3.DeleteCommand = cmd3;

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
        public static int cancelReservationDB(int resNum) {
            //Delete from hvk_pet_reservation_discount;
            //delete from hvk_pet_food;
            //delete from hvk_medication;
            //Update hvk_pet_reservation
            //set PR_SHARING_WITH = null;

            int result = 0;
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);

            string cmdStr=@"Delete from HVK_RESERVATION_DISCOUNT
            where RES_RESERVATION_NUMBER = :RESERVATION_NUMBER";

            string cmdStr2= @"Delete From hvk_pet_reservation_service
            where PR_PET_RES_NUMBER in (select pet_res_number
                                       from hvk_pet_reservation
                                       where RES_RESERVATION_NUMBER = :RESERVATION_NUMBER
                                       )";

            string cmdStr3= @"Delete from hvk_pet_reservation
            where RES_RESERVATION_NUMBER = :RESERVATION_NUMBER";

            string cmdStr4= @"Delete from HVK_Reservation
            where RESERVATION_NUMBER = :RESERVATION_NUMBER";

            
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("RESERVATION_NUMBER", resNum);
            OracleCommand cmd2 = new OracleCommand(cmdStr2, con);
            cmd2.Parameters.Add("RESERVATION_NUMBER", resNum);
            OracleCommand cmd3 = new OracleCommand(cmdStr3, con);
            cmd3.Parameters.Add("RESERVATION_NUMBER", resNum);
            OracleCommand cmd4 = new OracleCommand(cmdStr4, con);
            cmd4.Parameters.Add("RESERVATION_NUMBER", resNum);

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.DeleteCommand = cmd;
            OracleDataAdapter da2 = new OracleDataAdapter(cmd2);
            da2.DeleteCommand = cmd2;
            OracleDataAdapter da3 = new OracleDataAdapter(cmd3);
            da3.DeleteCommand = cmd3;
            OracleDataAdapter da4 = new OracleDataAdapter(cmd4);
            da4.DeleteCommand = cmd4;

            try {
                con.Open();
                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                cmd3.ExecuteNonQuery();
                cmd4.ExecuteNonQuery();
            }
            catch {
                result = -1;
            }
            finally {
                con.Close();
            }


            return result;
        }
        public static bool isDogInReservation(int petNum, int resNum) {
            
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr= @"select count(*) as countOfDogs
                                from hvk_pet_reservation pr
                                inner join hvk_reservation r 
                                on r.reservation_number = pr.RES_RESERVATION_NUMBER
                                where pr.PET_PET_NUMBER = :PETNUMBER
                                and r.RESERVATION_NUMBER = :RESNUMBER";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.CommandType = CommandType.Text;
            cmd.BindByName = true;
            cmd.Parameters.Add("PETNUMBER", petNum);
            cmd.Parameters.Add("RESNUMBER", resNum);

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;
            DataSet ds = new DataSet("DogInRes");
            da.Fill(ds, "hvk_DogInRes");

            int dog = Convert.ToInt32(ds.Tables[0].Rows[0]["countOfDogs"].ToString());
            // the above method returns the count of times the dog with input dog number occurs in the reservation
            // should be 1 or 0 (0 if the dog is not in the reservation)

            if (dog > 0)
                return true;
            else
                return false;
        }
    }
}
