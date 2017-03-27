﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Configuration;

namespace HawkeyehvkDB
{
    public class RunDB
    {
        public int totalLargeRunsDB()
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = "SELECT COUNT(*) FROM HVK_RUN WHERE RUN_SIZE = 'L'";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            int returned = -1;
            try
            {
                con.Open();
                returned = Convert.ToInt32(cmd.ExecuteScalar());
            }
            finally {
                con.Close();
            }
            return returned;
        }

        public int totalRegularRunsDB()
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = "SELECT COUNT(*) FROM HVK_RUN WHERE RUN_SIZE = 'R'";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            
            int returned = -1;
            try
            {
                con.Open();
                returned = Convert.ToInt32(cmd.ExecuteScalar());
            }
            finally
            {
                con.Close();
            }
            return returned;
        }

        public DataSet getReservationCountsDB(DateTime start, DateTime end)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT *
                            FROM
                              (SELECT COUNT(
                                CASE p.DOG_SIZE
                                  WHEN 'L'
                                  THEN 1
                                  ELSE NULL
                                END) AS LARGE_RESERVATIONS,
                                COUNT(
                                CASE p.DOG_SIZE
                                  WHEN 'L'
                                  THEN NULL
                                  ELSE 1
                                END) AS REGULAR_RESERVATIONS
                              FROM hvk_reservation r
                              JOIN hvk_pet_reservation pr
                              ON r.RESERVATION_NUMBER = pr.RES_RESERVATION_NUMBER
                              JOIN hvk_pet p
                              ON pr.PET_PET_NUMBER = p.PET_NUMBER
                              CROSS JOIN
                                (SELECT CAST(:endDate as DATE) - level + 1 AS DAY
                                FROM dual
                                  CONNECT BY LEVEL <= CAST(:endDate as DATE) - CAST(:startDate as DATE) + 1
                                )
                              WHERE DAY BETWEEN r.RESERVATION_START_DATE AND r.RESERVATION_END_DATE
                              GROUP BY DAY
                              ORDER BY COUNT(
                                CASE p.DOG_SIZE
                                  WHEN 'L'
                                  THEN 1
                                  ELSE NULL
                                END) + COUNT(
                                CASE p.DOG_SIZE
                                  WHEN 'L'
                                  THEN NULL
                                  ELSE 1
                                END) DESC,
                                DAY
                              )
                            WHERE rownum = 1";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.CommandType = CommandType.Text;
            cmd.BindByName = true;
            cmd.Parameters.Add("startDate", start);
            cmd.Parameters.Add("endDate", end);

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;
            DataSet ds = new DataSet("NumRunsReserved");
            da.Fill(ds, "hvk_numRunsReserved");
            return ds;
        }
    }
}