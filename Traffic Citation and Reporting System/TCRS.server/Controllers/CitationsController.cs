using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCRS.Database;
using TCRS.Database.Model;
using TCRS.Server.Tokens;
using TCRS.Shared.Objects.Citations;

namespace TCRS.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitationsController : Controller
    {
        private readonly IDataAccess _db;
        private readonly DatabaseContext _databaseContext;

        public CitationsController(IDataAccess db, IOptions<DatabaseContext> databaseContext)
        {
            _db = db;
            _databaseContext = databaseContext.Value;
        }
        [HttpGet]
        public ActionResult<IEnumerable<CitizenVehicleCitation>> GetCitationByLicense([FromQuery] String plate_number)
        {
            //Return type is wrapped in action result to allow NotFond to be returned

            //I pull licenseplate url query parameter here to be passed to database query
            if (plate_number == null || plate_number.Length > 8)
            {
                return NotFound("No License Plate Specified");
            }
            try
            {
                var citations = new List<CitizenVehicleCitation>();
                foreach (var citation in _db.GetCitationsByLicensePlate(plate_number, _databaseContext.Server))
                {
                    citations.Add(
                        new CitizenVehicleCitation
                        {
                            citation_id = citation.citation_id,
                            citation_number = citation.citation_number,
                            name = citation.Citation_Type.name,
                            fine = Double.Parse(citation.Citation_Type.fine),
                            date_recieved = citation.date_recieved,
                            vehicle_id = citation.Vehicle_Record.vehicle_id,
                            training_eligable = citation.Citation_Type.training_eligable,
                            plate_number = plate_number
                        }
                        );

                };

                return citations;
            }
            catch (Exception)
            {
                return NotFound("Not found");
            }
        }

        [HttpPost("IssueCitation")]
        public async Task<ActionResult<IEnumerable<CitationIssuingDisplayData>>> PostCitation([FromBody] CitationIssueData citationIssueData)
        {
            Citation Citation = new Citation
            {
                citation_number = Guid.NewGuid().ToString(), //Generate GUID
                date_recieved = DateTime.Now,
                citation_type_id = citationIssueData.citation_type_id,
                officer_id = citationIssueData.person_id
            };

            if (citationIssueData.licencePlate != null)
            {
                var FoundPlate = await _db.LoadData<License_Plate, DynamicParameters>(
                      "SELECT * FROM license_plate WHERE plate_number = @plate_number",
                      new DynamicParameters(new { plate_number = citationIssueData.licencePlate }),
                      _databaseContext.Server);
                if (FoundPlate != null)
                {
                    _db.SaveData<DynamicParameters>(
                       "INSERT INTO `Citation` " +
                       "(`citation_number`, `date_recieved`, `citation_type_id`, `officer_id`)" +
                       " VALUES (@citation_number, @date, @citation_type_id, @officer_id)",
                       new DynamicParameters(new { citation_number = Citation.citation_number, date = Citation.date_recieved, citation_type_id = Citation.citation_type_id, officer_id = Citation.officer_id }),
                       _databaseContext.Server
                       );

                    var NewCitation = await _db.LoadData<Citation, DynamicParameters>(
                        $"SELECT * FROM citation WHERE citation_number = {Citation.citation_number}",
                        new DynamicParameters(),
                        _databaseContext.Server
                        );

                    _db.SaveData<DynamicParameters>(
                       "INSERT INTO `Vehicle_Record`" +
                       " (`vehicle_id`, `citation_id`)" +
                       " VALUES (@vehicle_id, @citation_id)",
                       new DynamicParameters(
                           new { vehicle_id = FoundPlate.ToList().FirstOrDefault().vehicle_id, citation_id = NewCitation.FirstOrDefault().citation_id }
                           ),
                       _databaseContext.Server
                       );
                }

            }
            else if (citationIssueData.licence_id != null)
            {
                var FoundLicence = await _db.LoadData<License, DynamicParameters>(
                      "SELECT * FROM License  WHERE license_id = @license_id",
                      new DynamicParameters(new { license_id = citationIssueData.licence_id }),
                      _databaseContext.Server);
                if (FoundLicence != null)
                {

                    _db.SaveData<DynamicParameters>(
                       "INSERT INTO `Citation` " +
                       "(`citation_number`, `date_recieved`, `citation_type_id`, `officer_id`)" +
                       " VALUES (@citation_number, @date, @citation_type_id, @officer_id)",
                       new DynamicParameters(new
                       {
                           citation_number = Citation.citation_number,
                           date = Citation.date_recieved,
                           citation_type_id = Citation.citation_type_id,
                           officer_id = Citation.officer_id
                       }),
                       _databaseContext.Server
                       );

                    var NewCitation = await _db.LoadData<Citation, DynamicParameters>(
                        $"SELECT * FROM citation WHERE citation_number = {Citation.citation_number}",
                        new DynamicParameters(), _databaseContext.Server
                        );

                    _db.SaveData<DynamicParameters>(
                                       "INSERT INTO `driver_record`" +
                                       " (`citizen_id`, `citation_id`)" +
                                       " VALUES (@citizen_id, @citation_id)",
                                       new DynamicParameters(
                                           new
                                           {
                                               citizen_id = FoundLicence.ToList().FirstOrDefault().citizen_id,
                                               citation_id = NewCitation.FirstOrDefault().citation_id
                                           }
                                           ),
                                       _databaseContext.Server
                                       );
                    var list = new List<CitationIssuingDisplayData>();
                    var result = new CitationIssuingDisplayData
                    {
                        date_received = NewCitation.FirstOrDefault().date_recieved,
                        citation_number = NewCitation.FirstOrDefault().citation_number
                    };
                    list.Add(result);
                    return list;
                }
            }
            return NotFound("Licence plate or id not specified");
        }

        [HttpGet("All")]
        public IEnumerable<Citation_Type> GetCitationType()
        {
            return _db.GetAllCitationType<Citation_Type>(_databaseContext.Server, new Citation_Type());
        }
    }
}
