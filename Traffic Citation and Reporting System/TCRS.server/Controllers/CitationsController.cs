using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using TCRS.Database;
using TCRS.Database.Model;
using TCRS.Server.Tokens;
using TCRS.Shared.Helper;
using TCRS.Shared.Objects.Auth;
using TCRS.Shared.Objects.Citations;
using TCRS.Shared.Objects.Lookup;

namespace TCRS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitationsController : Controller
    {

        private readonly IDataAccess _db;
        private readonly DatabaseContext _databaseContext;
        public CitationsController(IDataAccess db, IOptions<DatabaseContext> databaseContext)
        {
            _db = db;
            _databaseContext = databaseContext.Value;
        }


        [HttpGet("/Lookup")]
        public ActionResult<IEnumerable<LookupCitationDisplayData>> CitationLookup([FromQuery] string citation_number)
        {
            if (IsValidCitationNumber(citation_number))
            {
                return NotFound("Invalid");
            }

            try
            {
                var Citation = _db.GetCitationAllInformationByNumber(citation_number, _databaseContext.Server);
                return Ok(Citation.Select(citation => new LookupCitationDisplayData
                {
                    //common
                    officer_id = citation.officer_id,
                    date_recieved = citation.date_recieved,
                    date_due = CalculateDueDate(citation),
                    citation_number = citation.citation_number,
                    name = citation.Citation_Type.name,
                    is_resolved = IsCitationResolved(citation),
                    //Citizen
                    first_name = (citation.Driver_Record == null) ? "" : citation.Driver_Record.Citizen.first_name,
                    last_name = (citation.Driver_Record == null) ? "" : citation.Driver_Record.Citizen.last_name,
                    middle_name = (citation.Driver_Record == null) ? "" : citation.Driver_Record.Citizen.middle_name,
                    license = (citation.Driver_Record == null) ? "" : citation.Driver_Record.Citizen.License.license_id,
                    license_class = (citation.Driver_Record == null) ? "" : citation.Driver_Record.Citizen.License.license_class,
                    //Vehicle
                    model = (citation.Vehicle_Record == null) ? "" : citation.Vehicle_Record.Vehicle.model,
                    plate_number = (citation.Vehicle_Record == null) ? "" : citation.Vehicle_Record.Vehicle.License_Plate.plate_number,
                }));
            }
            catch
            {
                return NotFound("Invalid Citation");
            }

        }

        private bool IsCitationResolved(Citation citation)
        {
            return (citation.is_resolved) ? true : citation.is_resolved ? true : _db.CheckIfCitationIsResolved(citation.citation_id, _databaseContext.Server);
        }

        private bool IsValidCitationNumber(string citation_number)
        {
            return citation_number == null || citation_number.Length > 36;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CitizenVehicleCitation>> GetCitationListByCitationNumber([FromQuery] String citation_number)
        {
            if (IsValidCitationNumber(citation_number))
            {
                return NotFound("Invalid");
            }

            var Citation = _db.GetCitationAllInformationByNumber(citation_number, _databaseContext.Server).ToList().FirstOrDefault();

            try
            {
                if (Citation.Vehicle_Record != null)
                {
                    var plate_number = Citation.Vehicle_Record.Vehicle.License_Plate.plate_number;
                    var citations = _db.GetCitationsByLicensePlate(plate_number, _databaseContext.Server);
                    var result = (citations.Select(citation => new CitizenVehicleCitation
                    {
                        citation_number = citation.citation_number,
                        name = citation.Citation_Type.name,
                        fine = Double.Parse(citation.Citation_Type.fine),
                        date_due = CalculateDueDate(citation),
                        date_recieved = citation.date_recieved,
                        vehicle_id = citation.Vehicle_Record.vehicle_id,
                        training_eligable = citation.Citation_Type.training_eligable,
                        //If Citation is not resolved then check in database if it has been resolved and update if necessary
                        is_resolved = IsCitationResolved(citation),
                        is_registered = false
                    }));

                    return Ok(result);

                }
                else
                {
                    var license_id = Citation.Driver_Record.Citizen.License.license_id;
                    var citations = _db.GetCitationsByLicense(license_id, _databaseContext.Server);

                    var result = citations.Select(citation => new CitizenVehicleCitation
                    {
                        citation_number = citation.citation_number,
                        name = citation.Citation_Type.name,
                        date_recieved = citation.date_recieved,
                        training_eligable = citation.Citation_Type.training_eligable,
                        date_due = CalculateDueDate(citation),
                        fine = Double.Parse(citation.Citation_Type.fine),
                        //If Citation is not resolved then check in database if it has been resolved and update if necessary
                        is_resolved = IsCitationResolved(citation),
                        is_registered = _db.CitationIsRegisteredToCourse(citation.citation_id, _databaseContext.Server)
                    });

                    return Ok(result);


                }
            }
            catch (Exception)
            {
                return NotFound("Not found");
            }
        }


        [HttpPost("IssueCitation")]
        [Authorize(Roles = Roles.HighwayPatrolOfficer + ", " + Roles.MunicipalOfficer)]
        public ActionResult<IEnumerable<CitationIssuingDisplayData>> PostCitation([FromHeader] string authorization, [FromBody] CitationIssueData citationIssueData)
        {

            if (_db.GetCitationTypeById(citationIssueData.citation_type_id, _databaseContext.Server) == null)
            {
                return BadRequest("Invalid Citation Type");
            }

            Citation Citation = new Citation
            {
                citation_number = Guid.NewGuid().ToString(), //Generate GUID
                date_recieved = DateTime.Now,
                citation_type_id = citationIssueData.citation_type_id,
                officer_id = (new User(authorization)).person_id
            };

            //Check data
            License FoundLicense = null;
            License_Plate FoundPlate = null;
            Citation NewCitation = null;

            try
            {
                //Get Data For Citizen or License Plate
                if (citationIssueData.licencePlate != null)
                {
                    FoundPlate = IEnumerableHandler.UnpackIEnumerable<License_Plate>(_db.GetVehicleInfoByLicencePlate(citationIssueData.licencePlate, _databaseContext.Server));

                }
                else if (citationIssueData.licence_id != null)
                {
                    FoundLicense = IEnumerableHandler.UnpackIEnumerable<License>(_db.GetLicenseInfoByLicence(citationIssueData.licence_id, _databaseContext.Server));
                }
                else
                {
                    return NotFound("Licence plate or id not specified");
                }


                //Post data
                if (FoundPlate != null)
                {
                    _db.PostVehicleCitation(Citation, FoundPlate, _databaseContext.Server);
                }
                else if (FoundLicense != null)
                {
                    _db.PostCitizenCitation(Citation, FoundLicense, _databaseContext.Server);
                }

                NewCitation = IEnumerableHandler.UnpackIEnumerable(_db.GetCitationByNumber(Citation.citation_number, _databaseContext.Server));


            }
            catch (Exception e)
            {
                return NotFound("Connection Error" + e.Message);
            }

            //Func<Citation, DateTime> calculateDueDate = (Citation citation) => citation.date_recieved.AddMonths(citation.Citation_Type.due_date_month);

            return IEnumerableHandler.PackageInList<CitationIssuingDisplayData>(
            (FoundLicense != null) ?
            new CitationIssuingDisplayData
            {
                first_name = FoundLicense.Citizen.first_name,
                middle_name = FoundLicense.Citizen.last_name,
                last_name = FoundLicense.Citizen.middle_name,
                //common data
                citation_number = Citation.citation_number,
                date_recieved = Citation.date_recieved,
                DateDue = CalculateDueDate(NewCitation),
                fine = NewCitation.Citation_Type.fine
            } : new CitationIssuingDisplayData
            {
                plate_number = citationIssueData.licencePlate,
                //common data
                citation_number = Citation.citation_number,
                date_recieved = Citation.date_recieved,
                DateDue = CalculateDueDate(NewCitation),
                fine = NewCitation.Citation_Type.fine
            }
            );
        }

        private Func<Citation, DateTime> CalculateDueDate = (Citation citation) => citation.date_recieved.AddMonths(citation.Citation_Type.due_date_month);

        [HttpGet("All")]
        public IEnumerable<Citation_Type> GetCitationType()
        {
            return _db.GetAllCitationType<Citation_Type>(_databaseContext.Server, new Citation_Type());
        }
    }
}
