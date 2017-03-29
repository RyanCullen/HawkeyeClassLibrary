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
    public class SearchDB
    {
        public int searchDB(String cmdStr , String parameterName , int parmNum)
        {
            
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add(parameterName, parmNum);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

            try
            {
               con.Open();
               return Convert.ToInt16(cmd.ExecuteScalar());
            }
            catch
            {
                return -1; 
            }
            finally
            {
                con.Close();
            }

        }

        public int searchConflictingReservations(int petNumber, DateTime startDate, DateTime endDate)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT COUNT(*)
                                FROM HVK_RESERVATION R
                                JOIN HVK_PET_RESERVATION PR
                                ON R.RESERVATION_NUMBER=PR.RES_RESERVATION_NUMBER
                                WHERE (R.RESERVATION_START_DATE BETWEEN :start AND :end OR R.RESERVATION_END_DATE BETWEEN :start AND :end)
                                AND PR.PET_PET_NUMBER = :petNum";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.BindByName = true;
            cmd.Parameters.Add("start", startDate);
            cmd.Parameters.Add("end", endDate);
            cmd.Parameters.Add("petNum", petNumber);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

            try
            {
                con.Open();
                return Convert.ToInt16(cmd.ExecuteScalar());
            }
            catch(Exception e)
            {
                Console.Write(e);
                return -1;
            }
            finally
            {
                con.Close();
            }


        }

        public char getPetSize(int petNumber)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT DOG_SIZE FROM HVK_PET WHERE PET_NUMBER = :petNum";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("petNum", petNumber);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

            try
            {
                con.Open();
                return Convert.ToChar(cmd.ExecuteScalar());
            }
            catch
            {
                return 'U';
            }
            finally
            {
                con.Close();
            }
        }
        public int searchPetDB(int petNumber)
        {
          string cmdStr =   @"SELECT COUNT(*)
FROM HVK_PET
WHERE
PET_NUMBER = :PET_NUMBER
GROUP BY PET_NUMBER
";

            return searchDB(cmdStr, "PET_NUMBER", petNumber); 

        }




        public int searchOwnerDB(int ownerNumber)
        {
            string cmdStr = @"SELECT COUNT(*)
FROM HVK_OWNER
WHERE
OWNER_NUMBER = :OWNER_NUMBER
GROUP BY OWNER_NUMBER
";

            return searchDB(cmdStr, "OWNER_NUMBER", ownerNumber);

        }

        public int searchReservationDB(int resNum)
        {
            string cmdStr = @"SELECT COUNT(*)
FROM HVK_RESERVATION
WHERE
Reservation_number = :RESERVATION_NUMBER
GROUP BY RESERVATION_NUMBER
";

            return searchDB(cmdStr, "RESERVATION_NUMBER", resNum);
        }


        public int searchVaccDB(int vacNum)
        {
            string cmdStr = @"SELECT COUNT(*)
FROM HVK_VACCINATION
WHERE
VACCINATION_NUMBER = :VACCINATION_NUMBER
GROUP BY VACCINATION_NUMBER
";

            return searchDB(cmdStr, "VACCINATION_NUMBER", vacNum);
        }




        public int searchPetResDB(int petRes)
        {
            string cmdStr = @"SELECT COUNT(*)
FROM HVK_PET_RESERVATION
WHERE
PET_RES_NUMBER = :PET_RES_NUMBER
GROUP BY 
PET_RES_NUMBER
";

            return searchDB(cmdStr, "PET_RES_NUMBER", petRes);
        }


        //check if pet has already a reservation in the range of date passed in 
        public int searchReservationForPet(int petNum)
        {

            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT RES.RESERVATION_START_DATE , RES.RESERVATION_END_DATE
FROM HVK_PET_RESERVATION PRES ,
  HVK_RESERVATION RES
WHERE PET_PET_NUMBER            = :PET_PET_NUMBER
AND RES.RESERVATION_NUMBER      = PRES.RES_RESERVATION_NUMBER";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.BindByName = true;
            cmd.Parameters.Add("PET_PET_NUMBER", petNum);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

            da.SelectCommand = cmd;
            DataSet ds = new DataSet("AvailableRuns");
            da.Fill(ds);
            for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DateTime start = Convert.ToDateTime(ds.Tables[0].Rows[i]["RESERVATION_START_DATE"].ToString()).Date;
                DateTime end = Convert.ToDateTime((ds.Tables[0].Rows[i]["RESERVATION_END_DATE"].ToString())).Date;
                if (searchConflictingReservations(petNum, start, end) > 0)
                    return -1;
            }



            try
            {
                con.Open();
                cmd.ExecuteNonQuery(); 

                return Convert.ToInt16(cmd.ExecuteScalar());
            }
            catch (Exception e)
            {
                Console.Write(e);
                return -1;
            }
            finally
            {
                con.Close();
            }

        }


    }
}
