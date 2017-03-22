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
    public class VetDB
    {
        public DataSet listVets()
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = "Select vet_name from HVK_veterinarian";
            OracleCommand cmd = new OracleCommand(cmdStr, con);

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

            DataSet ds = new DataSet("vetDataSet");
            da.Fill(ds, "hvk_veterinarian");
            return ds;
        }

        public DataSet getVetByOwnerNum(int ownerNum)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = "Select vet_name from HVK_veterinarian, HVK_owner where owner_number = :ownerNum and vet_vet_number = vet_number";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("ownerNum", ownerNum);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;

            DataSet ds = new DataSet("vetDataSet");
            da.Fill(ds, "hvk_veterinarian");
            return ds;
        }

        public DataSet addVet(int vetNum, string vetName, string vetPhone, string vetStreet, string vetCity, string vetProvince, string vetPostalCode)
        {
            return null;
        }

    }
}
