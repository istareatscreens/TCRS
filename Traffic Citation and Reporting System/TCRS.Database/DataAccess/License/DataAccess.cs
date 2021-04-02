using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TCRS.Database.Model;
using TCRS.Shared.Helper;

namespace TCRS.Database
{
    public partial class DataAccess
    {
        public IEnumerable<License> GetCitizenInfoByLicenseID(string license_id, string connectionString)
        {
            var sql = @$"SELECT * FROM (SELECT * FROM license where license_id = @license_id) as lic
		            LEFT JOIN citizen ON citizen.citizen_id = lic.citizen_id;";

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                //Not returning directly to allow for easier debugging
                var rows = connection.Query<License, Citizen, License>(sql, (License, Citizen) =>
                {
                    License.Citizen = Citizen;
                    return License;
                }, new { license_id = license_id }, splitOn: "license_id, citizen_id");

                return rows;
            }
        }

        public IEnumerable<License_Plate> GetVehicleInfoByLicensePlate(string plate_number, string connectionString)
        {
            var sql = @$"SELECT * FROM (SELECT * FROM license_plate where plate_number = @plate_number) as lic
		            LEFT JOIN vehicle ON vehicle.vehicle_id = lic.vehicle_id;";

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                //Not returning directly to allow for easier debugging
                var rows = connection.Query<License_Plate, Vehicle, License_Plate>(sql, (License_Plate, Vehicle) =>
                {
                    License_Plate.Vehicle = Vehicle;
                    return License_Plate;
                }, new { plate_number = plate_number }, splitOn: "plate_number, vehicle_id");

                return rows;
            }
        }
    }
}
