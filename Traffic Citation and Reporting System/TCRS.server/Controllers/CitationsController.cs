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

        [HttpPut("RemoveCitation")]
        [Authorize(Roles = Roles.HighwayPatrolOfficer + "," + Roles.Manager)]
        public ActionResult ResolveCitation(RemoveCitationObject RemoveCitationObject)
        {
            try
            {
                if (RemoveCitationObject == null || RemoveCitationObject.citation_number.Length > 36)
                {
                    return NotFound(new { message = "Invalid Citation Number Length" });
                }

                var Citation = _db.GetCitationByNumber(RemoveCitationObject.citation_number, _databaseContext.Server);
                if (Citation == null || Citation.Count() == 0)
                {
                    return BadRequest(new { message = "Invalid citation number" });
                }
                _db.UpdateCitationToResolved(Citation.ToList().FirstOrDefault().citation_id, _databaseContext.Server);
                return Ok(new { message = "Successfully Removed" });
            }
            catch
            {
                return BadRequest(new { message = "Bad Request" });
            }

        }

        [HttpGet("Lookup")]
        [Authorize(Roles = Roles.HighwayPatrolOfficer + "," + Roles.Manager)]
        public ActionResult<IEnumerable<LookupCitationDisplayData>> CitationLookup([FromQuery] string citation_number)
        {
            if (IsValidCitationNumber(citation_number))
            {
                return NotFound(new { message = "Invalid" });
            }

            try
            {
                var Citation = _db.GetCitationAllInformationByNumber(citation_number, _databaseContext.Server);
                var Person = _db.GetPersonInfo(Citation.ToList().FirstOrDefault().officer_id, _databaseContext.Server);


                return Ok(Citation.Select(citation => new LookupCitationDisplayData
                {
                    //common
                    officer_id = citation.officer_id,
                    officer_first_name = Person.first_name,
                    officer_last_name = Person.last_name,
                    Is_Municipal_Officer = Person.Municipal_Officer != null,
                    Is_Police_Officer = Person.Municipal_Officer != null,
                    dept = (Person.Municipality != null) ? Person.Municipality.name : Person.Police_Dept.name,
                    is_vehicle = citation.Vehicle_Record != null,
                    is_citizen = citation.Driver_Record != null,
                    //information
                    date_recieved = citation.date_recieved,
                    date_due = CalculateDueDate(citation),
                    citation_number = citation.citation_number,
                    name = citation.Citation_Type.name,
                    is_resolved = IsCitationResolved(citation),
                    //Citizen
                    first_name = (citation.Driver_Record == null) ? "" : citation.Driver_Record.Citizen.first_name,
                    last_name = (citation.Driver_Record == null) ? "" : citation.Driver_Record.Citizen.last_name,
                    middle_name = (citation.Driver_Record == null) ? "" : citation.Driver_Record.Citizen.middle_name,
                    dob = (citation.Driver_Record == null) ? new DateTime() : citation.Driver_Record.Citizen.dob,
                    home_address = (citation.Driver_Record == null) ? "" : citation.Driver_Record.Citizen.home_address,
                    license = (citation.Driver_Record == null) ? "" : citation.Driver_Record.Citizen.License.license_id,
                    is_revoked = (citation.Driver_Record == null) ? false : citation.Driver_Record.Citizen.License.is_revoked,
                    is_suspended = (citation.Driver_Record == null) ? false : citation.Driver_Record.Citizen.License.is_suspended,
                    license_class = (citation.Driver_Record == null) ? "" : citation.Driver_Record.Citizen.License.license_class,
                    //Vehicle
                    model = (citation.Vehicle_Record == null) ? "" : citation.Vehicle_Record.Vehicle.model,
                    vin = (citation.Vehicle_Record == null) ? "" : citation.Vehicle_Record.Vehicle.vin,
                    year_made = (citation.Vehicle_Record == null) ? 0 : citation.Vehicle_Record.Vehicle.year_made,
                    make = (citation.Vehicle_Record == null) ? "" : citation.Vehicle_Record.Vehicle.make,

                    plate_number = (citation.Vehicle_Record == null) ? "" : citation.Vehicle_Record.Vehicle.License_Plate.plate_number,
                    is_stolen = (citation.Vehicle_Record == null) ? false : citation.Vehicle_Record.Vehicle.stolen,
                    //Insurence
                    is_insured = (citation.Vehicle_Record != null) ?
                        citation.Vehicle_Record.Vehicle.Insurer != null : (citation.Driver_Record != null) ?
                        citation.Driver_Record.Citizen.Insurer != null : false,
                    insurer = (citation.Vehicle_Record != null) ?
                        (citation.Vehicle_Record.Vehicle.Insurer != null) ?
                        citation.Vehicle_Record.Vehicle.Insurer.name : "" : (citation.Driver_Record != null) ?
                        (citation.Driver_Record.Citizen.Insurer != null) ?
                        citation.Driver_Record.Citizen.Insurer.name : "" : ""
                }));
            }
            catch
            {
                return NotFound(new { message = "Invalid Citation" });
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
        [Authorize(Roles = Roles.HighwayPatrolOfficer + "," + Roles.Manager)]
        public ActionResult<IEnumerable<CitizenVehicleCitation>> GetCitationListByCitationNumber([FromQuery] String citation_number)
        {
            if (IsValidCitationNumber(citation_number))
            {
                return NotFound(new { message = "Invalid Citation Number" });
            }

            try
            {
                var Citation = _db.GetCitationAllInformationByNumber(citation_number, _databaseContext.Server).ToList().FirstOrDefault();
                //Check if citation is marked as resolved, if not check in the database and update citation to be resolved if it is resolved
                if (Citation.is_resolved || _db.CheckIfCitationIsResolved(Citation.citation_id, _databaseContext.Server) || _db.CitationIsRegisteredToCourse(Citation.citation_id, _databaseContext.Server))
                {
                    return NotFound(new { message = "Is Resolved or Registered for Course" });
                }

                if (Citation.Vehicle_Record != null)
                {
                    var plate_number = Citation.Vehicle_Record.Vehicle.License_Plate.plate_number;
                    //remove resolved citations (Would probably be better to filter at the database level)
                    var citations = _db.GetCitationsByLicensePlate(plate_number, _databaseContext.Server).ToList().FindAll(citation => !citation.is_resolved);
                    var result = (citations.Select(citation => new CitizenVehicleCitation
                    {
                        citation_number = citation.citation_number,
                        name = citation.Citation_Type.name,
                        citation_type_id = citation.citation_type_id,
                        fine = Double.Parse(citation.Citation_Type.fine),
                        date_due = CalculateDueDate(citation),
                        date_recieved = citation.date_recieved,
                        training_eligable = citation.Citation_Type.training_eligable,
                        //If Citation is not resolved then check in database if it has been resolved and update if necessary
                        is_registered = false,
                    }));

                    return Ok(result);

                }
                else
                {
                    var license_id = Citation.Driver_Record.Citizen.License.license_id;
                    //remove resolved citations (Would probably be better to filter at the database level)
                    var citations = _db.GetCitationsByLicense(license_id, _databaseContext.Server).ToList().FindAll(citation => !citation.is_resolved);

                    var result = citations.Select(citation => new CitizenVehicleCitation
                    {
                        citation_number = citation.citation_number,
                        citizen_id = citation.Driver_Record.citizen_id,
                        name = citation.Citation_Type.name,
                        citation_type_id = citation.citation_type_id,
                        date_recieved = citation.date_recieved,
                        training_eligable = citation.Citation_Type.training_eligable,
                        date_due = CalculateDueDate(citation),
                        fine = Double.Parse(citation.Citation_Type.fine),
                        //If Citation is not resolved then check in database if it has been resolved and update if necessary
                        is_registered = _db.CitationIsRegisteredToCourse(citation.citation_id, _databaseContext.Server)
                    });
                    return Ok(result);
                }
            }
            catch (Exception)
            {
                return NotFound(new { message = "Not found" });
            }
        }


        [HttpPost("IssueCitation")]
        [Authorize(Roles = Roles.HighwayPatrolOfficer + "," + Roles.MunicipalOfficer)]
        public ActionResult<IEnumerable<CitationIssuingDisplayData>> PostCitation([FromHeader] string authorization, [FromBody] CitationIssueData citationIssueData)
        {


            //Check data
            License FoundLicense = null;
            License_Plate FoundPlate = null;
            bool is_dangerous = false;
            bool is_wanted = false;
            Citation NewCitation = null;

            try
            {
                Citation Citation = new Citation
                {
                    citation_number = Guid.NewGuid().ToString(), //Generate GUID
                    date_recieved = DateTime.Now,
                    citation_type_id = citationIssueData.citation_type_id,
                    officer_id = (new User(authorization)).person_id
                };


                //Check citation type
                if (_db.GetCitationTypeById(citationIssueData.citation_type_id, _databaseContext.Server) == null)
                {
                    return BadRequest(new { message = "Invalid Citation Type" });
                }

                //Get Data For Citizen or License Plate
                if (citationIssueData.licencePlate != null)
                {
                    FoundPlate = IEnumerableHandler.UnpackIEnumerable<License_Plate>(_db.GetAllVehicleInfoByLicencePlate(citationIssueData.licencePlate, _databaseContext.Server));
                    //warrant information
                    //Deal with situaton where invalid licence is entered
                    var warrants = (FoundPlate != null) ? _db.GetVehicleWarrants(FoundPlate.vehicle_id, _databaseContext.Server) : null;
                    if (warrants != null)
                    {
                        is_wanted = warrants.Count() > 0;
                        is_dangerous = warrants.ToList().Exists((wanted) => wanted.Wanted.dangerous);
                    }

                }
                else if (citationIssueData.licence_id != null)
                {
                    FoundLicense = IEnumerableHandler.UnpackIEnumerable<License>(_db.GetAllLicenseInfoByLicence(citationIssueData.licence_id, _databaseContext.Server));
                    //warrant information
                    //Deal with situaton where invalid licence is entered
                    var warrants = (FoundLicense != null) ? _db.GetCitizenWarrants(FoundLicense.citizen_id, _databaseContext.Server) : null;
                    if (warrants != null)
                    {
                        is_wanted = warrants.Count() > 0;
                        is_dangerous = warrants.ToList().Exists((wanted) => wanted.Wanted.dangerous);
                    }
                }
                else
                {
                    return NotFound(new { message = "Licence plate or id not specified" });
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

                NewCitation = IEnumerableHandler.UnpackIEnumerable(_db.GetCitationAllInformationByNumber(Citation.citation_number, _databaseContext.Server));

                //Func<Citation, DateTime> calculateDueDate = (Citation citation) => citation.date_recieved.AddMonths(citation.Citation_Type.due_date_month);

                return IEnumerableHandler.PackageInList<CitationIssuingDisplayData>(
                (FoundLicense != null) ?
                new CitationIssuingDisplayData
                {
                    is_citizen = true,
                    first_name = FoundLicense.Citizen.first_name,
                    middle_name = FoundLicense.Citizen.last_name,
                    last_name = FoundLicense.Citizen.middle_name,
                    license_id = FoundLicense.license_id,
                    is_suspended = FoundLicense.is_suspended,
                    is_revoked = FoundLicense.is_revoked,
                    home_address = FoundLicense.Citizen.home_address,
                    dob = FoundLicense.Citizen.dob,
                    //common data
                    citation_number = Citation.citation_number,
                    insurer = NewCitation.Driver_Record.Citizen.Insurer != null ? NewCitation.Driver_Record.Citizen.Insurer.name : "",
                    date_recieved = Citation.date_recieved,
                    DateDue = CalculateDueDate(NewCitation),
                    fine = NewCitation.Citation_Type.fine,
                    is_dangerous = is_dangerous,
                    is_wanted = is_wanted
                } : new CitationIssuingDisplayData
                {
                    is_vehicle = true,
                    plate_number = citationIssueData.licencePlate,
                    vin = NewCitation.Vehicle_Record.Vehicle.vin,
                    model = NewCitation.Vehicle_Record.Vehicle.model,
                    make = NewCitation.Vehicle_Record.Vehicle.make,
                    year_made = NewCitation.Vehicle_Record.Vehicle.year_made,
                    stolen = NewCitation.Vehicle_Record.Vehicle.stolen,

                    //common data
                    citation_number = Citation.citation_number,
                    insurer = NewCitation.Vehicle_Record.Vehicle.Insurer != null ? NewCitation.Vehicle_Record.Vehicle.Insurer.name : "",
                    date_recieved = Citation.date_recieved,
                    DateDue = CalculateDueDate(NewCitation),
                    fine = NewCitation.Citation_Type.fine,
                    is_dangerous = is_dangerous,
                    is_wanted = is_wanted
                }
                );

            }
            catch (Exception e)
            {
                return NotFound(new { message = e.Message });
            }
        }

        private Func<Citation, DateTime> CalculateDueDate = (Citation citation) => citation.date_recieved.AddMonths(citation.Citation_Type.due_date_month);

        [HttpGet("All")]
        [Authorize(Roles = Roles.HighwayPatrolOfficer + "," + Roles.Manager)]
        public IEnumerable<Citation_Type> GetCitationType()
        {
            return _db.GetAllCitationType<Citation_Type>(_databaseContext.Server, new Citation_Type());
        }
    }
}
