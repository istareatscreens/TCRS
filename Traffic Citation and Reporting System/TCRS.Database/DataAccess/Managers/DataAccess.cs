using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCRS.Database.Model;

//Managers
namespace TCRS.Database
{
    public partial class DataAccess
    {
        public IEnumerable<Police_Dept> GetPoliceDeptEmployeesByManager(int manager_id, string connectionString)
        {
            var sql = @$"SELECT * FROM (SELECT * FROM police_dept WHERE manager_id = @manager_id) as mid
		        LEFT JOIN highway_patrol_officer ON highway_patrol_officer.police_dept_id = mid.police_dept_id
                LEFT JOIN person ON person.person_id = highway_patrol_officer.person_id";
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                var rows = connection.Query<Police_Dept, Highway_Patrol_Officer, Person, Police_Dept>(sql, (Police_Dept, Highway_Patrol_Officer, Person) =>
                {
                    //Police_Dept.Highway_Patrol_Officers = Highway_Patrol_Officer;
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
                    //Municipality.Municipal_Officers = Municipal_Officer;
                    Municipality.Person = Person;
                    return Municipality;
                }, new { manager_id = manager_id }, splitOn: "munic_id, person_id, person_id");

                return rows;
            }
        }
    }
}
