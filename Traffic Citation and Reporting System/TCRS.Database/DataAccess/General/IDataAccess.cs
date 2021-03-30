using System.Collections.Generic;
using TCRS.Database.Model;

namespace TCRS.Database
{
    //General
    public partial interface IDataAccess
    {
        IEnumerable<License> GetLicenseInfoByLicence(string license_id, string connectionString);
        IEnumerable<Citizen> GetCitizenById(int citizen_id, string connectionString);
        IEnumerable<License_Plate> GetVehicleInfoByLicencePlate(string licencePlate, string connectionString);
    }
}
