﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HawkeyehvkDB;


namespace HawkeyehvkBLL
{
    class Search
    {
        public bool validatePetNumber(int petNum)
        {
            SearchDB db = new SearchDB();
            return db.searchPetDB(petNum) == 1 ? true : false;
        }


        public bool validateOwnerNumber(int ownNum)
        {
            SearchDB db = new SearchDB();
            return db.searchOwnerDB(ownNum) == 1 ? true : false;
        }

        public bool validateReservationNumber(int resNum)
        {
            SearchDB db  = new SearchDB();
            return db.searchReservationDB(resNum) == 1 ? true : false;

        }

        public bool validateVaccNumber(int vaccNum)
        {
            SearchDB db = new SearchDB();
            return db.searchVaccDB(vaccNum) == 1 ? true : false;
        }

    }
}
