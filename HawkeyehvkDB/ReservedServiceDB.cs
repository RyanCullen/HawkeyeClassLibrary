using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace HawkeyehvkDB {
    public class ReservedServiceDB {

        public int addReservedService(int petResNum, int serviceNum) {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"INSERT INTO HVK_PET_RESERVATION_SERVICE VALUES
                                (
                                    NULL,
                                    :petResNum,
                                    :serviceNum
                                )";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("petResNum", petResNum);
            cmd.Parameters.Add("serviceNum", serviceNum);
            try {
                con.Open();
                cmd.ExecuteNonQuery();
                return 0;
            } catch {
                return -1;
            } finally {
                con.Close();
            }
        }

        public int deleteReservedServiceDB(int petResNum, int serviceNum) {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"DELETE FROM HVK_PET_RESERVATION_SERVICE
                                WHERE PR_PET_RES_NUMBER = :petResNum
                                AND SERV_SERVICE_NUMBER = :serviceNumber";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("petResNum", petResNum);
            cmd.Parameters.Add("serviceNum", serviceNum);
            try {
                con.Open();
                cmd.ExecuteNonQuery();
                return 0;
            } catch {
                return -1;
            } finally {
                con.Close();
            }
        }

    }
}
