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
    //Citations
    public partial class DataAccess
    {
        private IEnumerable<Citation> PostCitation(Citation Citation, string connectionString)
        {
            SaveData<DynamicParameters>("INSERT INTO `Citation` " +
                "(`citation_number`, `date_recieved`, `citation_type_id`, `officer_id`) " +
                "VALUES (@citation_number, @date, @citation_type_id, @officer_id)",
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

        public IEnumerable<Citation> GetCitationsByLicense(string license_id, string connectionString)
        {
            var sql = @$"SELECT * FROM (SELECT license_id, citizen_id as license_citizen_id, expiration_date, is_revoked, is_suspended, license_class FROM license where license_id = @license_id) as lic
                LEFT JOIN driver_record ON driver_record.citizen_id = lic.license_citizen_id
                LEFT JOIN citation ON citation.citation_id = driver_record.citation_id
                LEFT JOIN citation_type ON citation_type.citation_type_id = citation.citation_type_id";

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                //Not returning directly to allow for easier debugging
                var rows = connection.Query<License, Driver_Record, Citation, Citation_Type, Citation>(sql, (License, Driver_Record, Citation, Citation_Type) =>
              {
                  Citation.Driver_Record = Driver_Record;
                  Citation.Citation_Type = Citation_Type;
                  return Citation;
              }, new { license_id = license_id }, splitOn: "license_id, citizen_id, citation_id, citation_type_id");

                return rows;
            }
        }

        public IEnumerable<Citation> GetCitationByNumber(string citation_number, string connectionString)
        {
            var sql = "SELECT * FROM (SELECT citation_id, date_recieved, citation_type_id as type_id, officer_id FROM citation WHERE citation_number = @citation_number) as cit " +
                    "LEFT JOIN citation_type ON citation_type.citation_type_id = cit.type_id";

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

        public IEnumerable<Citation> GetCitationAllInformationByNumber(string citation_number, string connectionString)
        {
            var citation_id = (SyncLoadData<Citation, Citation>(
                            "SELECT citation_id FROM citation WHERE citation_number = @citation_number", new Citation { citation_number = citation_number }, connectionString)).ToList().FirstOrDefault().citation_id;

            var Vehicle_Record = (SyncLoadData<Vehicle_Record, Citation>(
                "SELECT * FROM vehicle_record WHERE citation_id = @citation_id", new Citation { citation_id = citation_id }, connectionString));

            var Driver_Record = (SyncLoadData<Driver_Record, Citation>(
                "SELECT * FROM driver_record WHERE citation_id = @citation_id", new Citation { citation_id = citation_id }, connectionString));

            if (Vehicle_Record.Count() != 0)
            {

                var sql = "SELECT * FROM (SELECT citation_id, citation_number, date_recieved, citation_type_id as type_id, officer_id, is_resolved FROM citation WHERE citation_id = @citation_id) as cit " +
                        "LEFT JOIN citation_type ON citation_type.citation_type_id = cit.type_id " +
                        "LEFT JOIN (SELECT * FROM vehicle_record WHERE vehicle_record.citation_id = @citation_id) as v_record ON v_record.citation_id = cit.citation_id " +
                        "LEFT JOIN (SELECT vehicle_id, vin, name, stolen, make, registered, model, year_made, citizen_id, insurer_id as insurer_ident FROM vehicle) as vehicle_table ON vehicle_table.vehicle_id = v_record.vehicle_id " +
                        "LEFT JOIN license_plate ON license_plate.vehicle_id = vehicle_table.vehicle_id " +
                        "LEFT JOIN insurer ON insurer.insurer_id = vehicle_table.insurer_ident";

                using (IDbConnection connection = new MySqlConnection(connectionString))
                {
                    //Not returning directly to allow for easier debugging
                    var rows = connection.Query<Citation, Citation_Type, Vehicle_Record, Vehicle, License_Plate, Insurer, Citation>(sql, (Citation, Citation_Type, Vehicle_Record, Vehicle, License_Plate, Insurer) =>
                      {
                          Citation.Citation_Type = Citation_Type;
                          Citation.citation_type_id = Citation_Type.citation_type_id;
                          Citation.Vehicle_Record = Vehicle_Record;
                          if (Citation.Vehicle_Record != null)
                          {
                              Citation.Vehicle_Record.Vehicle = Vehicle;
                              //      Citation.Vehicle_Record.Vehicle.Insurer = Insurer;
                              Citation.Vehicle_Record.Vehicle.License_Plate = License_Plate;
                          }
                          return Citation;
                      }, new { citation_id = citation_id }, splitOn: "citation_id, citation_type_id, vehicle_id, vehicle_id, plate_number, insurer_id");

                    return rows;
                }

            }
            else if (Driver_Record.Count() != 0)
            {

                var sql = "SELECT * FROM (SELECT citation_id, citation_number, date_recieved, citation_type_id as type_id, officer_id, is_resolved FROM citation WHERE citation_id = @citation_id) as cit " +
                       "LEFT JOIN citation_type ON citation_type.citation_type_id = cit.type_id " +
                       "LEFT JOIN (SELECT * FROM driver_record WHERE driver_record.citation_id = @citation_id) as d_record ON d_record.citation_id = cit.citation_id " +
                       "LEFT JOIN (SELECT citizen_id, first_name, middle_name, last_name, dob, home_address, insurer_id as insurer_ident  FROM citizen) as citizen_table ON citizen_table.citizen_id = d_record.citizen_id " +
                       "LEFT JOIN license ON license.citizen_id = citizen_table.citizen_id " +
                       "LEFT JOIN insurer ON insurer.insurer_id = citizen_table.insurer_ident";

                using (IDbConnection connection = new MySqlConnection(connectionString))
                {
                    //Not returning directly to allow for easier debugging
                    var rows = connection.Query<Citation, Citation_Type, Driver_Record, Citizen, License, Insurer, Citation>(sql, (Citation, Citation_Type, Driver_Record, Citizen, License, Insurer) =>
                      {
                          Citation.Citation_Type = Citation_Type;
                          Citation.citation_type_id = Citation_Type.citation_type_id;
                          Citation.Driver_Record = Driver_Record;
                          if (Driver_Record != null)
                          {
                              Citation.Driver_Record.Citizen = Citizen;
                              Citation.Driver_Record.Citizen.License = License;
                              Citation.Driver_Record.Citizen.Insurer = Insurer;
                          }
                          return Citation;
                      }, new { citation_id = citation_id }, splitOn: "citation_id, citation_type_id, citizen_id, citizen_id, license_id, insurer_id");

                    return rows;
                }
            }
            else
            {
                throw new Exception("Invalid citation number passed");
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

        public bool CheckIfCitationIsResolved(int citation_id, string connectionString)
        {
            var sql = "SELECT * FROM (SELECT * FROM citation WHERE citation_id = @citation_id) as cit " +
                    "LEFT JOIN payment ON payment.citation_id = cit.citation_id " +
                    "LEFT JOIN registration ON payment.citation_id = cit.citation_id";

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                //Not returning directly to allow for easier debugging
                var rows = connection.Query<Citation, Payment, Registration, Citation>(sql, (Citation, Payment, Registration) =>
              {
                  Citation.Payment = Payment;
                  Citation.Registration = Registration;
                  return Citation;
              }, new { citation_id = citation_id }, splitOn: "citation_id, citation_id, citizen_id");

                var Citation = IEnumerableHandler.UnpackIEnumerable<Citation>(rows);
                if (Citation.Payment != null || (Citation.Registration != null && (rows.ToList().Exists(citation => citation.Registration.passed))))
                {
                    UpdateCitationToResolved(citation_id, connectionString); //set course to resolved
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool CitationBelongsToCitizen(int citation_id, int citizen_id, string connectionString)
        {
            var sql = "SELECT * FROM driver_record WHERE citation_id = @citation_id AND citizen_id = @citizen_id";
            return LoadData<Driver_Record, DynamicParameters>(sql, new DynamicParameters(new { citation_id = citation_id, citizen_id = citizen_id }), connectionString) != null;
        }

        public bool CitationBelongsToVehicle(int citation_id, int vehicle_id, string connectionString)
        {
            var sql = "SELECT * FROM vehicle_record WHERE citation_id = @citation_id AND vehicle_id = @vehicle_id";
            return LoadData<Vehicle_Record, DynamicParameters>(sql, new DynamicParameters(new { citation_id = citation_id, vehicle_id = vehicle_id }), connectionString) != null;

        }
        public void UpdateCitationToResolved(int citation_id, string connectionString)
        {
            var sql = @"UPDATE citation SET is_resolved = '1' WHERE (citation_id = @citation_id)"; //set citation as resolved
            UpdateData<DynamicParameters>(sql, new DynamicParameters(new { citation_id = citation_id }), connectionString);
        }

        public IEnumerable<Citation_Type> GetCitationTypeById(int citation_type_id, string connectionString)
        {
            return SyncLoadData<Citation_Type, Citation_Type>(
                "SELECT * FROM citation_type WHERE citation_type_id = @citation_type_id",
                new Citation_Type { citation_type_id = citation_type_id },
                connectionString
                );
        }
        public bool CitationIsRegisteredToCourse(int citation_id, string connectionString)
        {
            var sql = @"SELECT COUNT(*) FROM (SELECT * FROM registration WHERE citation_id = @citation_id) as reg " +
                      "LEFT JOIN course ON course.course_id = reg.course_id AND course.scheduled > @date ";
            var count = GetCount<DynamicParameters>(sql, new DynamicParameters(new { citation_id = citation_id, date = DateTime.Now }), connectionString);
            return 0 < count;
        }

        public void PayForCitation(Payment Payment, string connectionString)
        {
            var sql = @"INSERT INTO Payment (`citation_id`, `payment`, `payment_date`, `made_by`, `payment_method`) " +
                       "VALUES (@citation_id, @payment, @payment_date, @made_by, @payment_method)";
            SaveData<Payment>(sql, Payment, connectionString);
        }

    }
}
