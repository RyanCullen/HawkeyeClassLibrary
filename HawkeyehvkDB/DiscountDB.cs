using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;

namespace HawkeyehvkDB
{
    public class DiscountDB
    {
        public DataSet listReservationDiscounts(int reservationNumber)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT D.DISCOUNT_NUMBER, D.DISCOUNT_DESCRIPTION, D.DISCOUNT_PERCENTAGE, D.DISCOUNT_TYPE
                            FROM HVK_RESERVATION_DISCOUNT R
                            JOIN HVK_DISCOUNT D
                            ON R.DISC_DISCOUNT_NUMBER = D.DISCOUNT_NUMBER
                            WHERE R.RES_RESERVATION_NUMBER = :RESERVATION_NUMBER";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("OWNER_NUMBER", reservationNumber);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;
            DataSet ds = new DataSet("resDiscountDataSet");
            da.Fill(ds, "hvk_res_discount");
            return ds;
        }

        public DataSet listPetReservationDiscounts(int petReservationNumber)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT D.DISCOUNT_NUMBER, D.DISCOUNT_DESCRIPTION, D.DISCOUNT_PERCENTAGE, D.DISCOUNT_TYPE
                            FROM HVK_PET_RESERVATION_DISCOUNT R
                            JOIN HVK_DISCOUNT D
                            ON R.DISC_DISCOUNT_NUMBER = D.DISCOUNT_NUMBER
                            WHERE R.PR_PET_RES_NUMBER = :PET_RESERVATION_NUMBER";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("PET_RESERVATION_NUMBER", petReservationNumber);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;
            DataSet ds = new DataSet("petResDiscountDataSet");
            da.Fill(ds, "hvk_pet_res_discount");
            return ds;
        }
    }
}
