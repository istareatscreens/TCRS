using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCRS.Database.Model;

namespace TCRS.Database
{
    public partial interface IDataAccess
    {
        IEnumerable<License> GetCitizenInfoByLicenseID(string license_id, string connectionString);
        IEnumerable<License_Plate> GetVehicleInfoByLicensePlate(string plate_number, string connectionString);
    }
}
