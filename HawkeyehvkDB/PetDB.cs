using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Configuration;

namespace HawkeyehvkDB
{
    public class PetDB
    {
        public DataSet listPetsDB(int ownerNumber)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleConnection con = new OracleConnection(conString);
            string cmdStr = @"SELECT PET_NUMBER, PET_NAME, PET_GENDER, 
                              PET_FIXED, PET_BREED, PET_BIRTHDATE, DOG_SIZE, SPECIAL_NOTES 
                              FROM HVK_PET WHERE OWN_OWNER_NUMBER = :OWNER_NUMBER 
                              ORDER BY PET_NUMBER";
            OracleCommand cmd = new OracleCommand(cmdStr, con);
            cmd.Parameters.Add("OWNER_NUMBER", ownerNumber);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.SelectCommand = cmd;
            DataSet ds = new DataSet("petDataSet");
            da.Fill(ds, "hvk_pet");
            return ds;
        }
    }
}
