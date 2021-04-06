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
        public IEnumerable<License_Plate> GetVehicleInfoByLicencePlate(string plate_number, string connectionString)
        {
            var sql = "SELECT * FROM (SELECT plate_number, vehicle_id as v_id, expired FROM license_plate WHERE plate_number = @plate_number) as l " +
            "LEFT JOIN (SELECT vehicle_id, vin, name, stolen, make, registered, model, year_made, citizen_id, insurer_id as insur_id  FROM vehicle) as veh ON veh.vehicle_id = l.v_id " +
            "LEFT JOIN insurer ON insurer.insurer_id = veh.insur_id " +
            "LEFT JOIN citizen ON citizen.citizen_id = veh.citizen_id ";

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                //Not returning directly to allow for easier debugging
                var rows = connection.Query<License_Plate, Vehicle, Insurer, Citizen, License_Plate>(sql, (License_Plate, Vehicle, Insurer, Citizen) =>
               {
                   if (Vehicle != null)
                   {
                       License_Plate.vehicle_id = Vehicle.vehicle_id; //fix with alias
                       License_Plate.Vehicle = Vehicle;
                       License_Plate.Vehicle.Insurer = Insurer;
                       License_Plate.Vehicle.Citizen = Citizen;
                   }
                   if (Insurer != null)
                   {
                       License_Plate.Vehicle.insurer_id = Insurer.insurer_id; // fix alias
                   }
                   return License_Plate;
               }, new { plate_number = plate_number }, splitOn: "plate_number, vehicle_id, insurer_id, citizen_id");

                return rows;
            }
        }

        public IEnumerable<License> GetLicenseInfoByLicence(string license_id, string connectionString)
        {
            var sql = @"SELECT * FROM (SELECT license_id, citizen_id as cit_id, expiration_date, is_revoked, is_suspended, license_class FROM License  WHERE license_id = @license_id) as lic
                        LEFT JOIN (SELECT citizen_id, first_name, middle_name, last_name, dob, home_address, insurer_id as cit_insurer_id FROM citizen) as cit ON cit.citizen_id = lic.cit_id
                        LEFT JOIN insurer ON insurer.insurer_id = cit.cit_insurer_id";
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                //Not returning directly to allow for easier debugging
                var rows = connection.Query<License, Citizen, Insurer, License>(sql, (License, Citizen, Insurer) =>
               {
                   License.Citizen = Citizen;
                   if (Citizen != null)
                   {
                       License.citizen_id = Citizen.citizen_id; //needed to alias citizen id
                       if (Insurer != null)
                       {
                           License.Citizen.Insurer = Insurer;
                           License.Citizen.insurer_id = Insurer.insurer_id;
                       }
                   }
                   return License;
               }, new { license_id = license_id }, splitOn: "license_id, citizen_id, insurer_id");

                return rows;
            }
        }

    }
}
