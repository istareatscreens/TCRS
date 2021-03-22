using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using TCRS.Database.Model;

namespace TCRS.Database
{
    public class DataAccess : IDataAccess
    {
        //get Data queries
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
        
        public  IEnumerable<Citation> GetCitationsByLicensePlate(string plate_number, string connectionString)
        {
            var sql = @$"SELECT * FROM (SELECT * FROM license_plate where plate_number = @plate_number) as plate
                LEFT JOIN vehicle_record ON vehicle_record.vehicle_id = plate.vehicle_id
                LEFT JOIN citation ON citation.citation_id = vehicle_record.citation_id
                LEFT JOIN citation_type ON citation_type.citation_type_id = citation.citation_type_id";

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                //Not returning directly to allow for easier debugging
                var rows =  connection.Query<License_Plate, Vehicle_Record, Citation, Citation_Type, Citation> (sql, (License_Plate, Vehicle_Record, Citation,Citation_Type) =>
                {
                    Citation.Vehicle_Record = Vehicle_Record;
                    Citation.Citation_Type = Citation_Type;
                    return Citation;
                }, new { plate_number = plate_number }, splitOn: "plate_number, vehicle_id, citation_id, citation_type_id");

                return rows;
            }
        }

        public IEnumerable<Citation> GetCitationsByCitizen(string first_name, string connectionString)
        {
            var sql = @$"SELECT * FROM (SELECT * FROM citizen WHERE first_name = @first_name) as fname	
	            LEFT JOIN driver_record ON driver_record.citizen_id = fname.citizen_id
	            LEFT JOIN citation ON citation.citation_id = driver_record.citation_id
                LEFT JOIN citation_type ON citation_type.citation_type_id = citation.citation_type_id";
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                var rows = connection.Query<Citizen, Driver_Record, Citation, Citation_Type, Citation>(sql, (Citizen, Driver_Record, Citation, Citation_Type) =>
                {
                    Citation.Driver_Record = Driver_Record;
                    Citation.Citation_Type = Citation_Type;
                    return Citation;
                }, new { first_name = first_name }, splitOn: "citizen_id, citizen_id, citation_id, citation_type_id");

                return rows;
            }
        }

        public IEnumerable<Police_Dept> GetPoliceDeptEmployeesByManager(int manager_id, string connectionString)
        {
            var sql = @$"SELECT * FROM (SELECT * FROM police_dept WHERE manager_id = @manager_id) as mid
		        LEFT JOIN highway_patrol_officer ON highway_patrol_officer.police_dept_id = mid.police_dept_id
                LEFT JOIN person ON person.person_id = highway_patrol_officer.person_id";
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                var rows = connection.Query<Police_Dept, Highway_Patrol_Officer, Person, Police_Dept>(sql, (Police_Dept, Highway_Patrol_Officer, Person) =>
                {
                    Police_Dept.Highway_Patrol_Officers = (ICollection<Highway_Patrol_Officer>)Highway_Patrol_Officer;
                    Police_Dept.Persons = Person;
                    return Police_Dept;
                }, new { manager_id = manager_id }, splitOn: "police_dept_id, person_id, person_id");

                return rows;
            }
        }

        public IEnumerable<Municipality> GetMunicipalOfficersByManager(int manager_id, string connectionString)
        {
            var sql = @$"SELECT * FROM (SELECT * FROM municipality WHERE manager_id = @manager_id) as mid
		        LEFT JOIN municipal_officer ON municipal_officer.munic_id = mid.munic_id
                LEFT JOIN person ON person.person_id = municipal_officer.person_id";
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                var rows = connection.Query<Municipality, Municipal_Officer, Person, Municipality>(sql, (Municipality, Municipal_Officer, Person) =>
                {
                    Municipality.Municipal_Officers = (ICollection<Municipal_Officer>)Municipal_Officer;
                    Municipality.Person = Person;
                    return Municipality;
                }, new { manager_id = manager_id }, splitOn: "munic_id, person_id, person_id");

                return rows;
            }
        }
    }
}
