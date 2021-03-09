using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCRS_db.Model;

namespace TCRS_db
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

        public void SaveData<U>(string sql, U parameters, string connectionString)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                connection.Execute(sql, parameters);
            }

        }

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
                var rows = await connection.QueryAsync<Person, Client_Admin, Highway_Patrol_Officer, Municipal_Officer, School_Rep, Municipality, Police_Dept, Person>(sql, (Person, Client_Admin, Highway_Patrol_Officer, Municipal_Officer, School_Rep, Municipality, Police_Dept) => {
                    Person.Client_Admin = Client_Admin;
                    Person.Highway_Patrol_Officer = Highway_Patrol_Officer;
                    Person.Municipal_Officer = Municipal_Officer;
                    Person.School_Rep = School_Rep;
                    Person.Municipality = Municipality;
                    Person.Police_Dept = Police_Dept;
                    return Person;
                }, new { email= person.email, password= person.password}, splitOn: "person_id, person_id, person_id, person_id, munic_id, police_dept_id"); 

                return (Person)(rows.FirstOrDefault<Person>());
            }
        }

    }
}
