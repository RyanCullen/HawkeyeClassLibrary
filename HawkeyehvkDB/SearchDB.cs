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
OWNER_NUMBER = :RESERVATION_NUMBER
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


    }
}
