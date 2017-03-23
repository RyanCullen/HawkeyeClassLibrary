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
    public class OwnerDB
    {
        public DataSet listOwners()
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = "SELECT OWNER_NUMBER, OWNER_LAST_NAME ||', '||OWNER_FIRST_NAME \"OWNER_NAME\" FROM HVK_OWNER ORDER BY OWNER_LAST_NAME";
            OracleCommand cmd = new OracleCommand(cmdStr, con);

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

            DataSet ds = new DataSet("ownerDataSet");
            da.Fill(ds, "hvk_owner");
            return ds;
        }

        public DataSet listOwner(int ownerNum)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = "SELECT OWNER_NUMBER, OWNER_LAST_NAME ||', '||OWNER_FIRST_NAME \"OWNER_NAME\" FROM HVK_OWNER WHERE OWNER_NUMBER = :OWNER_NUMBER ORDER BY OWNER_LAST_NAME";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("OWNER_NUMBER", ownerNum);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

            DataSet ds = new DataSet("ownerDataSet");
            da.Fill(ds, "hvk_owner");
            return ds;
        }
    }
}
