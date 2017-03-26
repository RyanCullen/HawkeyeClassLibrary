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
    public class PetReservationDB
    {
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
        public DataSet NumberOfRunsAvailableDB(DateTime start, DateTime end)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT CASE P.DOG_SIZE WHEN 'L' THEN 'L'
									ELSE 'M/S'
							END AS DOG_SIZE,
							    COUNT(*)
							FROM HVK_PET_RESERVATION PR
							JOIN HVK_RESERVATION RES
							    ON PR.RES_RESERVATION_NUMBER = RES.RESERVATION_NUMBER
							JOIN HVK_PET P
							    ON PR.PET_PET_NUMBER = P.PET_NUMBER
							WHERE(RES.RESERVATION_START_DATE >= :START_DATE
							    AND RES.RESERVATION_START_DATE <= :END_DATE)
							        OR(RES.RESERVATION_END_DATE >= :START_DATE
							            AND RES.RESERVATION_END_DATE <= :END_DATE)
							        OR(RES.RESERVATION_START_DATE <= :START_DATE
							            AND RES.RESERVATION_END_DATE >= :END_DATE)
							GROUP BY CASE P.DOG_SIZE WHEN 'L' THEN 'L'
									ELSE 'M/S' END";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("START_DATE", start);
            cmd.Parameters.Add("END_DATE", end);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;
            DataSet ds = new DataSet("NumRuns");
            da.Fill(ds, "hvk_numRuns");
            return ds;
        }
    }
}
