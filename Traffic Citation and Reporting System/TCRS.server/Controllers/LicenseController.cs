using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using TCRS.Database;
using TCRS.Server.Tokens;
using TCRS.Shared.Objects.Auth;
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
        [Authorize(Roles = Roles.HighwayPatrolOfficer + "," + Roles.Manager)]
        public ActionResult<IEnumerable<LookupCitizenDisplayData>> GetCitizenInfoByLicenseID([FromQuery] string license_id)
        {
            //Return type is wrapped in action result to allow NotFond to be returned
            //I pull licenseplate url query parameter here to be passed to database query
            if (license_id == null)
            {
                return NotFound(new { message = "Citizen with this License ID does not exist" });
            }
            try
            {
                var citizenData = _db.GetAllLicenseInfoByLicence(license_id, _databaseContext.Server);
                if(citizenData == null || citizenData.Count()==0)
                {
                    return NotFound(new { message = "Citizen with this License ID does not exist" });
                }
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
                    insurer_name = (citizen.Citizen.insurer_id != 0) ? citizen.Citizen.Insurer.name : null,
                    is_insured = citizen.Citizen.insurer_id != 0,
                    CitizenWantedData = (citizenWantedList != null && citizenWantedList.Count() != 0) ? citizenWantedList.Select(record => new WarrantData
                    {
                        reference_no = record.Wanted.reference_no,
                        dangerous = record.Wanted.dangerous,
                        crime = record.Wanted.crime,
                        status = record.Wanted.active_status
                    }) : new List<WarrantData>(),
                    CitationData = (citationData != null) ? citationData.Select(citation => new CitationData
                    {
                        citation_number = citation.citation_number,
                        name = citation.Citation_Type.name,
                        date_recieved = citation.date_recieved,
                        date_due = citation.date_recieved.AddDays(citation.Citation_Type.due_date_month),
                        fine = Double.Parse(citation.Citation_Type.fine)
                    }) : new List<CitationData>()
                }
            );
                return Ok(citizen);
            }
            catch (Exception)
            {
                return NotFound(new { message = "Not found" });
            }
        }

        [HttpGet("GetVehicleInfoByLicensePlate")]
        [Authorize(Roles = Roles.HighwayPatrolOfficer + "," + Roles.Manager)]
        public ActionResult<IEnumerable<LookupVehicleDisplayData>> LookupVehicleInfoByLicensePlate([FromQuery] string plate_number)

        {
            //Return type is wrapped in action result to allow NotFond to be returned

            //I pull licenseplate url query parameter here to be passed to database query
            if (plate_number == null)
            {
                return NotFound(new { message = "Vehicle with this Plate number does not exist" });
            }
            try
            {
                var vehicle = _db.GetAllVehicleInfoByLicencePlate(plate_number, _databaseContext.Server).ToList();
                if (vehicle == null || vehicle.Count() == 0)
                {
                    return NotFound(new { message = "Vehicle with this Plate number does not exist" });
                }
                var vehicleWantedList = _db.GetVehicleWarrants(vehicle.FirstOrDefault().vehicle_id, _databaseContext.Server);
                var citationData = _db.GetCitationsByLicensePlate(plate_number, _databaseContext.Server).ToList().FindAll(citation => !citation.is_resolved);
                var owner_id = vehicle.ToList().FirstOrDefault().Vehicle.citizen_id;
                LookupCitizenDisplayData owner = null;
                if (owner_id != null || owner_id != 0)
                {
                    owner = _db.GetCitizenById((int)owner_id, _databaseContext.Server).ToList().Select(owner => new LookupCitizenDisplayData
                    {
                        first_name = owner.first_name,
                        middle_name = owner.middle_name,
                        last_name = owner.last_name,
                        dob = owner.dob,
                        home_address = owner.home_address,
                    }).FirstOrDefault();
                }
                var vehicleData = vehicle.Select(vehicle => new LookupVehicleDisplayData
                {
                    plate_number = vehicle.plate_number,
                    vin = vehicle.Vehicle.vin,
                    name = vehicle.Vehicle.name,
                    stolen = vehicle.Vehicle.stolen,
                    make = vehicle.Vehicle.make,
                    registered = vehicle.Vehicle.registered,
                    model = vehicle.Vehicle.model,
                    year_made = vehicle.Vehicle.year_made,
                    citizen_id = vehicle.Vehicle.citizen_id,
                    insurer_id = vehicle.Vehicle.insurer_id,
                    insurer_name = (vehicle.Vehicle.insurer_id != null || vehicle.Vehicle.insurer_id != 0) ? vehicle.Vehicle.Insurer.name : null,
                    is_insured = vehicle.Vehicle.insurer_id != null || vehicle.Vehicle.insurer_id != 0,
                    Owner = owner,
                    WarrantData = (vehicleWantedList != null && vehicleWantedList.Count() != 0) ? vehicleWantedList.Select(record => new WarrantData
                    {
                        reference_no = record.Wanted.reference_no,
                        dangerous = record.Wanted.dangerous,
                        crime = record.Wanted.crime,
                        status = record.Wanted.active_status
                    }) : new List<WarrantData>(),
                    CitationData = (citationData != null) ? citationData.Select(citation => new CitationData
                    {
                        citation_number = citation.citation_number,
                        name = citation.Citation_Type.name,
                        date_recieved = citation.date_recieved,
                        date_due = citation.date_recieved.AddDays(citation.Citation_Type.due_date_month),
                        fine = Double.Parse(citation.Citation_Type.fine)
                    }) : new List<CitationData>()
                });

                return Ok(vehicleData);
            }
            catch (Exception)
            {
                return NotFound(new { message = "Not found" });
            }
        }
    }
}
