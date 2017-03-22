using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace HawkeyehvkDB
{
    class ServiceDB
    {
        public DataSet listServices(long petRes) {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = "";
            OracleCommand cmd = new OracleCommand(cmdStr, con);

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

            DataSet ds = new DataSet("ServiceDataSet");
            da.Fill(ds, "hvk_Service");
            return ds;
        }
    }
}
