using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TCRS.Database.Model;

namespace TCRS.Database
{
    //User
    public partial class DataAccess
    {
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

        //Refresh Token
        public void SaveRefreshToken(RefreshToken refreshToken, string connectionString)
        {
            SaveData<RefreshToken>("INSERT INTO refreshtoken (person_id, token, expiry_date) VALUES (@person_id, @token, @expiry_date);", refreshToken, connectionString);
        }

        //Person lookup
        public Person GetPersonInfo(int person_id, string connectionString)
        {
            var sql = @$"SELECT * FROM (SELECT * FROM person WHERE person_id = @person_id) as p
                LEFT JOIN client_admin ON client_admin.person_id =  p.person_id
                LEFT JOIN highway_patrol_officer ON highway_patrol_officer.person_id = p.person_id
                LEFT JOIN municipal_officer ON municipal_officer.person_id = p.person_id
                LEFT JOIN school_rep ON school_rep.person_id = p.person_id
                LEFT JOIN municipality ON municipality.munic_id = municipal_officer.munic_id
                LEFT JOIN police_dept ON police_dept.police_dept_id = highway_patrol_officer.police_dept_id";

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                //Not returning directly to allow for easier debugging
                var rows = connection.Query<Person, Client_Admin, Highway_Patrol_Officer, Municipal_Officer, School_Rep, Municipality, Police_Dept, Person>(sql, (Person, Client_Admin, Highway_Patrol_Officer, Municipal_Officer, School_Rep, Municipality, Police_Dept) =>
               {
                   Person.Client_Admin = Client_Admin;
                   Person.Highway_Patrol_Officer = Highway_Patrol_Officer;
                   Person.Municipal_Officer = Municipal_Officer;
                   Person.School_Rep = School_Rep;
                   Person.Municipality = Municipality;
                   Person.Police_Dept = Police_Dept;
                   return Person;
               }, new { person_id = person_id }, splitOn: "person_id, person_id, person_id, person_id, munic_id, police_dept_id");

                return (Person)(rows.FirstOrDefault<Person>());
            }
        }

    }
}
