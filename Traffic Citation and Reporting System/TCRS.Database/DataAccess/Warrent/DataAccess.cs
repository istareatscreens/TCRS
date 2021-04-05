using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TCRS.Database.Model;

namespace TCRS.Database
{
    //Warrant

    public partial class DataAccess
    {

        public void ChangeWarrantStatus(int reference_no, bool active_status, string connectionString)
        {
            var sql = @"UPDATE wanted SET active_status = @active_status WHERE (reference_no = @reference_no)"; //set course as resolved
            UpdateData<DynamicParameters>(sql, new DynamicParameters(new { active_status = active_status, reference_no = reference_no }), connectionString);
        }
        private Wanted CreateWarrant(Wanted Wanted, string connectionString)
        {
            var sql = "INSERT INTO `Wanted` (`reference_no`, `dangerous`, `crime`) VALUES (@reference_no, @dangerous, @crime)";
            SaveData<Wanted>(sql, Wanted, connectionString);
            sql = "SELECT * FROM wanted WHERE dangerous = @dangerous, reference_no = @reference_no, crime=@crime";
            var warrents = SyncLoadData<Wanted, Wanted>(sql, Wanted, connectionString);
            return warrents.ToList().LastOrDefault();
        }
        public void CreateCitizenWanted(Wanted Wanted, int citizen_id, string connectionString)
        {
            var newWanted = CreateWarrant(Wanted, connectionString);
            SaveData<DynamicParameters>("INSERT INTO `Wanted_Citizen` (`citizen_id`, `wanted_id`) VALUES(@citizen_id, @newWanted)",
            new DynamicParameters(new { citizen_id = citizen_id, wanted_id = newWanted.wanted_id }), connectionString);
        }
        public void CreateVehicleWanted(Wanted Wanted, int vehicle_id, string connectionString)
        {
            var newWanted = CreateWarrant(Wanted, connectionString);
            SaveData<DynamicParameters>("INSERT INTO `Wanted_Vehicle` (`vehicle_id`, `wanted_id`) VALUES(@vehicle_id, @newWanted)",
            new DynamicParameters(new { vehicle_id = vehicle_id, wanted_id = newWanted.wanted_id }), connectionString);
        }
        public IEnumerable<Wanted_Vehicle> GetVehicleWarrants(int vehicle_id, string connectionString)
        {
            var sql = @$"SELECT * FROM (SELECT * FROM wanted_vehicle WHERE vehicle_id=@vehicle_id ) as w
                LEFT JOIN wanted ON wanted.wanted_id = w.wanted_id AND active_status=1";

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                //Not returning directly to allow for easier debugging
                var rows = connection.Query<Wanted_Vehicle, Wanted, Wanted_Vehicle>(sql, (Wanted_Vehicle, Wanted) =>
                {
                    Wanted_Vehicle.Wanted = Wanted;
                    return Wanted_Vehicle;
                }
            , new { vehicle_id = vehicle_id }, splitOn: "vehicle_id, wanted_id");

                return rows;
            }
        }

        public IEnumerable<Wanted_Citizen> GetCitizenWarrants(int citizen_id, string connectionString)
        {
            var sql = @$"SELECT * FROM (SELECT * FROM wanted_citizen WHERE citizen_id=@citizen_id) as w
                LEFT JOIN wanted ON wanted.wanted_id = w.wanted_id AND active_status=1";

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                //Not returning directly to allow for easier debugging
                var rows = connection.Query<Wanted_Citizen, Wanted, Wanted_Citizen>(sql, (Wanted_Citizen, Wanted) =>
                {
                    Wanted_Citizen.Wanted = Wanted;
                    return Wanted_Citizen;
                }
            , new { citizen_id = citizen_id }, splitOn: "citizen_id, wanted_id");

                return rows;
            }
        }



    }
}
