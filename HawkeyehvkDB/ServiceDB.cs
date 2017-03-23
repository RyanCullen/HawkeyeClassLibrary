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
    public class ServiceDB
    {
        public DataSet listServicesDB(long petRes) {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"select s.service_number, s.service_description, rs.service_frequency, dr.DAILY_RATE_NUMBER, dr.DAILY_RATE_DOG_SIZE
from HVK_SERVICE s, HVK_PET_RESERVATION_SERVICE rs, HVK_Daily_rate dr
where rs.serv_service_number = s.service_number
and dr.serv_service_number = s.service_number
and rs.PR_PET_RES_NUMBER = "+petRes;
            OracleCommand cmd = new OracleCommand(cmdStr, con);

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

            DataSet ds = new DataSet("ServiceDataSet");
            da.Fill(ds, "hvk_Service");
            return ds;
        }
    }
}
