﻿using System.Collections.Generic;
using TCRS.Database.Model;

namespace TCRS.Database
{
    //Citations
    public partial interface IDataAccess
    {
        void PostCitizenCitation(Citation Citation, License License, string connectionString);
        void PostVehicleCitation(Citation Citation, License_Plate License_Plate, string connectionString);
        IEnumerable<Citation> GetCitationsByLicensePlate(string plate_number, string connectionString);
        IEnumerable<Citation> GetCitationByNumber(string citation_number, string connectionString);
        IEnumerable<T> GetAllCitationType<T>(string connectionString, T Model);
    }
}
