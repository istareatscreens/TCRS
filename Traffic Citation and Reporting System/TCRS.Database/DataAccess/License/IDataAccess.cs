using System.Collections.Generic;
using TCRS.Database.Model;

namespace TCRS.Database
{
    //License
    public partial interface IDataAccess
    {
        IEnumerable<License> GetCitizenInfoByLicenseID(string license_id, string connectionString);
        IEnumerable<License_Plate> GetVehicleInfoByLicensePlate(string plate_number, string connectionString);
        IEnumerable<License> GetAllLicenseInfoByLicence(string license_id, string connectionString);
        IEnumerable<License_Plate> GetAllVehicleInfoByLicencePlate(string licencePlate, string connectionString);
    }
}
