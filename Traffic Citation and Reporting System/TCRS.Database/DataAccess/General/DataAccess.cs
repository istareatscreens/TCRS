using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using TCRS.Database.Model;

namespace TCRS.Database
{
    //General
    public partial class DataAccess
    {
        public IEnumerable<Citizen> GetCitizenById(int citizen_id, string connectionString)
        {
            return SyncLoadData<Citizen, Citizen>(@"SELECT * FROM citizen WHERE citizen_id = @citizen_id", new Citizen { citizen_id = citizen_id }, connectionString);
        }
        public IEnumerable<License_Plate> GetVehicleInfoByLicencePlate(string licencePlate, string connectionString)
        {
            return SyncLoadData<License_Plate, DynamicParameters>(
                                   "SELECT * FROM license_plate WHERE plate_number = @plate_number",
                                   new DynamicParameters(new { plate_number = licencePlate }),
                                   connectionString);
        }

        public IEnumerable<License> GetLicenseInfoByLicence(string license_id, string connectionString)
        {
            var sql = @"SELECT * FROM (SELECT license_id expiry_date, is_revoked, is_suspended, license_class, citizen_id as license_citizen_id FROM License  WHERE license_id = @license_id) as lic
                        INNER JOIN citizen ON citizen.citizen_id = lic.license_citizen_id";
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                //Not returning directly to allow for easier debugging
                var rows = connection.Query<License, Citizen, License>(sql, (License, Citizen) =>
               {
                   License.Citizen = Citizen;
                   License.citizen_id = Citizen.citizen_id; //needed to alias citizen id
                   return License;
               }, new { license_id = license_id }, splitOn: "license_id, citizen_id");

                return rows;
            }
        }

        public IEnumerable<Wanted_Citizen> GetWantedCitizenInfoByCitizenId(int citizen_id, string connectionString)
        {
            var sql = @"SELECT * FROM (SELECT * FROM wanted_citizen WHERE citizen_id = @citizen_id) as cit
                        LEFT JOIN wanted ON wanted.wanted_id = cit.wanted_id;";
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                //Not returning directly to allow for easier debugging
                var rows = connection.Query<Wanted_Citizen, Wanted, Wanted_Citizen>(sql, (Wanted_Citizen, Wanted) =>
                {
                    Wanted_Citizen.Wanted = Wanted;
                    return Wanted_Citizen;
                }, new { citizen_id = citizen_id }, splitOn: "citizen_id, wanted_id");

                return rows;
            }
        }

    }
}
