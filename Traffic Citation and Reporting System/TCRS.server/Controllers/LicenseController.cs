using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using TCRS.Database;
using TCRS.Server.Tokens;
using TCRS.Shared.Objects.Lookup;
using TCRS.Shared.Objects.LookupPortal;
using TCRS.Shared.Objects.Warrant;

namespace TCRS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicenseController : Controller
    {
        private readonly IDataAccess _db;
        private readonly DatabaseContext _databaseContext;
        public LicenseController(IDataAccess db, IOptions<DatabaseContext> databaseContext)
        {
            _db = db;
            _databaseContext = databaseContext.Value;
        }

        [HttpGet]
        public ActionResult<IEnumerable<LookupCitizenDisplayData>> GetCitizenInfoByLicenseID([FromQuery] string license_id)
        {
            //Return type is wrapped in action result to allow NotFond to be returned
            //I pull licenseplate url query parameter here to be passed to database query
            if (license_id == null)
            {
                return NotFound("Citizen with this License ID does not exist");
            }
            try
            {
                var citizenData = _db.GetCitizenInfoByLicenseID(license_id, _databaseContext.Server);
                var citizenWantedList = _db.GetCitizenWarrants(citizenData.ToList().FirstOrDefault().citizen_id, _databaseContext.Server);
                var citationData = _db.GetCitationsByLicense(license_id, _databaseContext.Server).ToList().FindAll(citation => !citation.is_resolved);
                var citizen = citizenData.ToList().Select(citizen => new LookupCitizenDisplayData
                {
                    first_name = citizen.Citizen.first_name,
                    middle_name = citizen.Citizen.middle_name,
                    last_name = citizen.Citizen.last_name,
                    dob = citizen.Citizen.dob,
                    home_address = citizen.Citizen.home_address,
                    license_id = citizen.license_id,
                    expiration_date = citizen.expiration_date,
                    is_revoked = citizen.is_revoked,
                    is_suspended = citizen.is_suspended,
                    license_class = citizen.license_class,
                    CitizenWantedData = (citizenWantedList != null) ? citizenWantedList.Select(record => new WarrantData
                    {
                        reference_no = record.Wanted.reference_no,
                        dangerous = record.Wanted.dangerous,
                        crime = record.Wanted.crime,
                        status = record.Wanted.active_status
                    }) : null,
                    CitationData = (citationData != null) ? citationData.Select(citation => new CitationData
                    {
                        citation_number = citation.citation_number,
                        name = citation.Citation_Type.name,
                        date_recieved = citation.date_recieved,
                        date_due = citation.date_recieved.AddDays(citation.Citation_Type.due_date_month),
                        fine = Double.Parse(citation.Citation_Type.fine)
                    }) : null
                }
            );
                return Ok(citizen);
            }
            catch (Exception)
            {
                return NotFound("Not found");
            }
        }

        [HttpGet("GetVehicleInfoByLicensePlate")]
        public ActionResult<IEnumerable<LookupVehicleDisplayData>> LookupVehicleInfoByLicensePlate([FromQuery] string plate_number)

        {
            //Return type is wrapped in action result to allow NotFond to be returned

            //I pull licenseplate url query parameter here to be passed to database query
            if (plate_number == null)
            {
                return NotFound("Vehicle with this Plate number does not exist");
            }
            try
            {
                var vehicle = _db.GetVehicleInfoByLicencePlate(plate_number, _databaseContext.Server).ToList();
                if (vehicle == null)
                {
                    return NotFound(new { message = "Vehicle with this Plate number does not exist" });
                }
                var vehicleWantedList = _db.GetVehicleWarrants(vehicle.FirstOrDefault().vehicle_id, _databaseContext.Server);
                var citationData = _db.GetCitationsByLicense(plate_number, _databaseContext.Server).ToList().FindAll(citation => !citation.is_resolved);
                var vehicleData = vehicle.Select(vehicle => new LookupVehicleDisplayData
                {
                    vehicle_id = vehicle.Vehicle.vehicle_id,
                    vin = vehicle.Vehicle.vin,
                    name = vehicle.Vehicle.name,
                    stolen = vehicle.Vehicle.stolen,
                    make = vehicle.Vehicle.make,
                    registered = vehicle.Vehicle.registered,
                    model = vehicle.Vehicle.model,
                    year_made = vehicle.Vehicle.year_made,
                    citizen_id = vehicle.Vehicle.citizen_id,
                    insurer_id = vehicle.Vehicle.insurer_id,
                    WarrantData = (vehicleWantedList != null) ? vehicleWantedList.Select(record => new WarrantData
                    {
                        reference_no = record.Wanted.reference_no,
                        dangerous = record.Wanted.dangerous,
                        crime = record.Wanted.crime,
                        status = record.Wanted.active_status
                    }) : null,
                    CitationData = (citationData != null) ? citationData.Select(citation => new LookupCitationDisplayData
                    {
                        citation_number = citation.citation_number,
                        name = citation.Citation_Type.name,
                        date_recieved = citation.date_recieved,
                        date_due = citation.date_recieved.AddDays(citation.Citation_Type.due_date_month),
                        fine = Double.Parse(citation.Citation_Type.fine)
                    }) : null
                });

                return Ok(vehicleData);
            }
            catch (Exception)
            {
                return NotFound("Not found");
            }
        }
    }
}
