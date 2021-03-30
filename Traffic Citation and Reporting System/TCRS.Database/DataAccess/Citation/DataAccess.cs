using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using TCRS.Database.Model;
using TCRS.Shared.Helper;

namespace TCRS.Database
{
    //Citations
    public partial class DataAccess
    {
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

        public IEnumerable<T> GetAllCitationType<T>(string connectionString, T Model)
        {

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {

                string sqlQuery = @"Select * FROM Citation_Type";
                return connection.Query<T>(sqlQuery);
            }
        }



    }
}
