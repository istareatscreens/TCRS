using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using TCRS.Database;
using TCRS.Database.Model;
using TCRS.Server.Tokens;
using TCRS.Shared.Helper;
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
        public ActionResult<IEnumerable<CitationIssuingDisplayData>> PostCitation([FromBody] CitationIssueData citationIssueData)
        {
            Citation Citation = new Citation
            {
                citation_number = Guid.NewGuid().ToString(), //Generate GUID
                date_recieved = DateTime.Now,
                citation_type_id = citationIssueData.citation_type_id,
                officer_id = citationIssueData.person_id
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

            Func<Citation, DateTime> calculateDueDate = (Citation citation) => citation.date_recieved.AddMonths(citation.Citation_Type.due_date_month);

            return IEnumerableHandler.PackageInList<CitationIssuingDisplayData>(
            (FoundLicense != null) ?
            new CitationIssuingDisplayData
            {
                first_name = FoundLicense.Citizen.first_name,
                middle_name = FoundLicense.Citizen.last_name,
                last_name = FoundLicense.Citizen.middle_name,
                //common data
                citation_number = Citation.citation_number,
                date_received = Citation.date_recieved,
                DateDue = calculateDueDate(NewCitation),
                fine = NewCitation.Citation_Type.fine
            } : new CitationIssuingDisplayData
            {
                plate_number = citationIssueData.licencePlate,
                //common data
                citation_number = Citation.citation_number,
                date_received = Citation.date_recieved,
                DateDue = calculateDueDate(NewCitation),
                fine = NewCitation.Citation_Type.fine
            }
            );
        }

        [HttpGet("All")]
        public IEnumerable<Citation_Type> GetCitationType()
        {
            return _db.GetAllCitationType<Citation_Type>(_databaseContext.Server, new Citation_Type());
        }
    }
}
