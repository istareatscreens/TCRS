using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TCRS.Database.Model;
using TCRS.Shared.Helper;

namespace TCRS.Database
{
    public class DataAccess : IDataAccess
    {
        //get Data queries
        public IEnumerable<T> SyncLoadData<T, U>(string sql, U parameters, string connectionString)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                //Not returning directly to allow for easier debugging
                var rows = connection.Query<T>(sql, parameters);
                return rows;
            }
        }

        public async Task<IEnumerable<T>> LoadData<T, U>(string sql, U parameters, string connectionString)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                //Not returning directly to allow for easier debugging
                var rows = await connection.QueryAsync<T>(sql, parameters);
                return rows;
            }

        }

        public IEnumerable<T> GetAll<T>(string connectionString, T Model)
        {

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {

                string sqlQuery = @"Select * FROM Person";
                return connection.Query<T>(sqlQuery);
            }
        }

        public IEnumerable<T> GetAllCitationType<T>(string connectionString, T Model)
        {

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {

                string sqlQuery = @"Select * FROM Citation_Type";
                return connection.Query<T>(sqlQuery);
            }
        }

        public void SaveData<U>(string sql, U parameters, string connectionString)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                connection.Execute(sql, parameters);
            }

        }

        //TODO Move this server side
        //Refresh Token
        public void SaveRefreshToken(RefreshToken refreshToken, string connectionString)
        {
            SaveData<RefreshToken>("INSERT INTO refreshtoken (person_id, token, expiry_date) VALUES (@person_id, @token, @expiry_date);", refreshToken, connectionString);
        }

        //Login
        public async Task<Person> GetUser(Person person, string connectionString)
        {
            var sql = @$"SELECT * FROM (SELECT * FROM person WHERE email = @email AND password = @password) as p
                LEFT JOIN client_admin ON client_admin.person_id =  p.person_id
                LEFT JOIN highway_patrol_officer ON highway_patrol_officer.person_id = p.person_id
                LEFT JOIN municipal_officer ON municipal_officer.person_id = p.person_id
                LEFT JOIN school_rep ON school_rep.person_id = p.person_id
                LEFT JOIN municipality ON municipality.manager_id = p.person_id
                LEFT JOIN police_dept ON police_dept.manager_id = p.person_id";

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                //Not returning directly to allow for easier debugging
                var rows = await connection.QueryAsync<Person, Client_Admin, Highway_Patrol_Officer, Municipal_Officer, School_Rep, Municipality, Police_Dept, Person>(sql, (Person, Client_Admin, Highway_Patrol_Officer, Municipal_Officer, School_Rep, Municipality, Police_Dept) =>
                {
                    Person.Client_Admin = Client_Admin;
                    Person.Highway_Patrol_Officer = Highway_Patrol_Officer;
                    Person.Municipal_Officer = Municipal_Officer;
                    Person.School_Rep = School_Rep;
                    Person.Municipality = Municipality;
                    Person.Police_Dept = Police_Dept;
                    return Person;
                }, new { email = person.email, password = person.password }, splitOn: "person_id, person_id, person_id, person_id, munic_id, police_dept_id");

                return (Person)(rows.FirstOrDefault<Person>());
            }
        }

        //Citation
        public IEnumerable<Citation> GetCitationsByLicensePlate(string plate_number, string connectionString)
        {
            var sql = @$"SELECT * FROM (SELECT * FROM license_plate where plate_number = @plate_number) as plate
                LEFT JOIN vehicle_record ON vehicle_record.vehicle_id = plate.vehicle_id
                LEFT JOIN citation ON citation.citation_id = vehicle_record.citation_id
                LEFT JOIN citation_type ON citation_type.citation_type_id = citation.citation_type_id";

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                //Not returning directly to allow for easier debugging
                var rows = connection.Query<License_Plate, Vehicle_Record, Citation, Citation_Type, Citation>(sql, (License_Plate, Vehicle_Record, Citation, Citation_Type) =>
              {
                  Citation.Vehicle_Record = Vehicle_Record;
                  Citation.Citation_Type = Citation_Type;
                  return Citation;
              }, new { plate_number = plate_number }, splitOn: "plate_number, vehicle_id, citation_id, citation_type_id");

                return rows;
            }
        }

        //General
        public IEnumerable<Citation> GetCitationByNumber(string citation_number, string connectionString)
        {
            var sql = "SELECT * FROM (SELECT citation_id, date_recieved, citation_type_id as type_id, officer_id FROM citation WHERE citation_number = @citation_number) as cit" +
                    " LEFT JOIN citation_type ON citation_type.citation_type_id = cit.type_id";

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                //Not returning directly to allow for easier debugging
                var rows = connection.Query<Citation, Citation_Type, Citation>(sql, (Citation, Citation_Type) =>
              {
                  Citation.Citation_Type = Citation_Type;
                  Citation.citation_type_id = Citation_Type.citation_type_id;
                  return Citation;
              }, new { citation_number = citation_number }, splitOn: "citation_id, citation_type_id");

                return rows;
            }
        }


        //Issue Citation
        public IEnumerable<License_Plate> GetVehicleInfoByLicencePlate(string licencePlate, string connectionString)
        {
            return SyncLoadData<License_Plate, DynamicParameters>(
                                   "SELECT * FROM license_plate WHERE plate_number = @plate_number",
                                   new DynamicParameters(new { plate_number = licencePlate }),
                                   connectionString);
        }

        public IEnumerable<License> GetLicenseInfoByLicence(string license_id, string connectionString)
        {

            /*
            var sql = @"SELECT * FROM (SELECT * FROM License  WHERE license_id = @license_id) as lic" +
                "LEFT JOIN citizen ON citizen.citizen_id = lic.citizen_id";
            */
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

        private IEnumerable<Citation> PostCitation(Citation Citation, string connectionString)
        {
            SaveData<DynamicParameters>("INSERT INTO `Citation` " +
                "(`citation_number`, `date_recieved`, `citation_type_id`, `officer_id`)" +
                " VALUES (@citation_number, @date, @citation_type_id, @officer_id)",
                new DynamicParameters(new { citation_number = Citation.citation_number, date = Citation.date_recieved, citation_type_id = Citation.citation_type_id, officer_id = Citation.officer_id }),
                connectionString);
            return SyncLoadData<Citation, DynamicParameters>("SELECT * FROM Citation WHERE citation_number=@citation_number", new DynamicParameters(new { citation_number = Citation.citation_number }), connectionString);
        }

        public void PostCitizenCitation(Citation Citation, License License, string connectionString)
        {
            var NewCitation = IEnumerableHandler.UnpackIEnumerable<Citation>(PostCitation(Citation, connectionString));
            SaveData<DynamicParameters>("INSERT INTO `driver_record`" +
                                                  " (`citizen_id`, `citation_id`)" +
                                                  " VALUES (@citizen_id, @citation_id)",
                                                  new DynamicParameters(
                                                      new
                                                      {
                                                          citizen_id = License.citizen_id,
                                                          citation_id = NewCitation.citation_id
                                                      }
                                                      ),
                                                 connectionString
                                                  );
        }

        public void PostVehicleCitation(Citation Citation, License_Plate License_Plate, string connectionString)
        {
            var NewCitation = IEnumerableHandler.UnpackIEnumerable<Citation>(PostCitation(Citation, connectionString));

            SaveData<DynamicParameters>(
               "INSERT INTO `Vehicle_Record`" +
               " (`vehicle_id`, `citation_id`)" +
               " VALUES (@vehicle_id, @citation_id)",
               new DynamicParameters(
                   new { vehicle_id = License_Plate.vehicle_id, citation_id = NewCitation.citation_id }),
               connectionString
               );
        }



    }
}
